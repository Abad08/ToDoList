using Microsoft.EntityFrameworkCore;
using ToDoList.Contexto;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TareasService : ITareas
    {
        private readonly ToDoListContext _context;

        public TareasService(ToDoListContext context)
        {
            _context = context;
        }

        public List<object> GetListaTareas(int IdUsuario)
        {
            return _context.Tareas.Where(t => t.IdUsuario == IdUsuario).Select(t => new { t.Id, t.Nombre, t.Descripcion, Estado = t.Estado.Nombre}).ToList<object>();
        }

        public object GetTarea(int idUsuario, int IdTarea)
        {
            var tarea = _context.Tareas.Where(t => t.Id == IdTarea && t.IdUsuario == idUsuario).Select(t => new { t.Id, t.Nombre, t.Descripcion, Estado = t.Estado.Nombre }).FirstOrDefault();
            
            return tarea ?? null;
        }

        public bool PostTarea(int idUsuario, string nombreTarea, string tDescripcion, int idEstado)
        {
            var usuarioExiste = _context.Usuario.Any(u => u.Id == idUsuario);
            if (!usuarioExiste) {return false;}

            var estadoExiste = _context.Estado.Any(e => e.Id == idEstado);
            if (!estadoExiste) return false; // Si el estado no existe, retorna false

            var nuevaTarea = new Tareas
            {
                IdUsuario = idUsuario,
                Nombre = nombreTarea,
                Descripcion = tDescripcion,
                IdEstado = idEstado 
            };

            _context.Tareas.Add(nuevaTarea);
            _context.SaveChanges();
            return true;
        }

        public bool PatchTarea(int idUsuario, int idTarea, string? nombreTarea, string? tDescripcion, int? estado)
        {
            var tarea = _context.Tareas.FirstOrDefault(t => t.Id == idTarea && t.IdUsuario == idUsuario);
            if (tarea == null) return false; 

            // Si el nombre de la tarea no está vacío, lo actualiza
            if (!string.IsNullOrWhiteSpace(nombreTarea))
            { tarea.Nombre = nombreTarea; }

            if (!string.IsNullOrWhiteSpace(tDescripcion))
            { tarea.Descripcion = tDescripcion; }

            // Si el estado no es nulo y es válido en la base de datos, lo actualiza
            if (estado.HasValue && _context.Estado.Any(e => e.Id == estado.Value))
            {
                tarea.IdEstado = estado.Value;
            }

            _context.SaveChanges();
            return true;
        }

        public bool DeleteTarea(int idUsuario, int idTarea)
        {
            var tarea = _context.Tareas.FirstOrDefault(t => t.IdUsuario == idUsuario && t.Id == idTarea);
            if (tarea == null) { return false; }

            _context.Tareas.Remove(tarea);
            _context.SaveChanges();
            return true;
        }
    }
}
