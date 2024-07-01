using System.ComponentModel.DataAnnotations;
namespace aspnetcoremvc.Areas.Identity.AccountModel;
public class RegisterModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;

    [Required]
    [StringLength(100, MinimumLength = 10)]
    public string UserName { get; set; } = String.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;


    [Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = String.Empty;

}