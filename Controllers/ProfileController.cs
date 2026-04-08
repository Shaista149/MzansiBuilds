using Microsoft.AspNet.Identity;
using MzansiBuilds.Models;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MzansiBuilds.Controllers
{
    /// <summary>
    /// Handles viewing and editing developer profiles
    /// </summary>
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET : logged-in user's own profile page
        [Authorize]
        public ActionResult Index()
        {
            var developer = GetCurrentDeveloper();
            if (developer == null)
                return RedirectToAction("Index", "Home");
            return View(developer);
        }

        // POST : update username and bio
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(string username, string bio)
        {
            var developer = GetCurrentDeveloper();
            if (developer == null)
                return RedirectToAction("Index", "Home");

            if (string.IsNullOrWhiteSpace(username))
            {
                TempData["Error"] = "Username cannot be empty.";
                return RedirectToAction("Index");
            }
            if (username.Length > 50)
            {
                TempData["Error"] = "Username must be 50 characters or less.";
                return RedirectToAction("Index");
            }

            var taken = db.Developers.Any(d => d.Username == username && d.DeveloperId != developer.DeveloperId);
            if (taken)
            {
                TempData["Error"] = "That username is already taken. Please choose another.";
                return RedirectToAction("Index");
            }

            developer.Username = username.Trim();
            developer.Bio = bio?.Trim();
            db.SaveChanges();

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Index");
        }

        // POST : soft delete the account and sign out
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccount()
        {
            var developer = GetCurrentDeveloper();
            if (developer == null)
                return RedirectToAction("Index", "Home");

            // Remove any pending collab requests they sent
            var pendingRequests = db.CollaborationRequests
                .Where(r => r.RequesterId == developer.DeveloperId && r.Status == "Pending")
                .ToList();
            db.CollaborationRequests.RemoveRange(pendingRequests);

            developer.IsDeleted = true;
            db.SaveChanges();

            HttpContext.GetOwinContext().Authentication.SignOut(
                Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        // GET : public profile view for any developer
        public ActionResult View(int id)
        {
            var developer = db.Developers
                .Include(d => d.Projects)
                .FirstOrDefault(d => d.DeveloperId == id);

            if (developer == null)
                return HttpNotFound();

            return View(developer);
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