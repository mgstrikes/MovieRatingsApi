using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {
                var movies = _movieRatingsRepository.GetAll();
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
        [Route("GetByFilter")]
        public IActionResult GetAllItems(string title, int? releaseYear)
        {
            try
            {
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
    }
}