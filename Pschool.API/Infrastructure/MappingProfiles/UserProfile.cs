using AutoMapper;
using Pschool.API.Dto;
using Pschool.API.Enum;
using PSchool.API.DAL.Entities;
using System.Runtime;

namespace Pschool.API.Infrastructure.MappingProfiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ForMember(w => w.Role, e => e.MapFrom(s => (Role)s.RoleId))
                .ForMember(w => w.ParentId, e => e.MapFrom(s => s.Relationship.Id))
                .ReverseMap();
            
        }
    }
}
