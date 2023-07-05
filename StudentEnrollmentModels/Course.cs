using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentEnrollmentModels
{
    public class Course: BaseModel
    {
        
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credit { get; set; }
        //[Required]
      //  public bool IsCertified { get; set; }

    }
}