using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviegramApi.DataModels
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
