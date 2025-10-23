
using System.Threading.Tasks;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MeuProjetoApi.Services
{


    public class BancoDados
    {
        private Supabase.Client _supabase;

        public BancoDados(Supabase.Client supabaseClient)
        {
            _supabase = supabaseClient;
        }

        public async Task<List<User>> MostrarTodos()
        {
            var resultado = await _supabase.From<User>().Get();
            return resultado.Models;
        }

        public async Task<User> Mostrar(int chave)
        {
            var usuario = await _supabase.From<User>().Where(x => x.Id == chave).Single();
            return usuario;
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

        public async Task Atualizar(string nome, string subistituir)
        {
            var novo = await _supabase.From<User>().Where(x => x.Name == nome).Set(x => x.Name, subistituir).Update();
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