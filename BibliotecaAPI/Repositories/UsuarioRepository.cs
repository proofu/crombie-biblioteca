﻿using BibliotecaAPI.Models;
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

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"              
                SELECT
                    u.ID,
                    u.Nombre,
                    u.TipoUsuario,
                    p.PrestamoISBN,
                    p.PrestamoUsuarioID,
                    l.ISBN,
                    l.Titulo,
                    l.Autor,
                    l.isAvailable
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
                    var usuarios = connection.Query<Usuario, Prestamo, Libro, Usuario>(
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
                                if (libro != null)
                                {
                                    prestamo.Libro = libro; 
                                }
                                currentUsuario.Prestamos.Add(prestamo);
                            }
                            return currentUsuario;
                        },
                        splitOn: "PrestamoISBN, ISBN"
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

        public bool DeleteUsuario(int usuarioId)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open(); // Open the connection
                using (var transaction = connection.BeginTransaction()) // Begin a transaction
                {
                    try
                    {
                       
                        var deletePrestamosSql = @"DELETE FROM Prestamos WHERE PrestamoUsuarioID = @UsuarioID";
                        connection.Execute(deletePrestamosSql, new { UsuarioID = usuarioId }, transaction);

                       
                        var deleteUsuarioSql = @"DELETE FROM Usuarios WHERE ID = @UsuarioID";
                        var rowsAffected = connection.Execute(deleteUsuarioSql, new { UsuarioID = usuarioId }, transaction);

                       
                        transaction.Commit();

                        return rowsAffected > 0; 
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); 
                        Console.WriteLine($"Error: {ex.Message}");
                        return false; 
                    }
                }
            }
        }


    }




    //_____________________
}

