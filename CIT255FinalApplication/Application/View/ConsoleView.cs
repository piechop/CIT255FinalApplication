using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganizer
{
    public class ConsoleView
    {
        #region ENUMERABLES


        #endregion

        #region FIELDS

        //
        // window size
        //
        private const int WINDOW_WIDTH = ViewSettings.WINDOW_WIDTH;
        private const int WINDOW_HEIGHT = ViewSettings.WINDOW_HEIGHT;

        //
        // horizontal and vertical margins in console window for display
        //
        private const int DISPLAY_HORIZONTAL_MARGIN = ViewSettings.DISPLAY_HORIZONTAL_MARGIN;
        private const int DISPALY_VERITCAL_MARGIN = ViewSettings.DISPALY_VERITCAL_MARGIN;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        /// <summary>
        /// method to display the manager menu and get the user's choice
        /// </summary>
        /// <returns></returns>
        public static Enum.ManagerAction GetUserActionChoice()
        {
            Enum.ManagerAction userActionChoice = Enum.ManagerAction.None;
            //
            // set a string variable with a length equal to the horizontal margin and filled with spaces
            //
            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

            //
            // set up display area
            //
            DisplayReset();

            //
            // display the menu
            //
            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Movie Organizer Menu", WINDOW_WIDTH));
            DisplayMessage("");

            Console.WriteLine(
                leftTab + "1. Display All Movies" + Environment.NewLine +
                leftTab + "2. Display a Movie Detail" + Environment.NewLine +
                leftTab + "3. Add a Movie" + Environment.NewLine +
                leftTab + "4. Delete a Movie" + Environment.NewLine +
                leftTab + "5. Edit a Movie" + Environment.NewLine +
                leftTab + "E. Exit" + Environment.NewLine);

            DisplayMessage("");
            DisplayPromptMessage("Enter the number/letter for the menu choice: ");
            ConsoleKeyInfo userResponse = Console.ReadKey(true);

            switch (userResponse.KeyChar)
            {
                case '1':
                    userActionChoice = Enum.ManagerAction.ListAllMovies;
                    break;
                case '2':
                    userActionChoice = Enum.ManagerAction.DisplayMovieDetail;
                    break;
                case '3':
                    userActionChoice = Enum.ManagerAction.AddMovie;
                    break;
                case '4':
                    userActionChoice = Enum.ManagerAction.DeleteMovie;
                    break;
                case '5':
                    userActionChoice = Enum.ManagerAction.UpdateMovie;
                    break;
                case '6':
                    break;
                case 'E':
                case 'e':
                    userActionChoice = Enum.ManagerAction.Quit;
                    break;
                default:
                    DisplayMessage("");
                    DisplayMessage("");
                    DisplayMessage("It appears you have selected an incorrect choice.");
                    DisplayMessage("");
                    DisplayMessage("Press any key to try again or the ESC key to exit.");

                    userResponse = Console.ReadKey(true);
                    if (userResponse.Key == ConsoleKey.Escape)
                    {
                        userActionChoice = Enum.ManagerAction.Quit;
                    }
                    break;
            }

            return userActionChoice;
        }

        /// <summary>
        /// method to display all Movie info
        /// </summary>
        public static void DisplayAllMovies(List<Movie> Movies)
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Display All Movies", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage("All of the existing movies are displayed below;");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Title".PadRight(25));

            DisplayMessage(columnHeader.ToString());

            foreach (Movie Movie in Movies)
            {
                StringBuilder MovieInfo = new StringBuilder();

                MovieInfo.Append(Movie.ID.ToString().PadRight(8));
                MovieInfo.Append(Movie.Title.PadRight(25));

                DisplayMessage(MovieInfo.ToString());
            }
        }

        /// <summary>
        /// method to get the user's choice of Movie id
        /// </summary>
        public static int GetMovieID(List<Movie> Movies)
        {
            int MovieID = -1;

            DisplayAllMovies(Movies);

            DisplayMessage("");
            DisplayPromptMessage("Enter the movie ID: ");

            MovieID = ConsoleUtil.ValidateIntegerResponse("Please enter the movie ID: ", Console.ReadLine());

            return MovieID;
        }

        /// <summary>
        /// method to display a Movie info
        /// </summary>
        public static void DisplayMovie(Movie Movie)
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Movie Details", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage(String.Format("Movie: {0}", Movie.Title));
            DisplayMessage("");

            DisplayMessage(String.Format("ID: {0}", Movie.ID.ToString()));
            DisplayMessage("");

            DisplayMessage(String.Format("Genre(s):"));
            foreach (Enum.Genre genre in Movie.Genre)
            {
                string movieGenre = genre.ToString();
                if (movieGenre.Contains("_"))
                {
                    movieGenre = movieGenre.Replace('_', ' ');
                }
                DisplayMessage(String.Format(movieGenre));
            }
            DisplayMessage("");

            DisplayMessage(String.Format("Release Date: {0}", Movie.Release.ToShortDateString()));
            DisplayMessage("");

            DisplayMessage(String.Format("Length (min.): {0}", Movie.MinuteLength.ToString()));
            DisplayMessage("");

            DisplayMessage(String.Format("Director: {0}", Movie.Director));
            DisplayMessage("");

            DisplayMessage(String.Format("Producer: {0}", Movie.Producer));
            DisplayMessage("");
        }

        /// <summary>
        /// method to add a Movie info
        /// </summary>
        public static Movie AddMovie()
        {
            Movie Movie = new Movie();

            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Add A Movie", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayPromptMessage("Enter the Movie ID: ");
            Movie.ID = ConsoleUtil.ValidateIntegerResponse("Please enter the Movie ID: ", Console.ReadLine());
            DisplayMessage("");

            DisplayPromptMessage("Enter the Movie title: ");
            Movie.Title = Console.ReadLine();
            DisplayMessage("");
            
            return Movie;
        }

        public static Movie UpdateMovie(Movie Movie)
        {
            string userResponse = "";

            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Edit A Movie", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage(String.Format("Current Title: {0}", Movie.Title));
            DisplayPromptMessage("Enter a new title or just press Enter to keep the current title: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                Movie.Title = userResponse;
            }

            DisplayContinuePrompt();

            return Movie;
        }

        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Movie Organizer", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine(ConsoleUtil.Center("Press any key to continue.", WINDOW_WIDTH));
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }


        /// <summary>
        /// display the Exit prompt
        /// </summary>
        public static void DisplayExitPrompt()
        {
            DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            DisplayMessage("Thank you for using our Movie Organizer. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("Welcome to", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Movie Organizer", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a message in the message area
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            // message is not an empty line, display text
            if (message != "")
            {
                //
                // create a list of strings to hold the wrapped text message
                //
                List<string> messageLines;

                //
                // call utility method to wrap text and loop through list of strings to display
                //
                messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);
                foreach (var messageLine in messageLines)
                {
                    Console.WriteLine(messageLine);
                }
            }
            // display an empty line
            else
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// display a message in the message area without a new line for the prompt
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayPromptMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            //
            // create a list of strings to hold the wrapped text message
            //
            List<string> messageLines;

            //
            // call utility method to wrap text and loop through list of strings to display
            //
            messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);

            for (int lineNumber = 0; lineNumber < messageLines.Count() - 1; lineNumber++)
            {
                Console.WriteLine(messageLines[lineNumber]);
            }

            Console.Write(messageLines[messageLines.Count() - 1]);
        }


        #endregion
    }
}
