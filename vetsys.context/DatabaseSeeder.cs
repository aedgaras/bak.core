using vetsys.entities.Enums;
using vetsys.entities.Models;

namespace vetsys.context;

public class DatabaseSeeder
{
    public static async void SeedDatabase(ApplicationDbContext applicationDbContext)
    {
        var users = applicationDbContext.Users.ToList();

        if (users.Any()) return;

        var usersToAdd = new List<User>();

        var admin = new User
        {
            Username = "admin",
            Password = "admin",
            Role = Role.Admin,
            Classification = Classification.Veterinarian
        };

        var user = new User
        {
            Username = "user",
            Password = "user",
            Role = Role.User,
            Classification = Classification.Customer
        };


        if (applicationDbContext.Users.FirstOrDefault(x => x.Username == admin.Username) == null) usersToAdd.Add(admin);

        if (applicationDbContext.Users.FirstOrDefault(x => x.Username == user.Username) == null) usersToAdd.Add(user);

        await applicationDbContext.AddRangeAsync(usersToAdd);
        await applicationDbContext.SaveChangesAsync();
    }
}