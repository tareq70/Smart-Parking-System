using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Smart_Parking_System.Application_Layer.Dtos.Auth;
using Smart_Parking_System.Application_Layer.Interfaces;
using Smart_Parking_System.Domain_Layer.Entities;
using Smart_Parking_System.Infrastructure.Data;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Smart_Parking_System.Domain_Layer.Repositories
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbcontext dbContext;

        public AuthenticationServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, AppDbcontext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            this.dbContext = dbContext;
        }
        public async Task<ResponseDTO> LoginUserAsync(LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var token = GenerateJwtToken(user, role);

            return new ResponseDTO
            {
                Success = true,
                Token = token,
                Message = "User logged in successfully."
            };
        }


        public async Task<ResponseDTO> RegisterUserAsync(RegisterDTO register)
        {
            if (register.Password != register.ConfirmPassword)
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Passwords do not match."
                };

            var existingUserByUsername = await _userManager.FindByNameAsync(register.Username);
            if (existingUserByUsername != null)
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Username already exists."
                };

            var existingUserByEmail = await _userManager.FindByEmailAsync(register.Email);
            if (existingUserByEmail != null)
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Email already exists."
                };

            var user = new ApplicationUser
            {
                FullName = register.FullName,
                UserName = register.Username,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                SSN = register.SSN
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
                return new ResponseDTO
                {
                    Success = false,
                    Message = string.Join("; ", result.Errors.Select(e => e.Description))
                };

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(user, "User");

            var token = GenerateJwtToken(user, "User");

            return new ResponseDTO
            {
                Success = true,
                Token = token,
                Message = "User registered successfully."
            };
        }


        private string GenerateJwtToken(ApplicationUser user, string role)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
