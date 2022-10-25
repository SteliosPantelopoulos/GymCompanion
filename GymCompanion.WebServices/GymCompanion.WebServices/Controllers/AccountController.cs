using GymCompanion.Data;
using GymCompanion.Data.Models.General;
using GymCompanion.Data.ServicesModels.Account;
using GymCompanion.Data.ServicesModels.General;
using GymCompanion.WebServices.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymCompanion.WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetToken")]
        public async Task<ActionResult> GetToken([FromQuery] LoginModel loginModel)
        {
            //TODO PASS DATA SECURE
            TokenModel model = new();
            try
            {
                User user = (User)await _userManager.FindByNameAsync(loginModel.Username);
                bool test1 = user != null;
                bool test2 = await _userManager.CheckPasswordAsync(user, loginModel.Password);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                                                    {
                                                        new Claim(ClaimTypes.Name, user.UserName),
                                                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                                    };

                    foreach (var userRole in userRoles)
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                    var token = Token(authClaims);

                    object returnObject = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };

                    return Ok(returnObject);
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                return Unauthorized();
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            //TODO PASS DATA SECURE
            try
            {
                bool userExists = await _userManager.FindByNameAsync(registerModel.Username) != null;

                if (userExists)
                    return Conflict();
                else
                {
                    User user = new()
                    {
                        UserName = registerModel.Username,
                        Email = registerModel.Email,
                        Birthday = registerModel.Birthday,
                        Country = registerModel.Country,
                        Firstname = registerModel.Firstname,
                        Lastname = registerModel.Lastname,
                        RegistrationDate = DateTime.Now,
                        SecurityStamp = Guid.NewGuid().ToString(),

                    };

                    var result = await _userManager.CreateAsync(user, registerModel.Password);

                    if (!result.Succeeded)
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    else
                        return Ok();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult> Login([FromQuery] LoginModel loginModel)
        {
            //TODO PASS DATA SECURE
            try
            {
                User user = (User)await _userManager.FindByNameAsync(loginModel.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                                                    {
                                                        new Claim(ClaimTypes.Name, user.UserName),
                                                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                                    };

                    foreach (var userRole in userRoles)
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                    var token = Token(authClaims);

                    object returnObject = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };

                    return Ok(returnObject);
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }




        

        //[HttpPost]
        //[Route("register-admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        //{
        //    var userExists = await _userManager.FindByNameAsync(model.Username);
        //    if (userExists != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        //    IdentityUser user = new()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.Username
        //    };
        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        //    if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //    if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        //        await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //    if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        //    }
        //    if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await _userManager.AddToRoleAsync(user, UserRoles.User);
        //    }
        //    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        //}

        private JwtSecurityToken Token(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1.0),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}

