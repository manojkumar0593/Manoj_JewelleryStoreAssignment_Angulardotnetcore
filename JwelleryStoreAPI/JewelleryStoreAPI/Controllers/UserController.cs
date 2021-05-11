using AspNetCore.JwtAuthentication.PasswordHasing.Plugin;
using AutoMapper;
using JewelleryStore.DataAccess;
using JewelleryStore.DataAccess.Domain.Models;
using JewelleryStoreAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JewelleryStoreAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public IConfiguration Configuration { get; }

        public UserController(IUnitOfWork uow, IMapper mapper, ITokenService tokenService, IConfiguration configuration)
        {
            this.uow = uow;
            _mapper = mapper;
            _tokenService = tokenService;
            Configuration = configuration;
        }


        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login(UserViewModel user)
        {
            if (string.IsNullOrEmpty(user.EmailId)) {
                return BadRequest("Invalid User Details");
            }
            var userModel = _mapper.Map<User>(user);

            var userResult = uow.UserRepository.Login(userModel);
            if(userResult is null)
            {
                return Unauthorized("Invalid Credentials");
            }
            var loggedInUserData = _mapper.Map<UserViewModel>(userResult);
            loggedInUserData.Role = userResult.UserRole?.Role.Name;

            var usersClaims = new[]
             {
                new Claim(ClaimTypes.Name, loggedInUserData.EmailId),
                new Claim(ClaimTypes.NameIdentifier, loggedInUserData.UserName)
            };
            loggedInUserData.Token = _tokenService.GenerateAccessToken(
                usersClaims,
                Configuration["Tokens:Issuer"],
                Configuration["Tokens:Issuer"],
                Configuration["Tokens:Key"],
                "60"
            );
            return Ok(loggedInUserData);
        }

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<UserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
