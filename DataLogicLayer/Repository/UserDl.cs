using System;
using System.Collections.Generic;
using System.Text;
using DataLogicLayer.Data;
using DataLogicLayer.Interface;
using ModelLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataLogicLayer.Repository
{
    public class UserDl : IUserDL
    {
        private readonly ApplicationDbContext context;
        public UserDl(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Creating User
        public async Task<User> CreateUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        // Email Checking
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(x=>x.Email==email);
        }

        // Get User By id
        public async Task<User> GetUserByIdAsync(int UserId)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeUserId = null)
        {
            return await context.Users.AnyAsync(u => u.Email == email && (excludeUserId == null || u.UserId != excludeUserId));
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            user.ChangedAt = DateTime.Now;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int UserId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
            if (user == null)
            {
                return false;
            }
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await context.Users.ToListAsync();
        }


    }
}