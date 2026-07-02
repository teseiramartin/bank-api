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

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash,
            Role = RoleEnum.User
        };

        await _userRepository.AddAsync(user);

        return user.Id;
    }

    public async Task LoginAsync(LoginUserDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
            throw new InvalidOperationException("Usuario no encontrado.");

        var isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!isValidPassword)
            throw new InvalidOperationException("Credenciales inválidas.");
    }
}