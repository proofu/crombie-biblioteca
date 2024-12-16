namespace BibliotecaAPI.Models
{
    public class Prestamo
    {
        public int PrestamoISBN { get; set; }
        public Libro? Libro { get; set; }
        public int PrestamoUsuarioID { get; set; }
        public Usuario Usuario { get; set; }

    }
}
