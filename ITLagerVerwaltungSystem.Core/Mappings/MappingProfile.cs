using AutoMapper;
using ITLagerVerwaltungSystem.Core.Domain;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Material <-> MaterialIssueResponseDto
            CreateMap<Material, MaterialIssueResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // MaterialIssueRequestDto <-> Material
            CreateMap<MaterialIssueRequestDto, Material>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MaterialId));

            // Order <-> OrderResponseDto
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.MaterialIds, opt => opt.MapFrom(src => (src.Materials != null) ? src.Materials.Select(m => m.Id).ToList() : new List<int>()));

            // OrderRequestDto <-> Order
            CreateMap<OrderRequestDto, Order>()
                .ForMember(dest => dest.Materials, opt => opt.Ignore()); // Materials will be mapped manually
        }
    }
}
