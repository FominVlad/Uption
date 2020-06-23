using Microsoft.EntityFrameworkCore;
using Uption.Models;

namespace Uption
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Action> Actions { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<IpLocation> IpLocations { get; set; }
        public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<Models.Action>().HasKey(a => a.Id).HasName("PK_Actions");
            modelBuilder.Entity<ActionType>().HasKey(at => at.Id).HasName("PK_ActionTypes");
            modelBuilder.Entity<Message>().HasKey(m => m.Id).HasName("PK_Messages");
            modelBuilder.Entity<IpLocation>().HasKey(il => il.Ip).HasName("PK_IpLocations");

            // Configure foreign keys
            modelBuilder.Entity<Models.Action>().HasOne(at => at.ActionType)
                                                .WithMany(a => a.Actions)
                                                .HasForeignKey(at => at.ActionTypeId)
                                                .HasConstraintName("FK_Actions_ActionTypes");

            // Configure initial data
            modelBuilder.Entity<ActionType>().HasData(
                new ActionType[]
                {
                    new ActionType { Id = 1, Type = "PAGE_LOAD" },
                    new ActionType { Id = 2, Type = "EMAIL_SENT" }
                });
        }
    }
}
