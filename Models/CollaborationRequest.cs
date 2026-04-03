using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MzansiBuilds.Models
{
    /// <summary>
    /// Represents a request from a developer to collaborate on a project
    /// </summary>
    public class CollaborationRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        // Developer who sent the request
        [Required]
        public int RequesterId { get; set; }

        public string Message { get; set; }

        // "Pending", "Accepted", "Declined"
        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [ForeignKey("RequesterId")]
        public virtual Developer Requester { get; set; }
    }
}