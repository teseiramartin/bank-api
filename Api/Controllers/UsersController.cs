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
    private readonly IValidator<LoginUserDto> _loginValidator;
    
    public UsersController(IUserService userService, IValidator<RegisterUserDto> validator, IValidator<LoginUserDto> loginValidator)
    {
        _userService = userService;
        _validator = validator; 
        _loginValidator = loginValidator;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
    {
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }
        var userId = await _userService.RegisterUserAsync(dto);
        return Ok(new { UserId = userId });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto dto)
    {
        var result = await _loginValidator.ValidateAsync(dto);
        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }
        await _userService.LoginAsync(dto);

        return Ok();
    }
}