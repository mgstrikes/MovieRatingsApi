using System;
using System.Collections.Generic;

namespace MovieRatings
{
    public partial class Movies
    {
        public Movies()
        {
            UserRatings = new HashSet<UserRatings>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public int RuntimeMinutes { get; set; }
        public int ReleaseYear { get; set; }
        //public decimal AverageRating { get; set; }

        public ICollection<UserRatings> UserRatings { get; set; }
    }
}
