using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface ICampgroundDAO
    {
        IList<Campground> GetAllCampgrounds(int parkId);
        Campground GetCampgroundById(int campgroundId);
    }
}