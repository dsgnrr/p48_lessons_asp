using AutoMapper;
using p48_lessons_asp.DTO.Category;

namespace p48_lessons_asp.DTO;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Data.Entity.Category, CategoryDto>();
    }
}