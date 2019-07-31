using AutoMapper;
using DotNetCoreAngularDemo.Core.Models;
using DotNetCoreAngularDemo.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Mapping
{
    public class SimpleMapping : Profile
    {
        public SimpleMapping()
        {
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Make, MakeResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Photo, PhotoResource>();

            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(x => x.Contact, opt => opt.MapFrom(v => new ContactResource { Email = v.ContactEmail, Phone = v.ContactPhone, Name = v.ContactName }))
                .ForMember(x => x.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(v=>v.Make , opt=>opt.MapFrom(v=> new KeyValuePairResource { Id = v.Model.Make.Id, Name = v.Model.Make.Name }))
                .ForMember(v => v.Model, opt => opt.MapFrom(v => new KeyValuePairResource { Id = v.Model.Id, Name = v.Model.Name }))
                .ForMember(x => x.Contact, opt => opt.MapFrom(v => new ContactResource { Email = v.ContactEmail, Phone = v.ContactPhone, Name = v.ContactName }))
                .ForMember(x => x.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource { Id = vf.Feature.Id, Name=vf.Feature.Name })));


            CreateMap<VehicleQueryResource, VehicleQuery>();
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v=>v.Id,opt=>opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr,v)=> 
                {
                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                    foreach (var f in removedFeatures)
                        v.Features.Remove(f);

                    var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id }).ToList();
                    foreach(var f in addedFeatures)
                        v.Features.Add(f);
                });
        }
    }
}
