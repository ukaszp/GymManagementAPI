﻿using GymApi.Entities;
using GymApi.Exceptions;

namespace GymApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly GymDbContext _dbContext;

        public RoleService(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Role GetById(int id)
        {
            var role = _dbContext
                .Roles
                .FirstOrDefault(x => x.Id == id);

            if (role == null)
            {
                throw new NotFoundException("Role not found");
            }

            return role;
        }

        public IEnumerable<Role> GetAll()
        {
            var roles = _dbContext
                .Roles
                .ToList();

            return roles;
        }

        public int CreateRole(Role role)
        {
            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();

            return role.Id;
        }

        public void DeleteRole(int id)
        {
            var role = _dbContext
                .Roles
                .FirstOrDefault(u => u.Id == id);

            if (role is null)
                throw new NotFoundException("Role not found");

            _dbContext.Roles.Remove(role);
            _dbContext.SaveChanges();
        }

        public void UpdateRole(int id, Role role)
        {
            var roledb = _dbContext
              .Roles
              .FirstOrDefault(u => u.Id == id);

            if (role is null)
                throw new NotFoundException("Role not found");

            roledb.Name = role.Name;
            roledb.Description = role.Description;

            _dbContext.SaveChanges();
        }
    }
}

