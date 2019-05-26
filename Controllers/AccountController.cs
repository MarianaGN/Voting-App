using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AndriiCoursework.Auth;
using AndriiCoursework.DbContext;
using AndriiCoursework.Helpers;
using AndriiCoursework.Models;
using AndriiCoursework.Models.Jwt;
using AndriiCoursework.Models.Static;
using AndriiCoursework.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AndriiCoursework.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<Elector> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly JwtIssuerOptions _jwtOptions;
        public AccountController(
            UserManager<Elector> userManager, 
            ApplicationDbContext appDbContext, 
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _jwtFactory = jwtFactory;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            _jwtOptions = jwtOptions.Value;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = new Elector
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PassportNumber = model.PassportNumber,
                Password = model.Password,
                Email = model.Email,
                UserName = string.Concat(new[] {model.FirstName, model.LastName})
            };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _appDbContext.BallotForms.AddAsync(new BallotForm
            {
                DateOfElection = StaticData.DateOfElection,
                ElectorId = userIdentity.Id,
                UpdateDate = DateTime.Now
            });

            await _appDbContext.SaveChangesAsync();

            return new OkResult();
        }


        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]SignInViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            // Serialize and return the response
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await _jwtFactory.GenerateEncodedToken(credentials.UserName, identity),
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // get the user to verifty
                var userToVerify = await _userManager.FindByNameAsync(userName);

                if (userToVerify != null)
                {
                    // check the credentials  
                    if (await _userManager.CheckPasswordAsync(userToVerify, password))
                    {
                        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id.ToString()));
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
