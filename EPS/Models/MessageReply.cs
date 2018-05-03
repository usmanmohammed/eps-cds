using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Models
{
    public class MessageReply
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Required]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Required]
        [Display(Name = "Replied By")]
        public string RepliedBy { get; set; }
        [Display(Name = "Message")]
        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}
