using AutoMapper;

namespace College_Repository.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Name))
                .AddTransform<string>(n => string.IsNullOrEmpty(n) ? "No Data found" : n)
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Email) ? "Email not found" : src.Email));


            // Add reverse mapping
            CreateMap<StudentDTO, Student>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StudentName));

        }

    }
}
