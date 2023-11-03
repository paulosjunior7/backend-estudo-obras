namespace Obras.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Obras.Api.Models;
    using Obras.Business.SharedDomain.Helpers;
    using Obras.Data;
    using Obras.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IConfiguration configuration, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginDetails model)
        {
            // Check user exist in system or not
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return NotFound();
            }

            // Perform login operation
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (signInResult.Succeeded)
            {
                // Obtain token
                TokenDetails token = await GetJwtSecurityTokenAsync(user);
                return Ok(token);
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }

        public async Task<IActionResult> DeleteUser([FromQuery] string id)
        {
            try
            {
                var existingUserDetails = await _userManager.FindByIdAsync(id);
                if (existingUserDetails != null)
                {
                    await _userManager.DeleteAsync(existingUserDetails);
                }
                else
                {
                    return BadRequest(new { message = "User not found" });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { message = e.Message });
            }

            return Ok("User has been deleted");
        }

        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserModel model)
        {
            #region Users

            var userDetails =  new User {
                        Email = model.Email,
                        UserName = model.UserName,
                        PhoneNumber = model.PhoneNumber,
                        CompanyId = model.CompanyId,
                        NormalizedUserName = model.UserName.ToUpper(),
                        NormalizedEmail = model.Email.ToLower(),
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0
            };

            try
            {
                var existingUserDetails = await _userManager.FindByEmailAsync(model.Email);
                if (existingUserDetails == null)
                {
                    var resultUser = await _userManager.CreateAsync(userDetails);
                    if (resultUser.Succeeded)
                    {
                        var resultPassword = await _userManager.AddPasswordAsync(userDetails, model.Password);
                        if (resultPassword.Succeeded)
                        {
                            try
                            {
                                await _userManager.AddToRoleAsync(userDetails, model.Roles);
                            } catch (Exception e)
                            {
                                await _userManager.DeleteAsync(userDetails);
                                return BadRequest(new { message = "Verifique nome da Roles" });
                            }
                        }
                        else
                        {
                            await _userManager.DeleteAsync(userDetails);
                            return BadRequest(new { message = resultPassword.Errors });
                        }
                    } else
                    {
                        await _userManager.DeleteAsync(userDetails);
                        return BadRequest(new { message = resultUser.Errors });
                    }
                } else
                {
                    return BadRequest(new { message = "Email already used" });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { message = e.Message });
            }

            #endregion

            return Ok("User has been created");
        }

        [AllowAnonymous]
        public async Task<IActionResult> CreateDefaultUsers()
        {
            #region Roles

            var rolesDetails = new List<string>
            {
                    Constants.Roles.Customer,
                    Constants.Roles.Engineer,
                    Constants.Roles.Admin
            };

            foreach (string roleName in rolesDetails)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            #endregion

            #region Users

            var userDetails = new Dictionary<string, User>{
                {
                    Constants.Roles.Admin,
                    new User { 
                        Email = "admin@demo.com", 
                        UserName = "AdminUser", 
                        EmailConfirmed = true, 
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0
                    }
                }
            };

            try
            {

                foreach (var details in userDetails)
                {
                    var existingUserDetails = await _userManager.FindByEmailAsync(details.Value.Email);
                    if (existingUserDetails == null)
                    {
                        await _userManager.CreateAsync(details.Value);
                        await _userManager.AddPasswordAsync(details.Value, "Password");
                        await _userManager.AddToRoleAsync(details.Value, details.Key);
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }

            #endregion

            return Ok("Default User has been created");
        }

        public async Task<IActionResult> ProtectedPage()
        {
            // Obtain MailId from token
            ClaimsIdentity identity = _httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity;
            var userName = identity?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            return Ok(user);
        }

        private async Task<TokenDetails> GetJwtSecurityTokenAsync(User user)
        {
            var keyInBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JwtIssuerOptions:SecretKey").Value);
            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(keyInBytes), SecurityAlgorithms.HmacSha256);
            DateTime tokenExpireOn = DateTime.Now.AddDays(3);

            // Obtain Role of User
            IList<string> rolesOfUser = await _userManager.GetRolesAsync(user);

            // Add new claims
            List<Claim> tokenClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, rolesOfUser.FirstOrDefault()),
            };

            // Make JWT token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JwtIssuerOptions:Issuer").Value,
                audience: _configuration.GetSection("JwtIssuerOptions:Audience").Value,
                claims: tokenClaims,
                expires: tokenExpireOn,
                signingCredentials: credentials
            );

            // Return it
            TokenDetails TokenDetails = new TokenDetails
            {
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireOn = tokenExpireOn,
            };

            // Set current user details for busines & common library
            var currentUser = await _userManager.FindByEmailAsync(user.Email);

            // Add new claim details
            var existingClaims = await _userManager.GetClaimsAsync(currentUser);
            await _userManager.RemoveClaimsAsync(currentUser, existingClaims);
            await _userManager.AddClaimsAsync(currentUser, tokenClaims);

            return TokenDetails;
        }
    }
}
