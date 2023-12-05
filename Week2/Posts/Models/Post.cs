#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace Posts.Models;

public class Post
{

    [Key]
    public int PostId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string Topic { get; set; }

    [Required]
    [MinLength(2)]
    public string Body { get; set; }

    public string? ImgURL { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}