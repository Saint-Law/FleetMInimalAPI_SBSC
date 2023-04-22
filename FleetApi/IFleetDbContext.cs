using Microsoft.EntityFrameworkCore;

namespace FleetApi
{
    public interface IFleetDbContext
    {
         DbSet<Driver> Drivers { get; set; }
         DbSet<Car> Cars { get; set; }
         DbSet<Trip> Trips { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
