using AutoMapper;
using Azure.Identity;
using GymApi.Authentication;
using GymApi.Entities;
using GymApi.Exceptions;
using GymApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace GymApi.Services
{
    public class UserService : IUserService
    {
        private readonly GymDbContext _dbContext;
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IMembershipService _membershipService;

        public UserService(GymDbContext dbContext, ILogger<UserService> logger, IPasswordHasher passwordHasher, IMapper mapper, IMembershipService membershipService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _membershipService = membershipService;
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

            if (userdb is null)
                throw new NotFoundException("User not found");

            userdb.Name=user.Name;
            userdb.Email=user.Email;
            userdb.ContactNumber = user.ContactNumber;
            userdb.DateOfBirth = user.DateOfBirth;
            userdb.Role = user.Role;
            userdb.MembershipId = user.MembershipId;

            _dbContext.SaveChanges();
        }

        public int AddMembership(Membership membership, int id)
        {
            _membershipService.CreateMembership(membership);

            var userdb = _dbContext
              .Users
              .FirstOrDefault(u => u.Id == id);

            if (userdb is null)
                throw new NotFoundException("User not found");

            userdb.MembershipId = membership.Id;
            _dbContext.SaveChanges();
            return membership.Id;
        }     
        public void RemoveMembership(int userid)
        {
            var userdb = _dbContext
              .Users
              .FirstOrDefault(u => u.Id == userid);

            if (userdb is null)
                throw new NotFoundException("User not found");

            var membershipdb= _dbContext
                .MemberShips
                .FirstOrDefault(u => u.Id == userdb.MembershipId);
            _dbContext.SaveChanges();

        }
        public void UpdateMembership(int userid, Membership membership)
        {
            var userdb = _dbContext
             .Users
             .FirstOrDefault(u => u.Id == userid);

            if (userdb is null)
                throw new NotFoundException("User not found");

            _membershipService.UpdateMembership(userdb.MembershipId, membership);
        }

        public void PickPlan(int planid, int userid)
        {
            var userdb = _dbContext
            .Users
            .FirstOrDefault(u => u.Id == userid);

            if (userdb is null)
                throw new NotFoundException("User not found");

            var userMembershipId = userdb.MembershipId;

            _membershipService.SetMembershipPlan(planid, userMembershipId);
            _dbContext.SaveChanges(true);
        }

        public void AssignRole(int userId, int roleId) 
        { 
            var userdb = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);

            var roledb = _dbContext
               .Roles
               .FirstOrDefault(u => u.Id == roleId);

            if (userdb is null)
                throw new NotFoundException("user not found");
            if (roledb is null)
                throw new NotFoundException("Role not found");

            userdb.RoleId = roleId; 
            userdb.Role = roledb;
            _dbContext.SaveChanges();
        }

        public Membership GetUserMembership(int userId)
        {
            return _membershipService.GetById(userId);

        }

        public Plan GetUserPlan(int userId)
        {
            var userdb = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);

            if (userdb is null)
                throw new NotFoundException("user not found");

            var userMembershipId = userdb.MembershipId;

            var userMembershipDb = _dbContext
               .MemberShips
               .FirstOrDefault(u => u.Id == userMembershipId);

            if (userMembershipDb is null)
                throw new NotFoundException("membership not found");


            var userPlanId = userMembershipDb.PlanId;

            var userPlan= _dbContext
                .Plans
                .FirstOrDefault(p=>p.Id == userPlanId);

            if (userPlan is null)
                throw new NotFoundException("plan not found");

            return userPlan;
        }
    }
}
