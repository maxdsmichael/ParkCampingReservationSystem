using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface ISiteDAO
    {
        IList<Site> GetAvailableSites(int campgroundId, DateTime arrivalDate, DateTime departureDate);
        Site GetSiteById(int siteId);
    }
}