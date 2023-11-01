using AutoMapper.Configuration.Conventions;
using GymApi.Entities;
using GymApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GymApi.Services
{
    public class MembershipService:IMembershipService
    {
        private readonly GymDbContext _db;

        public MembershipService(GymDbContext db)
        {

            _db = db;
        }
        public int CreateMembership(Membership membership)
        {
            _db.MemberShips.Add(membership);
            _db.SaveChanges();
            return membership.Id;
        }

        public Membership GetById(int id)
        {
            var membership = _db
                .MemberShips
                .FirstOrDefault(x => x.Id == id);

            if (membership == null)
            {
                throw new NotFoundException("Membership not found");
            }

            return membership;
        }

        public IEnumerable<Membership> GetAll()
        {
            var memberships = _db
                .MemberShips
                .ToList();

            return memberships;
        }

        public void DeleteMembership(int id)
        {
            var membership = _db
                .MemberShips
                .FirstOrDefault(u => u.Id == id);

            if (membership is null)
                throw new NotFoundException("Membership not found");

            _db.MemberShips.Remove(membership);
            _db.SaveChanges();
        }

        public void UpdateMembership(int id, Membership membership)
        {
            var membershipdb = _db
              .MemberShips
              .FirstOrDefault(u => u.Id == id);

            if (membershipdb is null)
                throw new NotFoundException("Membership not found");

            membershipdb.StartDate = membership.StartDate;
            membershipdb.EndDate = membership.EndDate;
            membershipdb.Status  = membership.Status;
            _db.SaveChanges();
        }
    }
}
