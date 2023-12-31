using Shared;
using Shared.SpecialCases;

namespace RestServer.Data.DAOInterfaces;

public interface ICommentDao
{
    /// <summary>
    /// A async method that will return a list of comments using the movieID FK from DAO
    /// </summary>
    /// <param name="movieId">Movie ID FK</param>
    public Task<ICollection<Comment>> GetListAsync(int movieId);
    /// <summary>
    /// A get element that will return only one comment by his id
    /// </summary>
    /// <param name="id">Comment PK id</param>
    /// <returns></returns>
    public Task<Comment> GetElementAsync(int id);
    /// <summary>
    /// A method that will add to DAO a new comment
    /// </summary>
    /// <param name="element">The comment element that is comming from REST</param>
    public Task<Comment> AddElementAsync(CommentREST element);
    /// <summary>
    /// A method that will delete a comment base on his ID
    /// </summary>
    /// <param name="id">Comment PK ID</param>
    public Task DeleteElementAsync(int id);
    /// <summary>
    /// A method that will update the comment post in DB
    /// </summary>
    /// <param name="element">New comment coming from REST</param>
    public Task UpdateElementAsync(CommentREST element);

    /// <summary>
    /// Method to like a comment
    /// </summary>
    /// <param name="movieId"></param>
    /// <returns></returns>
    public Task LikeComment(long movieId);

    /// <summary>
    /// Method to remove a like from a comment
    /// </summary>
    /// <param name="movieId"></param>
    /// <returns></returns>
    public Task UnlikeComment(long movieId);
}