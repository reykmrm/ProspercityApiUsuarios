using Microsoft.EntityFrameworkCore;
using ProspercityApiUsuarios.Models;

namespace ProspercityApiUsuarios.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options)
         : base(options)
        {

        }

        public DbSet<TipoUsuarioModel> TipoUsuario { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }
    }
}
