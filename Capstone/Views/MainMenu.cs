using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CLI
{
    /// <summary>
    /// The top-level menu in our application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        // You may want to store some private variables here.  YOu may want those passed in 
        // in the constructor of this menu

        /// <summary>
        /// Constructor adds items to the top-level menu. You will likely have parameters  passed in
        /// here...
        /// </summary>
        protected IParkDAO parkDAO;
        protected ICampgroundDAO campgroundDAO;
        protected IReservationDAO reservationDAO;
        protected ISiteDAO siteDAO;
        protected Site site;
        protected Reservation reservation;
        //protected Campground campground;
        public int parkId;
        public Campground campground;
        public MainMenu(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, IReservationDAO reservationDAO, ISiteDAO siteDAO) : base("Main Menu")
        {
            // Set any private variables here.
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.reservationDAO = reservationDAO;
            this.siteDAO = siteDAO;
            //this.campground = campground;
        }

        protected override void SetMenuOptions()
        {
            // A Sample menu.  Build the dictionary here
            this.menuOptions.Add("1", "View available parks");
            this.menuOptions.Add("Q", "Quit program");
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
                case "1": // Do whatever option 1 is. You may prompt the user for more information
                          // (using the Helper methods), and then pass those values into some 
                          //business object to get something done.
                    ListAllParks();
                    parkId = GetInteger("Please select the park number: ");
                    Park park = parkDAO.GetParkById(parkId);
                    //Find if the park exists
                    if (park == null)
                    {
                        Pause($"Sorry park {parkId} does not exist");
                        return true;
                    }
                    //Code was found go into  the park menu
                    ParkSubMenu submenu = new ParkSubMenu(park, campground, site, reservation, parkDAO, campgroundDAO, siteDAO, reservationDAO);
                    submenu.Run();
                    return true;
                //case "2": // Do whatever option 2 is
                //    string name = GetString("What is your name?");
                //    WriteError($"Not yet implemented, {name}.");
                //    Pause("");
                //    return true;    // Keep running the main menu
                case "2": 
                    // Create and show the sub-menu
                    //SubMenu1 sm = new SubMenu1();
                    //sm.Run();
                    return true;    // Keep running the main menu
            }
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }


        private void PrintHeader()
        {
            SetColor(ConsoleColor.Yellow);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Main Menu"));
            ResetColor();
        }
        private void ListAllParks()
        {
            IList<Park> parks = parkDAO.GetAllParks();
            foreach (Park park in parks)
            {
                Console.WriteLine(park);
            }
        }
    }
}
