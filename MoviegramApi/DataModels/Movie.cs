﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviegramApi.DataModels
{
    /// <summary>
    /// A movie record in the database. Contains movie data, each movie has a reference to a number of showtimes
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Movie Identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Movie name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Thumbnail image data
        /// </summary>
        public byte[] Thumbnail { get; set; }

        /// <summary>
        /// Showtimes for this movie. Movie has a one to many relationship to Showtimes. 
        /// </summary>
        public ICollection<Showtime> Showtimes { get; set; }
    }
}
