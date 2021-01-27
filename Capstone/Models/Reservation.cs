using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int Reservation_ID { get; set; }
        public int Site_ID { get; set; }
        public string Name { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public DateTime Create_Date { get; set; }

    }
}
