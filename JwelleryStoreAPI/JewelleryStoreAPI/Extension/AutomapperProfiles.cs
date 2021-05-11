using AutoMapper;
using JewelleryStore.DataAccess.Domain.Models;
using JewelleryStoreAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryStoreAPI.Extension
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<JewelsViewModel, Jewel>().ReverseMap();
        }
    }
}
