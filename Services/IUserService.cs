using GymApi.Entities;
using GymApi.Models;

namespace GymApi.Services
{
    public interface IUserService
    {
        void CreateUser(CreateUserDto dto);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void DeleteUser(int id);
        void UpdateUser(int id, User user);
    }
}