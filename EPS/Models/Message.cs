using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public bool IsTreated { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public virtual ICollection<MessageReply> MessageReplies { get; set; }
    }
}
