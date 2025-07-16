using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Contexto
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> db):base(db) 
        {
            
        }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Tareas> Tareas { get; set; }

        public DbSet<Estado> Estado { get; set; }
    }
}
