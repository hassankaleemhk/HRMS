using DatabaseAccessLayer;
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
    public class RoleLogic
    {
        private readonly HRMSDbcontext _context;
        private readonly IConfiguration _configuration;

        public RoleLogic(HRMSDbcontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<bool> UpdateRole(RoleViewModel roleViewModel, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); // Use _configuration to access the key
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false

            };

            SecurityToken decryptedToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out decryptedToken);

            // Access claims from the decrypted token
            var claimsIdentity = principal.Identity as ClaimsIdentity;
            var nameIdentifierClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var nameClaim = claimsIdentity.FindFirst(ClaimTypes.Name);

            // Retrieve the values from the claims
            var nameIdentifier = nameIdentifierClaim.Value;
            var name = nameClaim.Value;

            // Check if the user has permission to update the role
            if (name == "Nancy")
            {
                int roleId = roleViewModel.RoleId;
                var role = await _context.Roles.FindAsync(roleId);

                if (role != null)
                {
                    role.RoleName = roleViewModel.RoleName;
                    await _context.SaveChangesAsync();
                    return true; // Role updated successfully
                }
            }

            return false; // Role not found or user doesn't have permission
        }

    }
}
