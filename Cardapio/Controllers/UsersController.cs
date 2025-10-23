
namespace MeuProjetoApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MeuProjetoApi.Models;
    using MeuProjetoApi.Services;
    using Supabase.Postgrest.Exceptions;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BancoDados _db;

        public UserController(BancoDados db)
        {
            _db = db;
        }

        //rota Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            try
            {
                var users = await _db.MostrarTodos();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar todos os usuarios: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _db.Mostrar(id);
                return Ok(user);
            }
            catch (PostgrestException)
            {
                return NotFound($"Usuario com ID {id} nao encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar usuario: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] UserCreationDto userDto)
        {
            try
            {
                await _db.Inserir(userDto.name, userDto.bio, userDto.Telefone);
                return StatusCode(201, "Usuario Criado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar usuario: {ex.Message}");
            }
        }

        [HttpPut("{nomeAntigo}")]
        public async Task<IActionResult> PutUser(string nomeAntigo, [FromBody] string novoNome)
        {
            try
            {
                await _db.Atualizar(nomeAntigo, novoNome);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar usuário: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _db.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar usuário: {ex.Message}");
            }
        }

    }
    
}