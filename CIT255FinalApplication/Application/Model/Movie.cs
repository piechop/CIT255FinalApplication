using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganizer
{
    public class Movie
    {
        #region Enums



        #endregion

        #region Fields

        private int _ID;
        private string _title;
        private List<Enum.Genre> _genre;
        private DateTime _release;
        private int _minuteLength;
        private string _director;
        private string _producer;

        #endregion

        #region Properties

        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if(value >= 0)
                {
                    _ID = value;
                }
                else
                {
                    throw new ArgumentException("Argument must be a positive number", "ID");
                }
            }
        }

        public string Title
        {
            get{
                return _title;
            }
            set{
                if(value.Length>2)
                {
                    _title = value;
                }
                else
                {
                    throw new ArgumentException("Argument must be greater than 2", "Title");
                }
            }
        }

        public List<Enum.Genre> Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
            }
        }

        public DateTime Release
        {
            get
            {
                return _release;
            }
            set
            {
                _release = value;
            }
        }

        public int MinuteLength
        {
            get
            {
                return _minuteLength;
            }
            set
            {
                if(value >= 1)
                {
                    _minuteLength = value;
                }
                else
                {
                    throw new ArgumentException("Argument must be greater than 0","Minute Length");
                }
            }
        }

        public string Director
        {
            get
            {
                return _director;
            }
            set
            {
                if(value.Length>=5 && value.Contains(" "))
                {
                    _director = value;
                }
                else
                {
                    throw new ArgumentException("Argument must be at least two words totaling a length of 5 or greater", "Director");
                }
            }
        }

        public string Producer
        {
            get
            {
                return _producer;
            }
            set
            {
                if (value.Length >= 5 && value.Contains(" "))
                {
                    _producer = value;
                }
                else
                {
                    throw new ArgumentException("Argument must be at least two words totaling a length of 5 or greater", "Producer");
                }
            }
        }

        #endregion

        #region Constructors

        public Movie()
        {
            _title = "Default";
            _genre = new List<Enum.Genre>();
            _release = new DateTime(1999, 3, 17);
            _minuteLength = 120;
            _director = "Default";
            _producer = "Default";
        }

        public Movie(int ID, string title,List<Enum.Genre> genre, DateTime release,
            int minuteLength, string director, string producer)
        {
            _ID = ID;
            _title = title;
            _genre = genre;
            _release = release;
            _minuteLength = minuteLength;
            _director = director;
            _producer = producer;
        }

        #endregion

        #region Methods

        public double GetHourLength()
        {
            return Math.Round((_minuteLength / 60.0), 1);
        }

        #endregion
    }
}
