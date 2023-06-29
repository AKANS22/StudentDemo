using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollmentModels;
using AutoMapper;
using StudentApi.DTOs.StudentDTO;
using StudentApi.DTOs.CourseDTOs;

namespace StudentApi;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", async (StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var students = await db.students.ToListAsync();
            return mapper.Map<List<StudentDTO>>(students);

        })
        .WithName("GetAllStudents")
        .WithOpenApi()
        .Produces<List<StudentDTO>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (string studentid, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            return await db.students.FindAsync(studentid)
             is Student model
                    ? Results.Ok(mapper.Map<StudentDTO>(model))
                     : Results.NotFound();
        })
        .WithName("GetStudentById")
        .WithOpenApi()
        .Produces<List<StudentDTO>>(StatusCodes.Status200OK);
        

        group.MapPut("/{id}", async (string studentid, StudentDTO student, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var foundModel = await db.courses.FindAsync(studentid);
            if (foundModel is null)
            {

                return Results.NotFound();
            }
            mapper.Map(student, foundModel);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateStudent")
        .WithOpenApi()
        
        .Produces<List<StudentDTO>>(StatusCodes.Status204NoContent);

        group.MapPost("/", async (StudentDTO studentDTO, StudentEnrollmentDbContext db, IMapper mapper) =>
        {

            var student = mapper.Map<Student>(studentDTO);
            db.students.Add(student);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Student/{student.StudentId}", student);
        })
        .WithName("CreateStudent")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (string studentid, StudentEnrollmentDbContext db) =>
        {
            if (await db.students.FindAsync(studentid) is Student student)
            {
                db.students.Remove(student);
                await db.SaveChangesAsync();
                return Results.Ok(student);

            }
            return Results.NotFound();
        });

    }
}
