namespace ApiAngular.Controllers.RetornoModel;

public class AudioPorIdRetorno
{
    public int Id { get; set; }
    public string ArquivoNome { get; set; } = "";
    public bool TipoEEntrada { get; set; }
    public string OperadorNome { get; set; } = "";
    public DateTime DataCriacao { get; set; }
}