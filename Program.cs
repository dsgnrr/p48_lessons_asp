using System.Globalization;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using p48_lessons_asp.Data;
using p48_lessons_asp.Models;
using p48_lessons_asp.Services.Random;
using p48_lessons_asp.Services.Slug;
using Slugify;
using Serilog;
using p48_lessons_asp.Services.StorageService;
using p48_lessons_asp.Services.Hash;
using p48_lessons_asp.Services.Kdf;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using p48_lessons_asp.Services.Token;

namespace p48_lessons_asp
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true).Build();

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

            try
            {
                Log.Information("Running web-application");
                var builder = WebApplication.CreateBuilder(args);
                ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");

                // підключення логера замість стандартного
                builder.Host.UseSerilog();
                builder.Services.AddSingleton<IRandomService, RandomService>();
                builder.Services.AddSingleton<ISlugHelper, SlugHelper>();
                builder.Services.AddSingleton<ISlugService, SlugService>();
                builder.Services.AddSingleton<IStorageService, ServerFileStorageService>();

                builder.Services.AddSingleton<IHashService, Md5HashService>();
                builder.Services.AddSingleton<IKdfService, HashKdfService>();

                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                builder.Services.AddValidatorsFromAssemblyContaining<Program>();
                // builder.Services.AddSingleton<IGenerator<Product>, ProductGenerator>();
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
                            Name = "dsgnrr",
                            Email = "dsgnrr@rnicrosoft-supprot.com"
                        }
                    });
                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                    c.EnableAnnotations();
                });

                var connectionString = builder.Configuration.GetConnectionString("MSSqlConnection");
                if (connectionString == null)
                {
                    throw new Exception("No connection string. Please provide in appsettings.json");
                }
                try
                {
                    builder.Services.AddDbContext<DataContext>(
                        options => options.UseSqlServer(connectionString)
                    );
                }
                catch (Exception)
                {
                    throw new Exception("Getting error while trying to connect to database");
                }


                #region JWT_CONFIGURATION
                var jwtSettings = builder.Configuration.GetSection("Jwt");
                var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Валідація назви сервіса який створив токен
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        // Валідація отримувача токена
                        ValidateAudience= true,
                        ValidAudience = jwtSettings["Audience"],
                        // Валідація життя токену
                        ValidateLifetime = true,

                        // Додавання ключа для підпису + його валідація
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ValidateIssuerSigningKey = true,
                        // Виключаємо додаткові 5 хвилин після прострочення токену
                        ClockSkew = TimeSpan.Zero
                    };
                });

                builder.Services.AddAuthorization();

                builder.Services.AddSingleton<ITokenService, JwtTokenService>();
                #endregion


                var app = builder.Build();


                // Мідлвеєр який логує запити
                app.UseSerilogRequestLogging();
                if (app.Environment.IsDevelopment())
                {
                    // створення кінцевої точки, яка буде містити згенерований JSON-файл з описом ендпоінтів
                    app.UseSwagger();
                    // кінцева точка але вже з графічним інтерфейсом
                    app.UseSwaggerUI();
                }

                app.UseStaticFiles();
                app.UseAuthorization();

                // Зв'язує класи контролери з маршрутами
                app.MapControllers();

                app.Run();

            }
            catch (Exception ex) when (ex is not HostAbortedException)
            {
                Console.WriteLine($"REAL ERROR: {ex.InnerException?.Message ?? ex.Message}");
                Log.Fatal(ex, "Application stopped with fatal error");
            }
            finally
            {
                // В будь-якому випадку, закриваємо логер та вивантажуємо буфер на диск
                Log.CloseAndFlush();
            }
        }

    }
}

