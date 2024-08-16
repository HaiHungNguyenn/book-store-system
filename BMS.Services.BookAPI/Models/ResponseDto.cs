using StatusCodes = BMS.Services.BookAPI.Enums.StatusCodes;

namespace BMS.Services.BookAPI.Models;

public class ResponseDto
{
    public object? Data { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public StatusCodes StatusCode { get; set; }= StatusCodes.OK;
}