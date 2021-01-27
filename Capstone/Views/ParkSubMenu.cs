using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace CLI
{
    /// <summary>
    /// A sub-menu 
    /// </summary>
    public class ParkSubMenu : CLIMenu
    {
        int campgroundId;
        int siteId;
        // Store any private variables here....

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>        
        protected IParkDAO parkDAO;
        protected ICampgroundDAO campgroundDAO;
        protected ISiteDAO siteDAO;
        protected IReservationDAO reservationDAO;
        private Park park;
        private Site site;
        public Campground campground;
        //public int parkId;
        public ParkSubMenu(Park park, Campground campground, Site site, Reservation reservation, IParkDAO parkDAO, ICampgroundDAO campgroundDao, ISiteDAO siteDAO, IReservationDAO reservationDAO) :
            base("Sub-Menu 1")
        {
            // Store any values passed in....
            this.park = park;
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDao;
            this.siteDAO = siteDAO;
            this.reservationDAO = reservationDAO;
            this.campground = campground;
            this.site = site;

        }

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "View Campgrounds");
            // this.menuOptions.Add("2", "Search for Reservation");
            this.menuOptions.Add("2", "Return to Previous Screen");
            this.quitKey = "2";// TODO Make quit key the same on all menus
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1": // Do whatever option 1 is
                    Console.WriteLine($"{ null,-3}Name{ null,-16} Open{ null,-6} Close{ null,-5} Daily Fee{ null,-6}");
                    ListAllCampgrounds();
                    ListSites();
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    this.siteId = GetInteger("Which site should be reserved (Enter 0 to cancel): ");
                    Site site = siteDAO.GetSiteById(siteId);
                    
                    if (siteId == 0)
                    {
                        Console.Clear();
                    }
                    else if (siteId == site.Campground_ID)
                    {
                        Console.WriteLine($"Please enter the name for your reservation: ");
                        string name = Console.ReadLine();
                        reservationDAO.CreateReservation(site.Site_ID, name, arrivalDate, departureDate, createdDate);

                    }                       

                    return true;
                case "2": // Do whatever option 2 is                                        
                    break;                   
            }
            return true;
        }

        
        private DateTime arrivalDate;
        private DateTime departureDate;
        private DateTime createdDate = DateTime.Now;
     
        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }

        protected override void AfterDisplayMenu()
        {
            base.AfterDisplayMenu();
            SetColor(ConsoleColor.Cyan);
            //Console.WriteLine("Display some data here, AFTER the sub-menu is shown....");

            ResetColor();
        }

        private void PrintHeader()
        {
            //strings to format 
            DateTime date = park.Establish_Date;
            int parkArea = park.Area;
            int population = park.Visitors;

            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Park Information Screen"));
            ResetColor();
            Console.WriteLine($"{park.Name}");
            Console.WriteLine($"Location: {park.Location, -40}"); //TODO: Fix spacing
            Console.WriteLine($"Established: {date.ToString("d", DateTimeFormatInfo.InvariantInfo)}");
            Console.WriteLine($"Area: {parkArea.ToString("N0")} sq km");
            Console.WriteLine($"Annual Visitors: {population.ToString("N0")}");
            Console.WriteLine("");
            Console.WriteLine(park.Description.ToString()); //TODO: Fix word wrap

            ResetColor();
        }
        private void ListAllCampgrounds()
        {
            IList<Campground> campgrounds = campgroundDAO.GetAllCampgrounds(park.Park_Id);
            foreach (Campground campground in campgrounds)
            {
                Console.WriteLine(campground);
            }
        }
        private void ListAllAvailableSites()
        {
            IList<Site> sites = siteDAO.GetAvailableSites(campgroundId, arrivalDate, departureDate);
            foreach (Site site in sites)
            {
                
               
                Console.WriteLine($"Site No.{ null,-10} Max Occup.{ null,-10} Accessible?{ null,-10} Max RV Length{ null,-10} Utility{null,-10} Cost{null,24}");
                Console.WriteLine($"{site} {TotalCostOfStay():C2}"); // TODO formatting
            }
        }
        private decimal TotalCostOfStay()
        {

            System.TimeSpan lengthOfStay = departureDate.Subtract(arrivalDate);
            int lengthOfStayDays = lengthOfStay.Days;
            decimal totalCostOfStay = (decimal)lengthOfStayDays * campground.Daily_Fee;
            return totalCostOfStay;

        }
        private void ListSites()
        {
            campgroundId = GetInteger("What Campground (Press 0 to cancel): ");
            campground = campgroundDAO.GetCampgroundById(campgroundId);
            if (campgroundId == 0)
            {
                Console.Clear();
            }
            else if (campgroundId == campground.Campground_ID)
            {
                Console.WriteLine("What is the arrival date (YYYY/MM/DD)? ");
               // Console.WriteLine($"{null, -26}");
                //int arrivalDateInt = Console.Read(); // TODO Figure out why you can't do a console.read on any type but int ???
                arrivalDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What is the departure date (YYYY/MM/DD)? ");
                departureDate = DateTime.Parse(Console.ReadLine());
                ListAllAvailableSites();
            }
        }

    }
}
