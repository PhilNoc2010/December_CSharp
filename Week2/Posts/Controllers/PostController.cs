using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Posts.Models;

namespace Posts.Controllers;

[SessionCheck]
public class PostController : Controller
{
    private readonly ILogger<PostController> _logger;

    private MyContext _context;

    public PostController(ILogger<PostController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("posts/new")]
    // public ViewResult NewPost()
    // {
    //     return View();
    // }
    public ViewResult NewPost() => View();

    // CREATE POSTS

    [HttpPost("posts/create")]
    public IActionResult CreatePost(Post newPost)
    {
        if (!ModelState.IsValid)
        {
            return View("NewPost");
        }

        newPost.UserId = (int)HttpContext.Session.GetInt32("UserId");

        _context.Posts.Add(newPost);
        // _context.Add(newPost);
        //! SAVE CHANGES TO THE DB, OR IT WON'T BE PERMANENT!
        _context.SaveChanges();


        // return RedirectToAction("AllPosts");
        // return Redirect($"/posts/{newPost.PostId}");
        return RedirectToAction("ViewPost", new { postId = newPost.PostId });
    }


    // GET ALL POSTS (Read)

    [HttpGet("posts")]
    public ViewResult AllPosts()
    {

        List<Post> PostsFromDb = _context.Posts.Include(p => p.Creator)
                                                        .Include(p => p.UserLikes)
                                                        .OrderByDescending(p => p.CreatedAt).ToList();

        // List<Post> PostsFromDb = _context.Posts.Include(p => p.Creator).OrderByDescending(p => p.CreatedAt).ToList();
        // List<Post> PostsFromDb = _context.Posts.OrderByDescending(p => p.CreatedAt).ToList();

        return View("AllPosts", PostsFromDb);
    }

    // Get One Post

    [HttpGet("posts/{postId}")]
    public IActionResult ViewPost(int postId)
    {
        // One to Many Query
        // Post? OnePost = _context.Posts.Include(p => p.Creator).FirstOrDefault(p => p.PostId == postId);
        //! Posts => UserPostLikes Table (UserLikes) => User Table (LikedBy) => User Table (Creator)

        Post? OnePost = _context.Posts
                                    .Include(p => p.UserLikes)
                                    .ThenInclude(upl => upl.LikedBy)
                                    .Include(p => p.Creator)
                                    .Include(p => p.UserComments)
                                    .ThenInclude(uc => uc.Commentor)
                                    .FirstOrDefault(p => p.PostId == postId);

        if (OnePost == null)
        {
            return RedirectToAction("AllPosts");
        }

        return View("ViewPost", OnePost);
    }


    // DELETE ONE POST

    [HttpPost("posts/{postId}/delete")]
    public RedirectToActionResult DeletePost(int postId)
    {
        Post? PostToDelete = _context.Posts.SingleOrDefault(p => p.PostId == postId);

        if (PostToDelete != null && PostToDelete.UserId == (int)HttpContext.Session.GetInt32("UserId"))
        {
            _context.Remove(PostToDelete);

            //! Save Changes!
            _context.SaveChanges();
        }

        return RedirectToAction("AllPosts");
    }


    // EDIT POSTS

    [HttpGet("posts/{postId}/edit")]
    public IActionResult EditPost(int postId)
    {
        Post? OnePost = _context.Posts.FirstOrDefault(p => p.PostId == postId);

        if (OnePost == null)
        {
            return RedirectToAction("AllPosts");
        }

        return View("EditPost", OnePost);
    }

    [HttpPost("posts/{postId}/update")]
    public IActionResult UpdatePost(int postId, Post editedPost)
    {
        Post? OldPost = _context.Posts.FirstOrDefault(p => p.PostId == postId);

        if (ModelState.IsValid)
        {
            OldPost.Topic = editedPost.Topic;
            OldPost.Body = editedPost.Body;
            OldPost.ImgURL = editedPost.ImgURL;

            OldPost.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("AllPosts");
        }

        return View("EditPost", editedPost);
    }


    // Like Action

    [HttpPost("posts/{postId}/likes")]
    public IActionResult ToggleLike(int postId)
    {
        int userId = (int)HttpContext.Session.GetInt32("UserId");

        UserPostLike? existingLike = _context.UserPostLikes.FirstOrDefault(upl => upl.PostId == postId && upl.UserId == userId);

        if (existingLike == null)
        {
            UserPostLike newLike = new() { UserId = userId, PostId = postId };
            _context.Add(newLike);
        }
        else
        {
            _context.Remove(existingLike);
        }

        _context.SaveChanges();

        // return RedirectToAction("AllPosts");

        Console.WriteLine("\n\n\n" + HttpContext.Request.Headers.Referer + "\n\n\n");

        return Redirect(HttpContext.Request.Headers.Referer);

    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}