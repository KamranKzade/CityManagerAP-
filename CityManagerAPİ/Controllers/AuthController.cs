using System.Text;
using CityManagerAPİ.Data;
using CityManagerAPİ.Dtos;
using CityManagerAPİ.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityManagerAPİ.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private IAuthRepository _authRepo;
	private IConfiguration _configuration;

	public AuthController(IAuthRepository authRepo, IConfiguration configuration)
	{
		_authRepo = authRepo;
		_configuration = configuration;
	}

	[HttpPost("Register")]
	public async Task<IActionResult> Register([FromBody] UserForRegisterDto dto)
	{
		if (await _authRepo.UserExists(dto.Username!))
		{
			ModelState.AddModelError("username", "Username already exists");
		}

		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var userToCreate = new User
		{
			Username = dto.Username
		};

		await _authRepo.RegisterAsync(userToCreate, dto.Username!);
		return StatusCode(StatusCodes.Status201Created);
	}


	[HttpPost("Login")]
	public async Task<IActionResult> Login([FromBody] UserForLoginDto dto)
	{
		var user = await _authRepo.LoginAsync(dto.Username!, dto.Password!);
	
		if(user == null)
		{
			return Unauthorized();
		}

		var word = _configuration.GetSection("AppSettings:Token").Value;
		var key = Encoding.ASCII.GetBytes(word);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(
				new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
					new Claim(ClaimTypes.Name,user.Username!)
				}),
			Expires=DateTime.Now.AddDays(1),
			SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512)
		};
		
		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);

		var tokenString = tokenHandler.WriteToken(token);
		return Ok(tokenString);
	}

}
