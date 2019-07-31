using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCoreAngularDemo.Core.Models;
using DotNetCoreAngularDemo.Core;
using DotNetCoreAngularDemo.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetCoreAngularDemo.Persistance;

namespace DotNetCoreAngularDemo.Controllers
{
    public class MastersController : Controller
    {
        private readonly DotNetCoreDbContext dbContext;
        private readonly IMapper mapper;

        public MastersController(DotNetCoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet("/api/master/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await dbContext.Makes.Include(m => m.Models).ToListAsync();
            var mappedData = mapper.Map<List<Make>, List<MakeResource>>(makes);
            return mappedData;
        }

        [HttpGet("/api/master/features")]
        public async Task<IEnumerable<Feature>> GetFeatures()
        {
            return await dbContext.Features.ToListAsync();
        }
    }
}