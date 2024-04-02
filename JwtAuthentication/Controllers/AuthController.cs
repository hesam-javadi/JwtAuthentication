using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthentication.DbModels;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class AuthController(MyDbContext db, IConfiguration config) : ControllerBase
    {
        [HttpPost, Route("Register")]
        public async Task<ActionResult<BaseResponseModel>> RegisterAsync([FromBody] RegisterRequestModel model)
        {
            var ret = new BaseResponseModel();
            try
            {
                if (db.Users.Any(u => u.UserName == model.UserName))
                {
                    ret.IsSuccess = false;
                    ret.ErrorMessages.Add("این نام کاربری از قبل موجود است.");
                    return ret;
                }
                if (db.Users.Any(u => u.Mobile == model.Mobile))
                {
                    ret.IsSuccess = false;
                    ret.ErrorMessages.Add("این شماره از قبل موجود است.");
                    return ret;
                }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
                var user = new User
                {
                    Mobile = model.Mobile,
                    UserName = model.UserName,
                    PasswordHash = passwordHash
                };
                db.Users.Add(user);
                await db.SaveChangesAsync();
                ret.IsSuccess = true;
            }
            catch
            {
                ret.IsSuccess = false;
                ret.ErrorMessages.Add("خطایی رخ داده است");
            }

            return Ok(ret);
        }

        [HttpPost, Route("Login")]
        public async Task<ActionResult<LoginResponseModel>> LoginAsync([FromBody] LoginRequestModel model)
        {

            var ret = new LoginResponseModel();
            try
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == model.UserName);
                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    ret.IsSuccess = false;
                    ret.ErrorMessages.Add("نام کاربری یا رمز عبور اشتباه است.");
                    return ret;
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>()
                {
                    new("UserId", user.Id.ToString()),
                    new("UserName", user.UserName),
                    new("Mobile", user.Mobile)
                };
                var sectoken = new JwtSecurityToken(config["Jwt:Issuer"],
                    config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddDays(90),
                    signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(sectoken);
                ret.IsSuccess = true;
                ret.Token = token;
            }
            catch (Exception e)
            {
                ret.IsSuccess = false;
                ret.ErrorMessages.Add("خطایی رخ داده است!");
            }
            return ret;
        }
    }
}