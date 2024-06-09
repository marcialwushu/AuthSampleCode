using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using VerificationJwt.Models;

namespace VerificationJwt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configuração do ServiceProvider
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var tokenValidator = serviceProvider.GetService<JwkService>();

            JwkService jwkService = new JwkService();
            // JWT que você gerou anteriormente
            var jwt = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkExNTYwQjY4Mjk0RjdFOTNGQjU2QzJDNkZGQUUzMDFGQzFEQTk4NjYiLCJ4NXQiOiJvVllMYUNsUGZwUDdWc0xHXzY0d0g4SGFtR1kiLCJ0eXAiOiJKV1QifQ.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNzE3OTExMjI2LCJzY29wZSI6InJlYWQ6bWVzc2FnZXMgd3JpdGU6bWVzc2FnZXMiLCJuYmYiOjE3MTc5MTEyMjYsImV4cCI6MTcxNzkxNDgyNSwiYXVkIjoiaHR0cHM6Ly9hcGkuZXhhbXBsZS5jb20ifQ.NwxVrhGn7a2WFyCRK0nkRZmZDshA0BR3ciVwB81We5ewaXNmaIl3kMFISCw-egKq1FB0k8iqxq-aQEjTIPFX7_sCx545xRcOGEIksDpmFKWD-OvzJFJF4-IkLsG8DbyXVtJp8zHDgclo3mWEPBZ1Y0LGm6r_-IV51SWv85yQ8x7umxEzgY_w0In8WjhEwMsziBkuWZ_Ye_e0yQ87AU5Un4qcv0LxGY9T5bLzl_T6zdi4p34IaeqKW_Gd_GuCFK88rkbVp19TmLAAzfFIHCcikGgvWRFiGBGnlhnoUAnIH0-60mVHxaWch1ZvUdqhmw9visXY_AVj09S2wctYmF6b3Q";

            // Obtendo objeto Jwk via JwkApi
            var jwkToken = tokenValidator.GetJwkAsync();

            // Obtendo chave publica via Jwk
            SecurityKey securityKey = jwkService.GetSecurityKey(jwkToken);

            // Carregar o certificado público
            //var certificate = new X509Certificate2("C:/Users/marci/source/repos/service/AuthSampleCode/GenerateJwt/certificate.crt");

            // Criar as credenciais de validação
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                //IssuerSigningKey = new X509SecurityKey(certificate),
                IssuerSigningKey = securityKey,
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

        private static void ConfigureServices(IServiceCollection services)
        {
            // Registra IHttpClientFactory
            services.AddHttpClient();

            // Registra TokenValidator
            services.AddTransient<JwkService>();
        }
    }
}
