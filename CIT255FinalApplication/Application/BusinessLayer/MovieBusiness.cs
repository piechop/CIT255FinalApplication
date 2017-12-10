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

        public void Insert(Movie movie)
        {
            List<Movie> movies = SortByAscendingID();
            bool fillIn = false;

            for(int i=0;i<movies.Count;i++)
            {
                if(i+1 != movies[i].ID)
                {
                    movie.ID = i + 1;
                    fillIn = true;
                    break;
                }
            }

            if(!fillIn)
            {
                movie.ID = movies.Count+1;
            }

            _repository.Insert(movie);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Update(Movie movie)
        {
            _repository.Update(movie);
        }

        public Movie SelectById(int id)
        {
            return _repository.SelectById(id);
        }

        public List<Movie> SelectAll()
        {
            return _repository.SelectAll();
        }

        public List<Movie> QueryPersonByRole(Enum.Role role, string name)
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
                        if(c==char.ToUpper(letter))
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
                foreach(Enum.Genre gen in movie.Genre)
                {
                    if (gen.Equals(genre))
                    {
                        sorted.Add(movie);
                        break;
                    }
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

        public List<Movie> SortByAscendingID()
        {
            List<Movie> movies = _repository.SelectAll();

            return movies.OrderBy(m => m.ID).ToList<Movie>();
        }

        public List<Movie> SortByDescendingID()
        {
            List<Movie> movies = _repository.SelectAll();

            return movies.OrderByDescending(m => m.ID).ToList<Movie>();
        }

        public void Dispose()
        {
            _repository = null;
        }

    }

}
