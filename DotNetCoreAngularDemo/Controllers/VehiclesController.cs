using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreAngularDemo.Resources;
using AutoMapper;
using DotNetCoreAngularDemo.Core.Models;
using DotNetCoreAngularDemo.Core;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreAngularDemo.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;

        public IUnitOfWork unitOfwork { get; }

        public VehiclesController(IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork unitOfwork) {
            this.mapper = mapper;
            this.vehicleRepository = vehicleRepository;
            this.unitOfwork = unitOfwork;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles(VehicleQueryResource filterResource)
        {
            var filter = mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
            var vehicles = await vehicleRepository.GetVehicles(filter);
            var result = mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(vehicles);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody]SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdatedOn = DateTime.Now;
            vehicleRepository.AddVehicle(vehicle);
            await unitOfwork.CompleteAsync();

            vehicle = await vehicleRepository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await vehicleRepository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdatedOn = DateTime.Now;
            await unitOfwork.CompleteAsync();

            vehicle = await vehicleRepository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id,includeRelated:false);

            if (vehicle == null)
                return NotFound();

            vehicleRepository.RemoveVehicle(vehicle);
            await unitOfwork.CompleteAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}