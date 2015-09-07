using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SermonAudioOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SermonAudioOrganizer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            var context = new ApplicationDbContext();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var applicationUser = context.Users.FirstOrDefault(user => user.UserName == "nalter@gmail.com");
            if (applicationUser == null)
            {
                applicationUser = new ApplicationUser() { UserName = "nalter@gmail.com" };
                userManager.Create(applicationUser);
            }

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            if (roleManager.FindByName("admin") == null)
            {
                roleManager.Create(new IdentityRole("admin"));
            }

            if (!userManager.IsInRole(applicationUser.Id, "admin"))
                userManager.AddToRole(applicationUser.Id, "admin");

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
