using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Models
{
    public class MovieReadModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int RuntimeMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public decimal AverageRating { get; set; }
    }
}
