using MzansiBuilds.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MzansiBuilds.Controllers
{
    /// <summary>
    /// Handles the Celebration Wall showing all completed projects
    /// </summary>
    public class CelebrationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Public wall - anyone can view completed projects
        public ActionResult Index()
        {
            var celebrations = db.Celebrations
                .Include(c => c.Project)
                .Include(c => c.Developer)
                .OrderByDescending(c => c.CelebratedAt)
                .ToList();

            return View(celebrations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}