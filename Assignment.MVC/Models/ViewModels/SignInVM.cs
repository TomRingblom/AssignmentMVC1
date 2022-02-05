using System.ComponentModel.DataAnnotations;

namespace Assignment.MVC.Models.ViewModels;

public class SignInVM
{
    [Display(Name = "E-postadress")]
    [Required(ErrorMessage = "Du måste ange en giltig E-postadress")]
    [StringLength(50, ErrorMessage = "OBS! Ange giltig E-postadress", MinimumLength = 6)]
    public string Email { get; set; }

    [Display(Name = "Lösenord")]
    [Required(ErrorMessage = "Du måste ange ett lösenord")]
    [DataType(DataType.Password)]
    [StringLength(50, ErrorMessage = "OBS! Lösenorder måste innehålla minst 8 tecken", MinimumLength = 8)]
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
}