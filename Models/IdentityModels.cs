using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MzansiBuilds.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // Links Identity account to Developer profile
        public virtual Developer Developer { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<CollaborationRequest> CollaborationRequests { get; set; }
        public DbSet<Celebration> Celebrations { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasRequired(p => p.Developer)
                .WithMany(d => d.Projects)
                .HasForeignKey(p => p.DeveloperId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Developer)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.DeveloperId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CollaborationRequest>()
                .HasRequired(cr => cr.Requester)
                .WithMany(d => d.SentRequests)
                .HasForeignKey(cr => cr.RequesterId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Celebration>()
                .HasRequired(c => c.Developer)
                .WithMany(d => d.Celebrations)
                .HasForeignKey(c => c.DeveloperId)
                .WillCascadeOnDelete(false);
            }
    }
}