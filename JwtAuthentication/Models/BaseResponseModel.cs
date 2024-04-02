
namespace JwtAuthentication.Models
{
    public class BaseResponseModel
    {
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
