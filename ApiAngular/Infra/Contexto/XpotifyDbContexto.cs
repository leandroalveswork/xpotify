using ApiAngular.Infra.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiAngular.Infra.Contexto;

public class XpotifyDbContexto : DbContext
{
    public XpotifyDbContexto(DbContextOptions<XpotifyDbContexto> options)
        : base(options)
    {
    }

    public DbSet<Audio> Audios { get; set; }
}