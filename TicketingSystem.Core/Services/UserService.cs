using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TicketingSystem.Core.DTOs;
using TicketingSystem.Core.Helpers;
using TicketingSystem.Core.JWT;
using TicketingSystem.Database.Entities;
using TicketingSystem.Database.Repositories;

namespace TicketingSystem.Core.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtFactory _jwtFactory;
        private readonly UserConverter _userConverter;

        public UserService(UserRepository repository, SignInManager<User> signInManager, UserManager<User> userManager, JwtFactory jwtFactory, UserConverter userConverter)
        {
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _userConverter = userConverter;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public void Save(dynamic context)
        {
            _repository.Save(context);
        }

        public List<UserDto> GetAll()
        {
            return _userConverter.ModelsToDtos(_repository.GetAll());
        }

        public UserDto Authenticate(string userName, string password)
        {
            var result = SignInAsync(userName, password);

            if (!result.Result.Succeeded)
            {
                return null;
            }

            UserDto user = this.GetByName(userName);

            if (user == null)
            {
                return null;
            }

            string[] roles = GetUserRoles(user).Result;

            var token = _jwtFactory.GenerateJwtToken(userName, user.Id, roles);
            user.Token = token;

            user.Password = null;

            return user;
        }

        private async Task<string[]> GetUserRoles(UserDto userDto)
        {
            User user = await _userManager.FindByIdAsync(userId: userDto.Id);
            List<string> roles = await _userManager.GetRolesAsync(user) as List<string>;

            if (roles == null || !roles.Any())
            {
                return null;
            }

            return roles.ToArray();
        }

        private async Task<SignInResult> SignInAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
            return result;
        }

        public UserDto GetById(string id)
        {
            return _userConverter.ModelToDto(_repository.GetById(id));
        }

        public UserDto GetByName(string name)
        {
            return _userConverter.ModelToDto(_repository.GetByName(name));
        }

        public dynamic Add(UserDto item)
        {
            User user = MapperHelper<User, UserDto>.ConvertToModel(item);
            var output = _repository.Add(user);

            output["Output"] = MapperHelper<User, UserDto>.ConvertToDto(output["Output"]);
            return output;
        }

        public dynamic Edit(UserDto item)
        {
            User user = MapperHelper<User, UserDto>.ConvertToModel(item);
            UserDto userDto = MapperHelper<User, UserDto>.ConvertToDto(_repository.Edit(user));
            return userDto;
        }
    }
}
