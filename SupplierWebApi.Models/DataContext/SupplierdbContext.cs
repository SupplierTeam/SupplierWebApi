using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierWebApi.Models.DataContext
{
    public class SupplierdbContext : DbContext
    {
        public SupplierdbContext(DbContextOptions<SupplierdbContext> options)
      : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("user");

                entity.Property(e => e.UserName).HasMaxLength(32);

                entity.Property(e => e.Password).HasMaxLength(32);

                entity.Property(e => e.IsDelete).HasMaxLength(32);

                entity.Property(e => e.Email).HasMaxLength(32);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.TelPhone).HasMaxLength(32);

                entity.Property(e => e.AddTime).HasColumnType("datetime");
            });


        }
    }


}
