#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace Posts.Models;

public class UserPostComment
{

    [Key]
    public int UUserPostCommentId { get; set; }

    public int UserId { get; set; }
    public int PostId { get; set; }

    [Required]
    [MinLength(2)]
    public string Body { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public User? Commentor { get; set; }
    public Post? CommentedPost { get; set; }

}