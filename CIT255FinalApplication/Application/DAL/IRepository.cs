using System;
using System.Collections.Generic;

namespace MovieOrganizer
{
    public interface IRepository
    {
        List<Movie> SelectAll();
        Movie SelectById(int id);
        void Insert(Movie obj);
        void Update(Movie obj);
        void Delete(int id);
        void Save();
    }
}
