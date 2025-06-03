using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Account
{
    public class Login
    {
        [Required]
        public string UserNameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
