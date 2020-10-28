using DesafioBackEndApi.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEndApi.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTSettings _jWTSettings;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<JWTSettings> jWTSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jWTSettings = jWTSettings.Value;
        }

        [HttpPost("auth")]
        public async Task<ActionResult> Registrar([FromBody]RegisterValidation usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new IdentityUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuario.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);
            return Ok(GerarJWT());
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login([FromBody]LoginValidation usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Password, false, true);

            if (result.Succeeded)
                return Ok(GerarJWT());

            return BadRequest("Usuário ou senha inválidos");
        }

        private string GerarJWT()
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //*Issuer = _jWTSettings.Issuer,
                //*Audience = _jWTSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(_jWTSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                         SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
