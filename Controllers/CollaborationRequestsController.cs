using Microsoft.AspNet.Identity;
using MzansiBuilds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MzansiBuilds.Controllers
{
    /// <summary>
    /// Handles collaboration requests between developers on projects
    /// </summary>
    public class CollaborationRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST - send a collaboration request on a project
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int projectId, string message)
        {
            var developer = GetCurrentDeveloper();
            if (developer == null)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            var project = db.Projects.Find(projectId);
            if (project == null)
                return HttpNotFound();

            // Can't request to collaborate on your own project
            if (project.DeveloperId == developer.DeveloperId)
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            // Check if a request already exists from this developer on this project
            var existingRequest = db.CollaborationRequests
                .FirstOrDefault(r => r.ProjectId == projectId
                    && r.RequesterId == developer.DeveloperId);

            if (existingRequest == null)
            {
                var request = new CollaborationRequest
                {
                    ProjectId = projectId,
                    RequesterId = developer.DeveloperId,
                    Message = message,
                    Status = "Pending",
                    CreatedAt = DateTime.Now
                };

                db.CollaborationRequests.Add(request);
                db.SaveChanges();
            }

            return RedirectToAction("Details", "Projects", new { id = projectId });
        }

        // POST - project owner responds to a request
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Respond(int requestId, string status)
        {
            var developer = GetCurrentDeveloper();
            if (developer == null)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            var request = db.CollaborationRequests.Find(requestId);
            if (request == null)
                return HttpNotFound();

            // Only the project owner can respond
            var project = db.Projects.Find(request.ProjectId);
            if (project.DeveloperId != developer.DeveloperId)
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            // Only allow valid status values
            if (status == "Accepted" || status == "Declined")
            {
                request.Status = status;
                db.SaveChanges();
            }

            return RedirectToAction("Details", "Projects", new { id = request.ProjectId });
        }

        // Helper method to get the currently logged in developer's profile
        private Developer GetCurrentDeveloper()
        {
            var userId = User.Identity.GetUserId();
            return db.Developers.FirstOrDefault(d => d.UserId == userId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}