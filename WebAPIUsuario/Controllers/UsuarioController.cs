using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIUsuario.DTOs;
using WebAPIUsuario.Services;

namespace WebAPIUsuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDto>> SaveUsuario(CreateUsuarioDto usuarioDto)
        {
            var usuarioCreado = await _usuarioService.CreateUsuarioAsync(usuarioDto);
            return StatusCode(StatusCodes.Status201Created, usuarioCreado);
        }

        [HttpPut("/api/[controller]/{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> UpdateUsuario(int id, UpdateUsuarioDto usuarioDto)
        {
            var usuarioActualizado = await _usuarioService.UpdateUsuarioAsync(id, usuarioDto);
            if (usuarioActualizado == null)
            {
                return NotFound();
            }

            return Ok(usuarioActualizado);
        }

        [HttpDelete("/api/[controller]/{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var eliminado = await _usuarioService.DeleteUsuarioAsync(id);
            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> FindById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }
    }
}
