namespace CrudItensWinForms;

public class UsuarioSistema
{
    public string NomeUsuario { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public bool EhAdmin { get; set; }
}