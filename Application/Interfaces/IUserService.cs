using Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    Task<Guid> RegisterUserAsync(RegisterUserDto dto);
    
    Task LoginAsync(LoginUserDto dto);
}