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
        public int AddMembership(Membership membership, int id);
        public void RemoveMembership(int userid);
        public void UpdateMembership(int userid, Membership membership);
        public void PickPlan(int planid, int userid);
        public void AssignRole(int userId, int roleId);
        public Membership GetUserMembership(int userId);
        public Plan GetUserPlan(int userId);




    }
}