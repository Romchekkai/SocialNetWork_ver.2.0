using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetWork.BL.ViewModels.Account
{
    public class UserEditViewModel
    {
        public string UserId { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "@Model.User.LastName")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Отчество", Prompt = "@Model.User.MiddleName")]
        public string MiddleName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "@Model.User.FirstName")]
        public string FirstName { get; set; }

        [EmailAddress]
        [Display(Name = "Email", Prompt = "@Model.User.Email")]
        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Никнейм", Prompt = "@Model.User.UserName")]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Статус", Prompt = "Введите статус")]
        public string Status { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "О себе", Prompt = "Введите данные о себе")]
        public string About { get; set; }

    }


}
