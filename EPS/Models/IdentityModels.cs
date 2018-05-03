using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EPS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<EPS.Models.Birthday> Birthdays { get; set; } //Remove

        public System.Data.Entity.DbSet<EPS.Models.Blog> Blogs { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.Post> Posts { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.Location> Locations { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.LocationImage> LocationImages { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.MessageReply> MessageReplies { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.PostImage> PostImages { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.Batch> Batches { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.CorpMemberRole> CorperRoles { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.CorpMember> CorpMembers { get; set; }

        public System.Data.Entity.DbSet<EPS.Models.CorpMemberImage> CorpMemberImages { get; set; } //Remove This!
    }
}