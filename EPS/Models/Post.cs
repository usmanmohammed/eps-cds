using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace EPS.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public byte[] Thumbnail { get; set; }
        [Display(Name = "Thumbnail Format")]
        public string ThumbnailContentType { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Display(Name = "Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}