using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace CLI
{
    /// <summary>
    /// A sub-menu 
    /// </summary>
    public class CampgroundSubMenu : CLIMenu
    {
        // Store any private variables here....

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>        
        protected IParkDAO parkDAO;
        private Park park;
        protected ICampgroundDAO campgroundDAO;
        private Campground campground;
        //the id of the selected park
        public CampgroundSubMenu(Park park, IParkDAO parkDAO, Campground campground, ICampgroundDAO campgroundDAO) :
            base("Sub-Menu 1")
        {
            // Store any values passed in....
            this.park = park;
            this.campground = campground;
            this.campgroundDAO = campgroundDAO;
        }
        //TODO 1: Pull in campground ID into submenu

        

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "Search for Available Reservation");
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
                    Console.WriteLine("hello world");
                    Pause("");
                    return true;
                case "2": // Do whatever option 2 is
                    break;                    
            }
            return true;
        }

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
            
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Hello world"));
            ResetColor();
            
            ResetColor();
        }

    }
}
