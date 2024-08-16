using System.ComponentModel.DataAnnotations;

namespace BMS.Services.AuthAPI.Models.Dto;

public class RegistrationRequestDto
{
    [Required]
    public required string Email { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    [Required]
    public required string Password { get; set; }
    public string? Role { get; set; }
}