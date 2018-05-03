using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace EPS.Models
{
    public class PostImage
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        [Display(Name = "Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}