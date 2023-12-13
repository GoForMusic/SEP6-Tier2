using Microsoft.AspNetCore.Mvc;
using RestServer.Data.DAOInterfaces;
using Shared;
using Shared.SpecialCases;

namespace RestServer.Controller;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private ICommentDAO _service;

    public CommentController(ICommentDAO _service)
    {
        this._service = _service;
    }
    
    /// <summary>
    /// A REST method that will return a list of comments from a movie
    /// </summary>
    /// <param name="movieID">Movie FK id</param>
    /// <returns>ICollection<Comment></returns>
    [HttpGet]
    [Route("movie/{movieID}/comments")]
    public async Task<ActionResult<ICollection<Comment>>> GetAll([FromRoute]int movieID)
    {
        try
        {
            ICollection<Comment> comments = await _service.GetListAsync(movieID);
            return Ok(comments);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// A method that will return one comment by his ID
    /// </summary>
    /// <param name="id">Comment PK ID</param>
    /// <returns>Comment Object</returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Comment>> GetByID([FromRoute] int id)
    {
        try
        {
            Comment comment = await _service.GetElementAsync(id);
            return Ok(comment);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// A method that will delete the comment by his ID
    /// </summary>
    /// <param name="id">Comment PK ID</param>
    /// <returns>Element deleted message</returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Comment>> DeleteByID([FromRoute] int id)
    {
        try
        {
            await _service.DeleteElementAsync(id);
            return Ok("Comment deleted: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// A patch method that will help for update an existing element in DB
    /// </summary>
    /// <param name="comment">The new Object</param>
    /// <returns>Message that will inform if the action was successfully or not</returns>
    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] CommentREST comment)
    {
        try
        {
            await _service.UpdateElementAsync(comment);
            return Ok("Comment updated: " + comment.Id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }  
    }
    
    [HttpPost]
    public async Task<ActionResult<Comment>> AddComment([FromBody] CommentREST comment)
    {
        try
        {
            Comment added = await _service.AddElementAsync(comment);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Method to like a comment
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{commentId}/likes")]
    public async Task<ActionResult> LikeComment(long commentId)
    {
        try
        {
            await _service.LikeComment(commentId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Method to unlike a comment
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{commentId}/unlike")]
    public async Task<ActionResult> UnlikeComment(long commentId)
    {
        try
        {
            await _service.UnlikeComment(commentId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}