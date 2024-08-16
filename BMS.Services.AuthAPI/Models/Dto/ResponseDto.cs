using StatusCodes = BMS.Services.AuthAPI.Enums.StatusCodes;

namespace BMS.Services.AuthAPI.Models.Dto;

public class ResponseDto
{
    public object? Data { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public Enums.StatusCodes StatusCode { get; set; } = StatusCodes.OK;
}