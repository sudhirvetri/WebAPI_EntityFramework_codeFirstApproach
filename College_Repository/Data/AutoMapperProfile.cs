using AutoMapper;

namespace College_Repository.Data
{
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Student,StudentDTO>();
    }        
}
}
