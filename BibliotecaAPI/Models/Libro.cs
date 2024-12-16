namespace BibliotecaAPI.Models
{
    public class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public bool isAvailable { get; set; }
        public List<Prestamo>? Prestamos { get; set; }
    }
}
