using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VerificationJwt.Models;

namespace VerificationJwt
{
        
    public class JwkService
    {
        public ILogger<JwkService> Logger ;

        private readonly IHttpClientFactory _httpClientFactory;

        public JwkService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public JwkService() { }

        public JwkKey GetJwkAsync()
        {
            JwkKey? jwkKey = new JwkKey();
            try
            {
                using HttpClient client = _httpClientFactory.CreateClient();

                var response =  client.GetStringAsync("http://localhost:5028/api/jwk/jwks.json").Result;

                return JsonConvert.DeserializeObject<JwkKey>(response);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error getting something fun to say: {Error}", ex);
            }
            return jwkKey;
        }

        public SecurityKey GetSecurityKey(JwkKey jwk)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(new RSAParameters
            {
                Modulus = Convert.FromBase64String(jwk.Modulus),
                Exponent = Convert.FromBase64String(jwk.Exponent)
            });
            return new RsaSecurityKey(rsa) { KeyId = jwk.KeyId };
        }
    }
    
}
