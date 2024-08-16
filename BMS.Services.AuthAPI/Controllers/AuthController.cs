using BMS.Services.AuthAPI.Models.Dto;
using BMS.Services.AuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StatusCodes = BMS.Services.AuthAPI.Enums.StatusCodes;

namespace BMS.Services.AuthAPI.Controllers;
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    protected ResponseDto _response;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
        _response = new ResponseDto();
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {

        var errorMessage = await _authService.Register(model);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            _response.IsSuccess = false;
            _response.Message= errorMessage;
            _response.StatusCode = StatusCodes.InternalServerError;
            return BadRequest(_response);
        }

        _response.StatusCode = StatusCodes.Created;
        return Ok(_response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var loginResponse = await _authService.Login(model);
        if (loginResponse.User == null)
        {
            _response.IsSuccess = false;
            _response.Message = "Username or password is incorrect";
            _response.StatusCode = StatusCodes.BadRequest;
            return BadRequest(_response);
        }
        _response.Data = loginResponse;
        _response.StatusCode = StatusCodes.OK;
        return Ok(_response);
    }
    
    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
    {
        var assignRoleSuccessful = await _authService.AssignRole(model.Email,model.Role.ToUpper());
        if (!assignRoleSuccessful)
        {
            _response.IsSuccess = false;
            _response.Message = "Error encountered";
            _response.StatusCode = StatusCodes.InternalServerError;
            return BadRequest(_response);
        }
        _response.StatusCode = StatusCodes.NoContent;
        return Ok(_response);
    }
}