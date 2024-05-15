using E_Commerce.core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(ApplicationUser applicationUser);
    }
}
