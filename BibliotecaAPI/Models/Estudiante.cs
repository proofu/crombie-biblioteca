using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using System.Runtime.CompilerServices;

namespace BibliotecaAPI.Models
{
    public class Estudiante : Usuario
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
