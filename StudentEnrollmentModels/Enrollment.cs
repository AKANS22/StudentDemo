﻿using System.ComponentModel.DataAnnotations;

namespace StudentEnrollmentModels
{
    public class Enrollment : BaseModel
    {
        [Key]
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get;}

    }
}