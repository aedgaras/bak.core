using bak.api.Models;

namespace bak.api.Context
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext context;
        public DatabaseSeeder(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async void SeedDatabase()
        {
            var users = context.Users.ToList();

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

            await context.AddRangeAsync(usersToAdd);
            await context.SaveChangesAsync();
            await context.DisposeAsync();
        }
    }
}
