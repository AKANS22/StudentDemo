using AutoMapper;
using StudentApi.DTOs.CourseDTOs;
using StudentApi.DTOs.EnrollmentDTO;
using StudentApi.DTOs.StudentDTO;
using StudentEnrollmentModels;

namespace StudentApi.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap <Course, CreditCourseDTO>().ReverseMap();
            
            CreateMap<Enrollment, EnrollmentDTO>().ReverseMap();
            CreateMap<Enrollment, EnrollmentPostDTO>().ReverseMap();
            
            CreateMap<Student, StudentDTO> ().ReverseMap();
            
        }
    }
}
