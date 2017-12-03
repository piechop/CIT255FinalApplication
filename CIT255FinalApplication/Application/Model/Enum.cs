using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganizer
{
    public class Enum
    {
        public enum Genre
        {
            None,
            Action,
            Horror,
            Drama,
            Adventure,
            Romance,
            Comedy,
            Thriller,
            Western,
            Animation,
            Documentary,
            Fantasy,
            Science_Fiction,
            Mystery
        }

        public enum ManagerAction
        {
            None,
            ListAllMovies,
            DisplayMovieDetail,
            DeleteMovie,
            AddMovie,
            UpdateMovie,
            Quit
        }

        public enum Role
        {
            None,
            Director,
            Producer
        }

    }
}
