namespace BibliotecaAPI.Models
{
    public class Usuario
    {
        public string? Nombre { get; set; }
        public int ID { get; set; }
        public string? TipoUsuario { get; set; }
        public List<Prestamo>? Prestamos { get; set; }

        public virtual string PrestarMaterial()
        {
            return $"{Nombre} está pidiendo material";
        }
        public virtual string DevolverMaterial()
        {
            return $"{Nombre} está devolviendo material";
        }
        
    }
}
