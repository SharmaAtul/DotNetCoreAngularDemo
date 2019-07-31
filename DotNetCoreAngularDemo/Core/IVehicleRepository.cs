using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreAngularDemo.Core.Models;

namespace DotNetCoreAngularDemo.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);

        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);

        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
    }
}