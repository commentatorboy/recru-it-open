using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using recru_it.Data;
using recru_it.Models;
using recru_it.Models.AccountViewModels;

namespace recru_it.Controllers
{
        public class JwtPacket
    {
        public string Token { get; set; }
        public string UserName { get; set; }
    }
    
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        readonly ApplicationDbContext context;

        public AuthController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
             _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody]LoginViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, lockoutOnFailure: false, isPersistent: false);

            if (user == null)
            {
                return NotFound("email or password incorrect");
            }
            return Ok(CreateJwtPacket(user));
        }

        [HttpPost("register")]
        public JwtPacket Register([FromBody]ApplicationUser user)
        {

            context.Users.Add(user);
            context.SaveChanges();

            return CreateJwtPacket(user);
        }

        
        [HttpGet]
        public ActionResult Get()
        {
            //middleware uses something with (watch chapter 47) and look into nameidentifier, this
            //also this only works because the first one is the tokenidentifer. This should be changed accordingly

            return Ok(GetSecureUser());
        }

          private ApplicationUser GetSecureUser()
        {
            var id = HttpContext.User.Claims.First().Value;
            
            var user = context.Users.SingleOrDefault(u => u.Id == id);
            
            return user;
        }

        JwtPacket CreateJwtPacket(ApplicationUser user)
        {

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrase"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id) 
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: DateTime.Now.AddMinutes(1));

            //gives us the encoded token as a string
            var encodedJwt = new JwtSecurityTokenHandler
                ().WriteToken(jwt);
            return new JwtPacket() { Token = encodedJwt, UserName = user.Email };

        }

    }
}