using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioService;

        public UsuarioController(IUsuario usuarioService)
        {
            _usuarioService = usuarioService;
        }

        //Post registrar
        [HttpPost("Register")]
        public IActionResult Registrar(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Se necesitan un usuario y contraseña para registrarse");
            }

            bool registro = _usuarioService.Registrar(username, password);

            if (!registro)
            {
                return BadRequest("Ya existe un usuario con este nombre.");
            }

            return Ok(new { success = true, message = "¡Usuario registrado exitosamente!" });
        }


    //Post Login
    [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var usuarioAutorizado = _usuarioService.Login(username, password);
            if (usuarioAutorizado == null)
            { return Unauthorized("Usuario o contraseña no válida.");}

            return Ok(new { Message = "Sesión iniciada exitosamente", IdUsuario = usuarioAutorizado.Id });


        }
    }
}
