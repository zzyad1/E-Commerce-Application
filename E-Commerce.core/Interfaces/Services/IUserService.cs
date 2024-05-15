using E_Commerce.core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Interfaces.Services
{
    public interface IUserService
    {
        public Task<UserDto> LoginAsync(LoginDto dto);
        public Task<UserDto> RegisterAsync(RegisterDto dto);
    }
}
