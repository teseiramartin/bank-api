using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<bool> EmailExistsAsync(string email);

    Task<User> AddAsync(User user);
}