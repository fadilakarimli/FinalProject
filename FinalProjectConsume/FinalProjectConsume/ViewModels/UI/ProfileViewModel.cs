using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.ViewModels.UI
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "İstifadəçi adı tələb olunur")]
        [Display(Name = "İstifadəçi adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email tələb olunur")]
        [EmailAddress(ErrorMessage = "Düzgün email formatı daxil edin")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
