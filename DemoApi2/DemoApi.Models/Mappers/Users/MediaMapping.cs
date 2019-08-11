using AutoMapper;
using DemoApi.Database.DatabaseContext;
using DemoApi.Models.Medias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Mappers.Users
{
    public class MediaMapping : Profile
    {
        public MediaMapping()
        {
            CreateMap<Medium,MediaModel >();
            CreateMap<MediaModel, Medium>();

        }
    }
}
