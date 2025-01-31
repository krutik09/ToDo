using AngularAuthAPI.DTO;
using AngularAuthAPI.Models;
using AutoMapper;

namespace AngularAuthAPI.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TodoReqDto,Todo>().ReverseMap();
            CreateMap<List<TodoReqDto>,List<Todo>>().ReverseMap();
        }
    }
}
