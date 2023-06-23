using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollmentModels;
namespace StudentApi;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Enrollment").WithTags(nameof(Enrollment));

        group.MapGet("/", async (StudentEnrollmentDbContext db) =>
        {
            return await db.enrollments.ToListAsync();
        })
        .WithName("GetAllEnrollments")
        .WithOpenApi()
        .Produces<List<Enrollment>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async  (int courseid, StudentEnrollmentDbContext db) =>
        {
            return await db.enrollments.AsNoTracking()
                .FirstOrDefaultAsync(model => model.CourseId == courseid)
                is Enrollment model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetEnrollmentById")
        .WithOpenApi()
        .Produces<Enrollment>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async  (int courseid, Enrollment enrollment, StudentEnrollmentDbContext db) =>
        {
            var affected = await db.enrollments
                .Where(model => model.CourseId == courseid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.CourseId, enrollment.CourseId)
                  .SetProperty(m => m.StudentId, enrollment.StudentId)
                  .SetProperty(m => m.CreatedDate, enrollment.CreatedDate)
                  .SetProperty(m => m.createdBy, enrollment.createdBy)
                  .SetProperty(m => m.updatedBy, enrollment.updatedBy)
                );

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateEnrollment")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (Enrollment enrollment, StudentEnrollmentDbContext db) =>
        {
            db.enrollments.Add(enrollment);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Enrollment/{enrollment.CourseId}",enrollment);
        })
        .WithName("CreateEnrollment")
        .WithOpenApi()
        .Produces<Enrollment>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async  (int courseid, StudentEnrollmentDbContext db) =>
        {
            var affected = await db.enrollments
                .Where(model => model.CourseId == courseid)
                .ExecuteDeleteAsync();

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteEnrollment")
        .WithOpenApi()
        .Produces<Enrollment>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
