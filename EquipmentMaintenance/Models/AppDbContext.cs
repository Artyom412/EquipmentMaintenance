using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Equipment> Equipments => Set<Equipment>();
        public DbSet<JobTitle> JobTitles => Set<JobTitle>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Material> Materials => Set<Material>();
        public DbSet<Statement> Statements => Set<Statement>();
        public DbSet<Subdivision> Subdivisions => Set<Subdivision>();
        public DbSet<UnitOfMeasurement> Units => Set<UnitOfMeasurement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
