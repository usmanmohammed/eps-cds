using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EPS.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string LocationAddress { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public byte[] Thumbnail { get; set; }
        [Display(Name = "Thumbnail Format")]
        public string ThumbnailContentType { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Additional Information")]
        public string AdditionalInformation { get; set; }
        public bool IsApproved { get; set; }
        public bool IsTreated { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public virtual ICollection<LocationImage> LocationImages { get; set; }
    }
}
