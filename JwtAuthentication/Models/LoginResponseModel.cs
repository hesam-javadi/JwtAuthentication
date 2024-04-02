namespace JwtAuthentication.Models
{
    public class LoginResponseModel : BaseResponseModel
    {
        public string Token { get; set; } = string.Empty;
    }
}
