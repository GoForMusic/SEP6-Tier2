using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public  class Directors
    {
        /// <summary>
        /// The id of the movie
        /// </summary>
        [Required]
        [ForeignKey("Movie")]
        public Movie movie_id { get; set; }

        /// <summary>
        /// The id of the person
        /// </summary>
        [Required]
        [ForeignKey("People")]
        public People person_id { get; set; }
        
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Directors()
        {

        }
    }
}
