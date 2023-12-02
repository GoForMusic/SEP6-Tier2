using System.ComponentModel.DataAnnotations;

namespace Shared
{
    /// <summary>
    /// Movie object
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Id of the movie
        /// Long because in db is bigint
        /// </summary>
        [Key]
        public long Id { get; set; } 

        /// <summary>
        /// Title of the movie
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Year of the movie was released
        /// Decimal because in the database it is numeric
        /// </summary>
        public int? Year { get; set; }
    }
}
