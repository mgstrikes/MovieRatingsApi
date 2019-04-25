using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Repository
{
    public interface IMovieRatingsRepository
    {
        List<Movies> GetAll();
        List<Movies> GetMoviesByFilter(string title, int? releaseYear);
    }
}
