using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCoreAngularDemo.Core;
using DotNetCoreAngularDemo.Core.Models;
using DotNetCoreAngularDemo.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DotNetCoreAngularDemo.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository vehicleRespository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;

        PhotosController(IHostingEnvironment host, IVehicleRepository vehicleRespository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options) {
            this.host = host;
            this.vehicleRespository = vehicleRespository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.photoSettings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(int vehicleId, IFormFile file)
        {
            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > photoSettings.MaxSize) return BadRequest("Max file size exceeded.");

            var extension = Path.GetExtension(file.FileName);

            if(!photoSettings.IsSupported(extension)) return BadRequest("Invalid file type.");

            var vehicle = await vehicleRespository.GetVehicle(vehicleId, includeRelated: false);

            if (vehicle == null) return NotFound();

            var fileUploadPath = Path.Combine(this.host.WebRootPath,"uploads");

            if (!Directory.Exists(fileUploadPath))
                Directory.CreateDirectory(fileUploadPath);

            var newFileName = Guid.NewGuid().ToString() + extension;

            var filePath = Path.Combine(fileUploadPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = newFileName };

            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            var photoResource = mapper.Map<Photo, PhotoResource>(photo);

            return Ok(photoResource);
        }
    }
}