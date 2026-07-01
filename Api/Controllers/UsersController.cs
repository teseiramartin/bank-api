using Application.DTOs;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<RegisterUserDto> _validator;
    
    public UsersController(IUserService userService, IValidator<RegisterUserDto> validator)
    {
        _userService = userService;
        _validator = validator; 
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
    {
        try
        {
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.ToDictionary());
            }
            var userId = await _userService.RegisterUserAsync(dto);
            return Ok(new { UserId = userId });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}