using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            string readerRoleId = "c3102863-3148-46fc-927c-ebc3c677ddd4";
            string writerRoleId = "a99cb378-7f11-4ff0-98e7-bc68f5207bbc";

            var roles = new List<IdentityRole>{
            new IdentityRole{Id=readerRoleId,ConcurrencyStamp=readerRoleId,Name="Reader",NormalizedName="Reader".ToUpper()},
            new IdentityRole{Id=writerRoleId,ConcurrencyStamp=writerRoleId,Name="Writer",NormalizedName="Writer".ToUpper()},
            };


            builder.Entity<IdentityRole>().HasData(roles);

        }

    }
}
