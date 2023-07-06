using Microsoft.EntityFrameworkCore;
using StudentEnrollmentModels.Contracts;
using StudentEnrollmentModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollmentModels.Repositories

{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(StudentEnrollmentDbContext db) : base(db)
        {
        }

        public async Task<Course> GetCourseLink(int courseId)
        {
            var course = await _db.courses
                .Include(q => q.enrollments).ThenInclude(q => q.Student)
                .FirstOrDefaultAsync(q => q.Id == courseId);

            return course;

        }
    }
}
