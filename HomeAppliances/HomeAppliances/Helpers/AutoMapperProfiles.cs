using AutoMapper;
using HomeAppliances.DTOs;
using HomeAppliances.Entities;

namespace HomeAppliances.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<BrandCreationDTO, Brand>();
        }
    }
}
