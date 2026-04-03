using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MzansiBuilds.Models
{
    /// <summary>
    /// Represents a comment left by a developer on a project
    /// </summary>
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int DeveloperId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [ForeignKey("DeveloperId")]
        public virtual Developer Developer { get; set; }
    }
}