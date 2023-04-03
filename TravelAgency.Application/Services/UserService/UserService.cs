﻿using Microsoft.AspNetCore.Identity;
using TravelAgency.Application.Models.User;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Repositories;

namespace TravelAgency.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService()
        {
            _passwordHasher = new PasswordHasher<User>();
            _userRepository = new UserRepository();
        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            var users = await _userRepository.GetAll();

            if(users.FirstOrDefault(u => u.Login ==  registerUserDto.Login) != null) 
                throw new Exception("User with given login already exists");

            var newUser = new User()
            {
                Login = registerUserDto.Login
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, registerUserDto.Password);

            await _userRepository.Add(newUser);
        }
    }
}
