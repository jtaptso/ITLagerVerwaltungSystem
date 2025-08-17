using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using System.Text;
using Microsoft.Extensions.Configuration;

namespace JwtTokenGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            // Read values from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string key = config["Jwt:Key"] ?? "";
            string issuer = config["Jwt:Issuer"] ?? "";
            string audience = config["Jwt:Audience"] ?? "";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "testuser"),
                new Claim(ClaimTypes.Name, "Test User"),
                new Claim(ClaimTypes.Role, "Manager"), // Change role as needed
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("iss", issuer),
                new Claim("aud", audience)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine("Your JWT token:");
            Console.WriteLine(jwt);
        }
    }
}
