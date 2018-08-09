using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    class NationalParkCLI
    {
        ParkSqlDAL _parkDal = new ParkSqlDAL(DatabaseConnectionString);
        CampgroundSqlDAL _campgroundDAL = new CampgroundSqlDAL(DatabaseConnectionString);
        ReservationSqlDAL _reservationDAL = new ReservationSqlDAL(DatabaseConnectionString);
        CampSiteSqlDAL _campsiteDAL = new CampSiteSqlDAL(DatabaseConnectionString);
        Dictionary<int, Park> _parks = new Dictionary<int, Park>();
        Dictionary<int, Campground> _campgrounds = new Dictionary<int, Campground>();
        Dictionary<int, Campsite> _campsites = new Dictionary<int, Campsite>();
        DateTime _arrivalDate;
        DateTime _departureDate;
        const string DatabaseConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NationalPark;Integrated Security = True";

        //public void RunCLI()
        //{
            //PrintHeader();
            //PrintMenu();
            //bool exit = false;

            //while (!exit)
            //{
            //Console.Clear();

            //PrintHeader();
            //PrintMenu();


            //char input = Console.ReadKey().KeyChar;
            //int userInput = CLIHelper.GetInteger("Select a park for more information or select 'q' to quit.");
            //if (userInput == 0 )
            //{
            //    exit = true;
            //}
            //else 
            //{

            //    //string inputStr = input.ToString();
            //    //int userInput = Int32.Parse(inputStr);
            //    if (userInput > 0 && userInput <= _parks.Count)
            //    {
            //        DisplayParkMenu(_parks[userInput]);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Please choose a valid option.");
            //        Console.ReadKey();
            //    }
            //}
            //}
        //}

       
        public void PrintHeader()
        {
            Console.WriteLine(@" _   _       _   _                   _   _____           _ ");
            Console.WriteLine(@"| \ | |     | | (_)                 | | |  __ \         | |");
            Console.WriteLine(@"|  \| | __ _| |_ _  ___  _ __   __ _| | | |__) |_ _ _ __| | _____");
            Console.WriteLine(@"| . ` |/ _` | __| |/ _ \| '_ \ / _` | | |  ___/ _` | '__| |/ / __|");
            Console.WriteLine(@"| |\  | (_| | |_| | (_) | | | | (_| | | | |  | (_| | |  |   <\__ \");
            Console.WriteLine(@"|_| \_|\__,_|\__|_|\___/|_| |_|\__,_|_| |_|   \__,_|_|  |_|\_\___/");
        }

        public void PrintMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                PrintHeader();
                Console.WriteLine();
                Console.WriteLine("Select a park for more information or select '0' to quit.");
                DisplayParks();
                Console.WriteLine("0) Quit");
                Console.WriteLine();
                int userInput = CLIHelper.GetInteger("Enter your selection: ");
               
                if (userInput == 0)
                {
                    exit = true;
                }
                else
                {
                    if (userInput > 0 && userInput <= _parks.Count)
                    {
                        DisplayParkMenu(_parks[userInput]);
                    }
                    else
                    {
                        Console.WriteLine("Please choose a valid option.");
                        Console.ReadKey();
                        //WriteLine "Press any key to continue" OR Bool (Do we want to display error message?)
                    }
                }
            }
        }

        private void DisplayParks()
        {
            _parks.Clear();
            List<Park> parksList = _parkDal.GetAllParks();
            Console.WriteLine();
            
            for (int index = 0; index < parksList.Count; index++)
            {
                Park park = parksList[index];              
                Console.WriteLine($"{index + 1}) {park.Name}");                
                _parks.Add(index+1, park);
            }
            //display parks should renamed (ie InitializeParksMenu)
        }

        private void DisplayParkInformation(Park park)
        {
            Console.Clear();
            Console.WriteLine("Park Information Screen");
            Console.WriteLine();
            Console.WriteLine($"{park.Name}");
            Console.WriteLine($"Location: {park.Location}");
            Console.WriteLine($"Established: {park.Establish_Date}");
            Console.WriteLine($"Area: {park.Area}");
            Console.WriteLine($"Annual Visitors: {park.Visitors}");
            Console.WriteLine();
            Console.WriteLine($"{park.Description}");
        }

        private void DisplayParkMenu(Park park)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                DisplayParkInformation(park);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1) View Campgrounds");
                Console.WriteLine("2) Return to Main Menu");

                Console.WriteLine();
                int userSel = CLIHelper.GetInteger("Enter your selection: ", true);

                //char userSel = Console.ReadKey().KeyChar; //use CLI Helper

                if (userSel == 1)
                {
                    ParkCampgroundMenu(park);
                }

                else if (userSel == 2)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Please choose a valid option.");
                    Console.ReadKey();
                }   
            }
        }

        private void DisplayCampgrounds(Park park)
        {
            _campgrounds.Clear();
            List<Campground> campgroundsList = _campgroundDAL.GetAllCampgrounds(park);
            Console.WriteLine();

            for (int index = 0; index < campgroundsList.Count; index++)
            {
                Campground campground = campgroundsList[index];
                Console.WriteLine("{0, -4}{1, -20}{2, -20}{3, -20}{4, -20}","#"+ (index + 1), campground.Name, campground.OpenFromMonthStr, campground.OpenToMonthStr, campground.DailyFee.ToString("c"));
                _campgrounds.Add(index + 1, campground);
            }
            //same change as display parks --  display in display park or create initialize park
        }

        private void ParkCampgroundMenu(Park park)
        {            
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Park Campgrounds");
                Console.WriteLine($"{park.Name} National Park Campgrounds");
                Console.WriteLine();
                Console.WriteLine(string.Format("{0, -4}{1, -20}{2, -20}{3, -20}{4, -20}", "", "Name", "Open", "Close", "Daily Fee"));
                
                DisplayCampgrounds(park);
                Console.WriteLine();               
                Console.WriteLine("Please choose an option below.");
                Console.WriteLine("1) Search for available reservation.");
                Console.WriteLine("2) Return to previous screen.");

                char userSel = Console.ReadKey().KeyChar;  // CLI Helper
               
                if (userSel == '1')
                {
                    DisplayReservationMenu(park);
                }

                else if (userSel == '2')
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Please choose a valid option.");
                    Console.ReadKey();
                }               
            }
        }

        private void DisplayReservationMenu(Park park)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Search for Campground Reservation");
                Console.WriteLine();
                Console.WriteLine(string.Format("{0, -4}{1, -20}{2, -20}{3, -20}{4, -20}", "", "Name", "Open", "Close", "Daily Fee"));
                DisplayCampgrounds(park);
                Console.WriteLine();
                int campgroundId = CLIHelper.GetInteger("Choose a campground ? (enter 0 to cancel)");

                if (campgroundId == 0)
                {
                    exit = true;
                }
                else
                {
                    while (!exit)
                    {
                        _arrivalDate = CLIHelper.GetDate("What is the arrival date? (MM/DD/YYYY)");
                        exit = ValidateArrivalDate(_arrivalDate);
                    }

                    exit = false;

                    while (!exit)
                    {
                        _departureDate = CLIHelper.GetDate("What is the departure date? (MM/DD/YYYY)");
                        exit = ValidateDepartureDate(_departureDate);
                    }

                    exit = false;
                    int numDays = (int)(_departureDate - _arrivalDate).TotalDays;
                    decimal totalCost = CalculateCost(campgroundId, numDays);
                    while (!exit)
                    {
                        Console.WriteLine();
                        Console.WriteLine(string.Format("{0, -10}{1, -22}{2, -20}{3, -20}{4, -20}{5, -20}", "Site No.", "Max Occup.", "Max RV Length", "Accessible?", "Utilities", "Cost"));
                        DisplayCampgroundSites(campgroundId, _arrivalDate, _departureDate);
                        Console.WriteLine();
                        int inputNum = CLIHelper.GetInteger("Which site should be reserved? (Enter 0 to cancel)");

                        if (inputNum == 0)
                        {
                            exit = true;
                        }

                        else
                        {
                            //if campgrounds are empty, return "No sites available for dates requested"

                            //Console.WriteLine("What name should the reservation be made under?");
                            //string resName = Console.ReadLine();
                            string resName = CLIHelper.GetString("What name should the reservation be made under?");
                            int confirmNum = _reservationDAL.BookReservation(inputNum, _arrivalDate, _departureDate, resName);
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine($"The reservation has been made and the confirmation number is {confirmNum}");
                            Console.ReadKey();
                           //writeline "press a key to return to menu"

                        }
                    }
                }
            }
        }

        private bool ValidateArrivalDate(DateTime selArrivalDate)
        {
            bool exit = false;
            DateTime now = DateTime.Now;
            now = now - now.TimeOfDay;
            
                if (selArrivalDate >= now)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Please provide a valid date.");
                                    
                }

            return exit;
        }

        private bool ValidateDepartureDate(DateTime selDepartureDate)
        {
            bool exit = false;    

                if (selDepartureDate >_arrivalDate)
                {                 
                    exit = true;
                }
                else
                {
                    Console.WriteLine("The departure date has to be greater than or equal to arrival date");                   
                }
            
            return exit;
        }

        private void DisplayCampgroundSites(int campgroundId, DateTime arriveDate, DateTime departDate)
        {
            _campsites.Clear();
            List<Campsite> campsites = _campsiteDAL.GetCampgroundSites(campgroundId, arriveDate, departDate);

            for (int index = 0; index < campsites.Count; index++)
            {
                int numDays = (int)(_departureDate - _arrivalDate).TotalDays;
                decimal totalCost = CalculateCost(campgroundId, numDays);
                //should be in campground

                Campsite campsite = campsites[index];
                Console.WriteLine("{0, -10}{1, -20}{2, -20}{3, -20}{4, -20}{5, -20}", campsite.SiteNumber, campsite.MaxOccupancy, campsite.MaxRvLengthStr, campsite.IsAccessibleStr, campsite.UtilitiesStr, totalCost.ToString("c"));
                _campsites.Add(index + 1, campsite);
            }
            
        }

        public decimal CalculateCost(int campId, int numDays)
        {
            decimal cost = _campgrounds[campId].DailyFee * numDays;
            return cost;
        }
        //should be in campground 

    }
}
