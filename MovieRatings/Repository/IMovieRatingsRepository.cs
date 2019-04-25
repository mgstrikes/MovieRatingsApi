using MovieRatings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Repository
{
    public interface IMovieRatingsRepository
    {
        IList<MovieReadModel> GetAll();
        IList<MovieReadModel> GetMoviesByFilter(string title, int? releaseYear);
        IList<MovieReadModel> GetTopMoviesByUser(long userId);
        bool UpdateOrAddRating(UpdateModel model);
    }
}
