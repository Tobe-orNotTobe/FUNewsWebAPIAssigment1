using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using FUNewsWebAPI.Extensions;

namespace FUNewsWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ISystemAccountService _service;
		private readonly IConfiguration _configuration;
		private readonly JwtService _jwtService;

		public AuthController(ISystemAccountService service, IConfiguration configuration, JwtService jwtService)
		{
			_service = service;
			_configuration = configuration;
			_jwtService = jwtService;
		}
		[HttpPost("login")]
		[AllowAnonymous]
		public IActionResult Login([FromBody] LoginModel login)
		{
			if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
			{
				return BadRequest("Email and password are required.");
			}

			// Check for admin account from appsettings.json
			var adminEmail = _configuration["AdminAccount:Email"];
			var adminPassword = _configuration["AdminAccount:Password"];
			var adminRole = _configuration.GetValue<int>("AdminAccount:Role");

			if (login.Email == adminEmail && login.Password == adminPassword)
			{
				return GenerateLoginResponse(0, "Admin", adminEmail, adminRole);
			}

			var accounts = _service.GetAccounts();
			var account = accounts.FirstOrDefault(a =>
				a.AccountEmail == login.Email &&
				a.AccountPassword == login.Password);

			if (account == null)
			{
				return Unauthorized("Invalid email or password.");
			}

			return GenerateLoginResponse(
				account.AccountId,
				account.AccountName,
				account.AccountEmail,
				account.AccountRole ?? 2
			);
		}

		private IActionResult GenerateLoginResponse(short id, string name, string email, int role)
		{
			var token = _jwtService.GenerateToken(id, email, role);
			var roleName = role switch
			{
				0 => "Admin",
				1 => "Staff",
				2 => "Lecturer",
				_ => "Guest"
			};

			return Ok(new
			{
				AccountId = id,
				AccountName = name,
				AccountEmail = email,
				AccountRole = role,
				Token = token,
				RoleName = roleName
			});
		}
	}

	public class LoginModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}