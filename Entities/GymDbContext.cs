using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GymApi.Entities
{
    public class GymDbContext:DbContext
    {
        readonly string _connectrionString=
            "Server = (localdb)\\MyDataBase; Database=GymDb;Trusted_Connection=True";


        public DbSet<Membership> MemberShips { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .Property(u => u.Name)
             .IsRequired()
             .HasMaxLength(25);

            modelBuilder.Entity<User>()
             .Property(u => u.LastName)
             .IsRequired()
             .HasMaxLength(25);

            modelBuilder.Entity<User>()
             .Property(u => u.Email)
             .IsRequired()
             .HasMaxLength(25);

            modelBuilder.Entity<Role>()
             .Property(u => u.Name)
             .IsRequired()
             .HasMaxLength(25);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectrionString);
        }
    }
}
