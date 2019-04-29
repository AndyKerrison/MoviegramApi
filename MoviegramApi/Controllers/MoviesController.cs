using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviegramApi.Data;
using MoviegramApi.DataModels;
using MoviegramApi.DomainModels;

namespace MoviegramApi.Controllers
{
    /// <summary>
    /// Controller for calls relating to movies
    /// </summary>
    public class MoviesController : Controller
    {
        private MoviegramContext _context;

        /// <summary>
        /// Controller for calls relating to movies
        /// </summary>
        /// <param name="context"></param>
        public MoviesController(MoviegramContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method returns full details on a single movie
        /// </summary>
        /// <param name="id">Id of the movie to return</param>
        /// <returns></returns>
        [HttpGet("getMovie/{id}")]
        public ActionResult<MovieDetailsModel> GetMovie(int id)
        {
            var movie = _context.Movies.Include(x => x.Showtimes).FirstOrDefault(x => x.Id == id);
            
            if (movie == null)
                return NotFound();

            var movieModel = new MovieDetailsModel()
            {
                Id = movie.Id,
                Name = movie.Name,
                Thumbnail = movie.Thumbnail,
                Description = movie.Description,
                Picture = movie.Picture,
                Publisher = movie.Publisher,
                Year = movie.Year,
                Showtimes = movie.Showtimes.OrderBy(y => y.ShowingDateTime).Select(y => new ShowtimeModel() { Showtime = y.ShowingDateTime }).ToList()
            };

            return Ok(movieModel);            
        }


        /// <summary>
        /// This method returns a list of movie summary data - movie name, showtimes, thumbnail image
        /// If a value is supplied for 'filter', the list of returned movies contain only those matching the filter.        
        /// </summary>
        /// <param name="filter">If present, returns only movies whose names contain the supplied text</param>
        /// <returns></returns>
        [HttpGet("getMovies")]        
        public ActionResult<List<MovieSummaryModel>> GetMovies([FromQuery]string filter = null)
        {
            //get a queryable, so tha we can adjust our query before we use it to popuulate our return data models
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
                Thumbnail = x.Thumbnail,
                Showtimes = x.Showtimes.OrderBy(y => y.ShowingDateTime).Select(y => new ShowtimeModel() { Showtime = y.ShowingDateTime }).ToList()
            }).ToList();

            return Ok(movieList);
        }
    }
}