#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace Posts.Models;
public class LogUser
{

    [Required]
    [EmailAddress]
    public string LogEmail { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string LogPassword { get; set; }

}
