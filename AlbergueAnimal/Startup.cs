using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlbergueAnimal.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AlbergueAnimal.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Rotativa.AspNetCore;

namespace AlbergueAnimal
{
    public class Startup//
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<Utilizador>(options =>
            //{
            //    // Password settings
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireLowercase = false;
            //})
            services.AddIdentity<Utilizador, IdentityRole>(options =>
            {
                options.User.AllowedUserNameCharacters = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+ ";
                options.SignIn.RequireConfirmedEmail = true;




            })
            .AddDefaultUI()
               .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IEmailSender, AlbergueAnimais.Areas.Identity.Services.EmailSender>();
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "350915215841-347mha3sfsf9t6rsgdje1idk0lfet09n.apps.googleusercontent.com";
                googleOptions.ClientSecret = "WLx9wLSq6XMyp9RcTB6um-ZF";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            RotativaConfiguration.Setup(env);
            CreateRolesAndAdminUser(serviceProvider);
        }






        private static void CreateRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            const string adminRoleName = "Administrator";
            string[] roleNames = { adminRoleName, "Manager", "Member" };

            foreach (string roleName in roleNames)
            {
                CreateRole(serviceProvider, roleName);
            }

            // Get these value from "appsettings.json" file.
            string adminUserEmail = "joaosilgo96@gmail.com";
            string adminPwd = "Jo@og0mes";
            string adminUserEmail2 = "150221016@estudantes.ips.pt";
            string adminPwd2 = "Bruno150221016!";
            string adminUserEmail3 = "inesessofias @gmail.com";
            string adminPwd3 = "Esw1234. - pass";
           
            //  string adminUserEmail = "joaosilgo96@gmail.com";
            //  string adminPwd = "Jo@og0mes";
            AddUserToRole(serviceProvider, adminUserEmail, adminPwd, adminRoleName);
            AddUserToRole(serviceProvider, adminUserEmail2, adminPwd2, adminRoleName);
            AddUserToRole(serviceProvider, adminUserEmail3, adminPwd3, adminRoleName);
            //  Utilizador user = await UserManager.FindByEmailAsync("joaosilgo96@gmail.com");
        }

        /// <summary>
        /// Create a role if not exists.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="roleName">Role Name</param>
        private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
            }
        }

        /// <summary>
        /// Add user to a role if the user exists, otherwise, create the user and adds him to the role.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="userEmail">User Email</param>
        /// <param name="userPwd">User Password. Used to create the user if not exists.</param>
        /// <param name="roleName">Role Name</param>
        private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail,
            string userPwd, string roleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Utilizador>>();

            Task<Utilizador> checkAppUser = userManager.FindByEmailAsync(userEmail);
            checkAppUser.Wait();


            Utilizador appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                Utilizador newAppUser = new Utilizador
                {
                  DBO=DateTime.Now,
                  Morada="ADMIN",
                  Genero="ADMIN",
                  Nome="AMIN",
                  
                    Email = userEmail,
                    UserName = userEmail,

                };
                newAppUser.EmailConfirmed = true;
                Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(newAppUser, userPwd);
                taskCreateAppUser.Wait();

                if (taskCreateAppUser.Result.Succeeded)
                {
                    appUser = newAppUser;
                }
            }

            Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
            newUserRole.Wait();
        }














        //    private async Task CreateRoles(IServiceProvider serviceProvider)
        //    {









        //        const string adminRoleName = "Administrator";
        //        string[] roleNames = { adminRoleName, "Manager", "Member" };

        //        foreach (string roleName in roleNames)
        //        {
        //            CreateRole(serviceProvider, roleName);
        //        }

        //        // Get these value from "appsettings.json" file.
        //        string adminUserEmail = "someone22@somewhere.com";
        //        string adminPwd = "_AStrongP1@ssword!";
        //        AddUserToRole(serviceProvider, adminUserEmail, adminPwd, adminRoleName);
        //    }

        //    /// <summary>
        //    /// Create a role if not exists.
        //    /// </summary>
        //    /// <param name="serviceProvider">Service Provider</param>
        //    /// <param name="roleName">Role Name</param>
        //    private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        //    {
        //        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //        Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
        //        roleExists.Wait();

        //        if (!roleExists.Result)
        //        {
        //            Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
        //            roleResult.Wait();
        //        }
        //    }

        //    /// <summary>
        //    /// Add user to a role if the user exists, otherwise, create the user and adds him to the role.
        //    /// </summary>
        //    /// <param name="serviceProvider">Service Provider</param>
        //    /// <param name="userEmail">User Email</param>
        //    /// <param name="userPwd">User Password. Used to create the user if not exists.</param>
        //    /// <param name="roleName">Role Name</param>
        //    private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail,
        //        string userPwd, string roleName)
        //    {
        //        var userManager = serviceProvider.GetRequiredService<UserManager<Utilizador>>();

        //        Task<Utilizador> checkAppUser = userManager.FindByEmailAsync(userEmail);
        //        checkAppUser.Wait();

        //        Utilizador appUser = checkAppUser.Result;

        //        if (checkAppUser.Result == null)
        //        {
        //            Utilizador newAppUser = new Utilizador
        //            {
        //                Email = userEmail,
        //                UserName = userEmail
        //            };

        //            Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(newAppUser, userPwd);
        //            taskCreateAppUser.Wait();

        //            if (taskCreateAppUser.Result.Succeeded)
        //            {
        //                appUser = newAppUser;
        //            }
        //        }

        //        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
        //        newUserRole.Wait();






















        //        //var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //        //var userManager = serviceProvider.GetRequiredService<UserManager<Utilizador>>();
        //        //Task<IdentityResult> roleResult;
        //        //string email = "joaosilgo96@gmail.com";

        //        ////Check that there is an Administrator role and create if not
        //        //Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Administrator");
        //        //hasAdminRole.Wait();

        //        //if (!hasAdminRole.Result)
        //        //{
        //        //    roleResult = roleManager.CreateAsync(new IdentityRole("Administrator"));
        //        //    roleResult.Wait();
        //        //}

        //        ////Check if the admin user exists and create it if not
        //        ////Add to the Administrator role

        //        //Task<Utilizador> testUser = userManager.FindByEmailAsync(email);
        //        //testUser.Wait();

        //        //if (testUser.Result == null)
        //        //{
        //        //    Utilizador administrator = new Utilizador();
        //        //    administrator.Email = email;
        //        //    administrator.UserName = email;

        //        //    Task<IdentityResult> newUser = userManager.CreateAsync(administrator, "_AStrongP@ssword!");
        //        //    newUser.Wait();

        //        //    if (newUser.Result.Succeeded)
        //        //    {
        //        //        Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Administrator");
        //        //        newUserRole.Wait();
        //        //    }
        //        //}








        //        //initializing custom roles   
        //        //var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //        //var UserManager = serviceProvider.GetRequiredService<UserManager<Utilizador>>();
        //        //string[] roleNames = { "Admin", "User", "HR" };
        //        //IdentityResult roleResult;

        //        //foreach (var roleName in roleNames)
        //        //{
        //        //    var roleExist = await RoleManager.RoleExistsAsync(roleName);
        //        //    if (!roleExist)
        //        //    {
        //        //        //create the roles and seed them to the database: Question 1  
        //        //        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
        //        //    }
        //        //}







        //        //var poweruser = new Utilizador
        //        //{

        //        //    UserName = Configuration["Joao"],
        //        //    Email = Configuration["joaosilgo96@gmail.com"],
        //        //};
        //        ////Ensure you have these values in your appsettings.json file
        //        //string userPWD = Configuration["Jo@og0mes"];

        //        //var _user = await UserManager.FindByEmailAsync("joaosilgo96@gmail.com");

        //        //if (_user == null)
        //        //{
        //        //    var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD.ToString());
        //        //    if (createPowerUser.Succeeded)
        //        //    {
        //        //        //here we tie the new user to the role
        //        //        await UserManager.AddToRoleAsync(poweruser, "Admin");

        //        //    }
        //        //}










        //        //Utilizador user = await UserManager.FindByEmailAsync("joaosilgo96@gmail.com");

        //        //if (user == null)
        //        //{
        //        //    user = new Utilizador()
        //        //    {
        //        //        UserName = "Joao",
        //        //        Email = "joaosilgo96@gmail.com",

        //        //        // SecurityStamp = Guid.NewGuid().ToString()
        //        //    };
        //        //    await UserManager.CreateAsync(user, "the.kratos000");
        //        //    await UserManager.AddToRoleAsync(user, "Admin");
        //        //}



        //        //    Utilizador user1 = await UserManager.FindByEmailAsync("joaosilgo96@gmail.com");

        //        //    if (user1 == null)
        //        //    {
        //        //        user1 = new Utilizador()
        //        //        {
        //        //            UserName = "joaosilgo96@gmail.com",
        //        //            Email = "joaosilgo96@gmail.com",
        //        //            SecurityStamp = Guid.NewGuid().ToString()
        //        //        };
        //        //        await UserManager.CreateAsync(user1, "Jo@og0mes");
        //        //    }
        //        //    await UserManager.AddToRoleAsync(user1, "User");

        //        //    Utilizador user2 = await UserManager.FindByEmailAsync("the.kratos000@gmail.com");

        //        //    if (user2 == null)
        //        //    {
        //        //        user2 = new Utilizador()
        //        //        {
        //        //            UserName = "150221016@estudantes.ips.pt",
        //        //            Email = "150221016@estudantes.ips.pt",
        //        //            SecurityStamp = Guid.NewGuid().ToString()
        //        //        };
        //        //        await UserManager.CreateAsync(user2, "150221016@Estudantes");
        //        //    }
        //        //    await UserManager.AddToRoleAsync(user2, "HR");

        //        //}



        //    }
        //}
    }
}