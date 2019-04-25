using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Repository
{
    public class MovieRatingsRepository: IMovieRatingsRepository
    {
        private readonly MoviesContext _context;
        public MovieRatingsRepository(MoviesContext context)
        {
            _context = context;
        }

        public List<Movies> GetAll()
        {
            return _context.Movies.ToList();
        }

        public List<Movies> GetMoviesByFilter(string title, int? releaseYear)
        {
            IQueryable<Movies> query = _context.Movies;
            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(d => d.Title.Contains(title));
            //if (!string.IsNullOrWhiteSpace(genre))
            //    query = query.Where(d => d.Genre == genre);
            if (releaseYear != null)
            {
                query = query.Where(d => d.ReleaseYear == releaseYear.Value);
            }

            return query.ToList();
        }
    }
}
