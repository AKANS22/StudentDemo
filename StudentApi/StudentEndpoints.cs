using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollmentModels;
namespace StudentApi;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", async (StudentEnrollmentDbContext db) =>
        {
            return await db.students.ToListAsync();
        })
        .WithName("GetAllStudents")
        .WithOpenApi()
        .Produces<List<Student>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async  (string studentid, StudentEnrollmentDbContext db) =>
        {
            return await db.students.AsNoTracking()
                .FirstOrDefaultAsync(model => model.StudentId == studentid)
                is Student model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetStudentById")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async  (string studentid, Student student, StudentEnrollmentDbContext db) =>
        {
            var affected = await db.students
                .Where(model => model.StudentId == studentid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.FirstName, student.FirstName)
                  .SetProperty(m => m.LastName, student.LastName)
                  .SetProperty(m => m.DateOfBirth, student.DateOfBirth)
                  .SetProperty(m => m.StudentId, student.StudentId)
                  .SetProperty(m => m.Picture, student.Picture)
                  .SetProperty(m => m.CreatedDate, student.CreatedDate)
                  .SetProperty(m => m.createdBy, student.createdBy)
                  .SetProperty(m => m.updatedBy, student.updatedBy)
                );

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateStudent")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (Student student, StudentEnrollmentDbContext db) =>
        {
            db.students.Add(student);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Student/{student.StudentId}",student);
        })
        .WithName("CreateStudent")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async  (string studentid, StudentEnrollmentDbContext db) =>
        {
            var affected = await db.students
                .Where(model => model.StudentId == studentid)
                .ExecuteDeleteAsync();

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteStudent")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
