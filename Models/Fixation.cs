using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedFixator.Models
{
    public class Fixation
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string VechicleNumber { get; set; }
        public double Speed { get; set; }
    }
}
