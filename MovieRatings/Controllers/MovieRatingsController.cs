using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRatings.Models;
using MovieRatings.Repository;

namespace MovieRatings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRatingsController : ControllerBase
    {
        private readonly IMovieRatingsRepository _movieRatingsRepository;

        public MovieRatingsController(IMovieRatingsRepository movieRatingsRepository)
        {
            _movieRatingsRepository = movieRatingsRepository;
        }

        [Produces("application/json")]
        [HttpGet]
        [Route("GetTopFiveMovies")]
        public IActionResult GetTopFive()
        {
            try
            {
                var movies = _movieRatingsRepository.GetAll();
                if (movies == null)
                {
                    return NotFound();
                }

                return Ok(movies.OrderBy(x=>x.AverageRating).Take(5));
            }

            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [Produces("application/json")]
        [HttpGet]
        [Route("GetByFilter")]
        public IActionResult GetAllItems(string title, int? releaseYear)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title) && releaseYear == null) return BadRequest();

                var movies = _movieRatingsRepository.GetMoviesByFilter(title, releaseYear);
                if (movies == null)
                {
                    return NotFound();
                }

                return Ok(movies);
            }

            catch (Exception)
            {
                return BadRequest();
            }

        }

        [Produces("application/json")]
        [HttpGet]
        [Route("GetTopMoviesByUser")]
        public IActionResult GetTopFiveMoviesByUser(long userId)
        {
            try
            {
                if (userId == 0) return BadRequest();
                var movies = _movieRatingsRepository.GetTopMoviesByUser(userId);

                if (movies == null)
                {
                    return NotFound();
                }

                return Ok(movies);
            }

            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("UpdateRating")]
        public IActionResult UpdateRating([FromBody]UpdateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = _movieRatingsRepository.UpdateOrAddRating(model);
                    if (status)
                        return Ok();
                    else return BadRequest();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}