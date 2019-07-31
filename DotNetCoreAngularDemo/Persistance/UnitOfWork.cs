using DotNetCoreAngularDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        DotNetCoreDbContext context;
        public UnitOfWork(DotNetCoreDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
