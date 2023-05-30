using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Pschool.API.Dto;
using Pschool.API.Enum;
using Pschool.API.Interfaces;
using PSchool.API.DAL.Entities;
using PSchool.API.DAL.Interfaces;
using System.Runtime.InteropServices;

namespace Pschool.API.Services
{
    public class MainService : IMainService

    {

        private readonly ITenantContext _appContext;
        private readonly IMapper _mapper;
        public MainService(ITenantContext appContext, IMapper mapper)
        {
            _appContext = appContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<UserDto>> GetUsersAsync(Role? role)
        {
            var user =  _appContext.Users.AsQueryable();
            if(role != null && role !=0)
            {
                user = user.Where(w => w.RoleId == (int)role).AsQueryable();
            }
            
           return await user.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int Id)
        {
            var user = await _appContext.Users.Where(w => w.UserId == Id).FirstOrDefaultAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(UserUpdateDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DateOfBirth = userDto.DateOfBirth,
                Email = userDto.Email,
                RoleId = (int)userDto.Role
            };
            _appContext.Users.Add(user);
            _appContext.BeginTransaction();

            if (userDto.Role == Role.Student)
            {
                if (userDto.ParentId != null && userDto.ParentId != 0 )
                {
                    _appContext.Commit();
                    _appContext.Relationships.Add(new Relationship { Id = user.UserId, ParentId = userDto.ParentId });
                }
                else
                {
                    _appContext.Rollback();
                    throw new Exception("Student should have Parent");
                }

            }
            else
            {
                _appContext.Commit();
                _appContext.Relationships.Add(new Relationship { Id = user.UserId, ParentId = null });
            }
            await _appContext.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> UpdateUserAsync(int userId, UserUpdateDto userDto)
        {
            var user = await _appContext.Users.Where(w => w.UserId == userId).FirstOrDefaultAsync();
            if (user != null) 
            {
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email; 
                user.DateOfBirth = userDto.DateOfBirth;
            }
            if(userDto.ParentId != null && userDto.ParentId != 0)
            {
               var relationship = await _appContext.Relationships.Where(w => w.Id == userId).FirstOrDefaultAsync();
               if (relationship != null) 
                { 
                    relationship.ParentId = userDto.ParentId;
                 
                }
            }
            await _appContext.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);

        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _appContext.Users.Where(w => w.UserId == userId).FirstOrDefaultAsync();
            if (user != null) 
            {
                try
                {
                    var relationship = await _appContext.Relationships.Where(w => w.Id == userId).FirstOrDefaultAsync();
                    _appContext.BeginTransaction();
                    _appContext.Relationships.Remove(relationship);
                    _appContext.Users.Remove(user);
                    _appContext.Commit();
                    await _appContext.SaveChangesAsync();
                }
                catch (Exception ex) 
                { 
                    _appContext.Rollback();
                }
            }
        }
    }
}
