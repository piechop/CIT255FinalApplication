using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganizer
{
    public class Controller
    {
        #region ENUMERABLES


        #endregion

        #region FIELDS

        bool _active = true;
        static IRepository _repository;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            _repository = new RepositoryXML();
            MovieOrganizerControl();
        }

        #endregion

        #region METHODS

        private void MovieOrganizerControl()
        {
            ConsoleView.DisplayWelcomeScreen();

            while (_active)
            {
                Enum.ManagerAction userActionChoice;

                userActionChoice = ConsoleView.GetUserActionChoice();

                switch (userActionChoice)
                {
                    case Enum.ManagerAction.None:
                        break;

                    case Enum.ManagerAction.ListAllMovies:
                        ListAllMovies();
                        break;

                    case Enum.ManagerAction.DisplayMovieDetail:
                        DisplayMovieDetail();
                        break;

                    case Enum.ManagerAction.AddMovie:
                        AddMovie();
                        break;

                    case Enum.ManagerAction.UpdateMovie:
                        UpdateMovie();
                        break;

                    case Enum.ManagerAction.DeleteMovie:
                        DeleteMovie();
                        break;

                    case Enum.ManagerAction.SortQueryMovies:
                        SortQueryMovies();
                        break;

                    case Enum.ManagerAction.Quit:
                        _active = false;
                        break;

                    default:
                        break;
                }
            }
            ConsoleView.DisplayExitPrompt();
        }

        private static void SortQueryMovies()
        {
            MovieBusiness movieBusiness = new MovieBusiness(_repository);
            bool response = false;

            using (movieBusiness)
            {
                while (!response)
                {
                    switch (ConsoleView.GetSortQueryChoice(out response))
                    {
                        case Enum.ManagerAction.QueryPersonByRole:
                            Enum.Role role = ConsoleView.RoleChoice();
                            string name = ConsoleView.GetNameQuery(role);
                            if(role!=Enum.Role.None && name!=null)
                            {
                                ConsoleView.BrowseAllMovies(movieBusiness.QueryPersonByRole(role, name), false, "Query: " + role + " " + name);
                            }
                            break;
                        case Enum.ManagerAction.QueryByFirstLetter:
                            char letter = ConsoleView.GetLetterQuery();
                            if (letter != '\u0000')
                            {
                                ConsoleView.BrowseAllMovies(movieBusiness.QueryByFirstLetter(letter), false, "Query First Letter: " + letter);
                            }
                            break;
                        case Enum.ManagerAction.QueryByGenre:
                            Enum.Genre gen = ConsoleView.GetGenreQuery();
                            if (gen != Enum.Genre.None)
                            {
                                ConsoleView.BrowseAllMovies(movieBusiness.QueryByGenre(gen), false, "Query Genre: " + gen);
                            }
                            break;
                        case Enum.ManagerAction.QueryByReleaseYear:
                            int year = ConsoleView.GetYearQuery();
                            if (year != -1)
                            {
                                ConsoleView.BrowseAllMovies(movieBusiness.QueryByReleaseYear(year), true, "Query Release Year: " + year);
                            }
                            break;
                        case Enum.ManagerAction.SortByAscendingTitle:
                            ConsoleView.BrowseAllMovies(movieBusiness.SortByAscendingTitle(), false, "Ascending Title");
                            break;
                        case Enum.ManagerAction.SortByDescendingTitle:
                            ConsoleView.BrowseAllMovies(movieBusiness.SortByDescendingTitle(), false, "Descending Title");
                            break;
                        case Enum.ManagerAction.SortByAscendingYear:
                            ConsoleView.BrowseAllMovies(movieBusiness.SortByAscendingYear(), true, "Ascending Year");
                            break;
                        case Enum.ManagerAction.SortByDescendingYear:
                            ConsoleView.BrowseAllMovies(movieBusiness.SortByDescendingYear(), true, "Descending Year");
                            break;
                        default:
                            //ConsoleView.DisplayMessage("");
                            //ConsoleView.DisplayMessage("");
                            //ConsoleView.DisplayMessage("It appears you have selected an incorrect choice.");
                            //ConsoleView.DisplayContinuePrompt();
                            break;
                    }
                }
            }
        }

        private static void ListAllMovies()
        {
            MovieBusiness movieBusiness = new MovieBusiness(_repository);
            List<Movie> movies;

            using (movieBusiness)
            {
                movies = movieBusiness.SelectAll();
                ConsoleView.BrowseAllMovies(movieBusiness.SortByAscendingID(),false,"");
                //ConsoleView.DisplayContinuePrompt();
            }
        }

        private static void DisplayMovieDetail()
        {
            MovieBusiness movieBusiness = new MovieBusiness(_repository);
            List<Movie> movies;
            Movie movie;
            int movieID;

            using (movieBusiness)
            {
                movies = movieBusiness.SelectAll();
                movieID = ConsoleView.GetMovieID(movieBusiness.SortByAscendingID());
                if (movieID != -2)
                {
                    movie = movieBusiness.SelectById(movieID);
                    ConsoleView.DisplayMovie(movie);
                    ConsoleView.DisplayContinuePrompt();
                }
            }
        }

        private static void AddMovie()
        {
            MovieBusiness movieBusiness = new MovieBusiness(_repository);
            Movie movie;

            movie = ConsoleView.AddMovie();
            using (movieBusiness)
            {
                if(movie!=null)
                {
                   movieBusiness.Insert(movie);
                }
                
            }

            //ConsoleView.DisplayContinuePrompt();
        }

        private static void UpdateMovie()
        {
            MovieBusiness movieBusiness = new MovieBusiness(_repository);
            List<Movie> movies;
            Movie movie;
            int movieID;

            using (movieBusiness)
            {
                movies = movieBusiness.SelectAll();
                movieID = ConsoleView.GetMovieID(movieBusiness.SortByAscendingID());
                movie = movieBusiness.SelectById(movieID);
                movie = ConsoleView.UpdateMovie(movie);
                if(movie!=null)
                {
                    movieBusiness.Update(movie);
                }
            }
        }

        private static void DeleteMovie()
        {
            MovieBusiness movieBusiness = new MovieBusiness(_repository);
            List<Movie> movies;
            string message;

            using (movieBusiness)
            {
                movies = movieBusiness.SelectAll();
                Movie movie = movieBusiness.SelectById(ConsoleView.GetMovieID(movieBusiness.SortByAscendingID()));
                
                if (movie!= null && movie.ID != -2)
                {
                    if (ConsoleView.ValidateEdit(movie, "Delete", "Movie "+movie.ID))
                    {
                        movieBusiness.Delete(movie.ID);
                        ConsoleView.DisplayReset();
                        
                        message = String.Format("Movie {0}: {1} has been deleted.", movie.ID,movie.Title);

                        ConsoleView.DisplayMessage(message);
                        ConsoleView.DisplayContinuePrompt();
                    }
                }
            }
            
        }

        #endregion

    }
}

