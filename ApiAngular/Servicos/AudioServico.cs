using ApiAngular.Infra.Contexto;
using ApiAngular.Infra.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiAngular.Servicos;

public class AudioServico
{
    private readonly XpotifyDbContexto _contexto;
    public AudioServico(XpotifyDbContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Audio>> ListarAsync()
    {
        return await _contexto.Audios
            .OrderBy(x => x.ArquivoNome)
            .ToListAsync();
    }

    public async Task<List<Audio>> ListarPorArquivoNomeAsync(string arquivoNome)
    {
        return await _contexto.Audios
            .AsNoTracking()
            .Where(x => string.IsNullOrEmpty(arquivoNome) || x.ArquivoNome.Contains(arquivoNome))
            .OrderBy(x => x.ArquivoNome)
            .ToListAsync();
    }

    public async Task IncluirAsync(Audio audio)
    {
        audio.DataCriacao = DateTime.Now;
        await _contexto.AddAsync(audio);
        await _contexto.SaveChangesAsync();
    }

    public async Task<Audio> ObterPorIdAsync(int id)
    {
        return await _contexto.Audios.AsNoTracking().FirstAsync(x => x.Id == id);
    }
    
    public async Task AlterarAsync(Audio audio)
    {
        var idDoAudio = audio.Id;
        audio.Id = 0;
        var audioDoBanco = await _contexto.Audios.FirstAsync(x => x.Id == idDoAudio);
        audioDoBanco.ArquivoNome = audio.ArquivoNome;
        audioDoBanco.TipoEEntrada = audio.TipoEEntrada;
        audioDoBanco.OperadorNome = audio.OperadorNome;
        _contexto.Update(audioDoBanco);
        await _contexto.SaveChangesAsync();
    }

    public async Task ExcluirAsync(int id)
    {
        var audioDoBanco = await _contexto.Audios.FirstAsync(x => x.Id == id);
        _contexto.Remove(audioDoBanco);
        await _contexto.SaveChangesAsync();
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _contexto.Audios.AnyAsync(x => x.Id == id);
    }
}