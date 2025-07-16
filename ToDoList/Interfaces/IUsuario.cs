using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface IUsuario
    {
        bool Registrar(string username, string password);
        Usuario Login(string username, string password);
    }
}
