using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using recru_it.Data;
using Microsoft.AspNetCore.Identity;
using recru_it.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace recru_it.Extensions
{
    public static class DbSeeder
    {

        public static async Task<IdentityResult> Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                IdentityResult result = new IdentityResult();
                /*bool hasUsers = context.Users.Count() > 0;
                if (hasUsers)
                {
                    DeleteAll(context);   // DB has been seeded and should be deleted
                    return result;
                }*/

                var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


                await CreateAllRoles(_roleManager);

                //Notice: Since this is async it can cause problems to the other functions, since they require the user, and those might not have been made yet.
                //Aka not thread safe
                result = await CreateAllUsers(_userManager);

                CreateAllTags(context);
                CreateAllCompanies(context);
                CreateAllProfiles(context);
                CreateAllJobPosts(context);
                CreateAllJobApplications(context);

                return result;
            }
        }

        public static async Task<IdentityResult> CreateAllUsers(UserManager<ApplicationUser> userManager)
        {
            IdentityResult result = new IdentityResult();


            for (var i = 0; i <= 10; i++)
            {
                var username = "User"+i;
                var email = username + "@gmail.com";

                ApplicationUser user = new ApplicationUser { UserName = username, Email = email };

                var password = username;
                result = await userManager.CreateAsync(user, "VeryLongPassWord1!"+i);
            }

            return result;


        }


        /* Remember to create roles first before assigning them to roles*/
        public static async Task<IdentityResult> CreateAllRoles(RoleManager<IdentityRole> roleManager)
        {
            IdentityResult result = new IdentityResult();
            for (var i = 0; i <= 10; i++)
            {
                var role = new IdentityRole();
                role.Name = "admin "+i;
                result = await roleManager.CreateAsync(role);
            }
            return result;
        }

        public static void CreateAllJobApplications(ApplicationDbContext dbContext)
        {
            Random rnd = new Random();
            int j = 1;
            for (var i = 0; i <= 10; i++)
            {
                j = rnd.Next(dbContext.Tags.Count())+1;
                dbContext.JobApplications.Add(new JobApplication
                {
                    CreatedAt = DateTime.Now.AddMinutes(i),
                    CreatedBy = "" + i,
                    Description = "" + i,
                    Tags = dbContext.Tags.Take(j).ToList(),
                    Title = "" + i,
                    UpdatedAt = DateTime.Now.AddMinutes(i),
                    User = dbContext.Users.FirstOrDefault()

                });

            }
            dbContext.SaveChanges();

        }
        /* Remember to create user and tags before doing this*/
        public static void CreateAllJobPosts(ApplicationDbContext dbContext)
        {
            Random rnd = new Random();
            int j = 1;
            for (var i = 0; i <= 10; i++)
            {
                j = rnd.Next(dbContext.Tags.Count())+1;
                dbContext.JobPosts.Add(new JobPost
                {
                    CreatedAt = DateTime.Now.AddMinutes(i),
                    CreatedBy = DateTime.Now.AddMinutes(i),
                    Description = "" + i,
                    Tags = dbContext.Tags.Take(j).ToList(),
                    Title = "" + i,
                    UpdatedAt = DateTime.Now.AddMinutes(i),
                    User = dbContext.Users.FirstOrDefault()

                });
            }
            dbContext.SaveChanges();

        }

        public static void CreateAllProfiles(ApplicationDbContext dbContext)
        {
            for (var i = 0; i <= 10; i++)
            {
                dbContext.Profiles.Add(new Profile
                {
                    Age = i,
                    FirstName = "name " + i,
                    Gender = "Male ",
                    LastName = "lastname " + i,
                    User = dbContext.Users.ToArray()[i]
                    

                });
            }
            dbContext.SaveChanges();

        }

        public static void CreateAllCompanies(ApplicationDbContext dbContext)
        {
            for (var i = 0; i <= 10; i++)
            {
                dbContext.Companies.Add(new Company
                {
                    
                    CVR = "CVR " + i,
                    Name = "Name " + i,
                    TypeOfCompany = "company type " + i,
                    User = dbContext.Users.ToArray()[i]

                });
            }
            dbContext.SaveChanges();

        }

        public static void CreateAllTags(ApplicationDbContext dbContext)
        {
            for (var i = 0; i <= 10; i++)
            {
                dbContext.Tags.Add(new Tag
                {
                    Descriptipon = "desc " + i,
                    Title = "title " + i

                });
            }
            dbContext.SaveChanges();

        }

        public static void DeleteAll(ApplicationDbContext dbContext)
        {
            var result = dbContext.Database.ExecuteSqlCommand("DELETE FROM [AspNetUserTokens]");
            /*
             * 
             * DELETE ALL 
             * 
             * 
             * DELETE FROM [dbo].[AspNetUserTokens]
                GO
                DELETE FROM [dbo].[Companies]
                GO
                DELETE FROM [dbo].[AspNetUserClaims]
                GO
                DELETE FROM [dbo].[AspNetUserLogins]
                GO
                DELETE FROM [dbo].[AspNetRoleClaims]
                GO
                DELETE FROM [dbo].[AspNetUserRoles]
                GO
                DELETE FROM [dbo].[AspNetUsers]
                GO
                DELETE FROM [dbo].[JobPosts]
                GO
                DELETE FROM [dbo].[JobApplications]
                GO
                DELETE FROM [dbo].[Tags]
                GO
                DELETE FROM [dbo].[Profiles]
                GO
                DELETE FROM [dbo].[AspNetRoles]
                */

        }

    }
}
