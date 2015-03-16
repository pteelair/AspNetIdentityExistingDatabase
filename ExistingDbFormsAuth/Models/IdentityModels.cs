using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ExistingDbFormsAuth.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class MyUser : IdentityUser
    //public class MyUser : IdentityUser<string, MyUserLogin, MyUserRole, MyUserClaim>, IUser, IUser<string>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MyUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<MyUser>
    {
        public ApplicationDbContext()
            : base("ExistingDbConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<MyUser>().ToTable("User");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        }
    }

    public class MyPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return password;
        }
        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            //return base.VerifyHashedPassword(hashedPassword, providedPassword);
            return hashedPassword.Equals(HashPassword(providedPassword)) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }    
}