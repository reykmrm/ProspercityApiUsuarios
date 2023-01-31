using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProspercityApiUsuarios.ClasesAux;
using ProspercityApiUsuarios.Models;
using ProspercityApiUsuarios.Models.DTO;
using ProspercityApiUsuarios.Services;

namespace ProspercityApiUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioServices _UsuarioService;

        public UsuariosController(UsuarioServices usuarioService)
        {
            _UsuarioService = usuarioService;
        }

        [HttpPost("recuperarContrasena")]
        public async Task<dynamic> RecuperarContrasena(DTOEmail email)
        {
            UsuarioModel emailExiste = await _UsuarioService.GetEmail(email.Email);
            if(emailExiste == null)
            {
                return new { result = "correo no encontrado" };
            }
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var clave = new String(Charsarr);

            EncriptarClave encriptarClave = new EncriptarClave();

            emailExiste.Contrasena = encriptarClave.Encriptar(clave);

            var resultado = await _UsuarioService.Update(emailExiste);
            if(resultado == null)
            {
                return new { result = "No se pudo generar una nueva contraseña" };
            }

            Correos nuevoCorreo = new Correos();
            var asunto = "Recuperacion de contraseña";
            var mensajeCorreo = "Su nuevas credenciales son:\n Correo: " + emailExiste.Email + " \nSu contraseña: " + clave + " \n No olvide cambiar su contraseña lo antes posible ";
            nuevoCorreo.enviarEMail(emailExiste.Email, mensajeCorreo, asunto);

            return new { result = "Nueva contraseña generada por favor revise su correo" }; ;
        }

        [HttpPost("register")]
        public async Task<IEnumerable<UserDTO>> GetUsuarios(DTOEmail email)
        {
            return await _UsuarioService.GetAll(email);
        }

        // GET: api/TipoUsuarioModels/5
        [HttpPost("GetUsuarioId")]
        public async Task<ActionResult<UsuarioModel>> GetUsuario(AuxId auxId)
        {
            int id = auxId.Id;
            var User = await _UsuarioService.GetById(id);
            if (User == null)
            {
                return await UserNotFound(id);
            }

            return User;
        }

        // PUT: api/TipoUsuarioModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("update")]
        public async Task<dynamic> ActualizarUsuario( UsuarioModel Usuario)
        {
            var id = Usuario.Id;
            var existeUser = await _UsuarioService.GetById(id);
            if (existeUser is not null)
            {
                return await _UsuarioService.Update(Usuario);    
            }
            else
            {
                return new { result = "No se puede actualizar el usuario" };
            }
        }

        // POST: api/TipoUsuarioModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<dynamic> CrearUsuarioModel(UsuarioModel Usuario)
        {
            var newUser = await _UsuarioService.Create(Usuario);
            return newUser;
        }

        [HttpPost("login")]
        public async Task<dynamic> Login(DTOLogin login)
        {
            return await _UsuarioService.Login(login);

        }

        // DELETE: api/TipoUsuarioModels/5
        [HttpPost("delete")]
        public async Task<dynamic> DeleteUsuarioModel(AuxId auxId)
        {
            var id = auxId.Id;
            var existeUser = await _UsuarioService.GetById(id);

            if (existeUser is not null)
            {
                return await _UsuarioService.Delete(id);
            }
            else
            {
                return UserNotFound(id);
            }
        }
        public async Task<dynamic> UserNotFound(int id)
        {
            return new { resul = $"El Tipo de usuario  con ID={id} no existe" };
        }

    }
}
