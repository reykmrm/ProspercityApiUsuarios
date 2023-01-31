using Microsoft.EntityFrameworkCore;
using ProspercityApiUsuarios.Data;
using ProspercityApiUsuarios.Models;

namespace ProspercityApiUsuarios.Services
{
    public class TipoUsuarioServices
    {
        private readonly Context _context;

        public TipoUsuarioServices(Context context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TipoUsuarioModel>> GetAll()
        {
            return await _context.TipoUsuario.ToListAsync();
        }

        public async Task<TipoUsuarioModel?> GetById(int id)
        {
            return await _context.TipoUsuario.FindAsync(id);
        }
        public async Task<TipoUsuarioModel> Create(TipoUsuarioModel tipoUsuario)
        {
            _context.TipoUsuario.Add(tipoUsuario);
            await _context.SaveChangesAsync();
            return tipoUsuario;
        }

        public async Task Update(int id, TipoUsuarioModel tipoUsuario)
        {
            var ExisteUsuario = await GetById(id);
            if (ExisteUsuario is not null)
            {
                ExisteUsuario.Name = tipoUsuario.Name;                
                await _context.SaveChangesAsync();
            }
        }


        public async Task Delete(int id)
        {
            var ExisteUsuario = await GetById(id);
            if (ExisteUsuario is not null)
            {
                _context.TipoUsuario.Remove(ExisteUsuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
