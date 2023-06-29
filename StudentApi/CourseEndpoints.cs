using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollmentModels;
using StudentApi.DTOs.CourseDTOs;
using AutoMapper;

namespace StudentApi;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Course").WithTags(nameof(Course));

        group.MapGet("/", async (StudentEnrollmentDbContext db, IMapper mapper) =>
        {
          
            var courses = await db.courses.ToListAsync();
            return mapper.Map<List<CourseDTO>>(courses);
        })
        .WithName("GetAllCourses")
        .WithOpenApi()
        .Produces<List<CourseDTO>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async  (int CourseId, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
             return await db.courses.FindAsync(CourseId)
                is Course model
                    ? Results.Ok(mapper.Map<CourseDTO> (model))
                    : Results.NotFound();
            
        })
        .WithName("GetCourseById")
        .Produces<List<CourseDTO>>(StatusCodes.Status200OK) .WithOpenApi()
        .WithOpenApi();

        group.MapPut("/{id}", async (int courseid, CourseDTO courseDTO, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var foundModel = await db.courses.FindAsync(courseid);
            if (foundModel is null)
            { 
            
                return Results.NotFound();
            }
            mapper.Map(courseDTO, foundModel);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCourse")
        .WithOpenApi();

        group.MapPost("/", async (CreditCourseDTO courseDTO, StudentEnrollmentDbContext db, IMapper mapper) =>
        {

            var course = mapper.Map<Course>(courseDTO);
            db.courses.Add(course);
            await db.SaveChangesAsync();
            return Results.Created($"Course/{course.CourseId}", course);
        })
        .WithName("CreateCourse")
        .WithOpenApi()
        .Produces(StatusCodes.Status204NoContent);

        group.MapDelete("/{id}", async  (int CourseId, StudentEnrollmentDbContext db) =>
        {
            if( await db.courses.FindAsync(CourseId) is Course course) 
            {
                db.courses.Remove(course);
                await db.SaveChangesAsync();
                return Results.Ok(course);
            
            }
            return Results.NotFound();
        })
        .WithName("DeleteCourse")
        .WithOpenApi();
    }
}
