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

            CreateMap<DocumentType, DocumentTypeDTO>().ReverseMap();
            CreateMap<DocumentTypeCreationDTO, DocumentType>();

            CreateMap<HomeAppliance, HomeApplianceDTO>().ReverseMap();
            CreateMap<HomeApplianceCreationDTO, HomeAppliance>();

            CreateMap<HomeAppliancePhoto, HomeAppliancePhotoDTO>().ReverseMap();
            CreateMap<HomeAppliancePhotoCreationDTO, HomeAppliancePhoto>();

            CreateMap<HomeApplianceType, HomeApplianceTypeDTO>().ReverseMap();
            CreateMap<HomeApplianceTypeCreationDTO, HomeApplianceType>();
        }
    }
}
