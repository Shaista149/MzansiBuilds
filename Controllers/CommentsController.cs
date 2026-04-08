using Microsoft.AspNet.Identity;
using MzansiBuilds.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MzansiBuilds.Controllers
{
    /// <summary>
    /// Handles posting comments on projects
    /// </summary>
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST : Comments are submitted from the project Details page
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int projectId, string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                var developer = GetCurrentDeveloper();
                if (developer == null)
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

                var comment = new Comment
                {
                    ProjectId = projectId,
                    DeveloperId = developer.DeveloperId,
                    Content = content,
                    CreatedAt = DateTime.Now
                };

                db.Comments.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Details", "Projects", new { id = projectId });
        }

        // POST : delete a comment, only the comment author can delete
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int commentId, int projectId)
        {
            var userId = User.Identity.GetUserId();
            var developer = db.Developers.FirstOrDefault(d => d.UserId == userId);

            if (developer == null)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            var comment = db.Comments.FirstOrDefault(c => c.CommentId == commentId && c.DeveloperId == developer.DeveloperId);

            if (comment == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            db.Comments.Remove(comment);
            db.SaveChanges();

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