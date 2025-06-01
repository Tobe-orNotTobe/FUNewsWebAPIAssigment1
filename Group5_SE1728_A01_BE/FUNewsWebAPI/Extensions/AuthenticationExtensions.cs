using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FUNewsWebAPI.Extensions
{
	public static class AuthenticationExtensions
	{
		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection("JwtSettings");
			var secretKey = jwtSettings["SecretKey"];
			if (string.IsNullOrEmpty(secretKey))
				throw new InvalidOperationException("Missing SecretKey in configuration");

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings["Issuer"],
					ValidAudience = jwtSettings["Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
					RoleClaimType = "Role"
				};
			});

			return services;
		}
	}

	public class JwtService
	{
		private readonly IConfiguration _configuration;

		public JwtService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateToken(short accountId, string email, int role)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var secretKey = jwtSettings["SecretKey"];
			if (string.IsNullOrEmpty(secretKey))
				throw new InvalidOperationException("Missing SecretKey in configuration");

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
			new System.Security.Claims.Claim("AccountId", accountId.ToString()),
			new System.Security.Claims.Claim("Email", email),
			new System.Security.Claims.Claim("Role", GetRoleName(role))
		};

			var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
				issuer: jwtSettings["Issuer"],
				audience: jwtSettings["Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: credentials
			);

			return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
		}

		private string GetRoleName(int role)
		{
			return role switch
			{
				0 => "Admin",
				1 => "Staff",
				2 => "Lecturer",
				_ => "Guest"
			};
		}
	}
}