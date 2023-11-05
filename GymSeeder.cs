using GymApi.Entities;

namespace GymApi
{
    public class GymSeeder
    {
        private readonly GymDbContext _db;
        public GymSeeder(GymDbContext db)
        {
                _db = db;
        }
        public void Seed(WebApplication app)
        {
            if(_db.Database.CanConnect())
            {
                if(_db.Roles.Any())
                {
                    var roles = GetRoles();
                    _db.Roles.AddRange(roles);
                    _db.SaveChanges();
                }

                /*if(!_db.Users.Any())
                {
                    var users = GetUsers();
                    _db.Users.AddRange(users);
                    _db.SaveChanges();
                }*/
            }
        }

        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                //true=female
                new User()
                {
                    Name = "Jane",
                    LastName = "Smith",
                    Email = "jane@example.com",
                    PasswordHash = "Hash456",
                    ContactNumber = "+1 987-654-3210",
                    Gender = true,
                    DateOfBirth = new DateTime(1985, 8, 25),
                    Role=new Role()
                    {
                        Name = "Customer",
                        Description="xdxd"
                    },
                   

                },
                 new User()
                {
                    Name = "Bob",
                    LastName = "Johnson",
                    Email = "bob@example.com",
                    PasswordHash = "Hash789",
                    ContactNumber = "+1 555-123-4567",
                    Gender = false,
                    DateOfBirth = new DateTime(1995, 3, 10),
                    Role=new Role()
                    {
                        Name = "Customer",
                        Description="xdxd"
                    },
                  
                    

                },
                 new User() 
                 {
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    PasswordHash = "Hash123",
                    ContactNumber = "+1 123-456-7890",
                    Gender = false,
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Role=new Role()
                    {
                        Name = "Customer",
                        Description="xdxd"
                    },
                   
                 }
            };

            return users;
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name="Admin",
                    Description="admin"
                },
                new Role()
                {
                    Name="Trainer",
                    Description = "xx"
                },
                new Role()
                {
                    Name="User",
                    Description = "xd"
                }
            };
            return roles;
        }
    }
}
