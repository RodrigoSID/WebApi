
namespace MeuProjetoApi.Services
{
    using System.Threading.Tasks;
    using Supabase.Postgrest.Attributes;
    using Supabase.Postgrest.Models;
    using MeuProjetoApi.ReadDto;
    using System.Linq;


    public class BancoDados
    {
        private Supabase.Client _supabase;

        public BancoDados(Supabase.Client supabaseClient)
        {
            _supabase = supabaseClient;
        }

        public async Task<List<UserResponseDto>> MostrarTodos()
        {
            var resultado = await _supabase.From<User>().Get();

            var listaDeDtos = resultado.Models.Select(user => new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Bio = user.Bio,
                Telefone = user.Telefone
            }).ToList();
            return listaDeDtos;
        }

        public async Task<UserResponseDto> Mostrar(int chave)
        {
            var usuario = await _supabase.From<User>().Where(x => x.Id == chave).Single();
            var dto = new UserResponseDto
            {
                Id = usuario.Id,
                Name = usuario.Name,
                Bio = usuario.Bio,
                Telefone = usuario.Telefone
            };
            return dto;
        }

        public async Task Inserir(string nome, string bio, string cell)
        {
            var novo = new User
            {
                Name = nome,
                Bio = bio,
                Telefone = cell
            };
            await _supabase.From<User>().Insert(novo);
        }

        public async Task<List<User>> Atualizar(int chave, int onde, string novoValor)
        {
            List<User> saida = null;
            if (onde == 1)
            {
                var resultado = await _supabase.From<User>().Where(x => x.Id == chave).Set(x =>x.Name, novoValor).Update();
                saida = resultado.Models;
            }
            else if (onde == 2)
            {
                var resultado = await _supabase.From<User>().Where(x => x.Id == chave).Set(x => x.Bio, novoValor).Update();
                saida = resultado.Models;
            }
            else if (onde == 3)
            {
                var resultado = await _supabase.From<User>().Where(x => x.Id == chave).Set(x => x.Telefone, novoValor).Update();
                saida = resultado.Models;
            }
            return saida;
        }

        public async Task Deletar(int chave)
        {
            await _supabase.From<User>().Where(x => x.Id == chave).Delete();
        }

    }

    [Table("user")]
    public class User : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("bio")]
        public string Bio { get; set; }

        [Column("cell")]
        public string Telefone { get; set; }
    }
}