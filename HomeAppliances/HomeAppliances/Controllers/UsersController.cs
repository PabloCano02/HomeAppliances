using AutoMapper;
using HomeAppliances.Data;
using HomeAppliances.DTOs;
using HomeAppliances.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HomeAppliances.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager, ApplicationDbContext context, IMapper mapper, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("register")] // api/users/register
        public async Task<ActionResult<AuthenticationAnswer>> Register(UserCredential userCredential)
        {
            var user = new IdentityUser
            {
                UserName = userCredential.Email,
                Email = userCredential.Email
            };

            var result = await _userManager.CreateAsync(user, userCredential.Password);

            if (result.Succeeded)
            {
                return await BuildToken(userCredential);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        private async Task<AuthenticationAnswer> BuildToken(UserCredential userCredential)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredential.Email),
                new Claim("whatever", "any value")
            };

            var user = await _userManager.FindByEmailAsync(userCredential.Email);
            var claimsDB = await _userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["llavejwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(2);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new AuthenticationAnswer()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };
        }

        [HttpPost("RolAssign")] // Assign Claim (rol)
        public async Task<ActionResult> AddClaimsToUser(string email, string claimname, string claimValue)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"El usuario elegido con el email: {email}, no existe");
                return BadRequest(new
                {
                    error = "El usario no existe"
                });
            }

            var userClaim = new Claim(claimname, claimValue);
            var result = await _userManager.AddClaimAsync(user, userClaim);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = $"Al usuario {user.Email}, se le asignó el claim de: {claimname} "
                });
            }
            return BadRequest(new
            {
                error = $"No fue posible asignarle el Claim: {claimname} al usuario {user.Email}"
            });

        }

        [HttpPost("login")] // Login
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationAnswer>> Login(UserCredential userCredential)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredential.Email,
                userCredential.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildToken(userCredential);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }
    }
}
