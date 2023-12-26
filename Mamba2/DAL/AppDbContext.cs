using Mamba2.Configuration;
using Mamba2.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mamba2.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet <EmployeePosition> EmployeePositions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var datas=ChangeTracker.Entries<BaseEntity>();
            foreach (var item in datas)
            {
                var entity=item.Entity;
                switch (item.State)
                {
                   
                    case EntityState.Modified:
                        entity.UpdatedDate = DateTime.Now;
                        break;
                    case EntityState.Added:
                        entity.AddedDate = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChanges();
        }
    }
}
