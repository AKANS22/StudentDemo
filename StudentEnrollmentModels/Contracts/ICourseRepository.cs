using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollmentModels.Contracts
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course> GetCourseLink(int courseId);
    }
}
