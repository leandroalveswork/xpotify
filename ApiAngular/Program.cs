using ApiAngular.Infra.Contexto;
using ApiAngular.Infra.Entidades;
using ApiAngular.Servicos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var estaConectadoABancoInMemory = builder.Configuration.GetValue<bool>("EstaConectadoABancoInMemory");
if (estaConectadoABancoInMemory)
{
    builder.Services.AddDbContext<XpotifyDbContexto>(
        options => options.UseInMemoryDatabase("XpotifyDb"));
}
else
{
    builder.Services.AddDbContext<XpotifyDbContexto>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("XpotifyDb")));
}
builder.Services.AddScoped<AudioServico>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

if (estaConectadoABancoInMemory)
{
    using (var scope = app.Services.CreateScope())
    {
        var audioServico = scope.ServiceProvider.GetRequiredService<AudioServico>();
        await audioServico.IncluirAsync(new Audio { Id = 101, ArquivoNome = "Sweather heather.wmv", TipoEEntrada = false, OperadorNome = "Leandro" });
        await audioServico.IncluirAsync(new Audio { Id = 102, ArquivoNome = "Jenny.wmv", TipoEEntrada = false, OperadorNome = "Leandro" });
        await audioServico.IncluirAsync(new Audio { Id = 103, ArquivoNome = "Whats gravar.wmv", TipoEEntrada = true, OperadorNome = "Roberta" });
        await audioServico.IncluirAsync(new Audio { Id = 104, ArquivoNome = "Playlist Casamento.wmv", TipoEEntrada = false, OperadorNome = "Paulo" });
    }
}

app.Run();
