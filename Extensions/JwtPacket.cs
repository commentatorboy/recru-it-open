using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using recru_it.Models;

namespace recru_it.Extensions
{
    public class JwtPacket
    {
        public string Token { get; set; }
        public string UserName { get; set; }

        public static JwtPacket CreateJwtPacket(ApplicationUser user)
        {

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrase"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: DateTime.Now.AddMinutes(10));

            //gives us the encoded token as a string
            var encodedJwt = new JwtSecurityTokenHandler
                ().WriteToken(jwt);
            return new JwtPacket() { Token = encodedJwt, UserName = user.Email };

        }
    }

}