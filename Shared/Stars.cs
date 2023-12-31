﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        /// PK
        /// </summary>
        [Key]
        public long? Id { get; set; }
        /// <summary>
        /// Movie Id
        /// </summary>
        [Required]
        [ForeignKey("Movie")]
        public Movie movie_id { get; set; }

        /// <summary>
        /// Person Id
        /// </summary>
        [Required]
        [ForeignKey("People")]
        public People person_id { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Stars()
        {
            movie_id = new Movie();
            person_id = new People();
        }
    }
}
