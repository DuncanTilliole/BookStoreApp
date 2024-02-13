using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTO.Auth;
using BookStoreApp.API.Statics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthController (ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var user = _mapper.Map<ApiUser>(userDTO);
            IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password);

            try
            {
                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(user, userDTO.Role);

                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing in {nameof(Register)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDTO userDTO)
        {
            _logger.LogInformation("Login Request");
            try
            {
                ApiUser? user = await _userManager.FindByEmailAsync(userDTO.Email);
                var passwordValid = await _userManager.CheckPasswordAsync(user, userDTO.Password);

                if (user == null || !passwordValid)
                {
                    return NotFound();
                }

                string tokenString = await GenerateToken(user);

                AuthResponse response = new AuthResponse
                {
                    Email = userDTO.Email,
                    Token = tokenString,
                    UserId = user.Id,
                };

                return response;  
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing in {nameof(Login)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            SymmetricSecurityKey? securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            SigningCredentials? credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            IList<string> roles = await _userManager.GetRolesAsync(user);
            List<Claim>? roleClaims =  roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            IList<Claim>? userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["JwtSettings:Duration"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
