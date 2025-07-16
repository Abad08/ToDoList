using ToDoList.Contexto;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly ToDoListContext _context;

        public UsuarioService(ToDoListContext context)
        {
            _context = context;
        }

        //Registra un usuario nuevo
        public bool Registrar(string username, string password)
        {
            //valida si el nombre de usuario ya existe
            username = username.Trim();
            if (_context.Usuario.Any(u => u.Username == username))
                return false;
            else
            {
                //Crea un usuario nuevo con los valores recibidos
                var usuarioNuevo = new Usuario { Username = username, Password = password };

                _context.Usuario.Add(usuarioNuevo);
                _context.SaveChanges();
                return true;
            }
        }

        //Login para usuarios ya existentes
        public Usuario Login(string username, string password)
        {
            return _context.Usuario.SingleOrDefault(u => u.Username == username && u.Password == password);
        }

        
    }
}
