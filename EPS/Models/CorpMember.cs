using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Models
{
    public class CorpMember
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "State Code")]
        public string StateCode { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Required]
        [Display(Name = "Batch")]
        public int BatchId { get; set; }
        public Batch Batch { get; set; }
        [Display(Name = "Is Exco")]
        public bool IsExco { get; set; }
        [Required]
        [Display(Name = "Primary Role")]
        public int CorpMemberRoleId { get; set; }
        public CorpMemberRole CorpMemberRole { get; set; }
        public byte[] Thumbnail { get; set; }
        [Display(Name = "Thumbnail Format")]
        public string ThumbnailContentType { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Required]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public virtual ICollection<CorpMemberImage> CorpMemberImages { get; set; }
    }
}
