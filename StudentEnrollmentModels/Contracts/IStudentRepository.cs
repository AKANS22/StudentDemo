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

