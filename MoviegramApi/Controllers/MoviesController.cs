using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviegramApi.Data;
using MoviegramApi.DataModels;
using MoviegramApi.DomainModels;

namespace MoviegramApi.Controllers
{
    public class MoviesController : Controller
    {
        private MoviegramContext _context;

        public MoviesController(MoviegramContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method returns a list of movie summary data - movie name, showtimes, thumbnail image
        /// If a value is supplied for 'filter', the list of returned movies contain only those matching the filter.
        /// The filter checks for movie names containing the supplied text
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("getMovies")]
        [HttpGet("getMovies/{filter}")]
        public ActionResult<List<MovieSummaryModel>> GetMovies(string filter = null)
        {
            IQueryable<Movie> movies = _context.Movies.AsQueryable();

            //if a filter was specified, add a filter to our query
            if (!string.IsNullOrEmpty(filter))
            {
                movies = movies.Where(x => x.Name.Contains(filter));
            }

            //add a default order
            movies = movies.OrderBy(x => x.Name);

            //execute query and move data into domain model
            var movieList = movies.Select(x => new MovieSummaryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Showtimes = new List<ShowtimeModel>()
            }).ToList();

            return movieList;
        }
    }
}