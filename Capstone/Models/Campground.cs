using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        public int Campground_ID { get; set; }
        public int Park_ID { get; set; }
        public string Name { get; set; }
        public int Open_From_Month { get; set; }
        public int Open_To_Month { get; set; }
        public decimal Daily_Fee { get; set; }
        public override string ToString()
        {
            return $"{Campground_ID}) {Name, -20} {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Open_From_Month), -10} {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Open_To_Month), -10} {Daily_Fee:C2}";
        }

    }
}
