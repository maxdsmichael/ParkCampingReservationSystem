using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        public int Site_ID { get; set; }
        public int Campground_ID { get; set; }
        public int Site_Number { get; set; }
        public int Max_Occupancy { get; set; }
        public bool Accessible { get; set; }
        public int Max_Rv_Length { get; set; }
        public bool Utilities { get; set; }

        public override string ToString()
        {
            return $"{Site_Number, -18} {Max_Occupancy, -20} {(Accessible ? "Yes" : "N/A"),-21} {Max_Rv_Length, -23} {(Utilities ? "Yes" : "N/A"), -17}"; //TODO Fix formatting
        }

    }

}
