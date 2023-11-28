using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// A movie has stars
    /// </summary>
    public class Stars
    {
        /// <summary>
        /// Movie Id
        /// </summary>
        public Movie MovieId { get; set; }

        /// <summary>
        /// Person Id
        /// </summary>
        public People PersonId { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Stars()
        {

        }
    }
}
