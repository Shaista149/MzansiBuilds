using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MzansiBuilds.Models
{
    /// <summary>
    /// Represents a milestone achieved on a project
    /// </summary>
    public class Milestone
    {
        [Key]
        public int MilestoneId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime AchievedAt { get; set; } = DateTime.Now;

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}