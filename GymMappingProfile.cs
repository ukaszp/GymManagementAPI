using AutoMapper;
using GymApi.Entities;
using GymApi.Models;

namespace GymApi
{
    public class GymMappingProfile:Profile
    {
        public GymMappingProfile()
        {
            CreateMap<User, CreateUserDto>();
        }
    }
}
