using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    //private readonly SymmetricSecurityKey _key;

    //public TokenService(IConfiguration config)
    //{
    //    _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    //}


    ////////////////////////////////////////////////
    ///////////////////////////////////////////////////
    //
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appSettings.");
        if (tokenKey.Length < 64) throw new Exception("Your tokenKey needsto be longer.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName)
            //new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            //new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
