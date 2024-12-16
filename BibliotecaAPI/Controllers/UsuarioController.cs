using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //List<Usuario> usuarios = new List<Usuario>();

        // GET: api/<UsuarioController>
        private readonly UsuarioService _UsuarioService;
        /*
        public UsuarioController(UsuarioService usuarioService)
        {
            //_UsuarioService = usuarioService;
            _UsuarioService = usuarioService;
        }
         */
        public UsuarioController(UsuarioService usuarioService)
        {
            //_UsuarioService = usuarioService;
            //_UsuarioService = new UsuarioService("C:\\Users\\Alfred\\source\\repos\\BibliotecaAPI\\BibliotecaAPI\\BibliotecaDB.xlsx");
            _UsuarioService = usuarioService;
        }

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            IEnumerable<Usuario> usuariosTemp = _UsuarioService.GetAll();
            return usuariosTemp;
        }
        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {            
             
            if (usuario == null)
            {
                return BadRequest("datos de usuario inválido");
            }

            var inserted = _UsuarioService.PostUsuario(usuario);
            if (inserted)
            {
                return Ok("usuario ingresado");
            } else
            {
                return BadRequest("hubo un problema al insertar usuario");
            }

        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            
            Usuario? userFound = _UsuarioService.GetById(id);
            if (userFound == null)
            {
                return NotFound();
            }
            return Ok(userFound);
             
        }


        /*
        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //Usuario? userFound = _UsuarioService.Get(id);
            if (userFound == null)
            {
                return NotFound();
            }
            return Ok(userFound);
        }
        [HttpPost("PrestarMaterial")]
        public IActionResult PrestarMaterial([FromBody] Usuario usuario)
        {
            var response =_UsuarioService.PrestarMaterial(usuario);
            return Ok(new {message=response});
        }
        [HttpPost("DevolverMaterial")]
        public IActionResult DevolverMaterial([FromBody] Usuario usuario)
        {
            var response =_UsuarioService.DevolverMaterial(usuario);
            return Ok(new {message=response});
        }


        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            Usuario? usuarioNew = _UsuarioService.Post(usuario);
            if (usuarioNew == null)
            {
                return BadRequest("datos requeridos");
            }
            else if (usuarioNew.LibrosPrestados.Count > 0)
            {
                return BadRequest("no se puede ingresar libros prestados al iniciar");
            }
            return CreatedAtAction(nameof(Get), new { id = usuario.ID }, usuario);
        }
        /*

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            Usuario? usuarioModified = _UsuarioService.Put(id, usuario);
            if (usuarioModified == null)
            {
                return NotFound();
            }
            return Ok(usuarioModified);
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
         
         */
    }
}
