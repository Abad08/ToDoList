using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Estado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(12)]
        public string Nombre { get; set; }
    }
}
