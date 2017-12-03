using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MovieOrganizer
{
    class InitializeDataFileXML
    {

        public InitializeDataFileXML()
        {

        }

        private static string[] RandomizeNames()
        {
            List<string> names = new List<string>();
            List<string> firstNames = new List<string>();
            List<string> lastNames = new List<string>();

            const char delineator = ',';

            // initialize a StreamReader object for reading
            StreamReader sReader = new StreamReader("Data//firstNames.csv");

            using (sReader)
            {
                // keep reading lines of text until the end of the file is reached
                while (!sReader.EndOfStream)
                {
                    firstNames.Add(sReader.ReadLine());
                }
            }

            sReader = new StreamReader("Data//lastNames.csv");

            using (sReader)
            {
                // keep reading lines of text until the end of the file is reached
                while (!sReader.EndOfStream)
                {
                    lastNames.Add(sReader.ReadLine());
                }
            }

            Random random = new Random();
            for (int i=0;i<100;i++)
            {
                string name;

                name = firstNames[random.Next(1,firstNames.Count)] + " " + lastNames[random.Next(1, lastNames.Count)];
                names.Add(name);
            }

            return names.ToArray();
        }

        private static string[] InitTitles()
        {
            List<string> titles = new List<string>();

            StreamReader sReader = new StreamReader("Data//titles.csv");

            using (sReader)
            {
                // keep reading lines of text until the end of the file is reached
                while (!sReader.EndOfStream)
                {
                    titles.Add(sReader.ReadLine());
                }
            }

            return titles.ToArray();
        }

        private static string CapitalizeFirst(string phrase)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(phrase);
        }

        public static void AddTestData()
        {
            string[] names = RandomizeNames();
            string[] titles = InitTitles();
            List<Movie> Movies = new List<Movie>();

            // initialize the IList of movies - note: no instantiation for an interface
            Random random = new Random();

            for (int i=0;i<100;i++)
            {
                int ID = i + 1;
                Movie movie = RandomizeMovie(ID, random, names, titles);
                Movies.Add(movie);
            }

            WriteAllMovies(Movies, DataSettings.dataFilePath);
        }

        private static Movie RandomizeMovie(int ID, Random random, string[] names, string[] titles)
        {
            Movie movie = new Movie();

            movie.Title = CapitalizeFirst(titles[random.Next(1,titles.Length)]);

            movie.ID = ID;
            
            int value = random.Next(1, 14);

            List<Enum.Genre> genres = new List<Enum.Genre>();
            genres.Add((Enum.Genre)value);
            if(random.Next(0,2)==0)
            {
                value = random.Next(1, 14);
                if(!genres.Contains((Enum.Genre)value))
                {
                    genres.Add((Enum.Genre)value);
                }
            }
            movie.Genre = genres;
            
            try
            {
                DateTime date = new DateTime(random.Next(1930, 2018), random.Next(1, 13), random.Next(1, 31));
                movie.Release = date;
            }
            catch(Exception e)
            {
                
            }

            movie.MinuteLength = random.Next(30, 181);

            movie.Producer = names[random.Next(names.Length)];
            movie.Director = names[random.Next(names.Length)];

            return movie; 
        }

        /// <summary>
        /// method to write all Movie info to the data file
        /// </summary>
        /// <param name="Movies">list of Movie info</param>
        /// <param name="dataFilePath">path to the data file</param>
        public static void WriteAllMovies(List<Movie> Movies, string dataFilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Movie>), new XmlRootAttribute("Movies"));

            using (StreamWriter sWriter = new StreamWriter(dataFilePath))
            {
                serializer.Serialize(sWriter, Movies);
            }
        }
    }
}
