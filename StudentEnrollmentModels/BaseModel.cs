namespace StudentEnrollmentModels
{
    public abstract class BaseModel
    {
        int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? UpdatedDate { get;}
        public string updatedBy { get; set; }

    }

    public class Student: BaseModel 
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StudentId { get; set; } = string.Empty;

    }
}