using Microsoft.EntityFrameworkCore;
using MovieRatings.Models;
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

        public IList<MovieReadModel> GetAll()
        {
            var movies = _context.Movies.Include("UserRatings").ToList();
            return CreateMovieReadModel(movies);
        }

        public IList<MovieReadModel> GetMoviesByFilter(string title, int? releaseYear)
        {
            IQueryable<Movies> query = _context.Movies.Include("UserRatings");
            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(d => d.Title.Contains(title));
            //if (!string.IsNullOrWhiteSpace(genre))
            //    query = query.Where(d => d.Genre == genre);
            if (releaseYear != null)
            {
                query = query.Where(d => d.ReleaseYear == releaseYear.Value);
            }

            var movies = query.ToList();
            return CreateMovieReadModel(movies);
        }

        public IList<MovieReadModel> GetTopMoviesByUser(long userId)
        {
            IList<long> movieIds = _context.UserRatings.Where(x => x.UserId == userId).OrderByDescending(z => z.Rating)
 .Select(y => y.MovieId).Take(5).ToList();

            IQueryable<Movies> query = _context.Movies.Include("UserRatings").Where(m => movieIds.Contains(m.Id));
            var movies = query.ToList();
            return CreateMovieReadModel(movies);
        }

        public bool UpdateOrAddRating(UpdateModel model)
        {
            try
            {
                if (_context.Movies.Where(x => x.Id == model.MovieId).FirstOrDefault() == null ||
                    _context.Users.Where(x => x.Id == model.UserId).FirstOrDefault() == null) return false;

                var data = _context.UserRatings.Where(x => x.MovieId == model.MovieId && x.UserId == model.UserId).FirstOrDefault();
                if (data == null)
                {
                    var entity = new UserRatings()
                    {
                        MovieId = model.MovieId,
                        UserId = model.UserId,
                        Rating = model.Rating
                    };

                    _context.UserRatings.Add(entity);
                }
                else
                {
                    data.Rating = model.Rating;
                    _context.UserRatings.Update(data);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static IList<MovieReadModel> CreateMovieReadModel(IList<Movies> movies)
        {
            var moviesList = new List<MovieReadModel>();
            foreach (var item in movies)
            {
                moviesList.Add(new MovieReadModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    ReleaseYear = item.ReleaseYear,
                    RuntimeMinutes = item.RuntimeMinutes,
                    AverageRating = CalculateAverageRating(item.UserRatings.ToList())
                });
            }

            return moviesList;
        }
        private static decimal CalculateAverageRating(List<UserRatings> ratings)
        {
            if (ratings.Count == 0) return 0.00M;
            var count = ratings.Count();
            decimal sum = 0.00M;
            foreach (var item in ratings)
            {
                sum = sum + item.Rating;
            }

            var avg = sum / count;
            return Math.Round(avg * 2) / 2;
        }
    }
}
