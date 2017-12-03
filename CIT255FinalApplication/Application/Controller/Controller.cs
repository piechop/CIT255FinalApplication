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

                    case Enum.ManagerAction.Quit:
                        _active = false;
                        break;

                    default:
                        break;
                }
            }
            ConsoleView.DisplayExitPrompt();
        }

        private static void ListAllMovies()
        {
            MovieBusiness MovieBusiness = new MovieBusiness(_repository);
            List<Movie> Movies;

            using (MovieBusiness)
            {
                Movies = MovieBusiness.SelectAll();
                ConsoleView.DisplayAllMovies(Movies);
                ConsoleView.DisplayContinuePrompt();
            }
        }

        private static void DisplayMovieDetail()
        {
            MovieBusiness MovieBusiness = new MovieBusiness(_repository);
            List<Movie> Movies;
            Movie Movie;
            int MovieID;

            using (MovieBusiness)
            {
                Movies = MovieBusiness.SelectAll();
                MovieID = ConsoleView.GetMovieID(Movies);
                Movie = MovieBusiness.SelectById(MovieID);
            }

            ConsoleView.DisplayMovie(Movie);
            ConsoleView.DisplayContinuePrompt();
        }

        private static void AddMovie()
        {
            MovieBusiness MovieBusiness = new MovieBusiness(_repository);
            Movie Movie;

            Movie = ConsoleView.AddMovie();
            using (MovieBusiness)
            {
                _repository.Insert(Movie);
            }

            ConsoleView.DisplayContinuePrompt();
        }

        private static void UpdateMovie()
        {
            MovieBusiness MovieBusiness = new MovieBusiness(_repository);
            List<Movie> Movies;
            Movie Movie;
            int MovieID;

            using (MovieBusiness)
            {
                Movies = MovieBusiness.SelectAll();
                MovieID = ConsoleView.GetMovieID(Movies);
                Movie = MovieBusiness.SelectById(MovieID);
                Movie = ConsoleView.UpdateMovie(Movie);
                MovieBusiness.Update(Movie);
            }
        }

        private static void DeleteMovie()
        {
            MovieBusiness MovieBusiness = new MovieBusiness(_repository);
            List<Movie> Movies;
            int MovieID;
            string message;

            using (MovieBusiness)
            {
                Movies = MovieBusiness.SelectAll();
                MovieID = ConsoleView.GetMovieID(Movies);
                MovieBusiness.Delete(MovieID);
            }

            ConsoleView.DisplayReset();

            // TODO refactor
            message = String.Format("Movie ID: {0} had been deleted.", MovieID);

            ConsoleView.DisplayMessage(message);
            ConsoleView.DisplayContinuePrompt();
        }

        #endregion

    }
}

