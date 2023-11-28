using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Actors, directors
    /// </summary>
    public class People
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Full name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// BirthYear
        /// </summary>
        public decimal? BirthYear { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public People()
        {

        }
    }
}
