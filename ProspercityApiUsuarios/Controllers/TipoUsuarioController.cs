using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProspercityApiUsuarios.Data;
using ProspercityApiUsuarios.Models;
using ProspercityApiUsuarios.Services;

namespace ProspercityApiUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {       
        private readonly TipoUsuarioServices _tipouserService;

        public TipoUsuarioController(Context context, TipoUsuarioServices tipouserService)
        {            
            _tipouserService = tipouserService;
        }

        // GET: api/TipoUsuarioModels
        [HttpGet]
        public async Task<IEnumerable<TipoUsuarioModel>> GetTipoUsuarios()
        {
            return await _tipouserService.GetAll();
        }

        // GET: api/TipoUsuarioModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuarioModel>> GetTipoUsuarioModel(int id)
        {
            var tipoUser= await _tipouserService.GetById(id);
            if(tipoUser == null)
            {
                return UserNotFound(id);
            }

            return tipoUser;
        }

        // PUT: api/TipoUsuarioModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUsuarioModel(int id, TipoUsuarioModel tipoUsuarioModel)
        {
            if (id != tipoUsuarioModel.Id)
            {
                return BadRequest(new
                {
                    message = $"El ID =({id}) de la url no coincide con el id ({tipoUsuarioModel.Id}) del cuerpo de la solicitud"
                });
            }

            var existeUser = await _tipouserService.GetById(id);
            if (existeUser is not null)
            {
                await _tipouserService.Update(id, tipoUsuarioModel);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/TipoUsuarioModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoUsuarioModel>> PostTipoUsuarioModel(TipoUsuarioModel tipoUsuarioModel)
        {
            var newTipoUser = await _tipouserService.Create(tipoUsuarioModel);
            return CreatedAtAction(nameof(GetTipoUsuarioModel), new { id = newTipoUser.Id }, newTipoUser);
        }

        // DELETE: api/TipoUsuarioModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoUsuarioModel(int id)
        {
            var existeTipoUser = await _tipouserService.GetById(id);

            if (existeTipoUser is not null)
            {
                await _tipouserService.Delete(id);
                return Ok();
            }
            else
            {
                return UserNotFound(id);
            }
        }       
        public NotFoundObjectResult UserNotFound(int id)
        {
            return NotFound(new { message = $"El Tipo de usuario  con ID={id} no existe" });
        }
    }
}
