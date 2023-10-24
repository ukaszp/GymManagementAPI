using AutoMapper;
using GymApi.Authentication;
using GymApi.Entities;
using GymApi.Exceptions;
using GymApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace GymApi.Services
{
    public class UserService : IUserService
    {
        private readonly GymDbContext _dbContext;
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(GymDbContext dbContext, ILogger<UserService> logger, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public User GetById(int id)
        {

            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
                throw new NotFoundException("User not found");

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            var users = _dbContext
                .Users
                .ToList();

            return users;
        }

        public void CreateUser(CreateUserDto dto)
        {
            var passwordHash=_passwordHasher.Hash(dto.Password);
            var newUser = new User()
            { 
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                ContactNumber = dto.ContactNumber,
                PasswordHash = passwordHash,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _logger.LogWarning($"User with id: {id} DELETE action invoked");

            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User not found");

            _dbContext .Users.Remove(user);
            _dbContext.SaveChanges ();
        }

        public void UpdateUser(int id, User user)
        {
            var userdb = _dbContext
              .Users
              .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User not found");

            userdb.Name=user.Name;
            userdb.Email=user.Email;
            userdb.ContactNumber = user.ContactNumber;
            userdb.DateOfBirth = user.DateOfBirth;
            userdb.Role = user.Role;
            userdb.MembershipId = user.MembershipId;

            _dbContext.SaveChanges();
        }

    }
}
