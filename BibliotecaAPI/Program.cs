
using BibliotecaAPI.Services;
using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using Dapper;
using BibliotecaAPI.Models;
using Z.Dapper.Plus;
using BibliotecaAPI.Repositories;


namespace BibliotecaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            //esta es la instancia en el program>>>>>
            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddScoped<LibroService>();
            builder.Services.AddScoped<UsuarioService>();
            //builder.Services.AddSingleton<UsuarioService>();
            //builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<LibroRepository>();
            builder.Services.AddScoped<UsuarioRepository>();

            //logger code
            var logFilePath = Path.Combine(
                //
                AppDomain.CurrentDomain.BaseDirectory,   // Ruta base del proyecto una vez compilado
                "Logs",                                 // Nombre de la carpeta
                "api-errors.log"                        // Nombre del archivo
            );
            //Console.WriteLine("directorio log");
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            /*
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)); // Crear el directorio si no existe

            builder.Services.AddSingleton(new FileLogger(logFilePath));
             */
            //______________________

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            // prueba dapper
            /*
            string connectionString = "Server=DESKTOP-GTHPLNL;    Database=biblioteca;   Integrated Security=true; TrustServerCertificate=True;";
            //string connectionString = "\"Server=DESKTOP-GTHPLNL;    Database=biblioteca;   Integrated Security=true;\"";
            var connection = new SqlConnection(connectionString);
            connection.CreateTable<Usuario>();
            var seedUsuario = new List<Usuario>();
            //seedUsuario.Add(new Usuario() { ID = 1, Nombre = "paco", TipoUsuario = "Estudiante", LibrosPrestados = [] });
            //seedUsuario.Add(new Usuario() { ID = 2, Nombre = "fdfdfd", TipoUsuario = "Estudiante", LibrosPrestados = [] });
            //connection.BulkInsert(seedUsuario);
            //prueba


            var sql = "INSERT INTO Usuario (ID, Nombre, TipoUsuario, LibrosPrestados) VALUES (@ID, @Nombre, @TipoUsuario, @LibrosPrestados)";

            var anonymousProduct = new Usuario
            { ID = 1, Nombre = "paco", TipoUsuario = "Estudiante", LibrosPrestados = [] };

            var rowsAffected1 = connection.Execute(sql, anonymousProduct);

            var product = new Usuario
            { ID = 2, Nombre = "jose", TipoUsuario = "Estudiante", LibrosPrestados = [] };

            var rowsAffected2 = connection.Execute(sql, product);

            var insertedProducts = connection.Query<Usuario>("SELECT * FROM Productos").ToList();
            */




            //__________________________




            /*
            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
            .SetMinimumLevel(LogLevel.Trace)
            .AddConsole());

            ILogger<UsuarioService> logger = app.Services.GetRequiredService<ILogger<UsuarioService>>();
            var usuarioLog = new UsuarioService(logger);
             */

            /*
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("Program");
            logger.LogInformation("Hello World! Logging is {Description}.", "fun");

            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger<Program>();
            logger.LogInformation("Hello World! Logging is {Description}.", "fun");
             */

            app.UseAuthorization();

            //app.UseMiddleware<ErrorLoggingMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}
