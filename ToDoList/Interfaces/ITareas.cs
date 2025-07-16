using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITareas
    {
        List<object> GetListaTareas(int IdUsuario);
        object GetTarea(int idUsuario, int IdTarea);
        bool PostTarea(int idUsuario, string nombreTarea, string descripcion, int estado);
        bool PatchTarea(int idUsuario, int IdTarea, string? nombreTarea, string? descripcion, int? estado);
        bool DeleteTarea(int idUsuario, int IdTarea);
    }
}
