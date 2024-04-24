using Fiap.Data;
using Fiap.Data.Context;
using Fiap.Data.Interfaces;
using Fiap.Domain.Commands.Class;
using Fiap.Domain.Commands.ClassStudent;
using Fiap.Domain.Commands.Student;
using Fiap.Domain.Entities;
using Fiap.Service.Handlers;
using Fiap.Service.Interfaces;
using Fiap.Service.Services;
using Fiap.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<FiapDbContext>(options =>
    {
        options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Fiap.Data"));
    });

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowAnyHeader();
        });
    });

    var services = GetServiceCollection(builder);
}

IServiceCollection GetServiceCollection(WebApplicationBuilder builder)
{
    // Adicionando serviços
    var services = builder.Services;
    services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

    services.AddScoped<IReadRepository<Class>, ApplicationRepository<Class>>();
    services.AddScoped<IWriteRepository<Class>, ApplicationRepository<Class>>();

    services.AddScoped<IReadRepository<Student>, ApplicationRepository<Student>>();
    services.AddScoped<IWriteRepository<Student>, ApplicationRepository<Student>>();

    services.AddScoped<IReadRepository<ClassStudent>, ApplicationRepository<ClassStudent>>();
    services.AddScoped<IWriteRepository<ClassStudent>, ApplicationRepository<ClassStudent>>();

    services.AddScoped<IClassService, ClassService>();
    services.AddScoped<IStudentService, StudentService>();
    services.AddScoped<IClassStudentService, ClassStudentService>();


    services.AddScoped<IHandler<CreateClassCommand>, CreateClassHandler>();
    services.AddScoped<IHandler<UpdateClassCommand>, UpdateClassHandler>();

    services.AddScoped<IHandler<CreateStudentCommand>, CreateStudentHandler>();
    services.AddScoped<IHandler<UpdateStudentCommand>, UpdateStudentHandler>();

    services.AddScoped<IHandler<CreateClassStudentCommand>, CreateClassStudentHandler>();


    return services;
}