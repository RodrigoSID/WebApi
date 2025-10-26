using MeuProjetoApi.Services;

var builder = WebApplication.CreateBuilder(args);

var url = builder.Configuration["SUPABASE_URL"]?? "https://cqcsveiymkeropkxpyzx.supabase.co";
var key = builder.Configuration["SUPABASE_KEY"] ?? "";

builder.Services.AddControllers();

builder.Services.AddSingleton <Supabase.Client>(provider =>
{
    var options = new Supabase.SupabaseOptions { AutoConnectRealtime = true };

    var supabaseClient = new Supabase.Client(url, key, options);

    supabaseClient.InitializeAsync().Wait();

    return supabaseClient;
});

builder.Services.AddScoped<BancoDados>();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();
