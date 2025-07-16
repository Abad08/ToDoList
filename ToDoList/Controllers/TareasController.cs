using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly ITareas _TareasService;

        public TareasController(ITareas tareasService)
        {
            _TareasService = tareasService;
        }

        [HttpGet]
        public IActionResult GetListaTareas(int idUsuario)
        {
            var tareas = _TareasService.GetListaTareas(idUsuario);
            return Ok(tareas);
        }

        [HttpGet("{idUsuario}/{idTarea}")]
        public IActionResult GetTarea(int idUsuario, int idTarea)
        {
            var tarea = _TareasService.GetTarea(idUsuario, idTarea);
            if (tarea == null) { return NotFound("La tarea que desea encontrar no existe o pertenece a otro usuario."); }
            return Ok(tarea);
        }

        [HttpPost("post")]
        public IActionResult PostTarea(int idUsuario, string nombreTarea, string tDescripcion, int estado)
        {
            bool tUpload = _TareasService.PostTarea(idUsuario, nombreTarea, tDescripcion, estado);

            if (!tUpload) { return BadRequest("No se ha podido crear la tarea"); }
            return Ok( new { message = "¡Tarea creada exitosamente!" });
        }

        [HttpPatch("patch")]
        public IActionResult PatchTarea(int idUsuario, int idTarea, string? nombreTarea, string? tDescripcion, int? estado)
        {
            bool tPatch = _TareasService.PatchTarea(idUsuario, idTarea, nombreTarea, tDescripcion, estado);

            if (!tPatch) { return BadRequest("La tarea que desea encontrar no existe o pertenece a otro usuario."); }
            return Ok("¡Tarea actualizada exitosamente!");
        }

        [HttpDelete("delete/{idUsuario}/{idTarea}")]
        public IActionResult DeleteTarea(int idUsuario, int idTarea)
        {
            bool tDelete = _TareasService.DeleteTarea(idUsuario, idTarea);

            if (!tDelete) { return NotFound("La tarea que desea eliminar no existe o pertenece a otro usuario"); }
            return Ok("¡Tarea eliminada exitosamente!");
        }
    }
}
