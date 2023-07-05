using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollmentModels
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course
                {
                    CourseId = 1,
                    Title = "Minimal Api Developement",
                    Credit = 3,
                    //IsCertified = false

                },
                new Course
                {
                    CourseId = 2,
                    Title = "second Minimal Api Developement",
                    Credit = 4,
                    //IsCertified = false

                }); 


        }
    }

}
