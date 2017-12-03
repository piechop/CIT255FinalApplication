using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganizer
{
    class MovieBusiness : IDisposable
    {
        IRepository _repository;

        public MovieBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public void Insert(Movie Movie)
        {
            _repository.Insert(Movie);
        }

        public void Delete(int ID)
        {
            _repository.Delete(ID);
        }

        public void Update(Movie Movie)
        {
            _repository.Update(Movie);
        }

        public Movie SelectById(int ID)
        {
            return _repository.SelectById(ID);
        }

        public List<Movie> SelectAll()
        {
            return _repository.SelectAll();
        }

        public List<Movie> QueryPersonByRole(string name, Enum.Role role)
        {
            List<Movie> movies = SelectAll();
            List<Movie> sorted = new List<Movie>();

            switch(role)
            {
                case Enum.Role.Director:
                    foreach(Movie movie in movies)
                    {
                        if(movie.Director.ToUpper()==name.ToUpper())
                        {
                            sorted.Add(movie);
                        }
                    }
                    break;
                case Enum.Role.Producer:
                    foreach (Movie movie in movies)
                    {
                        if (movie.Producer.ToUpper() == name.ToUpper())
                        {
                            sorted.Add(movie);
                        }
                    }
                    break;
                default:
                    break;
            }

            return sorted;
        }

        public List<Movie> QueryByFirstLetter(char letter)
        {
            List<Movie> movies = SelectAll();
            List<Movie> sorted = new List<Movie>();

            foreach(Movie movie in movies)
            {
                char[] mov = movie.Title.ToCharArray();
                foreach(char c in mov)
                {
                    if(Char.IsLetter(c))
                    {
                        if(c==letter)
                        {
                            sorted.Add(movie);
                        }
                        break;
                    }
                }
            }

            return sorted;
        }

        public List<Movie> QueryByGenre(Enum.Genre genre)
        {
            List<Movie> movies = SelectAll();
            List<Movie> sorted = new List<Movie>();

            foreach(Movie movie in movies)
            {
                if(movie.Genre.Equals(genre))
                {
                    sorted.Add(movie);
                }
            }

            return sorted;
        }

        public List<Movie> QueryByReleaseYear(int year)
        {
            List<Movie> movies = SelectAll();
            List<Movie> sorted = new List<Movie>();

            foreach(Movie movie in movies)
            {
                if(movie.Release.Year==year)
                {
                    sorted.Add(movie);
                }
            }

            return sorted;
        }

        public List<Movie> SortByAscendingYear()
        {
            List<Movie> movies = _repository.SelectAll();

            return movies.OrderBy(m => m.Release.Year).ToList<Movie>();
        }

        public List<Movie> SortByDescendingYear()
        {
            List<Movie> movies = _repository.SelectAll();

            return movies.OrderByDescending(m => m.Release.Year).ToList<Movie>();
        }

        public List<Movie> SortByAscendingTitle()
        {
            List<Movie> movies = _repository.SelectAll();

            return movies.OrderBy(m => m.Title).ToList<Movie>();
        }

        public List<Movie> SortByDescendingTitle()
        {
            List<Movie> movies = _repository.SelectAll();

            return movies.OrderByDescending(m => m.Title).ToList<Movie>();
        }

        public void Dispose()
        {
            _repository = null;
        }

    }

}
