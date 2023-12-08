using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Posts.Models;

namespace Posts.Controllers;

[SessionCheck]
public class CommentController : Controller
{
    private readonly ILogger<CommentController> _logger;

    private MyContext _context;

    public CommentController(ILogger<CommentController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    [HttpPost("comments/create")]
    public IActionResult AddComment(UserPostComment newComment)
    {

        if (!ModelState.IsValid)
        {
            // To Render the OnePost View we'll need an object with the information for the view.

            Post? OnePost = _context.Posts.Include(p => p.UserLikes)
                                        .ThenInclude(ul => ul.LikedBy)
                                        .Include(p => p.Creator)
                                        .FirstOrDefault(p => p.PostId == newComment.PostId);

            return View("../Post/ViewPost", OnePost);
        }

        newComment.UserId = (int)HttpContext.Session.GetInt32("UserId");

        _context.Add(newComment);
        _context.SaveChanges();

        return RedirectToAction("ViewPost", "Post", new { postId = newComment.PostId });


    }


    [HttpPost("comments/{commentId}/delete")]
    public IActionResult DeleteComment(int commentId)
    {

        UserPostComment? CommentInDb = _context.UserPostComments.SingleOrDefault(upc => upc.UUserPostCommentId == commentId);

        if (CommentInDb != null)
        {
            _context.Remove(CommentInDb);
            _context.SaveChanges();
        }

        return RedirectToAction("ViewPost", "Post", new { postId = CommentInDb.PostId });

    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}