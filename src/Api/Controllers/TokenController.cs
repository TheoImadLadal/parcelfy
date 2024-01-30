namespace parcelfy.Api.Controllers;

[ApiController]
[Route("token")]
[Produces("application/json")]
public class TokenController(IConfiguration configuration) : ControllerBase
{
	private readonly IConfiguration _configuration = configuration;

	/// <summary>
	/// Generate token
	/// </summary>
	/// <param name="user"></param>
	/// <returns></returns>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult Post(User user)
	{
		if (user.UserName == "parcelfy" && user.Password == "parcelfy123")
		{
			var issuer = _configuration["Authentication:Issuer"];
			var audience = _configuration["Authentication:Audience"];
			var key = Encoding.ASCII.GetBytes(_configuration["Authentication:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("Id", Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
					new Claim(JwtRegisteredClaimNames.Email, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti,
					Guid.NewGuid().ToString())
				}),
				Expires = DateTime.UtcNow.AddMinutes(5),
				Issuer = issuer,
				Audience = audience,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var stringToken = tokenHandler.WriteToken(token);
			return Ok(stringToken);
		}
		return Unauthorized();
	}
}
