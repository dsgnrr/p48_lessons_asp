using Microsoft.AspNetCore.Builder;

// Файл старту, тут запускається сервер

// як запустити ASP проєкт
// # запуск проєкту 
// dotnet run 
namespace p48_lessons_asp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Main запускає сервер(бекенд)
            /* Для створення додатку, необхідно створити білдер
            Білдер займається налаштуванням додатку:
            - конфігурація
            - логування
            - контейнери DI(Dependency Injection)
            - підключення бд
            - створення власних серівсів

            на основі білдера створюється додаток
            */

            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            /*
            У додатку налаштовується
            - рутінг
            - мідлвеєри
            - обробка запитів
            */


            // MapGet - (route)ендпоінт який отримує GET запити
            // отримує два параметри: перший- назва еднопінту
            // другий - лямбда-хендлер

            /*
            На кожен HTTP Method є свій мап
            MapGet - GET
            MapPost - POST
            MapPut - PUT
            MapPatch - PATCH
            */
            // Мідлвеєр який дозволяє шукати файли у проєкті
            app.UseDefaultFiles();

            // Мідлвеєр який включає роздачу статичних файлів сервером.
            app.UseStaticFiles();

            /* Коли ми підключаємо роздачу статичних файлів
            і в нас є індекс.html необхідність в ендопінті / відпадає,
            замість нашої логіки почне роздаватися html
            */
            // app.MapGet("/", () => "Hello World!");

            app.MapGet("/greet/{name}", (string name) =>
            {
                return $"Hello, Dear {name}";
            });

            app.MapGet("/health", () =>
            {
                return "Server is running";
            });

            app.MapGet("/object", () =>
            {
               return new
               {
                  status = 200,
                  message = "Ok",
                  data = new
                  {
                      username = "Ivan",
                      accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
                  } 
               } ;
            });
            // спрацьовує екранування тому HTML нормально не відображається у браузері
            // Щоб спрацювало необхідно передати у заголовках Content-Type
            app.MapGet("/index", () =>
            {
                
               return "<h1>This Header1</h1>"; 
            });
            /*
            1-й Спосіб вказати тип контенту: Використовуємо клас-хелпер, у якому можна вказати тип контенту.
            */
            app.MapGet("/htmlText", () =>
            {
                var result = "<h1>This Header1</h1>";

                return Results.Content(result, contentType:"text/html", System.Text.Encoding.UTF8);
            });

            /*
            2-й спосіб використовуємо HttpContext
            */

            app.MapGet("/beatyHtml", (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                return "<h1 style=\"color: darkgreen; font-family: sans-serif;\">This Header1</h1>"; 
            });



            // Запуск додатку: запуск вбудованого веб-сервера Kestrel,
            // який слухає усі вхідні запити
            // для сервера створюється порт на якому він слухає
            app.Run();

            /*Завдання:
            Взяти реакт проєкт, збілдити його, і роздати бандли за допмогою ASP
            */
        }
    }
}

// Ctrl+K, Ctrl+F - formatrting code
