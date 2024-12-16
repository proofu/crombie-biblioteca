using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace BibliotecaAPI.Services
{
    public class LibroService
    {
        public List<Libro> libros;
        private readonly LibroRepository _libroRepository;
        public LibroService(LibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
            //libros = new List<Libro>();
        }
        //public List<Libro> libros = new List<Libro>();

        //____________________DAPPER

        public IEnumerable<Libro> GetAll()
        {
            IEnumerable<Libro> libros = _libroRepository.GetAll();
            return libros;
        }
        public Libro GetByISBN(string isbn)
        {
            return _libroRepository.GetLibroByISBN(isbn);
        }

        ///

        /*
        public Libro Post(Libro libro)
        {

            // llamar esta funcion desde controller
            try
            {
                libros.Add(libro);
                return libro;
            }
            catch (Exception)
            {
                Console.WriteLine("ha habido un error");
                throw;
            }
            // después llmamos data y ahí se inserta

        }
        public Libro? Get(string isbn)
        {
            

            Libro? libro = libros.Find(l => l.ISBN == isbn);

            return libro;
        }
        public Libro? Put(string isbn, Libro newLibro)
        {
            Libro? libro = libros.Find(l => l.ISBN == isbn);
            if (libro != null)
            {
                libro.ISBN = newLibro.ISBN;
                libro.Titulo = newLibro.Titulo;
                libro.Autor = newLibro.Autor;
                libro.isAvailable = newLibro.isAvailable;
            }
            return libro;
        }
        public void Delete(Libro libro)
        {
            libros.Remove(libro);
        }
         */
    }
}
