using Microsoft.EntityFrameworkCore;
using MoviegramApi.Controllers;
using MoviegramApi.Data;
using MoviegramApi.DataModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviegramApi.UnitTests
{
    /// <summary>
    /// Set of tests to be run on the MoviesController class.
    /// </summary>
    [TestFixture]   
    public class MoviesControllerTests
    {
        [Test]
        public void GetMoviesTest()
        {
            //arrange            
            var builder = new DbContextOptionsBuilder<MoviegramContext>().UseInMemoryDatabase();
            var context = new MoviegramContext(builder.Options);
            var movies = Enumerable.Range(1, 10).Select(i => new Movie { Id = i, Name = $"Test Movie {i}" });
            context.Movies.AddRange(movies);
            context.SaveChanges();

            //act
            var moviesController = new MoviesController(context);
            var result = moviesController.GetMovies();

            //assert            
            Assert.AreEqual(10, result.Value.Count);
        }
    }
}
