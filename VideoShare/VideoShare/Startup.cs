using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using VideoShare.Models;

[assembly: OwinStartupAttribute(typeof(VideoShare.Startup))]
namespace VideoShare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                if (!RoleManager.RoleExists("Admin"))
                {
                    var roleresult = RoleManager.Create(new IdentityRole("Admin"));
                }

                if (!RoleManager.RoleExists("User"))
                {
                    var roleresult = RoleManager.Create(new IdentityRole("User"));
                }

                var adminUser = UserManager.FindByEmail("Admin@email.com");

                if (adminUser == null)
                {
                    var admin = new ApplicationUser();
                    admin.UserName = "Admin@email.com";
                    admin.Email = "Admin@email.com";
                    UserManager.Create(admin, "P@ssw0rd");
                    UserManager.AddToRole(admin.Id, "Admin");
                }
            }

            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
