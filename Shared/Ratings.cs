using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared
{
    /// <summary>
    /// Rating of the movie
    /// </summary>
    public class Ratings
    {
        /// <summary>
        /// The PK
        /// </summary>
        [Key]
        public long? Id { get; set; }
        /// <summary>
        /// The id of the movie
        /// </summary>
        [Required]
        [ForeignKey("Movie")]
        public Movie movie_id { get; set; }

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
