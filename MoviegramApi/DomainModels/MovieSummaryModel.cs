using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviegramApi.DomainModels
{
    /// <summary>
    /// Summary model for a movie - contains id, name, thumbnail, and showtimes
    /// </summary>
    public class MovieSummaryModel
    {
        /// <summary>
        /// Id of the movie
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the movie
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An ordered list of showtimes for the movie
        /// </summary>
        public List<ShowtimeModel> Showtimes { get; set; }

        /// <summary>
        /// byte data for the thumbnail image for this mmovie
        /// </summary>
        public byte[] Thumbnail { get; set; }
    }
}
