namespace BibliotecaAPI.Models
{
    public class Profesor : Usuario
    {
        public override string PrestarMaterial()
        {
            return $"{Nombre} {TipoUsuario} está pidiendo material prestado";
        }
        public override string DevolverMaterial()
        {
            return $"{Nombre} {TipoUsuario} está devolviendo material prestado";
        }

    }
}
