using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreatDefulteUserAndAdmins();
        }
        public void CreatDefulteUserAndAdmins()
        {
            var rolemanger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usermanger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole Role = new IdentityRole();
            if (!rolemanger.RoleExists("adminnns"))
            {
                Role.Name = "adminnns";
                rolemanger.Create(Role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Josephhhhhhhhhhh";
                user.Email = "jo@yahoo.com";
                var check = usermanger.Create(user, "123456aA@");
                if (check.Succeeded)
                {
                    usermanger.AddToRole(user.Id, "adminnns");
                }

            }
        }
    }
}
