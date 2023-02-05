using AutoMapper;
using bak.api.Dtos;
using bak.api.Enums;
using bak.api.Extensions;
using bak.api.Models;

namespace bak.api.Configurations;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<User, UserDto>()
            .ForMember(dto => dto.Role,
                opt => opt.MapFrom(model => model.Role.GetDescription()))
            .ForMember(dto => dto.Classification,
                opt => opt.MapFrom(model => model.Classification.GetDescription()));
        CreateMap<UserDto, User>()
            .ForMember(model => model.Role,
                opt => opt.MapFrom(dto => (Role)Enum.Parse(typeof(Role), dto.Role, true)))
            .ForMember(model => model.Classification,
                opt => opt.MapFrom(dto =>
                    (Classification)Enum.Parse(typeof(Classification), dto.Classification, true)));
        
        CreateMap<Case, CaseDto>()
            .ForMember(dto => dto.Status,
                opt => opt.MapFrom(c => c.Status.GetDescription()));
        CreateMap<CaseDto, Case>()
            .ForMember(model => model.Status,
                opt => opt.MapFrom(dto => (CaseStatus)Enum.Parse(typeof(CaseStatus), dto.Status, true)));
    }
}