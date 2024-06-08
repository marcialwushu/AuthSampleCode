using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;

namespace VerificationJwt
{
    class Program
    {
        static void Main(string[] args)
        {
            // JWT que você gerou anteriormente
            var jwt = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkExNTYwQjY4Mjk0RjdFOTNGQjU2QzJDNkZGQUUzMDFGQzFEQTk4NjYiLCJ4NXQiOiJvVllMYUNsUGZwUDdWc0xHXzY0d0g4SGFtR1kiLCJ0eXAiOiJKV1QifQ.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNzE3ODc3OTIwLCJzY29wZSI6InJlYWQ6bWVzc2FnZXMgd3JpdGU6bWVzc2FnZXMiLCJuYmYiOjE3MTc4Nzc5MjAsImV4cCI6MTcxNzg4MTUxNiwiYXVkIjoiaHR0cHM6Ly9hcGkuZXhhbXBsZS5jb20ifQ.C-Py_L64Sd-ukd-bluv-tb3T8JfucdJOCcWBJ09WztNRyt0nnlHUNALjIOpNdesV3dmXr1dfG7cf8xWLm0cX0pZb9yhDf3YKo2Bbx6E5bGyQfsLnfJb0muq6yvTLErj_AjrMDIZVRXX4krhiIjHOY07u2N1_CAK3pl2gxhHOML8dpb-DQ13R-37B9ujHBqfBTJJv4-emJviOv4LyL8UZxpg_lNiww1bQv7ulm0G81T6C49LICyV8-fw9JKdru057wPuKVWEWq5CG0N4h08OE00YO0wgMDVsFr2ebbVflLrgSg-sx1mD8cdn197anquyIKRbIVwUoAdfUe3Jd-aAkCw";

            // Carregar o certificado público
            var certificate = new X509Certificate2("C:/Users/marci/source/repos/service/AuthSampleCode/GenerateJwt/certificate.crt");

            // Criar as credenciais de validação
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new X509SecurityKey(certificate),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidAudience = "https://api.example.com",
                ValidateLifetime = true
            };

            // Token handler para validar o token
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validar o token
                var principal = tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);

                Console.WriteLine("JWT is valid");
                Console.WriteLine("Claims:");
                foreach (var claim in principal.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }

                // Verificar o escopo
                var scopeClaim = principal.FindFirst("scope")?.Value;
                if (scopeClaim != null)
                {
                    var scopes = scopeClaim.Split(' ');
                    Console.WriteLine("Scopes:");
                    foreach (var scope in scopes)
                    {
                        Console.WriteLine(scope);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JWT validation failed: {ex.Message}");
            }
        }
    }
}
