using DotNetCoreAngularDemo.Core;
using DotNetCoreAngularDemo.Core.Models;
using DotNetCoreAngularDemo.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Persistance
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DotNetCoreDbContext context;
        public VehicleRepository(DotNetCoreDbContext context)
        {
            this.context = context;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            QueryResult<Vehicle> result = new QueryResult<Vehicle>();

            var query = context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                    .Include(v => v.Model)
                    .ThenInclude(vm => vm.Make)
                    .AsQueryable();

            if (queryObj.MakeId.HasValue && queryObj.MakeId!=0)
                query = query.Where(x => x.Model.MakeId == queryObj.MakeId);

            var sortColumnMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v=>v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
            };

            query = query.ApplySorting(queryObj, sortColumnMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();
            return result;
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRealted = true)
        {
            if (!includeRealted)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                    .Include(v => v.Model)
                    .ThenInclude(vm => vm.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }
    }
}
