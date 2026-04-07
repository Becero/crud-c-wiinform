namespace CrudItensWinForms;

public class MovimentoEstoque
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public int ItemId { get; set; }
    public string ItemNome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public string Usuario { get; set; } = string.Empty;
}