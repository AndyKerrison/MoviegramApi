using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviegramApi.Controllers;
using MoviegramApi.Data;
using MoviegramApi.DataModels;
using MoviegramApi.DomainModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviegramApi.UnitTests
{
    /// <summary>
    /// Set of tests to be run on the MoviesController class.
    /// Tests are written in AAA format - Arrange, Act, Assert
    /// </summary>
    [TestFixture]
    public class MoviesControllerTests
    {
        /// <summary>
        /// Basic test for GetMovies - populates an in memory db with 10 movies, we expect 10 movies to be returned
        /// </summary>
        [Test]
        public void GetAllMoviesTest()
        {
            //arrange            
            var builder = new DbContextOptionsBuilder<MoviegramContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new MoviegramContext(builder.Options);
            var movies = Enumerable.Range(1, 10).Select(i => new Movie { Id = i, Name = $"Test Movie {i}" });
            context.Movies.AddRange(movies);
            context.SaveChanges();

            //act
            var moviesController = new MoviesController(context);
            var result = moviesController.GetMovies();

            //assert            
            OkObjectResult okResult = result.Result as OkObjectResult;
            var movieData = (List<MovieSummaryModel>)okResult.Value;            
            Assert.AreEqual(10, movieData.Count);
        }

        /// <summary>
        /// Test data for GetMovies using the filter
        /// </summary>
        public static IEnumerable<TestCaseData> GetMoviesWithFilterTestData
        {
            get
            {
                //with no filter, we expect all 5 movies to return
                yield return new TestCaseData("", 5).SetName("{m} 1 - no filter, return all"); ;

                //no movies match this name
                yield return new TestCaseData("zbnmbsjhjs", 0).SetName("{m} 2 - no matches"); ;

                //two movies have 'test' in their title
                yield return new TestCaseData("test", 2).SetName("{m} 3 - multiple matches"); ;

                //one movie matches this
                yield return new TestCaseData("unique", 1).SetName("{m} 4 - single match"); ;
            }
        }

        /// <summary>
        /// Test the filter field on the GetMovies function
        /// </summary>
        /// <param name="filter">filter text</param>
        /// <param name="expectedMatches">number of matching records expected</param>
        [Test, TestCaseSource(nameof(GetMoviesWithFilterTestData))]
        public void MoviesWithFilterTest(string filter, int expectedMatches)
        {
            //arrange            
            //here we are creating a new in-memory db for each test
            var builder = new DbContextOptionsBuilder<MoviegramContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new MoviegramContext(builder.Options);
            var movie1 = new Movie { Id = 1, Name = "first movie" };
            var movie2 = new Movie { Id = 2, Name = "test one" };
            var movie3 = new Movie { Id = 3, Name = "aaaaatestaaaaa" };
            var movie4 = new Movie { Id = 4, Name = "another movie" };
            var movie5 = new Movie { Id = 5, Name = "uniquename" };
            context.Movies.Add(movie1);
            context.Movies.Add(movie2);
            context.Movies.Add(movie3);
            context.Movies.Add(movie4);
            context.Movies.Add(movie5);
            context.SaveChanges();

            //act
            var moviesController = new MoviesController(context);
            var result = moviesController.GetMovies(filter);

            //assert            
            OkObjectResult okResult = result.Result as OkObjectResult;
            var movieData = (List<MovieSummaryModel>)okResult.Value;

            //check we got the right number of matches
            Assert.AreEqual(expectedMatches, movieData.Count);

            //check all our matches had the filter text
            foreach(var movie in movieData)
            {
                Assert.True(movie.Name.Contains(filter));
            }
        }


        /// <summary>
        /// Test data for GetMovie function
        /// </summary>
        public static IEnumerable<TestCaseData> GetMovieTestData
        {
            get
            {
                //invalid movie id, fail expected
                yield return new TestCaseData(0, false).SetName("{m} 1 - invalid movie id"); ;

                //valid movie id, success expected
                yield return new TestCaseData(1, true).SetName("{m} 2 - valid movie id"); ;                
            }
        }
                
        /// <summary>
        /// GetMovie tests - if the movie Id is invalid, expect a NotFound result. Otherwise, check the movie matches
        /// In this test we create a new in memory db with a single movie record, call the api, and check the result
        /// against the expected return types of OkObjectResult and NotFound.
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="isExpectedSuccess"></param>
        [Test, TestCaseSource(nameof(GetMovieTestData))]
        public void GetMovieTest(int movieId, bool isExpectedSuccess)
        {
            //arrange            
            var builder = new DbContextOptionsBuilder<MoviegramContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new MoviegramContext(builder.Options);
            var movie = new Movie { Id = 1, Name = "test movie" };
            context.Movies.Add(movie);
            context.SaveChanges();

            //act
            var moviesController = new MoviesController(context);
            var result = moviesController.GetMovie(movieId);

            //assert         
            if (isExpectedSuccess)
            {
                Assert.IsInstanceOf<OkObjectResult>(result.Result);

                OkObjectResult okResult = result.Result as OkObjectResult;
                var movieData = (MovieDetailsModel)okResult.Value;
                Assert.AreEqual(movie.Name, movieData.Name); //check name of returned movie is the one we put in the db
            }
            else
            {
                Assert.IsInstanceOf<NotFoundResult>(result.Result);
            }
        }
    }
}
