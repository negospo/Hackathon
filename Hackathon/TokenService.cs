using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hackathon
{
    /// <summary>
    /// Classe responsável pela geração do JWT.
    /// </summary>
    public static class TokenService
    {
        /// <summary>
        /// Gera um JWT com as informações e permissões do cliente
        /// </summary>
        /// <param name="user">Usuário logado</param>
        public static Models.Authenticate.JWT GenerateToken(Models.Authenticate.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Configuration.GetValue<string>("JWToken:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Name", user.Name),
                }),
                Expires = DateTime.UtcNow.AddMinutes(Settings.Configuration.GetValue<int>("JWToken:ExpiresMinutes")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Models.Authenticate.JWT
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = "",
                Expires = tokenDescriptor.Expires
            };
        }
    }
}
