using MeuProjetoApi.Services;

var builder = WebApplication.CreateBuilder(args);

var url = builder.Configuration["SUPABASE_URL"]?? "https://cqcsveiymkeropkxpyzx.supabase.co";
var key = builder.Configuration["SUPABASE_KEY"] ?? "";

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    // Definindo a política de CORS chamada "PermitirFront"
    options.AddPolicy("PermitirFront", 
        builder =>
        {
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
