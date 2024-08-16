using Microsoft.AspNetCore.Identity;

namespace BMS.Services.AuthAPI.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}