namespace StudentEnrollmentModels
{
    public class Student: BaseModel 
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StudentId { get; set; } 
        public string Picture { get; set; }

    }
}