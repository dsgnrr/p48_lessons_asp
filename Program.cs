using System.Reflection;
using Microsoft.AspNetCore.Builder;
using p48_lessons_asp.Models;
using p48_lessons_asp.Services.Generator;

namespace p48_lessons_asp
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddSingleton<IGenerator<Product>, ProductGenerator>();
            // Додаємо сервіс, який буде перенаправляти запити на контролери
            builder.Services.AddControllers();
            // сервіс який збирає метадані про всі ендпоінти(маршрути) у додатку
            builder.Services.AddEndpointsApiExplorer();
            // сервіс генератор, який генрує специфікацію(JSON-файл), на основі якого будується веб-сторінка, з усіма ендпоінтами
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
                {
                    Title = "ASP.Net Core WebAPI Lessons",
                    Version = "v1.0",
                    Description = "Documentation for REST API on ASP.Net Core",
                    Contact = new Microsoft.OpenApi.OpenApiContact
                    {
                        Name="dsgnrr",
                        Email="dsgnrr@rnicrosoft-supprot.com"
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // створення кінцевої точки, яка буде містити згенерований JSON-файл з описом ендпоінтів
                app.UseSwagger();
                // кінцева точка але вже з графічним інтерфейсом
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            // Зв'язує класи контролери з маршрутами
            app.MapControllers();

            app.Run();
            
        }
        
    }
}

