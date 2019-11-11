using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.DAL.Data;
using ASP.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace ASP.Services
{
    static class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager)
            // создать БД, если она еще не создана
        {
            context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole {Name = "admin", NormalizedName = "admin"};
                    // создать роль manager
                    var result = await roleManager.CreateAsync(roleAdmin);
            }

            // проверка наличия пользователей
            if (!context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new ApplicationUser {Email = "user@mail.ru", UserName = "user@mail.ru"};
                await userManager.CreateAsync(user, "123456");
                // создать пользователя admin@mail.ru
                var admin = new ApplicationUser {Email = "admin@mail.ru", UserName = "admin@mail.ru"};
                await userManager.CreateAsync(admin, "123456");
                // назначить роль admin
                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");

            }
            if (!context.BootsGroups.Any())
            {
                context.BootsGroups.AddRange
                (new List<BootsGroup>
                    {
                    new BootsGroup { GroupName="LeatherBoots"},
                    new BootsGroup {GroupName="Ugg"},
                    new BootsGroup { GroupName="Sneakers"},
                    new BootsGroup {GroupName="Slippers"},
                    new BootsGroup {GroupName="Galoshes"},
                    new BootsGroup {GroupName="Macasiness"}
                    }
                );
                await context.SaveChangesAsync();
            }
            // проверка наличия объектов
            if (!context.Bootses.Any())
            {
                context.Bootses.AddRange
                (new List<Boots>
                    {
                        new Boots
                        {
                            BootsName = "Estella", Description = "for woman ",
                            Size = 280, BootsGroupId = 1, Image = "9816213-1.jpg"
                        },
                        new Boots
                        {
                            BootsName = "Adidas", Description = "sport",
                            Size = 315, BootsGroupId = 3, Image = "snikkers.jpg"
                        },
                        new Boots
                        {
                            BootsName = "DED", Description = "My predok",
                            Size = 340, BootsGroupId = 5, Image = "galosesh.jpg"
                        },
                        new Boots
                        { 
                            BootsName = "House", Description = "comfortable slippers",
                            Size = 275, BootsGroupId = 4, Image = "slippers.jpg"
                        },
                        new Boots
                        {
                             BootsName = "CARLABEI", Description = "africa style",
                            Size = 285, BootsGroupId = 6, Image = "macasins.jpg"
                        }
                    }
                );
                await context.SaveChangesAsync();
            }

        } 
    
    }
    
}

