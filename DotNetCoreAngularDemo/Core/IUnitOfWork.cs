﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
