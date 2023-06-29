using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentModels;
using StudentApi;
using StudentApi.Configuration;

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

builder.Services.AddAutoMapper(typeof (MapperConfig));

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

app.MapStudentEndpoints();

app.MapEnrollmentEndpoints();

app.MapCourseEndpoints();




app.Run();
