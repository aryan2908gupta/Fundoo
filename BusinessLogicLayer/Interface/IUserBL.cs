using System;
using System.Collections.Generic;
using System.Text;
using ModelLayer.Dto;

namespace BusinessLogicLayer.Interface
{
   public interface IUserBL
    {

        Task<UserResponseDto> LoginUserAsync(string email, string password);
        Task<UserResponseDto> RegisterUserAsync(UserSignUpRequestDto userRequestDto);
        Task<UserResponseDto> UpdateUserAsync(int UserId, UserSignUpRequestDto userRequestDto);


        Task<bool> ChangePasswordAsync(int UserId, string oldPassword, string newPassword);
        Task<bool> ForgetPasswordAsync(string email, string newPassword);

        Task<UserResponseDto> GetUserByIdAsync(int UserId);
    }
}
