using System.ComponentModel.DataAnnotations;

namespace SocialNetWork.BL.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login", Prompt = "Введите логин")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
