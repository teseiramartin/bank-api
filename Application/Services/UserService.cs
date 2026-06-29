using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Guid> RegisterUserAsync(RegisterUserDto dto)
    {
        var exists = await _userRepository.EmailExistsAsync(dto.Email);

        if (exists)
            throw new InvalidOperationException("El email ya está registrado.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = dto.Password, // TODO: Hash the password before storing it
            Role = RoleEnum.User
        };

        await _userRepository.AddAsync(user);

        return user.Id;
    }
}