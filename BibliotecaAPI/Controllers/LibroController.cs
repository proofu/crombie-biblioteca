using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        //private readonly LibroService _LibroService = new LibroService();
        private readonly LibroService _LibroService;
        public LibroController(LibroService libroService)
        {
            _LibroService = libroService;
        }


        // instanciar libro service

        // GET ALL: api/<Libro>
        [HttpGet]
        public IEnumerable<Libro> Get()

        {
            IEnumerable<Libro> librosTemp = _LibroService.GetAll();
            return librosTemp;
        }

        // GET api/<Libro>/5
        [HttpGet("{isbn}")]
        public IActionResult Get(string isbn)
        {
            Libro? libro = _LibroService.GetByISBN(isbn);


            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }
        // POST api/<Libro>
        [HttpPost]
        public IActionResult Post([FromBody] Libro libro)
        {
            
            if (libro == null)
            {
                return BadRequest("datos de libro inválido");
            }
            var inserted = _LibroService.PostLibro(libro);
            if (inserted)
            {
                return Ok("libro ingresado.");
            } else
            {
                return BadRequest("hubo un problema al insertar libro");
            }

        }

        // PUT api/<Libro>/5
        [HttpPut("{isbn}")]
        public IActionResult Put([FromBody] Libro libro)
        {
            bool libroModified = _LibroService.UpdateLibro(libro);
            if (libroModified == false)
            {
                return NotFound();
            }
            return Ok();
        }

        // DELETE api/<Libro>/5
        [HttpDelete("{isbn}")]
        public IActionResult Delete(string isbn)
        {
            bool deleted = _LibroService.DeleteLibro(isbn);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok("libro borrado");
        }

        /*
        // POST api/<Libro>
        [HttpPost]
        public IActionResult Post([FromBody] Libro libro)
        {
            //desde acá llamamos al service
            _LibroService.Post(libro);

            return CreatedAtAction(nameof(Get), new { isbn = libro.ISBN }, libro);

        }

        // PUT api/<Libro>/5
        [HttpPut("{isbn}")]
        public IActionResult Put(string isbn, [FromBody] Libro libro)
        {
            Libro? libroModified = _LibroService.Put(isbn, libro);
            if (libroModified == null)
            {
                return NotFound();
            }
            return Ok(libroModified);
        }

        
        [HttpPut("prestarLibro/{id}")]
        public IActionResult PrestarLibro(int id, string isbn)
        {
            //verificar que el libro esté disponible
            //asociarlo al usuario
            //pasarlo a no disponible
            return Ok();
        }

        
        [HttpPut("devolverLibro/{id}")]
        public IActionResult DevolverLibro(int id, string isbn)
        {
            //verificar que el usuario tenga el libro
            //borrarlo del usuario
            //pasarlo a disponible
            return Ok();
        }
        

        // DELETE api/<Libro>/5
        [HttpDelete("{isbn}")]
        public IActionResult Delete(string isbn)
        {
            Libro? libro = _LibroService.Get(isbn);
            if(libro == null)
            {
                return NotFound();
            }
            _LibroService.Delete(libro);
            return NoContent();
        }
         */
    }
}
