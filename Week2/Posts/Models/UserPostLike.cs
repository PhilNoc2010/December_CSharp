#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace Posts.Models;

public class UserPostLike
{

    [Key]
    public int UserPostLikeId { get; set; }

    public int UserId { get; set; }
    public int PostId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public User? LikedBy { get; set; }
    public Post? LikedPost { get; set; }

}