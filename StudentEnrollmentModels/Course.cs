using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentEnrollmentModels
{
    public class Course: BaseModel
    {
        
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credit { get; set; }
        public List<Enrollment> enrollments { get; set; } = new List<Enrollment>();
        //[Required]
      //  public bool IsCertified { get; set; }

    }
}