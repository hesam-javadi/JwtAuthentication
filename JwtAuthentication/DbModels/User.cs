using System.ComponentModel.DataAnnotations;

namespace JwtAuthentication.DbModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
    }
}
