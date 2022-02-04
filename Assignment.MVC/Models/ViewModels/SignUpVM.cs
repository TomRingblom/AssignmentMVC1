using System.ComponentModel.DataAnnotations;

namespace Assignment.MVC.Models.ViewModels;

public class SignUpVM
{
    [Display(Name = "Förnamn")]
    [Required(ErrorMessage = "Du måste ange ett förnamn")]
    [StringLength(50, ErrorMessage = "OBS! Förnamnet måste bestå av minst 2 tecken.", MinimumLength = 2)]
    public string FirstName { get; set; }

    [Display(Name = "Efternamn")]
    [Required(ErrorMessage = "Du måste ange ett förnamn")]
    [StringLength(50, ErrorMessage = "OBS! Efternamnet måste bestå av minst 2 tecken.", MinimumLength = 2)]
    public string LastName { get; set; }

    [Display(Name = "E-postadress")]
    [Required(ErrorMessage = "Du måste ange en giltig E-postadress")]
    [StringLength(50, ErrorMessage = "OBS! Ange giltig E-postadress", MinimumLength = 6)]
    public string Email { get; set; }

    [Display(Name = "Lösenord")]
    [Required(ErrorMessage = "Du måste ange ett lösenord")]
    [DataType(DataType.Password)]
    [StringLength(50, ErrorMessage = "OBS! Lösenorder måste innehålla minst 8 tecken", MinimumLength = 8)]
    public string Password { get; set; }

    [Display(Name = "Bekräfta lösenord")]
    [Required(ErrorMessage = "Du måste bekräfta lösenordet")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Lösenorden matchar inte")]
    public string ConfirmPassword { get; set; }

    [Display(Name = "Gatuadress")]
    [Required(ErrorMessage = "Du måste ange en gatuadress")]
    [StringLength(50, ErrorMessage = "OBS! Gatuadressen måste bestå av minst 2 tecken.", MinimumLength = 2)]
    public string StreetName { get; set; }

    [Display(Name = "Postnummer")]
    [Required(ErrorMessage = "Du måste ange ett postnummer")]
    [StringLength(5, ErrorMessage = "OBS! Postnumret måste bestå av 5 siffror.", MinimumLength = 5)]
    public string PostalCode { get; set; }

    [Display(Name = "Ort")]
    [Required(ErrorMessage = "Du måste ange en ort")]
    [StringLength(50, ErrorMessage = "OBS! Ortnamnet måste bestå av minst 2 tecken.", MinimumLength = 2)]
    public string City { get; set; }

}