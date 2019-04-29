using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviegramApi.DomainModels
{
    /// <summary>
    /// Full details of a movie
    /// </summary>
    public class MovieDetailsModel
    {
        /// <summary>
        /// Movie Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Movie name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Publisher of the movie
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Movie description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Year the movie was published
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// byte data for movie thumbnail image
        /// </summary>
        public byte[] Thumbnail { get; set; }

        /// <summary>
        /// byte data for full movie image
        /// </summary>
        public byte[] Picture { get; set; }

        /// <summary>
        /// List of showtimes associated with this movie
        /// </summary>
        public List<ShowtimeModel> Showtimes { get; set; }
    }
}
