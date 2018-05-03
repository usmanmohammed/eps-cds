using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Models
{
    public class Batch
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Batch Name")]
        public string BatchName { get; set; }
        [Required]
        [Display(Name = "Batch Code")]
        public string BatchCode { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
    }
}
