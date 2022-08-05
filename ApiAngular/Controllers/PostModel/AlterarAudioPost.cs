namespace ApiAngular.Controllers.PostModel;

public class AlterarAudioPost
{
    public int Id { get; set; }
    public string ArquivoNome { get; set; } = "";
    public bool TipoEEntrada { get; set; }
    public string OperadorNome { get; set; } = "";
}