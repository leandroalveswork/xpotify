using ApiAngular.Controllers.PostModel;
using ApiAngular.Controllers.RetornoModel;
using ApiAngular.Infra.Entidades;
using ApiAngular.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace ApiAngular.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AudioController : ControllerBase
{
    private readonly AudioServico _audioServico;
    public AudioController(AudioServico audioServico)
    {
        _audioServico = audioServico;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<List<AudioListadoRetorno>> Listar()
    {
        return (await _audioServico.ListarAsync())
            .Select(audio => new AudioListadoRetorno
            {
                Id = audio.Id,
                ArquivoNome = audio.ArquivoNome,
                TipoEEntrada = audio.TipoEEntrada,
                OperadorNome = audio.OperadorNome,
                DataCriacao = audio.DataCriacao
            })
            .ToList();
    }

    [HttpGet]
    [Route("listarPorArquivoNome")]
    public async Task<List<AudioListadoRetorno>> ListarPorArquivoNome(string arquivoNome)
    {
        return (await _audioServico.ListarPorArquivoNomeAsync(arquivoNome))
            .Select(audio => new AudioListadoRetorno
            {
                Id = audio.Id,
                ArquivoNome = audio.ArquivoNome,
                TipoEEntrada = audio.TipoEEntrada,
                OperadorNome = audio.OperadorNome,
                DataCriacao = audio.DataCriacao
            })
            .ToList();
    }

    [HttpPost]
    [Route("incluir")]
    public async Task Incluir([FromBody]NovoAudioPost audio)
    {
        var audioComoEntidade = new Audio
        {
            ArquivoNome = audio.ArquivoNome,
            TipoEEntrada = audio.TipoEEntrada,
            OperadorNome = audio.OperadorNome
        };
        await _audioServico.IncluirAsync(audioComoEntidade);
    }

    [HttpGet]
    [Route("obterPorId")]
    public async Task<AudioPorIdRetorno> ObterPorId(int id)
    {
        var audioDoBanco = await _audioServico.ObterPorIdAsync(id);
        return new AudioPorIdRetorno
        {
            Id = audioDoBanco.Id,
            ArquivoNome = audioDoBanco.ArquivoNome,
            TipoEEntrada = audioDoBanco.TipoEEntrada,
            OperadorNome = audioDoBanco.OperadorNome,
            DataCriacao = audioDoBanco.DataCriacao
        };
    }

    [HttpPost]
    [Route("alterar")]
    public async Task Alterar([FromBody]AlterarAudioPost audio)
    {
        var audioComoEntidade = new Audio
        {
            Id = audio.Id,
            ArquivoNome = audio.ArquivoNome,
            TipoEEntrada = audio.TipoEEntrada,
            OperadorNome = audio.OperadorNome
        };
        await _audioServico.AlterarAsync(audioComoEntidade);
    }

    [HttpGet]
    [Route("excluir")]
    public async Task Excluir(int id)
    {
        await _audioServico.ExcluirAsync(id);
    }

    [HttpGet]
    [Route("existe")]
    public async Task<bool> Existe(int id)
    {
        return await _audioServico.ExisteAsync(id);
    }
}
