using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MzansiBuilds.Models
{
    /// <summary>
    /// Represents a celebration entry when a project is marked as complete
    /// </summary>
    public class Celebration
    {
        [Key]
        public int CelebrationId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int DeveloperId { get; set; }

        public DateTime CelebratedAt { get; set; } = DateTime.Now;

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [ForeignKey("DeveloperId")]
        public virtual Developer Developer { get; set; }
    }
}