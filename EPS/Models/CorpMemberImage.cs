using System.ComponentModel.DataAnnotations;

namespace EPS.Models
{
    public class CorpMemberImage
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        [Display(Name = "Corper")]
        public int CorperId { get; set; }
        public CorpMember Corper { get; set; }
    }
}