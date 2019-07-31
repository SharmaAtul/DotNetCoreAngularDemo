using DotNetCoreAngularDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public KeyValuePairResource Model { get; set; }
        public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        public ContactResource Contact { get; set; }
        public ICollection<KeyValuePairResource> Features { get; set; }
        public DateTime LastUpdateOn { get; set; }

        public VehicleResource()
        {
            this.Features = new Collection<KeyValuePairResource>();
        }
    }
}
