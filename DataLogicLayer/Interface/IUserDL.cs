using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLogicLayer.Interface
{
    public interface IUserDL
    {
        Task<User> CreateUserAsync(User user) ;
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email, int? excludeUserId = null);

        Task<User> UpdateUserAsync(User user) ;
        Task<User> GetUserByIdAsync(int UserId);

        Task<bool> DeleteUserAsync(int UserId);

        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}

/*
 Why we are returning User in Task

 var createdUser = await repo.CreateUserAsync(user);
Console.WriteLine(createdUser.UserId); // got id after insert

 */