using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspercityApiUsuarios.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string apellido { get; set; }

        [JsonIgnore]
        [ForeignKey("TipoUsuario")]
        public int IdTipoUsuario { get; set; }        
        public virtual TipoUsuarioModel? TipoUsuario { get; set; }
       
        public string? Telefono { get; set; }

        public string Email { get; set; }
        public string Usuario { get; set; }

        public string Contrasena { get; set; }

        public string Estado { get; set; }
    }
}
