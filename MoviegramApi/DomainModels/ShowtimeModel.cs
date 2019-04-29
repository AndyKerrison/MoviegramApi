using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviegramApi.DomainModels
{
    /// <summary>
    /// Showtime model. Contains a DateTime relating to the movie showing time and date
    /// </summary>
    public class ShowtimeModel
    {
        /// <summary>
        /// Showtime of the movie
        /// </summary>
        public DateTime Showtime { get; set; }
    }
}
