
namespace MeuProjetoApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MeuProjetoApi.Models;
    using MeuProjetoApi.Services;
    using MeuProjetoApi.ReadDto;
    using MeuProjetoApi.UpdateDto;
    using Supabase.Postgrest.Exceptions;

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly BancoDados _db;

        public UsersController(BancoDados db)
        {
            _db = db;
        }

        //rota Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
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
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
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

        [HttpPost("{userDto}")]
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

        [HttpPut("{chave}")]
        public async Task<IActionResult> PutUser(int chave, [FromBody] UpdateDto dados)
        {
            try
            {
                await _db.Atualizar(chave, dados.Onde, dados.NovoValor);
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