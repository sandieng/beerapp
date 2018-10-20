﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BeerRosterAPI.Services
{
    public class JwtService
    {
        private const string _key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
        private static string _updatedJwtToken = "";

        public static string GenerateJwt(string userName)
        {
            

           // Create Security key  using private key above:
           // not that latest version of JWT using Microsoft namespace instead of System
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            // Also note that securityKey length should be >256b
            // so you have to make sure that your private key has a proper length
            //
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //  Finally create a Token
            var header = new JwtHeader(credentials);

            //Some PayLoad that contain information about the  customer
            var payload = new JwtPayload
            {
               { "userName", userName},
               { "expiryToken", DateTime.UtcNow.AddMinutes(10)},
            };

            //
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token to String so you can use it in your client
            _updatedJwtToken = handler.WriteToken(secToken);

            return _updatedJwtToken;
        }

        internal static string UpdateJwt(string bearerToken)
        {
            // Extract bearer token
            if (bearerToken != null)
            {
                var decodedToken = DecodeJwt(bearerToken);
                var userName = decodedToken.Payload["userName"].ToString();
                var updatedToken = GenerateJwt(userName);

                return updatedToken;
            }

            return "";
        }

        internal static JwtSecurityToken DecodeJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var reversedToken = handler.ReadToken(token);

            return (JwtSecurityToken) reversedToken;
        }

        internal static string GetUpdatedJwt()
        {
            return _updatedJwtToken;
        }
    }
}
