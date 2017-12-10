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

        private static int _page = 0;

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
                leftTab + "6. Sort/Query Movies" + Environment.NewLine +
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
                    userActionChoice = Enum.ManagerAction.SortQueryMovies;
                    break;
                case 'E':
                case 'e':
                    if (ValidateExit())
                    {
                        userActionChoice = Enum.ManagerAction.Quit;
                    }
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
                       if(ValidateExit())
                        {
                            userActionChoice = Enum.ManagerAction.Quit;
                        }
                    }
                    break;
            }

            return userActionChoice;
        }

        public static Enum.ManagerAction GetSortQueryChoice(out bool response)
        {
            Enum.ManagerAction userActionChoice = Enum.ManagerAction.None;
            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);
            string userResponse = null;
            response = false;
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Movie Organizer Sort/Query", WINDOW_WIDTH));
            DisplayMessage("");

            Console.WriteLine(
                leftTab + "1. Query Person by Role (Producer/Director)" + Environment.NewLine +
                leftTab + "2. Query by First Letter" + Environment.NewLine +
                leftTab + "3. Query by Genre" + Environment.NewLine +
                leftTab + "4. Query by Release Year" + Environment.NewLine +
                leftTab + "5. Sort by Release Year" + Environment.NewLine +
                leftTab + "6. Sort by Title" + Environment.NewLine);

            DisplayMessage("");
            DisplayPromptMessage("Enter the number corresponding to the Query/Sort option or press ESC to return to the main menu: ");
            //ConsoleKeyInfo userResponse = Console.ReadKey(true);
            ConsoleKeyInfo info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
            if(info.Key == ConsoleKey.Enter)
            {
                switch (userResponse)
                {
                    case "1":
                        userActionChoice = Enum.ManagerAction.QueryPersonByRole;
                           
                        break;
                    case "2":
                        userActionChoice = Enum.ManagerAction.QueryByFirstLetter;
                            
                        break;
                    case "3":
                        userActionChoice = Enum.ManagerAction.QueryByGenre;
                           
                        break;
                    case "4":
                        userActionChoice = Enum.ManagerAction.QueryByReleaseYear;
                            
                        break;
                    case "5":
                        userActionChoice = AscendingChoice(Enum.ManagerAction.SortByAscendingYear);
                        break;
                    case "6":
                        userActionChoice = AscendingChoice(Enum.ManagerAction.SortByAscendingTitle);
                        break;
                    default:
                        DisplayMessage("");
                        DisplayMessage("");
                        DisplayMessage("It appears you have selected an incorrect choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
            else if(info.Key == ConsoleKey.Escape)
            {
                response = true;
            }

            return userActionChoice;
        }

        public static Enum.Role RoleChoice()
        {
            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);
            Enum.Role choice = Enum.Role.None;
            string userResponse = null;

            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Movie Organizer - Query by Role", WINDOW_WIDTH));
            DisplayMessage("");

            Console.WriteLine(
                leftTab + "1. Director" + Environment.NewLine +
                leftTab + "2. Producer" + Environment.NewLine);

            DisplayMessage("");
            DisplayPromptMessage("Enter the number corresponding to the role of the person you wish to query: ");
            //ConsoleKeyInfo userResponse = Console.ReadKey(true);
            ConsoleKeyInfo info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
            if (info.Key == ConsoleKey.Enter)
            {
                switch (userResponse)
                {
                    case "1":
                        choice = Enum.Role.Director;
                        break;
                    case "2":
                        choice = Enum.Role.Producer;
                        break;
                    default:
                        choice = Enum.Role.None;
                        DisplayMessage("");
                        DisplayMessage("");
                        DisplayMessage("It appears you have selected an incorrect choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
            return choice;
        }

        public static string GetNameQuery(Enum.Role role)
        {
            string userResponse = null;
            bool response = false;
            ConsoleKeyInfo info;

            while (role != Enum.Role.None && !response)
            {
                DisplayReset();
                DisplayPromptMessage("Enter the name of the " + role.ToString() + " you wish to query:");
                info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
                if (info.Key == ConsoleKey.Enter)
                {
                    response = true;
                }
                else if (info.Key == ConsoleKey.Escape)
                {
                    userResponse = null;
                    response = true;
                }
            }

            return userResponse;
        }

        public static char GetLetterQuery()
        {
            string userResponse = null;
            bool response = false;
            char letter = '\u0000';
            ConsoleKeyInfo info;

            while (!response)
            {
                DisplayReset();
                DisplayPromptMessage("Enter the first letter of the movies' titles you wish to query:");
                info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
                if (info.Key == ConsoleKey.Enter)
                {
                    if (char.TryParse(userResponse, out letter) && char.IsLetter(letter))
                    {
                        response = true;
                    }
                    else
                    {
                        DisplayReset();
                        if (!char.IsLetter(letter))
                        {
                            DisplayMessage("Input is not a letter.");
                        }
                        DisplayMessage("Please enter a valid letter.");
                        DisplayContinuePrompt();
                    }
                }
                else if (info.Key == ConsoleKey.Escape)
                {
                    letter = '\u0000';
                    userResponse = null;
                    response = true;
                }
            }

            return letter;
        }

        public static Enum.Genre GetGenreQuery()
        {
            string userResponse = null;
            bool response = false;
            Enum.Genre gen = Enum.Genre.None;
            ConsoleKeyInfo info;

            while (!response)
            {
                DisplayReset();
                DisplayMessage("Available Genres:");
                DisplayMessage("");
                ListGenres();
                DisplayMessage("");
                DisplayPromptMessage("Enter the number corresponding to the genre of the movies you wish to query:");
                info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
                if (info.Key == ConsoleKey.Enter)
                {
                    if (System.Enum.TryParse(userResponse, out gen) && gen != Enum.Genre.None)
                    {
                        response = true;
                    }
                    else
                    {
                        DisplayReset();

                        DisplayMessage("Please enter a valid number that corresponds to a genre.");
                        DisplayContinuePrompt();
                    }
                }
                else if (info.Key == ConsoleKey.Escape)
                {
                    gen = Enum.Genre.None;
                    userResponse = null;
                    response = true;
                }
            }

            return gen;
        }

        public static int GetYearQuery()
        {
            string userResponse = null;
            bool response = false;
            int year = -1;
            ConsoleKeyInfo info;

            while (!response)
            {
                DisplayReset();
                DisplayPromptMessage("Enter the release year of the movies you wish to query:");
                info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
                if (info.Key == ConsoleKey.Enter)
                {
                    if (int.TryParse(userResponse, out year) && year > 0)
                    {
                        response = true;
                    }
                    else
                    {
                        DisplayReset();
                        if (year < 1)
                        {
                            DisplayMessage("Year must be greater than 0.");
                        }
                        DisplayMessage("Please enter a valid number.");
                        DisplayContinuePrompt();
                    }
                }
                else if (info.Key == ConsoleKey.Escape)
                {
                    year = -1;
                    userResponse = null;
                    response = true;
                }
            }

            return year;
        }

        private static Enum.ManagerAction AscendingChoice(Enum.ManagerAction choice)
        {
            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);
            string userResponse = null;

            string characteristic = "Year";
            if(choice == Enum.ManagerAction.SortByAscendingTitle)
            {
                characteristic = "Title";
            }
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Movie Organizer - Sort " + characteristic, WINDOW_WIDTH));
            DisplayMessage("");

            Console.WriteLine(
                leftTab + "1. Ascending" + Environment.NewLine +
                leftTab + "2. Descending" + Environment.NewLine);

            DisplayMessage("");
            DisplayPromptMessage("Enter the number to sort by ascending or descending order: ");
            //ConsoleKeyInfo userResponse = Console.ReadKey(true);
            ConsoleKeyInfo info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
            if (info.Key == ConsoleKey.Enter)
            {
                switch (userResponse)
                {
                    case "1":
                        break;
                    case "2":
                        if (choice == Enum.ManagerAction.SortByAscendingTitle)
                        {
                            choice = Enum.ManagerAction.SortByDescendingTitle;
                        }
                        else if (choice == Enum.ManagerAction.SortByAscendingYear)
                        {
                            choice = Enum.ManagerAction.SortByDescendingYear;
                        }
                        break;
                    default:
                        choice = Enum.ManagerAction.None;
                        DisplayMessage("");
                        DisplayMessage("");
                        DisplayMessage("It appears you have selected an incorrect choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
            else if(info.Key == ConsoleKey.Escape)
            {
                choice = Enum.ManagerAction.None;
            }
            return choice;
        }

        private static bool ValidateExit()
        {
            bool? response = false;
            do
            {
                DisplayReset();
                DisplayPromptMessage("Are you sure you wish to exit? (y/n): ");
                response = GetYesValidation();
            }
            while (response != null && !(bool)response);

            if (response == null)
            {
                response = false;
            }

            return (bool)response;
        }

        public static void BrowseAllMovies(List<Movie> movies, bool displayYear, string info)
        {
            bool finished = false;
            while (!finished)
            {
                DisplayAllMovies(movies, displayYear);
                DisplayMessage("");
                DisplayMessage(info);
                DisplayMessage("");
                DisplayPromptMessage("Press any key to return to the main menu or the left and right arrow keys to browse the entries: ");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        ScrollLeft(movies.Count);
                        break;
                    case ConsoleKey.RightArrow:
                        ScrollRight(movies.Count);
                        break;
                    default:
                        finished = true;
                        break;
                }
            }
        }

        /// <summary>
        /// method to display all Movie info
        /// </summary>
        private static void DisplayAllMovies(List<Movie> movies, bool displayYear)
        {
            ResetDisplayAllMoviesMessage();

            DisplayMessage("All of the existing movies are displayed below;");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Title".PadRight(25));
            if(displayYear)
            {
                columnHeader.Append("Year".PadRight(42));
            }

            DisplayMessage(columnHeader.ToString());

            for(int i=_page*10;i<(_page*10)+10;i++)
            {
                StringBuilder MovieInfo = new StringBuilder();

                try { 
                    MovieInfo.Append(movies[i].ID.ToString().PadRight(8));
                    MovieInfo.Append(movies[i].Title.PadRight(25));
                    if(displayYear)
                    {
                        MovieInfo.Append(movies[i].Release.Year.ToString().PadRight(42));
                    }
                }
                catch(Exception e)
                {

                }

                DisplayMessage(MovieInfo.ToString());
            }

            DisplayMessage("");
            DisplayMessage("Entries: " + _page*10 + " - " + ((_page*10)+10));

            //foreach (Movie Movie in Movies)
            //{
            //    StringBuilder MovieInfo = new StringBuilder();

            //    MovieInfo.Append(Movie.ID.ToString().PadRight(8));
            //    MovieInfo.Append(Movie.Title.PadRight(25));

            //    DisplayMessage(MovieInfo.ToString());
            //}
        }

        /// <summary>
        /// method to get the user's choice of Movie id
        /// </summary>
        public static int GetMovieID(List<Movie> movies)
        {
            int movieID = -1;
            do
            {
                DisplayAllMovies(movies, false);

                DisplayMessage("");
                DisplayPromptMessage("Enter the movie ID or press the left or right arrow keys to scroll through entries. Press ESC to return to the main menu: ");
                
                bool containsID = false;
                string result = null;
                ConsoleKeyInfo info = GetUserInputWithEscape(Console.ReadKey(true), out result);

                if (info.Key == ConsoleKey.Enter)
                {
                    movieID = (int.TryParse(result, out movieID) ? movieID : -1);
                    foreach (Movie mov in movies)
                    {
                        if (mov.ID == movieID)
                        {
                            containsID = true;
                            break;
                        }
                    }

                    if (movieID == -1 || !containsID)
                    {
                        ResetEditMovieMessage();
                        Console.WriteLine(ConsoleUtil.Center("Please enter a valid number for the Movie's ID.", WINDOW_WIDTH));
                        if (!containsID && movieID >= 0)
                        {
                            Console.WriteLine(ConsoleUtil.Center("Movie ID not found in Movie collections.",WINDOW_WIDTH));
                            movieID = -1;
                        }
                        DisplayContinuePrompt();
                    }
                }
                else if(info.Key == ConsoleKey.LeftArrow)
                {
                    ScrollLeft(movies.Count);
                }
                else if(info.Key == ConsoleKey.RightArrow)
                {
                    ScrollRight(movies.Count);
                }
                else if(info.Key == ConsoleKey.Escape)
                {
                    movieID = -2;
                }
            }
            while (movieID == -1);
            
            return movieID;
        }

        private static void ScrollLeft(int movies)
        {
            int maxPages = (int)Math.Ceiling(movies / 10.0)-1;

            _page--;
            if(_page<0)
            {
                _page = maxPages;
                if(_page == -1)
                {
                    _page = 0;
                }
            }
        }

        private static void ScrollRight(int movies)
        {
            int maxPages = (int)Math.Ceiling(movies / 10.0)-1;

            _page++;
            if (_page > maxPages)
            {
                _page = 0;
            }
        }

        private static ConsoleKeyInfo GetUserInputWithEscape(ConsoleKeyInfo info, out string result)
        {
            StringBuilder builder = new StringBuilder();

            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape && info.Key != ConsoleKey.LeftArrow && info.Key != ConsoleKey.RightArrow)
            {
                if (info.Key != ConsoleKey.UpArrow && info.Key != ConsoleKey.DownArrow)
                {
                    
                    if (info.Key == ConsoleKey.Backspace && builder.Length > 0)
                    {
                        builder.Remove(builder.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                    else
                    {
                        Console.Write(info.KeyChar);
                        builder.Append(info.KeyChar);
                    }
                }

                info = Console.ReadKey(true);
            }
            result = builder.ToString();

            return info;
        }

        /// <summary>
        /// method to display a Movie info
        /// </summary>
        public static void DisplayMovie(Movie movie)
        {
            if (movie != null)
            {
                DisplayReset();

                DisplayMessage("");
                Console.WriteLine(ConsoleUtil.Center("Movie Details", WINDOW_WIDTH));
                DisplayMessage("");

                DisplayMessage(String.Format("Movie: {0}", movie.Title));
                DisplayMessage("");

                DisplayMessage(String.Format("ID: {0}", movie.ID.ToString()));
                DisplayMessage("");

                DisplayMessage(String.Format("Genre(s):"));
                foreach (Enum.Genre genre in movie.Genre)
                {
                    string movieGenre = genre.ToString();
                    if (movieGenre.Contains("_"))
                    {
                        movieGenre = movieGenre.Replace('_', ' ');
                    }
                    DisplayMessage(String.Format(movieGenre));
                }
                DisplayMessage("");

                DisplayMessage(String.Format("Release Date: {0}", movie.Release.ToShortDateString()));
                DisplayMessage("");

                DisplayMessage(String.Format("Length (min.): {0}", movie.MinuteLength.ToString()));
                DisplayMessage("");

                DisplayMessage(String.Format("Director: {0}", movie.Director));
                DisplayMessage("");

                DisplayMessage(String.Format("Producer: {0}", movie.Producer));
                DisplayMessage("");
            }
        }

        /// <summary>
        /// method to add a Movie info
        /// </summary>
        public static Movie AddMovie()
        {
            Movie movie = new Movie();
            
            movie = AddTitle(movie);
            if(movie!=null)
                movie = AddGenres(movie);
            if(movie!=null)
                movie = AddRelease(movie);
            if (movie != null)
                movie = AddLength(movie);
            if (movie != null)
                movie = AddDirector(movie);
            if (movie != null)
                movie = AddProducer(movie);

            if(movie!=null && !ValidateMovie(movie))
            {
                movie = null;
            }
            
            return movie;
        }

        private static void ResetAddMovieDisplay()
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Add A Movie", WINDOW_WIDTH));
            DisplayMessage("");
        }

        private static Movie AddTitle(Movie movie)
        {
            string userResponse = null;
            bool response = false;
            ConsoleKeyInfo info;

            while (!response)
            {
                ResetAddMovieDisplay();
                DisplayPromptMessage("Enter the Movie's title or press ESC to return to the main menu: ");
                try
                {
                    info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
                    if (info.Key == ConsoleKey.Enter)
                    {
                        if (ValidateEdit(movie, "Title: ", userResponse))
                        {
                            movie.Title = userResponse;
                            response = true;
                        }
                    }
                    else if(info.Key == ConsoleKey.Escape)
                    {
                        response = true;
                        movie = null;
                    }

                }
                catch (ArgumentException e)
                {
                    ResetEditMovieMessage();
                    Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                    DisplayContinuePrompt();
                }
            }
            return movie;
        }

        private static Movie AddGenres(Movie movie)
        {
            string userResponse = null;
            bool response = false;
            ConsoleKeyInfo info;
            int genres = 0;

            while (!response)
            {
                ResetAddMovieDisplay();
                DisplayMessage("Available genres:");
                DisplayMessage("");
                ListGenres();
                DisplayMessage("");
                if(genres==1)
                {
                    DisplayMessage("If the movie only has one genre, enter 0.");
                }
                DisplayPromptMessage("Enter the Movie's "+ (genres==1?"second":"first") + " genre using a number from the list above or press ESC to return to the main menu: ");
                try
                {
                    info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
                    if (info.Key == ConsoleKey.Enter)
                    {
                        Enum.Genre gen;
                        bool validResponse = System.Enum.TryParse(userResponse, out gen);

                        if (!validResponse || (gen == Enum.Genre.None&&genres==0))
                        {
                            DisplayReset();
                            if(gen==Enum.Genre.None && genres == 0)
                            {
                                DisplayMessage("A movie must have at least one genre.");
                            }
                            DisplayMessage("Please enter a number corresponding to the genre from the list.");
                            DisplayContinuePrompt();
                        }
                        else if (ValidateEdit(movie, "Genre: ", gen.ToString()))
                        {
                            if(!movie.Genre.Contains(gen))
                            {
                                if(gen != Enum.Genre.None)
                                {
                                    movie.Genre.Add(gen);
                                }
                                genres++;
                                if (genres == 2)
                                {
                                    response = true;
                                }
                            }
                            else if(movie.Genre.Contains(gen))
                            {
                                DisplayMessage("");
                                DisplayMessage("You can only have one type of genre per movie.");
                                DisplayContinuePrompt();
                            }
                        }
                    }
                    else if (info.Key == ConsoleKey.Escape)
                    {
                        response = true;
                        movie = null;
                    }

                }
                catch (ArgumentException e)
                {
                    ResetEditMovieMessage();
                    Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                    DisplayContinuePrompt();
                }
            }
            return movie;
        }

        private static Movie AddRelease(Movie movie)
        {
            string userResponse = null;
            bool response = false;
            ConsoleKeyInfo info;

            while (!response)
            {
                ResetAddMovieDisplay();
                DisplayPromptMessage("Enter the Movie's release date (mm/dd/yyyy) or press ESC to return to the main menu: ");
                try
                {
                    info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
                    if (info.Key == ConsoleKey.Enter)
                    {
                        DateTime date;
                        bool validResponse = DateTime.TryParse(userResponse, out date);

                        if(!validResponse)
                        {
                            DisplayReset();
                            DisplayMessage("Please enter a date in the format mm/dd/yyyy.");
                            DisplayContinuePrompt();
                        }
                        else if (ValidateEdit(movie, "Release Date: ", date.ToShortDateString()))
                        {
                            movie.Release = date;
                            response = true;
                        }
                    }
                    else if (info.Key == ConsoleKey.Escape)
                    {
                        response = true;
                        movie = null;
                    }

                }
                catch (ArgumentException e)
                {
                    ResetEditMovieMessage();
                    Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                    DisplayContinuePrompt();
                }
            }
            return movie;
        }

        private static Movie AddLength(Movie movie)
        {
            string userResponse = null;
            bool response = false;
            ConsoleKeyInfo info;

            while (!response)
            {
                ResetAddMovieDisplay();
                DisplayPromptMessage("Enter the Movie's length in minutes or press ESC to return to the main menu: ");
                try
                {
                    info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
                    if (info.Key == ConsoleKey.Enter)
                    {
                        int minutes;
                        bool validResponse = int.TryParse(userResponse, out minutes);

                        if (!validResponse)
                        {
                            DisplayReset();
                            DisplayMessage("Please enter a valid number.");
                            DisplayContinuePrompt();
                        }
                        else if (ValidateEdit(movie, "Length (minutes): ", minutes.ToString()))
                        {
                            movie.MinuteLength = minutes;
                            response = true;
                        }
                    }
                    else if (info.Key == ConsoleKey.Escape)
                    {
                        response = true;
                        movie = null;
                    }

                }
                catch (ArgumentException e)
                {
                    ResetEditMovieMessage();
                    Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                    DisplayContinuePrompt();
                }
            }
            return movie;
        }

        private static Movie AddDirector(Movie movie)
        {
            string userResponse = null;
            bool response = false;
            ConsoleKeyInfo info;

            while (!response)
            {
                ResetAddMovieDisplay();
                DisplayPromptMessage("Enter the Movie's Director or press ESC to return to the main menu: ");
                try
                {
                    info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
                    if (info.Key == ConsoleKey.Enter)
                    {
                        if (ValidateEdit(movie, "Director: ", userResponse))
                        {
                            movie.Director = userResponse;
                            response = true;
                        }
                    }
                    else if (info.Key == ConsoleKey.Escape)
                    {
                        response = true;
                        movie = null;
                    }

                }
                catch (ArgumentException e)
                {
                    ResetEditMovieMessage();
                    Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                    DisplayContinuePrompt();
                }
            }
            return movie;
        }

        private static Movie AddProducer(Movie movie)
        {
            string userResponse = null;
            bool response = false;
            ConsoleKeyInfo info;

            while (!response)
            {
                ResetAddMovieDisplay();
                DisplayPromptMessage("Enter the Movie's Producer or press ESC to return to the main menu: ");
                try
                {
                    info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);
                    if (info.Key == ConsoleKey.Enter)
                    {
                        if (ValidateEdit(movie, "Producer: ", userResponse))
                        {
                            movie.Producer = userResponse;
                            response = true;
                        }
                    }
                    else if (info.Key == ConsoleKey.Escape)
                    {
                        response = true;
                        movie = null;
                    }

                }
                catch (ArgumentException e)
                {
                    ResetEditMovieMessage();
                    Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                    DisplayContinuePrompt();
                }
            }
            return movie;
        }

        public static Movie UpdateMovie(Movie movie)
        {
            bool validResponse = false;
            if (movie != null)
            {
                do
                {
                    ResetEditMovieMessage();

                    string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

                    DisplayMovie(movie);

                    Console.WriteLine(
                    leftTab + "1. Edit Title" + Environment.NewLine +
                    leftTab + "2. Edit Genres" + Environment.NewLine +
                    leftTab + "3. Edit Release Date" + Environment.NewLine +
                    leftTab + "4. Edit Length (Minutes)" + Environment.NewLine +
                    leftTab + "5. Edit Director" + Environment.NewLine +
                    leftTab + "6. Edit Producer" + Environment.NewLine);

                    DisplayPromptMessage("Enter the number corresponding to the variable you wish to edit or press ESC to return to the main menu: ");
                    switch (GetUserKey())
                    {
                        case '1':
                            UpdateTitle(movie);
                            break;
                        case '2':
                            UpdateGenres(movie);
                            break;
                        case '3':
                            UpdateReleaseDate(movie);
                            break;
                        case '4':
                            UpdateLength(movie);
                            break;
                        case '5':
                            UpdateDirector(movie);
                            break;
                        case '6':
                            UpdateProducer(movie);
                            break;
                        case '\u0000':
                            validResponse = true;
                            break;
                        case 'n':
                        case 'N':
                            break;
                        default:
                            ResetEditMovieMessage();

                            DisplayPromptMessage("Invalid choice. Please enter a number between 1 and 6.");
                            DisplayMessage("");
                            DisplayContinuePrompt();
                            break;
                    }
                }
                while (!validResponse);
            }
            return movie;
        }

        public static bool ValidateEdit(Movie movie, string action, string userResponse)
        {
            bool? response;
            do
            {
                DisplayMovie(movie);
                DisplayMessage("");
                DisplayMessage(action + ": " + userResponse);
                DisplayMessage("");
                DisplayPromptMessage("Continue? (y/n): ");

                response = GetYesValidation();
            }
            while (response != null && !(bool)response);

            if(response == null)
            {
                response = false;
            }

            return (bool)response;
        }

        public static bool ValidateMovie(Movie movie)
        {
            bool? response;
            do
            {
                DisplayMovie(movie);
                DisplayMessage("");
                DisplayPromptMessage("Add movie? (y/n): ");

                response = GetYesValidation();
            }
            while (response != null && !(bool)response);

            if (response == null)
            {
                response = false;
            }

            return (bool)response;
        }

        private static bool UpdateProducer(Movie movie)
        {
            bool response = false;
            string userResponse = null;

            ResetEditMovieMessage();

            DisplayMessage(String.Format("Movie {0}: {1}", movie.ID, movie.Title));

            DisplayMessage(String.Format("Current Producer: {0}", movie.Producer));
            DisplayMessage("");
            DisplayPromptMessage("Enter a new name for the Producer or press ESC to return to the previous menu: ");
            ConsoleKeyInfo info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);

            if (info.Key == ConsoleKey.Enter)
            {
                try
                {
                    if (ValidateEdit(movie,"New Producer",userResponse))
                    {
                        movie.Producer = userResponse;
                        response = true;

                        ResetEditMovieMessage();
                        DisplayMovie(movie);

                        Console.WriteLine(ConsoleUtil.Center("Producer's name changed successfully.", WINDOW_WIDTH));
                        DisplayMessage("");
                    }
                    else
                    {
                        ResetEditMovieMessage();
                        DisplayMessage("Producer not edited. Returning to the previous menu.");
                    }
                }
                catch (ArgumentException e)
                {
                    ResetEditMovieMessage();
                    Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                }
                DisplayContinuePrompt();
            }

            return response;
        }

        private static bool UpdateDirector(Movie movie)
        {
            bool response = false;
            string userResponse = null;

            ResetEditMovieMessage();

            DisplayMessage(String.Format("Movie {0}: {1}", movie.ID, movie.Title));

            DisplayMessage(String.Format("Current Director: {0}", movie.Director));
            DisplayMessage("");
            DisplayPromptMessage("Enter a new name for the Director or press ESC to return to the previous menu: ");
            ConsoleKeyInfo info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);

            if (info.Key == ConsoleKey.Enter)
            {
                try
                {
                    if (ValidateEdit(movie, "New Director",userResponse))
                    {
                        movie.Director = userResponse;
                        response = true;

                        ResetEditMovieMessage();
                        DisplayMovie(movie);

                        Console.WriteLine(ConsoleUtil.Center("Director's name changed successfully.", WINDOW_WIDTH));
                        DisplayMessage("");
                    }
                    else
                    {
                        ResetEditMovieMessage();
                        DisplayMessage("Director not edited. Returning to the previous menu.");
                    }
                }
                catch (ArgumentException e)
                {
                    ResetEditMovieMessage();
                    Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                }
                DisplayContinuePrompt();
            }

            return response;
        }

        private static bool UpdateLength(Movie movie)
        {
            bool response = false;
            string userResponse = null;
            int minutes;

            ResetEditMovieMessage();
            DisplayMessage(String.Format("Movie {0}: {1}",movie.ID,movie.Title));

            DisplayMessage(String.Format("Current Length (Minutes): {0}", movie.MinuteLength));
            DisplayMessage("");
            DisplayPromptMessage("Enter a new length greater than 0 in minutes or press ESC to return to the previous menu: ");
            ConsoleKeyInfo info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);

            if(info.Key == ConsoleKey.Enter)
            {
                if(int.TryParse(userResponse,out minutes))
                {
                    try
                    {
                        if (ValidateEdit(movie,"New Length",minutes.ToString()))
                        {
                            movie.MinuteLength = minutes;
                            response = true;

                            ResetEditMovieMessage();
                            DisplayMovie(movie);

                            Console.WriteLine(ConsoleUtil.Center("Length (minutes) changed successfully.", WINDOW_WIDTH));
                            DisplayMessage("");
                        }
                        else
                        {
                            ResetEditMovieMessage();
                            DisplayMessage("Length not edited. Returning to the previous menu.");
                        }
                    }
                    catch(ArgumentException e)
                    {
                        ResetEditMovieMessage();
                        Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                    }
                    DisplayContinuePrompt();
                }
                else
                {
                    DisplayMessage("");
                    DisplayMessage("Please enter a valid number.");
                    DisplayContinuePrompt();
                }
            }

            return response;
        }

        private static bool UpdateReleaseDate(Movie movie)
        {
            bool response = false;
            string userResponse = null;

            ResetEditMovieMessage();

            DisplayMessage(String.Format("Movie {0}: {1}", movie.ID, movie.Title));

            DisplayMessage(String.Format("Current Release Date: {0}", movie.Release.ToShortDateString()));
            DisplayMessage("");
            DisplayPromptMessage("Enter a new release date or press ESC to return to the previous menu\n(mm/dd/yyyy): ");
            ConsoleKeyInfo info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);

            if(info.Key == ConsoleKey.Enter)
            {
                DateTime date;
                if(DateTime.TryParse(userResponse,out date))
                {
                    try
                    {
                        if (ValidateEdit(movie,"New Release",date.ToShortDateString()))
                        {
                            movie.Release = date;
                            response = true;

                            ResetEditMovieMessage();
                            DisplayMovie(movie);

                            Console.WriteLine(ConsoleUtil.Center("Release date changed successfully.", WINDOW_WIDTH));
                            DisplayMessage("");
                        }
                        else
                        {
                            ResetEditMovieMessage();
                            DisplayMessage("Release date not edited. Returning to the previous menu.");
                        }
                    }
                    catch(ArgumentException e)
                    {
                        ResetEditMovieMessage();
                        Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                    }
                    DisplayContinuePrompt();
                }
                else
                {
                    DisplayMessage("");
                    DisplayMessage("Please enter a date in the format mm/dd/yyyy.");
                    DisplayContinuePrompt();
                }
            }

            return response;
        }

        private static bool UpdateTitle(Movie movie)
        {
            bool response = false;

            if (movie != null)
            {
                ResetEditMovieMessage();

                string userResponse = null;

                DisplayMessage(String.Format("Current Title: {0}", movie.Title));
                DisplayMessage("");
                DisplayPromptMessage("Enter a new title or press ESC to return to the previous menu: ");
                ConsoleKeyInfo info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);

                if (info.Key == ConsoleKey.Enter)
                {
                    try
                    {
                        if (ValidateEdit(movie,"New Title",userResponse))
                        {
                            movie.Title = userResponse;

                            ResetEditMovieMessage();
                            DisplayMovie(movie);

                            Console.WriteLine(ConsoleUtil.Center("Title changed successfully.", WINDOW_WIDTH));
                            DisplayMessage("");

                            response = true;
                        }
                        else
                        {
                            ResetEditMovieMessage();
                            DisplayMessage("Title not edited. Returning to the previous menu.");
                        }
                    }
                    catch (ArgumentException e)
                    {
                        ResetEditMovieMessage();

                        Console.WriteLine(ConsoleUtil.Center(e.Message, WINDOW_WIDTH));
                    }

                    DisplayContinuePrompt();
                }
            }
            return response;
        }

        private static bool UpdateGenres(Movie movie)
        {
            bool response = false;
            char key = '\u0000';
            if (movie != null)
            {
                do
                {
                    ResetEditMovieMessage();

                    string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

                    DisplayMovieGenres(movie);

                    Console.WriteLine(
                    leftTab + "1. Remove a Genre" + Environment.NewLine +
                    leftTab + "2. Add a Genre" + Environment.NewLine);

                    DisplayPromptMessage("Enter a number corresponding to the action you wish to take or press ESC to return to the previous menu: ");
                    key = GetUserKey();
                    switch (key)
                    {
                        case '1':
                            if (movie.Genre.Count == 1)
                            {
                                ResetEditMovieMessage();

                                DisplayPromptMessage("A movie must have at least one genre. Please add a new genre before deleting.");
                                DisplayMessage("");
                                DisplayContinuePrompt();
                            }
                            else
                            {
                                response = RemoveGenre(movie);
                            }
                            break;
                        case '2':
                            response = AddGenre(movie);
                            break;
                        case '\u0000':
                            response = true;
                            break;
                        case 'n':
                        case 'N':
                            break;
                        default:
                            ResetEditMovieMessage();

                            DisplayPromptMessage("Invalid choice. Please enter 1 or 2.");
                            DisplayMessage("");
                            DisplayContinuePrompt();
                            break;
                    }
                }
                while (!response);
            }
            if(key=='\u0000')
            {
                response = false;
            }
            return response;
        }

        private static void DisplayGenres(List<Enum.Genre> genres)
        {
            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);
            DisplayMessage("");
            Console.WriteLine(
                leftTab + "Genres" + Environment.NewLine +
                leftTab + "1. " + genres[0] + Environment.NewLine + (genres.Count > 1 ?
                leftTab + "2. " + genres[1]: "") + Environment.NewLine);
        }

        private static bool RemoveGenre(Movie movie)
        {
            bool validResponse = false;
            Enum.Genre genre = Enum.Genre.None;
            List<Enum.Genre> genres = movie.Genre;

            if (movie != null)
            {
                do
                {
                    ResetEditMovieMessage();
                    DisplayGenres(genres);
                    DisplayPromptMessage("Enter the number corresponding to the genre you wish to remove or press ESC to return to the main menu: ");

                    switch (GetUserKey())
                    {
                        case '1':
                            genre = genres[0];
                            validResponse = true;
                            break;
                        case '2':
                            if (genres.Count > 1)
                            {
                                genre = genres[1];
                                validResponse = true;
                            }
                            else
                            {
                                ResetEditMovieMessage();

                                DisplayPromptMessage("There is only one genre. Please enter 1.");
                                DisplayMessage("");
                                DisplayContinuePrompt();
                            }
                            break;
                        case '\u0000':
                            validResponse = true;
                            break;
                        case 'n':
                        case 'N':
                            break;
                        default:
                            ResetEditMovieMessage();

                            DisplayPromptMessage("Your choice is not a valid number. Please try again.");
                            DisplayMessage("");
                            DisplayContinuePrompt();
                            break;
                    }
                }
                while (!validResponse);

                if (genre != Enum.Genre.None)
                {
                    if (ValidateEdit(movie,"Genre to remove",genre.ToString()))
                    {
                        genres.Remove(genre);

                        ResetEditMovieMessage();

                        DisplayMovieGenres(movie);
                        DisplayMessage("Genre correctly removed: " + genre);
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        ResetEditMovieMessage();
                        DisplayMessage("Genre not removed. Returning to the previous menu.");
                        validResponse = false;
                    }
                }
            }
            return validResponse;
        }
        
        private static void DisplayMovieGenres(Movie movie)
        {
            if (movie != null)
            {
                DisplayMessage("Movie " + movie.ID + ": " + movie.Title);
                DisplayMessage("Current Genre(s):");
                foreach (Enum.Genre gen in movie.Genre)
                {
                    string movieGenre = gen.ToString();
                    if (movieGenre.Contains("_"))
                    {
                        movieGenre = movieGenre.Replace('_', ' ');
                    }
                    DisplayMessage(movieGenre);
                }
                DisplayMessage("");
            }
        }

        private static bool AddGenre(Movie movie)
        {
            bool validResponse = false;
            string userResponse = null;
            ConsoleKeyInfo info;

            do
            {
                ResetEditMovieMessage();

                DisplayMovieGenres(movie);
                DisplayMessage("Valid genres:");
                DisplayMessage("");
                ListGenres();
                DisplayMessage("");
                DisplayPromptMessage("Enter the number of a genre from the list above you wish to add. \nPress ESC to return to the main menu: ");
                info = GetUserInputWithEscape(Console.ReadKey(true), out userResponse);

                if (info.Key == ConsoleKey.Enter)
                {
                    Enum.Genre gen;
                    validResponse = System.Enum.TryParse(userResponse, out gen);
                    if (!validResponse && gen != Enum.Genre.None)
                    {
                        DisplayMessage("Please enter a number corresponding to the genre from the list.");
                        DisplayContinuePrompt();
                    }
                    else if (validResponse && movie.Genre.Count < 2 && !movie.Genre.Contains(gen))
                    {
                        if (ValidateEdit(movie, "New Genre", gen.ToString()))
                        {
                            ResetEditMovieMessage();
                            movie.Genre.Add(gen);
                            DisplayMessage("Genre successfully added.");
                            DisplayMessage("");
                            DisplayMovieGenres(movie);
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            ResetEditMovieMessage();
                            DisplayMessage("Genre not added. Returning to the previous menu.");
                            validResponse = false;
                        }
                    }
                    else if (movie.Genre.Count >= 2)
                    {
                        ResetEditMovieMessage();
                        DisplayMessage("A movie can only have two genres. Please remove one before adding a new genre.");
                        DisplayContinuePrompt();
                        validResponse = false;
                    }
                    else
                    {
                        ResetEditMovieMessage();
                        DisplayMessage("A movie cannot have the same genre twice.");
                        DisplayContinuePrompt();
                        validResponse = false;
                    }
                }
                else if(info.Key == ConsoleKey.Escape)
                {
                    validResponse = false;
                    break;
                }
            }
            while (!validResponse);

            return validResponse;
        }

        private static void ResetDisplayAllMoviesMessage()
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Display All Movies", WINDOW_WIDTH));
            DisplayMessage("");
        }

        private static void ResetEditMovieMessage()
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Edit A Movie", WINDOW_WIDTH));
            DisplayMessage("");
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
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

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

            DisplayMessage("Navigate the organizer using the keyboard and follow onscreen instructions.");

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

        private static char GetUserKey()
        {
            ConsoleKeyInfo userKey = Console.ReadKey();
            char key = userKey.KeyChar;

            if(userKey.KeyChar == '\u0000')
            {
                key = '`';
            }

            if (userKey.Key == ConsoleKey.Escape)
            {
                bool? userResponse = false;

                do
                {
                    DisplayReset();

                    DisplayMessage("");
                    Console.WriteLine(ConsoleUtil.Center("Return to main menu", WINDOW_WIDTH));
                    DisplayMessage("");

                    DisplayMessage("Would you like to go back? (y/n)");
                    userResponse = GetYesValidation();
                    if(userResponse == null)
                    {
                        key = 'n';
                        userResponse = true;
                    }
                    else if((bool)userResponse)
                    {
                        key = '\u0000';
                    }
                }
                while ((bool)!userResponse);
            }

            return key;
        }

        private static bool? GetYesValidation()
        {
            bool? isYes = false;

            switch(Console.ReadKey().KeyChar)
            {
                case 'y':
                case 'Y':
                    isYes = true;
                    break;
                case 'n':
                case 'N':
                    isYes = null;
                    break;
                default:
                    DisplayReset();
                    Console.WriteLine(ConsoleUtil.Center("Please answer yes (y) or no (n).",WINDOW_WIDTH));
                    DisplayContinuePrompt();
                    break;
            }

            return isYes;
        }

        private static void ListGenres()
        {
            int i = 0;
            foreach (Enum.Genre genre in System.Enum.GetValues(typeof(Enum.Genre)))
            {
                if (genre != Enum.Genre.None)
                {
                    i++;
                    string movieGenre = genre.ToString();
                    if (movieGenre.Contains("_"))
                    {
                        movieGenre = movieGenre.Replace('_', ' ');
                    }
                    DisplayMessage(i + ". " + movieGenre);
                }
            }
        }

        #endregion
    }
}
