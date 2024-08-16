using BMS.Services.AuthAPI.Models.Dto;

namespace BMS.Services.AuthAPI.Services.Interfaces;

public interface IAuthService
{
    Task<string?> Register(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
}