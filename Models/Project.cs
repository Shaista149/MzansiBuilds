using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MzansiBuilds.Models
{
    /// <summary>
    /// Represents a project created by a developer to share progress publicly
    /// </summary>
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        // FK, which developer owns this project
        [Required]
        public int DeveloperId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        // "Idea", "In Progress", "MVP", "Completed"
        [StringLength(50)]
        public string Stage { get; set; }

        public string SupportNeeded { get; set; }

        public bool IsComplete { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties : "a project belongs to one developer"
        [ForeignKey("DeveloperId")]
        public virtual Developer Developer { get; set; }
        public virtual Celebration Celebration { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Milestone> Milestones { get; set; }
        public virtual ICollection<CollaborationRequest> CollaborationRequests { get; set; }
    }
}