using GymApi.Entities;

namespace GymApi.Services
{
    public interface IRoleService
    {
        int CreateRole(Role role);
        void DeleteRole(int id);
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        void UpdateRole(int id, Role role);
    }
}