using Airfare.Models;
using System.Data.Entity;

namespace Airfare.DataContext
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base(DBConnection.getConnection().ConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataBaseContext, Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<FlightModel> Flights { get; set; }
        public DbSet<FlightHotelModel> FlightHotels { get; set; }
        public DbSet<HotelModel> Hotels { get; set; }
        public DbSet<HotelRoomModel> HotelsRooms { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<SeasonModel> Seasons { get; set; }
        public DbSet<HostModel> Hosts { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
        public DbSet<PhoneModel> Phones { get; set; }
        public DbSet<CompanyPaymentModel> CompanyPayments { get; set; }
        public DbSet<SpotModel> Spots { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<CompanyContractModel> CompanyContracts { get; set; }
        public DbSet<EnvironmentModel> Environments { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
