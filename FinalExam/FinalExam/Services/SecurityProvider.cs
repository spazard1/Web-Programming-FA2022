using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FinalExam.Services
{
    public class SecurityProvider : ISecurityProvider
    {

        private RSA rsa;

        public SecurityProvider()
        {
            rsa = RSA.Create();
        }

        public string GetToken(List<Claim> claims)
        {
            var handler = new JwtSecurityTokenHandler();
            var credentials = new SigningCredentials(new RsaSecurityKey(rsa.ExportParameters(true)), SecurityAlgorithms.RsaSha256);
            var token = new JwtSecurityToken("www.webprogrammingserver.com", "www.bethel.edu", claims, DateTime.Now.Subtract(TimeSpan.FromMinutes(15)), DateTime.Now.AddDays(1), credentials);
            return handler.WriteToken(token);
        }

        public bool ValidateToken(string tokenString)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidIssuer = "www.webprogrammingserver.com",
                ValidAudience = "www.bethel.edu",
                IssuerSigningKey = new RsaSecurityKey(rsa.ExportParameters(false))
            };

            var handler = new JwtSecurityTokenHandler();

            try
            {
                handler.ValidateToken(tokenString, validationParameters, out SecurityToken token);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}
