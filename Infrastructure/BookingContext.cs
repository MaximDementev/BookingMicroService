using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BookingContext : DbContext 
    {
        public DbSet<Booking> Bookings {  get; set; }

        public DbSet<ETicket> ETickets { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            base.OnModelCreating(modelBuilder);
        }


    }

    public class BookingConfiguration : EntityTypeConfiguration<Booking>
    {

        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x.Booking => x.Id);
            builder.HasOne(x => x.Booking)
                .WithMany(x => x.ETickets)
                .WithOne(x => x.Booking);
        }
        
    }
}
