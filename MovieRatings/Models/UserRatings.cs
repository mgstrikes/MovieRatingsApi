using System;
using System.Collections.Generic;

namespace MovieRatings
{
    public partial class UserRatings
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long MovieId { get; set; }
        public decimal Rating { get; set; }

        public Movies Movie { get; set; }
        public Users User { get; set; }
    }
}
