
using Microsoft.EntityFrameworkCore;
using ToDoList.Contexto;
using ToDoList.Interfaces;
using ToDoList.Services;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options => 
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173").AllowAnyHeader()
                    .AllowAnyMethod().AllowCredentials();
                });    
                
            });
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ToDoListContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ITareas, TareasService>();
            builder.Services.AddScoped<IUsuario, UsuarioService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowReactApp");

            app.MapControllers();

            app.Run();
        }
    }
}
