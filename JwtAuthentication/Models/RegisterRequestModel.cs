using System.ComponentModel.DataAnnotations;

namespace JwtAuthentication.Models
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد نمایید")] 
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "پسوورد را وارد نمایید")]
        [MinLength(6, ErrorMessage = "پسوورد نماید از 6 کلمه کمتر باشد")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "شماره همراه را وارد نمایید")]
        [Phone(ErrorMessage = "شماره همراه معتبر نیست")]
        public string Mobile { get; set; } = string.Empty;
    }
}