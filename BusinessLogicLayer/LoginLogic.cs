using DatabaseAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class LoginLogic
    {
        private readonly HRMSDbcontext _context;
        private readonly IConfiguration _configuration;

        public LoginLogic(HRMSDbcontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<string> Login(EmployeeViewModel model, IConfiguration configuration)
        {
            Employee user = await _context.Employees.SingleOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

            if (user == null)
            {
                throw new Exception("Invalid username or password Try Again.");
            }

            // Create claims for the user
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.EmployeeId.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
    };

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Set token expiration time
                Issuer = configuration["Jwt:Issuer"],  // Add this
                Audience = configuration["Jwt:Audience"], // Add this
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }


        
    }
}
