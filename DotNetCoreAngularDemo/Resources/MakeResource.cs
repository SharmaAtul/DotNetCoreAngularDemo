using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; }

        public MakeResource()
        {
            this.Models = new Collection<KeyValuePairResource>();
        }
    }
}
