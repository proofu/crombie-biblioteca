using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BibliotecaAPI.Services
{
    public class UsuarioService
    {
        //private readonly string _filePath;
        //public List<Usuario> usuarios;
        /*
        private readonly ILogger<UsuarioLog> logger;

        public UsuarioLog(ILogger<UsuarioLog> logger)   
        {
            _logger = logger;
            var message = $"UsuarioLog created at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.
        }

        */
        /*
        public UsuarioService(string filePath)
        {
            //usuarios = new List<Usuario>();
            _filePath = filePath;
        }
        */
        private readonly UsuarioRepository _repository;
        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;            
        }
        //________________DAPPER

        public IEnumerable<Usuario> GetAll(){ 
            IEnumerable<Usuario> usuarios = _repository.GetAllUsuarios();
            return usuarios;
        }
        public Usuario GetById(int id)
        {
            Usuario usuario = _repository.GetUsuarioById(id);
            return usuario;
        }
        public bool PostUsuario(Usuario usuario)
        {
            return _repository.InsertUsuario(usuario);
        }


        //____________________
        public string PrestarMaterial(Usuario usuario)
        {
            return usuario.PrestarMaterial();
        }
        public string DevolverMaterial(Usuario usuario)
        {
            return usuario.DevolverMaterial();
        }

        /*
        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (XLWorkbook workbook = new XLWorkbook(_filePath))
            {
                IXLWorksheet worksheet = workbook.Worksheet("Usuarios");
                foreach (var row in worksheet.RowsUsed().Skip(2))
                {
                    usuarios.Add(new Usuario
                    {
                        ID = row.Cell(1).GetValue<int>(),
                        Nombre = row.Cell(2).GetValue<string>(),
                        TipoUsuario = row.Cell(3).GetValue<string>(),
                        //poblar de tipo libro, desde la tabla libro
                        LibrosPrestados = []
                    }
                    );
                }

            }

            return usuarios;
        }
         */
        /*
        public Usuario? Get(int id)
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (XLWorkbook workbook = new XLWorkbook(_filePath))
            {
                IXLWorksheet worksheet = workbook.Worksheet("Usuarios");
                foreach (var row in worksheet.RowsUsed().Skip(2))
                {
                    usuarios.Add(new Usuario
                    {
                        ID = row.Cell(1).GetValue<int>(),
                        Nombre = row.Cell(2).GetValue<string>(),
                        //poblar de tipo libro, desde la tabla libro
                        LibrosPrestados = []
                    }
                    );
                }

            }
            return usuarios.Find(u => u.ID == id);

        }
        public Usuario? Post(Usuario usuario)
        {
            //usuarios.Add(usuario);
            //return usuario;

            //trae los usuarios
            List<Usuario> usuarios = new List<Usuario>();
            using (XLWorkbook workbook = new XLWorkbook(_filePath))
            {
                IXLWorksheet worksheet = workbook.Worksheet("Usuarios");
                foreach (var row in worksheet.RowsUsed().Skip(2))
                {
                    usuarios.Add(new Usuario
                    {
                        ID = row.Cell(1).GetValue<int>(),
                        Nombre = row.Cell(2).GetValue<string>(),
                        TipoUsuario = row.Cell(3).GetValue<string>(),
                        //poblar de tipo libro, desde la tabla libro
                        LibrosPrestados = []
                    }
                    );
                }
                //sumar usuario nuevo
                usuarios.Add(usuario);

                int lastRow = 2;
                //guardar lista
                // Agregar datos a las celdas
                foreach (Usuario u in usuarios)
                {
                    worksheet.Cell(lastRow, 1).Value = u.ID;
                    worksheet.Cell(lastRow, 2).Value = u.Nombre;
                    worksheet.Cell(lastRow, 3).Value = u.TipoUsuario;
                    lastRow++;
                }
                //worksheet.Cell(2, 2).Value = usuario.LibrosPrestados;

                // Guardar el archivo
                //                string filePath = "Ejemplo.xlsx";
                workbook.SaveAs(_filePath);

                Console.WriteLine($"Archivo guardado en: {_filePath}");
                return usuario;
            }
        }
         */
        
        /*

        internal Usuario? Put(int id, Usuario newUser)
        {
            Usuario? userAModificar = usuarios.Find(u => u.ID == id);
            if (userAModificar != null)
            {
                userAModificar.ID = newUser.ID;
                userAModificar.Nombre = newUser.Nombre;
            }
            return userAModificar;
        }
         */
    }
}
