
using Microsoft.AspNetCore.Identity;

namespace SakilaApp.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Administrador", "Empleado", "Operador", "Consulta" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var users = new (string Email, string Role)[]
        {
            ("admin@espe.edu.ec", "Administrador"),
            ("empleado@espe.edu.ec", "Empleado"),
            ("operador@espe.edu.ec", "Operador"),
            ("consulta@espe.edu.ec", "Consulta")
        };

        foreach (var u in users)
        {
            var user = await userManager.FindByEmailAsync(u.Email);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = u.Email,
                    Email = u.Email,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "Admin123*");
            }

            if (!await userManager.IsInRoleAsync(user, u.Role))
            {
                await userManager.AddToRoleAsync(user, u.Role);
            }
        }
    }
}