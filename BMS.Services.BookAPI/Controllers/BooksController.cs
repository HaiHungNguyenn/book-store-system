using BMS.Services.BookAPI.Data;
using BMS.Services.BookAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StatusCodes = BMS.Services.BookAPI.Enums.StatusCodes;

namespace BMS.Services.BookAPI.Controllers;
[Route("/api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly ResponseDto _response;

    public BooksController(AppDbContext db)
    {
        _db = db;
        _response = new ResponseDto();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get()
    {
        var books = _db.Books.ToList();
        return Ok(new ResponseDto
        {
            Data = books,
        });
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public IActionResult GetById(int id)
    {
        var book = _db.Books.FirstOrDefault(b => b.BookId == id) ?? throw new NotFoundEntityException("Book not found.");
        return Ok(new ResponseDto
        {
            Data = book,
        });
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Post(Book request)
    {
        if (_db.Books.Any(b => b.BookId == request.BookId))
        {
            throw new DuplicateEntityException("Book ID already exists.");
        }
        await _db.Books.AddAsync(request);
        await _db.SaveChangesAsync();
        return StatusCode((int)StatusCodes.Created, new ResponseDto
        {
            StatusCode = StatusCodes.Created,
        });
    }
    
    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Put(Book request)
    {
        var existBook = _db.Books.FirstOrDefault(b => b.BookId == request.BookId) 
                        ?? throw new NotFoundEntityException("Book ID not found.");;
        existBook.Author = request.Author;
        existBook.Description = request.Description;
        existBook.Genre = request.Genre;
        existBook.Price = request.Price;
        existBook.Title = request.Title;
        existBook.PublishYear = request.PublishYear;
        _db.Books.Update(existBook);
        await _db.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = _db.Books.FirstOrDefault(b => b.BookId == id) ?? throw new NotFoundEntityException("Book ID not found.");
        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
public class DuplicateEntityException : Exception
{
    public DuplicateEntityException(string message) : base(message) { }
}
public class NotFoundEntityException : Exception
{
    public NotFoundEntityException(string message) : base(message) { }
}