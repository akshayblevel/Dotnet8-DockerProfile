using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Dotnet8_DockerProfile
{
    public class VehicleDb : DbContext
    {
        public VehicleDb(DbContextOptions<VehicleDb> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
