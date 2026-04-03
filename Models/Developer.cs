using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MzansiBuilds.Models
{
    /// <summary>
    /// Represents a developer profile linked to an ASP.NET Identity account
    /// </summary>
    public class Developer
    {
        [Key]
        public int DeveloperId { get; set; }

        // Links the Developer profile to the ASP.NET Identity user (login system)
        // One Identity user = one Developer profile

        public string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Bio { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties : EF uses these to understand relationships
        // "A developer has many projects"
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CollaborationRequest> SentRequests { get; set; }
        public virtual ICollection<Celebration> Celebrations { get; set; }
    }
}