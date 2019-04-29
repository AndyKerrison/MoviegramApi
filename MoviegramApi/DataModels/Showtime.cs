using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviegramApi.DataModels
{
    /// <summary>
    /// A showtime for a movie. 
    /// </summary>
    public class Showtime
    {
        /// <summary>
        /// Showing identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// DateTime of this showing
        /// </summary>
        public DateTime ShowingDateTime { get; set; }
        
        /// <summary>
        ///Movie this showing belongs to.
        /// </summary>
        public virtual Movie Movie { get; set; }

        /// <summary>
        /// Foreign key to Movie table
        /// </summary>
        public int MovieId { get; set; }
    }
}
