using System;
using System.Collections.Generic;
using System.Text;
using UESAN.SHOPPING.CORE.Core.DTOs;
using UESAN.SHOPPING.CORE.Core.Entities;
using UESAN.SHOPPING.CORE.Core.Interfaces;

namespace UESAN.SHOPPING.CORE.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> SignIn(string email, string password)
        {
            var user = await _userRepository.SignIn(email, password);
            var token = ""; // Aquí deberías generar un token JWT o similar para autenticación

            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            };

        }

        public async Task<int> Signup(UserCreateDTO userCreateDTO)
        {
            var user = new User
            {
                FirstName = userCreateDTO.FirstName,
                LastName = userCreateDTO.LastName,
                DateOfBirth = userCreateDTO.DateOfBirth,
                Email = userCreateDTO.Email,
                Password = userCreateDTO.Password, // En producción, asegúrate de hashear la contraseña
                Country = userCreateDTO.Country,
                Address = userCreateDTO.Address,
                Type = userCreateDTO.Type,
                IsActive = true//Valor por defecto para indicar que el usuario está activo
            };
            return await _userRepository.Signup(user);
        }
    }
}
