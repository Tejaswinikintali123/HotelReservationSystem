using HotelReservationSystemModels;

using Microsoft.EntityFrameworkCore;
using System;

namespace HotelBookingDBLayer
{
    public class BookingDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
      
        public DbSet<BookingDetails> BookingDetailsofRoom{get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelReservationSystem;Integrated Security=True;Connect Timeout=30;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.ID)
                .HasName("PK_Rooms");
                entity.Property(x => x.ID).ValueGeneratedOnAdd();



            });
         
            modelBuilder.Entity<BookingDetails>(entity =>
            {
                entity.HasKey(e => e.ID)
                .HasName("PK_BookingDetailsofRoom");
                entity.Property(x => x.ID).ValueGeneratedOnAdd();

            });



        }
    }
}
