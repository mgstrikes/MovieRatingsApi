using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Models
{
    public class UpdateModel
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public long MovieId { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "5")]
        public decimal Rating { get; set; }
    }
}
