using StudentEnrollmentModels;

namespace StudentApi.DTOs.EnrollmentDTO
{
    public class EnrollmentPostDTO
    {
        // for students

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Picture { get; set; }

        // for course
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credit { get; set; }
    }
}
