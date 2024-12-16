using Dapper;
using BibliotecaAPI.Models;
using Microsoft.Data.SqlClient;

namespace BibliotecaAPI.Repositories
{
    public class LibroRepository 
    {
        private readonly DapperContext _context;

        public LibroRepository(DapperContext context)
        {
            _context = context;
        }

        //___________DAPPER
        public IEnumerable<Libro> GetAll()
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                SELECT
                    Id,
                    Titulo,
                    Autor,
                    ISBN,
                    isAvailable
                FROM
                    Libros";

                    var libros = connection.Query<Libro>(sql);
                    return libros;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"Error al ejecutar la consulta SQL: {sqlEx.Message}");
                    return Enumerable.Empty<Libro>();
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "Error inesperado en GetAllProducto");
                    Console.WriteLine($"error inesperado en getallproducto:  {ex}");
                    return Enumerable.Empty<Libro>();
                }
            }
        }

        public Libro GetLibroByISBN(string isbn)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                SELECT 
                    ISBN, 
                    Titulo, 
                    Autor, 
                    isAvailable
                FROM 
                    Libros
                WHERE 
                    ISBN = @ISBN";

                    // Execute query and map result to the Libro object
                    var libro = connection.QuerySingleOrDefault<Libro>(sql, new { ISBN = isbn });

                    return libro; // Return the libro object or null if not found
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    throw new Exception("An error occurred while fetching the Libro.", sqlEx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                    throw new Exception("An unexpected error occurred in GetLibroById.", ex);
                }
            }
        }



        //_____________________

    }
}
