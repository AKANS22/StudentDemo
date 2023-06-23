using System.ComponentModel.DataAnnotations;

namespace StudentEnrollmentModels
{
    public class Course: BaseModel
    {
        
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credit { get; set; }

    }
}