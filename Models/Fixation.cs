using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedFixator.Models
{
    public class Fixation
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string VechicleNumber { get; set; }
        [Required]
        public double? Speed { get; set; }
    }
}
