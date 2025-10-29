using MeuProjetoApi.Services;

var builder = WebApplication.CreateBuilder(args);

var url = builder.Configuration["SUPABASE_URL"]?? "https://cqcsveiymkeropkxpyzx.supabase.co";
var key = builder.Configuration["SUPABASE_KEY"] ?? "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNxY3N2ZWl5bWtlcm9wa3hweXp4Iiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTc2MDc4MjcyNywiZXhwIjoyMDc2MzU4NzI3fQ.P_zFbNojpENE7mGXu9hgs4EaRECljhIKR7_nFVgnhbg";

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    // Definindo a política de CORS chamada "PermitirFront"
    options.AddPolicy("PermitirFront", 
        builder =>
        {
            // Este exemplo permite QUALQUER origem, método e cabeçalho.
            // Em produção, substitua AllowAnyOrigin() pelo endereço específico do seu frontend:
            // .WithOrigins("https://meufrontend.com", "http://localhost:3000")
            builder.AllowAnyOrigin() 
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddSingleton <Supabase.Client>(provider =>
{
    var options = new Supabase.SupabaseOptions { AutoConnectRealtime = true };

    var supabaseClient = new Supabase.Client(url, key, options);

    supabaseClient.InitializeAsync().Wait();

    return supabaseClient;
});

builder.Services.AddScoped<BancoDados>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirFront");


app.UseRouting();

app.MapControllers();

app.Run();
