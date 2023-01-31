using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ProspercityApiUsuarios.ClasesAux;
using ProspercityApiUsuarios.Data;
using ProspercityApiUsuarios.Models;
using ProspercityApiUsuarios.Models.DTO;
using SQLitePCL;

namespace ProspercityApiUsuarios.Services
{
    public class UsuarioServices
    {
        private readonly Context _context;

        public UsuarioServices(Context context)
        {
            _context = context;
        }


        public async Task<dynamic> GetAll(DTOEmail email)
        {
            var loginResul = await _context.Usuario
               .FirstOrDefaultAsync(x => x.Email == email.Email);

            if (loginResul is null)
            {
                return new { result = "No tienes permiso para ver esta informacion" };
            }

            return await _context.Usuario.Include(m=>m.TipoUsuario)
            .Where(u => u.IdTipoUsuario >= loginResul.IdTipoUsuario && u.Id != loginResul.Id)
            .Select(user => new UserDTO
            {   Id  =   user.Id,         
                Nombre = user.Nombre + " "+ user.apellido,
                TipoUser = user.TipoUsuario.Name,
                Telefono=user.Telefono,
                Email=user.Email,
                Usuario = user.Usuario
            }).ToListAsync();
        }

        public async Task<UsuarioModel?> GetById(int id)
        {
            return await _context.Usuario.FindAsync(id);
        }

        public async Task<UsuarioModel?> GetEmail(string email)
        {
            return await _context.Usuario.FirstAsync(x => x.Email == email);
        }

        public async Task<dynamic> Create(UsuarioModel Usuario)
        {
            EncriptarClave claveEncriptada = new EncriptarClave();
           var clave = claveEncriptada.Encriptar(Usuario.Contrasena);
            Usuario.Contrasena = clave;
            var loginResul = await _context.Usuario
               .SingleOrDefaultAsync(x => x.Email == Usuario.Email );

            if (loginResul != null)
            {
                return new { result = "Correo Registrado previamente" };
            }

            var usuarioResul = await _context.Usuario
               .SingleOrDefaultAsync(x => x.Usuario == Usuario.Usuario);

            if (usuarioResul != null)
            {
                return new { result = "Usuario Registrado previamente" };
            }

            _context.Usuario.Add(Usuario);
            await _context.SaveChangesAsync();
            return new { result = "Registro exitoso" }; 
        }

        public async Task<dynamic> Login(DTOLogin login)
        {
            EncriptarClave claveEncriptada = new EncriptarClave();
            var clave = claveEncriptada.Encriptar(login.Clave);
            login.Clave = clave;

            var loginResul = await _context.Usuario
                .SingleOrDefaultAsync(x => (x.Email == login.Correo || x.Usuario == login.Correo)
                && x.Contrasena == login.Clave);

            if (loginResul == null)
            {
                return new { result = "Credenciales incorrectas" };
            }

            return new { result = "ok", correo = loginResul.Email, usuario = loginResul.Nombre + " " + loginResul.apellido };

        }

        public async Task<dynamic> Update(UsuarioModel Usuario)
        {
            var ExisteUsuario = await GetById(Usuario.Id);
            var usuarioResul = await _context.Usuario
               .SingleOrDefaultAsync(x => x.Usuario == Usuario.Usuario && x.Id != Usuario.Id);

            if (usuarioResul != null)
            {
                return new { result = "Usuario Registrado previamente" };
            }
            if (ExisteUsuario is not null)
            {
                ExisteUsuario.Nombre = Usuario.Nombre;
                ExisteUsuario.apellido = Usuario.apellido;
                ExisteUsuario.Email = Usuario.Email;
                ExisteUsuario.Telefono=Usuario.Telefono;
                ExisteUsuario.IdTipoUsuario = Usuario.IdTipoUsuario;
                ExisteUsuario.Estado = Usuario.Estado;
                ExisteUsuario.Contrasena=Usuario.Contrasena;
                ExisteUsuario.Usuario = Usuario.Usuario;
                 _context.Usuario.Update(ExisteUsuario);
                await _context.SaveChangesAsync();
            }
            return new { result = "Registro actualizado" };
        }


        public async Task<dynamic> Delete(int id)
        {

            var loginResul = await _context.Usuario.FindAsync(id);

            if (loginResul.IdTipoUsuario == 2)
            {
                return new { result = "No puedes eliminar a este usuario" };
            }

            var ExisteUsuario = await GetById(id);
            if (ExisteUsuario is not null)
            {
                _context.Usuario.Remove(ExisteUsuario);
                await _context.SaveChangesAsync();
                return new { result = "Eliminado exitosamente" };
            }

            return new { resultt = "No puedes eliminar a este usuario" };
        }

    }
}
