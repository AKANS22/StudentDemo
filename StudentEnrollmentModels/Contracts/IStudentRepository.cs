using Microsoft.EntityFrameworkCore;
using StudentEnrollmentModels.Contracts;
using StudentEnrollmentModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollmentModels.Contracts
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student> GetStudentDetails(string studentId);
    }
}

namespace StudentEnrollmentModels.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentEnrollmentDbContext db) : base(db)
        {
        }

        public async Task<Student> GetStudentDetails(string studentId)
        {
            var student = await _db.students
                .Include(x => x.Enrollments).ThenInclude(q => q.Course)
                .FirstOrDefaultAsync(c => c.StudentId == studentId);

            return student;

        }
    }
}