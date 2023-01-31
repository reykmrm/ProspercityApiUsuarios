using System.ComponentModel.DataAnnotations.Schema;

namespace ProspercityApiUsuarios.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string apellido { get; set; }

        public string TipoUser { get; set; }

        public string Usuario { get; set; }

        public string? Telefono { get; set; }

        public string Email { get; set; }
    }
}
