using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Api.Tokens;

public interface ITokenService
{
    string Create(string username);
    (string Username, bool Authenticated) Validate(string token);
}

public class TokenService : ITokenService
{
    private string Secret = "asdv234234^&%&^%&^hjsdfb2%%%";
    private string Issuer = "http://mysite.com";
    private string Audience = "http://myaudience.com";
    private SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    private JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

    public string Create(string username)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new Claim[]{ new Claim(ClaimTypes.NameIdentifier, username) }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = Issuer,
            Audience = Audience,
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = TokenHandler.CreateToken(tokenDescriptor);
        return TokenHandler.WriteToken(securityToken);
    }

    public (string Username, bool Authenticated) Validate(string token)
    {
        try
        {
            var claimsPrincipal = TokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = SecurityKey
            }, out SecurityToken validatedToken);

            string nameIdentifier = claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return (nameIdentifier, true);
        }
        catch (Exception e)
        {
            return (string.Empty, false);
        }
    }
}