using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL.Security;
using WebAPI.DAL.Users;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.AuthProvider.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        public LoginController(SignInManager<User> _signInManager)
        {
            this._signInManager = _signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Token(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, true, true);
                if (result.Succeeded)
                {
                    //create token: header + payload >> claims + signature

                    //payload
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.Login),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    //signature
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("webapi-authentication-valid"));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "WebApi",
                        audience: "Postman",
                        claims: claims,
                        signingCredentials: credentials,
                        expires: DateTime.Now.AddMinutes(30)
                        );
                   

                    string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    
                    return Ok(tokenString); //200
                }
                return Unauthorized();//401
            }

            return BadRequest(); //400
        }
    }
}