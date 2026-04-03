using Microsoft.AspNet.Identity;
using MzansiBuilds.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MzansiBuilds.Controllers
{
    /// <summary>
    /// Handles all project related actions including the live feed, 
    /// creating, editing, viewing and deleting projects
    /// </summary>

    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Live Feed : shows all projects from all developers newest first
        public ActionResult Index()
        {
            var projects = db.Projects
                .Include(p => p.Developer)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();

            return View(projects);
        }

        // Details of a single project
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var project = db.Projects
                .Include(p => p.Developer)
                .Include(p => p.Comments.Select(c => c.Developer))
                .Include(p => p.Milestones)
                .FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }

        // Create : only for logged in users
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // Save new projects to DB
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,Stage,SupportNeeded")] Project project)
        {
            if (ModelState.IsValid)
            {
                // Get the logged in user's Developer profile
                var developer = GetCurrentDeveloper();
                if (developer == null)
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

                project.DeveloperId = developer.DeveloperId;
                project.IsComplete = false;

                db.Projects.Add(project);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
        }

        // Edit : only the project owner can edit
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var project = db.Projects.Find(id);

            if (project == null)
                return HttpNotFound();

            // Make sure only the owner can edit
            var developer = GetCurrentDeveloper();
            if (developer == null)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            if (project.DeveloperId != developer.DeveloperId)
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            return View(project);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,Title,Description,Stage,SupportNeeded,IsComplete")] Project project)
        {
            if (ModelState.IsValid)
            {
                var developer = GetCurrentDeveloper();
                if (developer == null)
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

                project.DeveloperId = developer.DeveloperId;
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
        }

        // Delete : only the project owner can delete
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var project = db.Projects
                .Include(p => p.Developer)
                .FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();

            return RedirectToAction("Index");
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
