namespace RussianBathHouse.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RussianBathHouse.Data.Models;


    public class BathHouseDbContext : IdentityDbContext
    {
        public BathHouseDbContext(DbContextOptions<BathHouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cabin> Cabins { get; init; }

        public DbSet<Reservation> Reservations { get; init; }

        public DbSet<Service> Services { get; init; }

        public DbSet<Accessory> Accessories { get; init; }

        public DbSet<ServiceReservationListViewModel> ReservationServices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Reservation>()
                .HasOne(r => r.Cabin)
                .WithMany(c => c.Reservations)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ServiceReservationListViewModel>()
                .HasKey(rs => new { rs.ServiceId, rs.ReservationId });

            builder.Entity<ServiceReservationListViewModel>()
                .HasOne(rs => rs.Service)
                .WithMany(s => s.ReservationServices)
                .HasForeignKey(s => s.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ServiceReservationListViewModel>()
                .HasOne(rs => rs.Reservation)
                .WithMany(r => r.ReservationServices)
                .HasForeignKey(r => r.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
