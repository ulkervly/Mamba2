using AutoMapper;
using Mamba2.DTOs;
using Mamba2.Entities;

namespace MyBiz.MappingProfiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<EmployeeCreateDto, Employee>().ReverseMap();
            CreateMap<EmployeeGetDto, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();
            CreateMap<PositionCreateDto, Position>().ReverseMap();
            CreateMap<PositionGetDto, Position>().ReverseMap();
            CreateMap<PositionUpdateDto, Position>().ReverseMap();
        }
    }
}
