using System.ComponentModel.DataAnnotations;

namespace StudentEnrollmentModels
{
    public abstract class BaseModel
    {
        
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.MinValue;
        public string createdBy { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get;} = DateTime.MinValue;
        public string updatedBy { get; set; } = string.Empty;

    }
}