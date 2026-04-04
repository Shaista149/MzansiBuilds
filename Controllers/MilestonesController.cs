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
    /// Handles logging milestone updates on projects
    /// </summary>
    public class MilestonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST only - milestones are added from the project Details page
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int projectId, string description)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                var developer = GetCurrentDeveloper();
                if (developer == null)
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

                var project = db.Projects.Find(projectId);

                if (project != null && project.DeveloperId == developer.DeveloperId)
                {
                    var milestone = new Milestone
                    {
                        ProjectId = projectId,
                        Description = description,
                        AchievedAt = DateTime.Now
                    };
                    db.Milestones.Add(milestone);
                    db.SaveChanges();
                }
                else
                {
                    TempData["Error"] = "You can only add milestones to your own projects.";
                }
            }
            return RedirectToAction("Details", "Projects", new { id = projectId });
        }

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