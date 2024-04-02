using System.ComponentModel.DataAnnotations;

namespace JwtAuthentication.Models
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد نمایید")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "پسوورد را وارد نمایید")]
        public string Password { get; set; } = string.Empty;
    }
}
