﻿using Shared;

namespace RestServer.Data.DAOInterfaces
{
    /// <summary>
    /// Interface which deals with accessing the movie table in the db
    /// </summary>
    public interface IMovieDAO
    {
        /// <summary>
        /// Search a movie with given text
        /// If st is given, it will return 5 movie that start with st
        /// if sta is given, it will return 5 movies that start with sta
        /// </summary>
        /// <param name="title">a text to search for in the database</param>
        /// <returns>List of movies</returns>
        public Task<List<Movie>> SearchMovie(string title);

        /// <summary>
        /// Filter the movies by a given year
        /// </summary>
        /// <param name="year">year to filter by</param>
        /// <returns>A list of 21 movies from the given year( if 21 exists)</returns>
        public Task<List<Movie>> FilterMoviesByYear(int year);
    }
}