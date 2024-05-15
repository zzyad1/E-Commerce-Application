using E_Commerce.core.DataTransferObjects;
using E_Commerce.core.Entities.Identity;
using E_Commerce.core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class UserServices : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManagar;
        private readonly ITokenService _tokenService;

        public UserServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManagar, 
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManagar = signInManagar;
            _tokenService = tokenService;
        }

        public async Task<UserDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is not null)
            {
                var result = await _signInManagar.CheckPasswordSignInAsync(user ,dto.Password,false);
                if (result.Succeeded)
                {
                    return new UserDto
                    {
                        Email = user.Email,
                        DisplayName = user.DisplayName,
                        Token = _tokenService.GenerateToken(user)
                    };
                }
            }
            return null;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is not null) throw new Exception("Email Exist");
            var appUser = new ApplicationUser
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                UserName = dto.DisplayName,
            };
            var result = await _userManager.CreateAsync(appUser , dto.Password );
            if (!result.Succeeded) throw new Exception("Errors");

            return new UserDto
            {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Token = _tokenService.GenerateToken(appUser)
            };
        }
    }
}
