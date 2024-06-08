using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;

namespace GenerateJwt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Carregar o certificado
            var certificate = new X509Certificate2("C:/Users/marci/source/repos/service/AuthSampleCode/GenerateJwt/jwt.sample.code.pfx", "123456");

            // Criar credenciais de assinatura
            var signingCredentials = new X509SigningCredentials(certificate);

            // Criar token handler
            var tokenHandler = new JwtSecurityTokenHandler();


            // Definir as informações do token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("sub", "1234567890"),
                    new System.Security.Claims.Claim("name", "John Doe"),
                    new System.Security.Claims.Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = "https://api.example.com",
                Claims = new Dictionary<string, object>
                {
                    { "scope", "read:messages write:messages" }
                },
                SigningCredentials = signingCredentials
            };

            // Criar o token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            Console.WriteLine("Generated JWT:");
            Console.WriteLine(tokenString);

        }
    } 
}