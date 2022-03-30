using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BachHoaMint.Models;


[assembly: OwinStartupAttribute(typeof(BachHoaMint.Startup))]
namespace BachHoaMint
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateRoleUser();
        }
        private void CreateRoleUser()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if(!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                role = new IdentityRole();
                role.Name = "Master";
                roleManager.Create(role);
                //tạo các user
                var user = new ApplicationUser();
                user.UserName = "admin@qlbh.com";
                user.Email = "admin@qlbh.com";
                var check = userManger.Create(user, "123456");
                if(check.Succeeded)
                    userManger.AddToRole(user.Id, "Admin");

                user = new ApplicationUser();
                user.UserName = "master@qlbh.com";
                user.Email = "master@qlbh.com";
                check = userManger.Create(user, "123456");
                if (check.Succeeded)
                    userManger.AddToRole(user.Id, "Master");

                user = new ApplicationUser();
                user.UserName = "master2@qlbh.com";
                user.Email = "master2@qlbh.com";
                check = userManger.Create(user, "123456");
                if (check.Succeeded)
                    userManger.AddToRole(user.Id, "Master");
            }
        }
    }
}
