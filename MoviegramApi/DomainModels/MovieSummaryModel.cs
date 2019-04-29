using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviegramApi.DomainModels
{
    public class MovieSummaryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ShowtimeModel> Showtimes { get; set; }
        public byte[] Thumbnail { get; internal set; }
    }
}
