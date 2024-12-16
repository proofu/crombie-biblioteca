using BibliotecaAPI.Models;
using Dapper;
using DocumentFormat.OpenXml.InkML;
using Microsoft.Data.SqlClient;

namespace BibliotecaAPI.Repositories
{
    public class UsuarioRepository
    {
        private readonly DapperContext _context;
        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }

        //___________DAPPER


        public IEnumerable<Usuario> GetAllUsuarios()
        {
            /*using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"              
                    SELECT
                        u.ID AS UsuarioID,
                        u.Nombre,
                        u.TipoUsuario,
                        p.PrestamoISBN,
                        p.PrestamoUsuarioID

                    FROM
                        Usuarios u
                    LEFT JOIN
                        Prestamos p
                    ON
                        u.ID = p.PrestamoUsuarioId";
                    var usuarioDictionary = new Dictionary<int, Usuario>();
                    //var usuario = connection.Query<Usuario, List<Prestamo>, Usuario>(
                    var usuario = connection.Query<Usuario, Prestamo, Usuario>(
                        sql,
                        (usuario, prestamo) =>
                        {

                            if (!usuarioDictionary.TryGetValue(usuario.ID, out var currentUsuario))
                            {
                                currentUsuario = usuario;
                                currentUsuario.Prestamos = new List<Prestamo>();
                                usuarioDictionary.Add(usuario.ID, currentUsuario);
                            }

                            if (prestamo != null)
                            {
                                //usuario.Prestamos = prestamo;
                                //return usuario;
                                currentUsuario.Prestamos.Add(prestamo);
                            }

                            return currentUsuario;
                        },
                        splitOn: "PrestamoISBN"
                        );
                    return usuarioDictionary.Values;
                    //return usuario;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                    return Enumerable.Empty<Usuario>();
                }
            }*/

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"              
                SELECT
                    u.ID AS UsuarioID,
                    u.Nombre,
                    u.TipoUsuario,
                    p.PrestamoISBN,
                    p.PrestamoUsuarioID,
                    l.ISBN as LibroISBN,
                    l.Titulo AS LibroTitulo,
                    l.Autor AS LibroAutor,
                    l.isAvailable AS LibroIsAvailable
                FROM
                    Usuarios u
                LEFT JOIN
                    Prestamos p
                ON
                    u.ID = p.PrestamoUsuarioId
                LEFT JOIN
                    Libros l 
                ON 
                    p.PrestamoISBN = l.ISBN";
                    var usuarioDictionary = new Dictionary<int, Usuario>();
                    //var usuario = connection.Query<Usuario, List<Prestamo>, Usuario>(
                    var usuario = connection.Query<Usuario, Prestamo, Libro, Usuario>(
                        sql,
                        (usuario, prestamo, libro) =>
                        {
                            if (!usuarioDictionary.TryGetValue(usuario.ID, out var currentUsuario))
                            {
                                currentUsuario = usuario;
                                currentUsuario.Prestamos = new List<Prestamo>();
                                usuarioDictionary.Add(usuario.ID, currentUsuario);
                            }
                            if (prestamo != null)
                            {
                                //usuario.Prestamos = prestamo;
                                //return usuario;
                                prestamo.Libro = libro;
                                currentUsuario.Prestamos.Add(prestamo);
                            }
                            return currentUsuario;
                        },
                        splitOn: "PrestamoISBN, LibroISBN"
                        );
                    return usuarioDictionary.Values;
                    //return usuario;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return Enumerable.Empty<Usuario>();
                }
            }
        }
        public bool InsertUsuario(Usuario usuario)
        {
            using(var connection = _context.CreateConnection()) {
                try
                {
                    var sql = @"
                            INSERT INTO Usuarios (Nombre,TipoUsuario)
                            VALUES (@Nombre, @TipoUsuario)";
                    var rowsAffected = connection.Execute(sql, usuario);
                    return rowsAffected > 0;
                    /*
                    if (rowsAffected == 0)
                    {
                        Console.WriteLine("no se insertaron registros para el producto");
                    }
                     */
                }
                catch (SqlException sqlEx)
                {

                    throw new Exception("ocurrió un error al insertar el usuario en la base de datos", sqlEx);
                    return false;
                }
                catch (Exception ex)
                {

                    throw new Exception("ocurrió un error inesperado en insertUsuario para {@Usuario}", ex);
                    return false;
                }
            }
        }
        public Usuario GetUsuarioById(int id)
        {
            var sql = @"SELECT ID, Nombre, TipoUsuario 
                FROM Usuarios 
                WHERE ID = @Id";

            using (var connection = _context.CreateConnection())
            {
                return connection.QueryFirstOrDefault<Usuario>(sql, new { Id = id });
            }
        }

    }




    //_____________________
}

