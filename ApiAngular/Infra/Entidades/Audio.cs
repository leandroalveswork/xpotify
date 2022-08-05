namespace ApiAngular.Infra.Entidades;

public class Audio
{
    public int Id { get; set; }
    public string ArquivoNome { get; set; } = "";
    public bool TipoEEntrada { get; set; }
    public string OperadorNome { get; set; } = "";
    public DateTime DataCriacao { get; set; }
}