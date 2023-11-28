using System;
using System.Collections.Generic;
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
        public Movie MovieId { get; set; }

        /// <summary>
        /// The id of the person
        /// </summary>
        public People PersonId { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Directors()
        {

        }
    }
}
