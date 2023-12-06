﻿using Microsoft.AspNetCore.Mvc;
using RestServer.Data.DAOInterfaces;
using Shared;

namespace RestServer.Controller
{

    public class MovieController : ControllerBase
    {
        /// <summary>
        /// Data access instance
        /// </summary>
        private readonly IMovieDAO _service;

        /// <summary>
        /// Constructor with injection of DataAccess
        /// </summary>
        /// <param name="service"></param>
        public MovieController(IMovieDAO service)
        {
            _service = service;
        }

        /// <summary>
        /// Method to return a list of 21 movies based on year
        /// </summary>
        /// <param name="year">the year</param>
        /// <param name="pageNumber">Use for pagination</param>
        /// <param name="pageSize">Default it will be 21 as empty query otherwise user can use is own</param>
        /// <returns>list of 21 movies based on year</returns>
        [HttpGet]
        [Route("/movies/year/{year}")]
        public async Task<ActionResult<List<Movie>>> FilterMoviesByYear(int year,[FromQuery] int pageNumber,[FromQuery] int pageSize)
        {
            try
            {
                ICollection<Movie> movies = await _service.FilterMoviesByYear(year,pageNumber==0?1:pageNumber,pageSize==0?21:pageSize);
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Method to return movies with a given title
        /// If st is given, it will return 5 movie that start with st
        /// if sta is given, it will return 5 movies that start with sta
        /// </summary>
        /// <param name="title">the title of the movie</param>
        /// <param name="pageNumber">Use for pagination</param>
        /// <param name="pageSize">Default it will be 21 as empty query otherwise user can use is own</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/movies/search/{title}")]
        public async Task<ActionResult<List<Movie>>> SearchMovie(string title,[FromQuery] int pageNumber,[FromQuery] int pageSize)
        {
            try
            {
                ICollection<Movie> movies = await _service.SearchMovie(title,pageNumber==0?1:pageNumber,pageSize==0?5:pageSize);
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        /// <summary>
        /// Method to return movies list between specific rating
        /// And will compere between n...n,9
        /// </summary>
        /// <param name="rate">Rate value 1...10</param>
        /// <param name="pageNumber">Use for pagination</param>
        /// <param name="pageSize">Default it will be 21 as empty query otherwise user can use is own</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/movies/rating/{rate}")]
        public async Task<ActionResult<List<Ratings>>> SearchByRating(int rate,[FromQuery] int pageNumber,[FromQuery] int pageSize)
        {
            try
            {
                
                Console.WriteLine(pageNumber + " " + pageSize);
                // Check if pageNumber==0 than use default page that is 1
                // Check if pageSize==0 than use 21 elements as default otherwise use the user input
                ICollection<Ratings> movies = await _service.FilterMoviesByRating(rate,pageNumber==0?1:pageNumber,pageSize==0?21:pageSize);
                
                
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
