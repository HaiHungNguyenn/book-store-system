using BMS.Services.AuthAPI.Models;

namespace BMS.Services.AuthAPI.Services.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
}