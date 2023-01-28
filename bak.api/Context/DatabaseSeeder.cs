using bak.api.Models;

namespace bak.api.Context
{
    internal class DatabaseSeeder
    {
        internal static async void SeedDatabase(ApplicationDbContext applicationDbContext)
        {
            var users = applicationDbContext.Users.ToList();

            if(users.Any())
            {
                return;
            }

            var usersToAdd = new List<User>();

            var admin = new User
            {
                Username = "admin",
                Password = "admin",
                Role = Role.Admin,
            };

            var user = new User
            {
                Username = "user",
                Password = "user",
                Role = Role.User
            };

            var manager = new User
            {
                Username = "manager",
                Password = "manager",
                Role = Role.User
            };

            usersToAdd.Add(admin);
            usersToAdd.Add(user);
            usersToAdd.Add(manager);

            await applicationDbContext.AddRangeAsync(usersToAdd);
        }
    }
}
