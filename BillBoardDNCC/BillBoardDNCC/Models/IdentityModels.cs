using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BillBoardDNCC.Models
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
        public virtual DbSet<BillBoard> BillBoards { get; set; }
        public virtual DbSet<BillBoardSize> BillBoardSizes { get; set; }
        public virtual DbSet<BillBoardType> BillBoardTypes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<TaxRange> TaxRanges { get; set; }
        public virtual DbSet<ZoneAndWard> ZoneAndWards { get; set; }
        public virtual DbSet<RegionWiseTax> RegionWiseTaxs { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<BillboardAssigned> BillboardAssigneds { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<TransportPool> TransportPools { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<ZoneWardArea> ZoneWardAreas { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}