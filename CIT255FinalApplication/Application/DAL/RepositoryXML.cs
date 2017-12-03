using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MovieOrganizer
{
    /// <summary>
    /// method to write all Movie information to the date file
    /// </summary>
    public class RepositoryXML : IDisposable, IRepository
    {
        private List<Movie> _Movies;

        public RepositoryXML()
        {
            _Movies = ReadMoviesData(DataSettings.dataFilePath);
        }

        /// <summary>
        /// method to read all Movie information from the data file and return it as a list of Movie objects
        /// </summary>
        /// <param name="dataFilePath">path the data file</param>
        /// <returns>list of Movie objects</returns>
        public List<Movie> ReadMoviesData(string dataFilePath)
        {
            List<Movie> Movies;

            XmlSerializer serializer = new XmlSerializer(typeof(List<Movie>), new XmlRootAttribute("Movies"));

            using (FileStream stream = File.OpenRead(DataSettings.dataFilePath))
            {
                Movies = (List<Movie>)serializer.Deserialize(stream);
            }

            return Movies;
        }

        /// <summary>
        /// method to write all of the list of Movies to the XML file
        /// </summary>
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Movie>), new XmlRootAttribute("Movies"));

            using (FileStream stream = File.OpenWrite(DataSettings.dataFilePath))
            {
                serializer.Serialize(stream, _Movies);
            }
        }

        /// <summary>
        /// method to add a new Movie
        /// </summary>
        /// <param name="Movie"></param>
        public void Insert(Movie Movie)
        {
            _Movies.Add(Movie);

            Save();
        }

        /// <summary>
        /// method to delete a Movie by Movie ID
        /// </summary>
        /// <param name="ID"></param>
        public void Delete(int ID)
        {
            _Movies.RemoveAll(sr => sr.ID == ID);

            Save();
        }

        /// <summary>
        /// method to update an existing Movie
        /// </summary>
        /// <param name="Movie">Movie object</param>
        public void Update(Movie Movie)
        {
            Delete(Movie.ID);
            Insert(Movie);

            Save();
        }

        /// <summary>
        /// method to return a Movie object given the ID
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>Movie object</returns>
        public Movie SelectById(int ID)
        {
            Movie Movie = null;

            Movie = _Movies.FirstOrDefault(sr => sr.ID == ID);

            return Movie;
        }

        /// <summary>
        /// method to return a list of Movie objects
        /// </summary>
        /// <returns>list of Movie objects</returns>
        public List<Movie> SelectAll()
        {
            return _Movies;
        }

        /// <summary>
        /// method to handle the IDisposable interface contract
        /// </summary>
        public void Dispose()
        {
            _Movies = null;
        }
    }
}
