using System;
using System.Collections.Generic;

namespace MovieRatings
{
    public partial class Users
    {
        public Users()
        {
            UserRatings = new HashSet<UserRatings>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<UserRatings> UserRatings { get; set; }
    }
}
