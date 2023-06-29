using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollmentModels;
using AutoMapper;
using StudentApi.DTOs.EnrollmentDTO;
using StudentApi.DTOs.CourseDTOs;
using Microsoft.PowerBI.Api.Models;

namespace StudentApi;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Enrollment").WithTags(nameof(Enrollment));

        group.MapGet("/", async (StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var enroll = await db.enrollments.ToListAsync();
            return mapper.Map<List<EnrollmentDTO>>(enroll);
            
        })
        .WithName("GetAllEnrollments")
        .WithOpenApi()
        .Produces<List<EnrollmentDTO>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int CourseId, StudentEnrollmentDbContext db, IMapper mapper) =>
        {

            return await db.enrollments.FindAsync(CourseId)
                is Enrollment model
                    ? Results.Ok(mapper.Map<EnrollmentDTO>(model))
                    : Results.NotFound();
            ;
        })
        .WithName("GetEnrollmentById")
        .Produces<List<EnrollmentDTO>>(StatusCodes.Status200OK).WithOpenApi()
        .WithOpenApi();
         

        group.MapPut("/{id}", async (int courseid, EnrollmentDTO enrollmentDTO, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var foundModel = await db.enrollments.FindAsync(courseid);
            if (foundModel is  null)
            {

                return Results.NotFound();
            }
            mapper.Map(enrollmentDTO, foundModel);
            await db.SaveChangesAsync();

            return Results.NoContent();


        })
        .WithName("UpdateEnrollment")
        .WithOpenApi()
        .Produces<List<EnrollmentDTO>>(StatusCodes.Status404NotFound);
        

        group.MapPost("/", async (EnrollmentPostDTO enrollmentDTO, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var enroll = mapper.Map<Enrollment>(enrollmentDTO);
            db.enrollments.Add(enroll);
            await db.SaveChangesAsync();
            return Results.Created($"Enrollment/{enrollmentDTO.CourseId}", enroll);
            
        })
        .WithName("CreateEnrollment")
        .WithOpenApi()
        .Produces<List<EnrollmentPostDTO>>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int CourseId, StudentEnrollmentDbContext db) =>
        {
            if (await db.enrollments.FindAsync(CourseId) is Enrollment enrollment)
            {
                db.enrollments.Remove(enrollment);
                await db.SaveChangesAsync();
                return Results.Ok(enrollment);

            }
            return Results.NotFound();
        });
       




    }
}
