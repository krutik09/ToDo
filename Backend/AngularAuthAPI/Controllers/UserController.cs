using AngularAuthAPI.Data;
using AngularAuthAPI.DTO;
using AngularAuthAPI.Interface;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUnitOfWork uow;
        private readonly DBC dBC;

        public IConfiguration Configuration { get; set; }
        public UserController(IUnitOfWork uow, IConfiguration configuration,DBC dBC)
        {
            this.uow = uow;
            Configuration = configuration;
            this.dBC = dBC;
        }
        [HttpGet("GetUSER")]
        [Authorize]
        public async Task<List<User>> GetUsers()
        {
            var data= dBC.Users.ToList();
            return data;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Signup(SignupReqDto signupReq)
        {
            if(await uow.UserRepository.UserAlreadyRegistered(signupReq.Username,signupReq.Email))
            {
                return BadRequest(new {Message="Username or email is already registered" });
            }
            uow.UserRepository.Register(signupReq.FirstName, signupReq.LastName, signupReq.Email, signupReq.Username, signupReq.Password);
            await uow.SaveAsync();
            return Ok(new { Message = "Successfully registered" });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.UserRepository.Authenticate(loginReq.username, loginReq.password);
            if (user == null)
            {
                return BadRequest(new {Message="Please register Yourself first"});
            }
            LoginResDto loginRes = new LoginResDto();
            loginRes.name = loginReq.username;
            loginRes.token = CreateJWT(user);
            return Ok(loginRes);
        }
        private string CreateJWT(User user)
        {
            var secretkey = Configuration.GetSection("JWT:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                //new Claim(ClaimTypes.NameIdentifier,user.id.ToString())
                new Claim("id",user.id.ToString())
            };
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
