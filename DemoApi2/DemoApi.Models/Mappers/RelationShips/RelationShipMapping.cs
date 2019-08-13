using AutoMapper;
using DemoApi.Common.Enums;
using DemoApi.Database.DatabaseContext;
using DemoApi.Models.RelationShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Mappers.RelationShips
{
    public class RelationShipMapping : Profile
    {
        public RelationShipMapping()
        {
            CreateMap<AddRelationShipModel, RelationShip>();
            CreateMap<UpdateRelationShipModel, RelationShip>()
                .ForMember(dst => dst.Status, opt => opt.MapFrom(s => (int)s.Status));
            CreateMap<RelationShip, ViewRelationShipModel>();
                 //.ForMember(dst => dst., opt => opt.Ignore());
            //   //.ForMember(dst => dst.Status, opt => opt.MapFrom(s => (RelationShipStatus?)s.Status));
            CreateMap<ViewRelationShipModel, RelationShip>();


        }
    }
}
