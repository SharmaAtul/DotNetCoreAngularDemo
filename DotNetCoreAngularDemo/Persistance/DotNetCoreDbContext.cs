using DotNetCoreAngularDemo.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Persistance
{
    public class DotNetCoreDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public DotNetCoreDbContext(DbContextOptions<DotNetCoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<VehicleFeature>().HasKey(
                vf => new {
                    vf.FeatureId,
                    vf.VehicleId
                });
        }
    }
}
