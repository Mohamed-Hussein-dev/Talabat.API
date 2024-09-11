using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Repository;

namespace Talabat.APIs.Controllers
{
   
    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;

        public AccountsController(UserManager<AppUser> userManager ,IMapper mapper,
                                  SignInManager<AppUser> signInManager , ITokenService tokenService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }
        // Register end Point

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>>Regester(RegesterDto model)
        {
            if(EmailExists(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "Email address is already Used"));
            }

            var User = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };
            var Result = await userManager.CreateAsync(User , model.Password);

            if (!Result.Succeeded) BadRequest(new ApiResponse(400));
            var ReturnUser = new UserDto()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Token = await tokenService.GenerateTokenAsync(User, userManager)
            };
            return Ok(ReturnUser);
        }

        //Log in end point
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LogInUserDto Model)
        {
            var user = await userManager.FindByEmailAsync(Model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));

            var Result = await signInManager.CheckPasswordSignInAsync(user, Model.Password, false);

            if(!Result.Succeeded) return Unauthorized(new ApiResponse(401));

            var ReturnUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.GenerateTokenAsync(user, userManager)
            };
            return Ok(ReturnUser);
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(Email);

            var ReturnedUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.GenerateTokenAsync(user, userManager)
            };

            return Ok(ReturnedUser);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<UserAddressDto>> GetCurrentUserAddress()
        {
            var user = await userManager.GetUserWithAddressAsync(User);
            var MappedAddress = mapper.Map<UserAddressDto>(user?.Address);
            return Ok(MappedAddress);
        }

        // Update User Address
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<UserAddressDto>> UpdateUserAddress(UserAddressDto UpdatedAddress)
        {
            var user = await userManager.GetUserWithAddressAsync(User);
            var MappedAddress = mapper.Map<Address>(UpdatedAddress);
            MappedAddress.Id = user.Address.Id;
            user.Address = MappedAddress;
            var Result = await userManager.UpdateAsync(user);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(UpdatedAddress);
        }


        // validat Email is Exist
        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> EmailExists(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            return user is not null;
        }
        

            
    }
}
