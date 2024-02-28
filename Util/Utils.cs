using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MvcMovie.Utils;

public abstract class Utils
{

    // Método para criptografar a senha
    public static string EncryptPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            // Convertendo a senha em bytes
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            // Calculando o hash SHA256
            byte[] hash = sha256.ComputeHash(bytes);

            // Convertendo o hash em uma string Base64
            return Convert.ToBase64String(hash);
        }
    }
}

public class AuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(string email, string role)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:Key"];
        //cria uma chave utilizando criptografia simétrica
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        //cria as credenciais do token
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
         new Claim("userName", email),
         new Claim(ClaimTypes.Role, role)
      };

        var token = new JwtSecurityToken( //cria o token
           issuer: issuer, //emissor do token
           audience: audience, //destinatário do token
           claims: claims, //informações do usuário
           expires: DateTime.Now.AddMinutes(30), //tempo de expiração do token
           signingCredentials: credentials); //credenciais do token

        var tokenHandler = new JwtSecurityTokenHandler(); //cria um manipulador de token

        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}
