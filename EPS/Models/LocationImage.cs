using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace EPS.Models
{
    public class LocationImage
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}