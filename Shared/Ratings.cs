using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Rating of the movie
    /// </summary>
    public class Ratings
    {
        /// <summary>
        /// The id of the movie
        /// </summary>
        public Movie MovieId { get; set; }

        /// <summary>
        /// Value of the rating
        /// </summary>
        public float RatingValue { get; set; }

        /// <summary>
        /// The number of ratings / votes
        /// </summary>
        public long Votes { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Ratings()
        {}
    }
}
