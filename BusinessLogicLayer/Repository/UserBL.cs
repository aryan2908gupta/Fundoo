using AutoMapper;
using BusinessLogicLayer.Interface;
using DataLogicLayer.Interface;
using DataLogicLayer.Repository;
using ModelLayer.Dto;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using BCrypt;

namespace BusinessLogicLayer.Repository
{
    public class UserBL : IUserBL
    {
        private readonly IMapper mapper;
        private readonly IUserDL userDL;

        public UserBL(IMapper mapper, IUserDL userDL)
        {
            this.mapper = mapper;
            this.userDL = userDL;
        }

        public async Task<UserResponseDto> LoginUserAsync(string email, string password)
        {
            var userExist = await  userDL.GetUserByEmailAsync(email);
            if (userExist == null)
            {
                throw new Exception("User not Found");
            }

            // Now we will verify Hash Password
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, userExist.Password);


            if (!isValidPassword)
                throw new Exception("Invalid Password");


            return mapper.Map<UserResponseDto>(userExist);
        }
        public async Task<UserResponseDto> RegisterUserAsync(UserSignUpRequestDto userRequestDto)
        {
            bool emailExists = await userDL.EmailExistsAsync(userRequestDto.Email);
            if (emailExists) { 
                throw new Exception($"{userRequestDto.Email} mail already exists");
            }
            // map dto -> entity
            var createdUser = mapper.Map<User>(userRequestDto);
            createdUser.Password = BCrypt.Net.BCrypt.HashPassword(userRequestDto.Password);
            await userDL.CreateUserAsync(createdUser);

           

            // map entity -> response dto
            return mapper.Map<UserResponseDto>(createdUser);

        }

        public async Task<UserResponseDto> UpdateUserAsync(int UserId, UserSignUpRequestDto userRequestDto)
        {
            var userExists = await userDL.GetUserByIdAsync(UserId);
            if (userExists == null)
            {
                throw new Exception("User Doesn't Exists");
            }

            // Now checking mail exist or not already
            bool emailExists = await userDL.EmailExistsAsync(userRequestDto.Email, UserId);
            if (emailExists)
                throw new Exception("Email already exists for another user");


            // Update
            // update properties
            userExists.FirstName = userRequestDto.FirstName;
            userExists.LastName = userRequestDto.LastName;
            userExists.Email = userRequestDto.Email;


            userExists.ChangedAt = DateTime.UtcNow;

            var updatedUser = await userDL.UpdateUserAsync(userExists);

            return mapper.Map<UserResponseDto>(updatedUser);

        }
        // Change Password
        public async Task<bool> ChangePasswordAsync(int UserId, string oldPassword, string newPassword)
        {
            var user = await userDL.GetUserByIdAsync(UserId);

            if (user == null)
                throw new Exception("User not found");

            bool isOldValid = BCrypt.Net.BCrypt.Verify(oldPassword, user.Password);
            if (!isOldValid)
                throw new Exception("Old password is incorrect");


            //  Hashing a  new password
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            user.ChangedAt = DateTime.UtcNow;

            await userDL.UpdateUserAsync(user);
            return true;
        }
        // Forget Password

        public async Task<bool> ForgetPasswordAsync(string email, string newPassword)
        {
            var user = await userDL.GetUserByEmailAsync(email);

            if (user == null)
                throw new Exception("User not found");

            //  Hash new password
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            user.ChangedAt = DateTime.UtcNow;


            await userDL.UpdateUserAsync(user);
            return true;
        }

        // GET USER BY ID
        public async Task<UserResponseDto> GetUserByIdAsync(int UserId)
        {
            var user = await userDL.GetUserByIdAsync(UserId);

            if (user == null) 
                throw new Exception("User not found");

            return mapper.Map<UserResponseDto>(user);
        }

    }
    }
