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

                    
                    var libro = connection.QuerySingleOrDefault<Libro>(sql, new { ISBN = isbn });

                    return libro; 
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

        public bool InsertLibro(Libro libro)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                            INSERT INTO Libros (ISBN, Titulo, Autor, isAvailable)
                            VALUES (@ISBN, @Titulo, @Autor, @IsAvailable)";

                    // Execute the query and check affected rows
                    var rowsAffected = connection.Execute(sql, new
                    {
                        libro.ISBN,
                        libro.Titulo,
                        libro.Autor,
                        libro.isAvailable
                    });

                    return rowsAffected > 0; 
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    throw new Exception("An error occurred while inserting the Libro into the database.", sqlEx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                    throw new Exception("An unexpected error occurred in InsertLibro.", ex);
                }
            }
        }

        public bool UpdateLibro(Libro libro)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                UPDATE Libros
                SET 
                    Titulo = @Titulo,
                    Autor = @Autor,
                    isAvailable = @IsAvailable
                WHERE 
                    ISBN = @ISBN";

                    // Ejecutar el query con los parámetros del libro
                    var rowsAffected = connection.Execute(sql, new
                    {
                        libro.Titulo,
                        libro.Autor,
                        libro.isAvailable,
                        libro.ISBN
                    });

                    return rowsAffected > 0; // Devuelve true si al menos una fila fue afectada
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    throw new Exception("Ocurrió un error al actualizar el libro en la base de datos.", sqlEx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                    throw new Exception("Ocurrió un error inesperado en UpdateLibro.", ex);
                }
            }
        }

        public bool DeleteLibro(string isbn)
        {

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            
                            var deletePrestamosSql = @"DELETE FROM Prestamos WHERE PrestamoISBN = @ISBN";
                            connection.Execute(deletePrestamosSql, new { ISBN = isbn }, transaction);

                            
                            var deleteLibroSql = @"DELETE FROM Libros WHERE ISBN = @ISBN";
                            var rowsAffected = connection.Execute(deleteLibroSql, new { ISBN = isbn }, transaction);

                            transaction.Commit();

                            return rowsAffected > 0;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Console.WriteLine($"Error: {ex.Message}");
                            throw new Exception("Error al eliminar el libro y sus dependencias.", ex);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    throw new Exception("Ocurrió un error con la base de datos.", sqlEx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw new Exception("Ocurrió un error inesperado al eliminar el libro.", ex);
                }
            }


        }





        //_____________________

    }
}
