using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentModels;

var builder = WebApplication.CreateBuilder(args);

var con = builder.Configuration.GetConnectionString("StudentDbConnections");
builder.Services.AddDbContext<StudentEnrollmentDbContext>(options =>
{
    options.UseSqlServer(con);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapGet("/courses", async (StudentEnrollmentDbContext context) =>
{
    await context.courses.ToListAsync();
});


app.MapGet("/courses{id}", async (StudentEnrollmentDbContext context, int id) =>
{
   return await context.courses.FindAsync(id) is Course course ? Results.Ok(course) : Results.NotFound();

});

app.MapPost("/courses", async (StudentEnrollmentDbContext context, Course course) =>
{
    await context.AddAsync(course);
    await context.SaveChangesAsync();

    return Results.Created($"/courses/{course.CourseId}", course);

});

app.MapPut("/courses{id}", async (StudentEnrollmentDbContext context, Course course, int id) =>
{
    var recordExist = await context.courses.AnyAsync( q=> q.CourseId == course.CourseId);
    if (!recordExist) return Results.NotFound();


    context.Update(course);
    await context.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("/courses{id}", async (StudentEnrollmentDbContext context, int id) =>
{
    var record = await context.courses.FindAsync(id);
    if (record == null) return Results.NotFound();
    {
        context.Remove(record);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }

});




app.Run();
