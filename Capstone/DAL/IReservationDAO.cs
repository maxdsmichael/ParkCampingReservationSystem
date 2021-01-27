using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface IReservationDAO
    {
        
        int CreateReservation(int site_id, string name, DateTime from_date, DateTime to_date, DateTime creation_date);
    }
}