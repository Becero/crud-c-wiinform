using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CrudItensWinForms;

public class UsuarioStore
{
    private readonly string _caminhoArquivo;
    private readonly List<UsuarioSistema> _usuarios = [];

    public UsuarioStore(string caminhoArquivo)
    {
        _caminhoArquivo = caminhoArquivo;
        CarregarOuCriarPadrao();
    }

    public bool ValidarCredenciais(string nomeUsuario, string senha, [NotNullWhen(true)] out UsuarioSistema? usuarioAutenticado)
    {
        usuarioAutenticado = null;

        var hash = GerarHash(senha);

        var usuario = _usuarios.FirstOrDefault(u =>
            string.Equals(u.NomeUsuario, nomeUsuario, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(u.SenhaHash, hash, StringComparison.Ordinal));

        if (usuario is null)
        {
            return false;
        }

        usuarioAutenticado = Clonar(usuario);
        return true;
    }

    public bool TrocarSenha(string nomeUsuario, string senhaAtual, string novaSenha)
    {
        var usuario = _usuarios.FirstOrDefault(u =>
            string.Equals(u.NomeUsuario, nomeUsuario, StringComparison.OrdinalIgnoreCase));

        if (usuario is null)
        {
            return false;
        }

        var hashAtual = GerarHash(senhaAtual);

        if (!string.Equals(usuario.SenhaHash, hashAtual, StringComparison.Ordinal))
        {
            return false;
        }

        usuario.SenhaHash = GerarHash(novaSenha);
        Salvar();
        return true;
    }

    public IReadOnlyList<UsuarioSistema> ListarUsuarios()
    {
        return _usuarios
            .OrderBy(u => u.NomeUsuario)
            .Select(Clonar)
            .ToList();
    }

    private void CarregarOuCriarPadrao()
    {
        if (File.Exists(_caminhoArquivo))
        {
            try
            {
                var json = File.ReadAllText(_caminhoArquivo);
                var usuariosCarregados = JsonSerializer.Deserialize<List<UsuarioSistema>>(json) ?? [];

                _usuarios.Clear();
                _usuarios.AddRange(usuariosCarregados.Where(u => !string.IsNullOrWhiteSpace(u.NomeUsuario)));
            }
            catch
            {
                _usuarios.Clear();
            }
        }

        if (_usuarios.Count == 0)
        {
            _usuarios.Add(new UsuarioSistema
            {
                NomeUsuario = "admin",
                SenhaHash = GerarHash("admin123"),
                EhAdmin = true
            });

            _usuarios.Add(new UsuarioSistema
            {
                NomeUsuario = "operador",
                SenhaHash = GerarHash("123456"),
                EhAdmin = false
            });

            Salvar();
        }
    }

    private void Salvar()
    {
        var json = JsonSerializer.Serialize(_usuarios, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_caminhoArquivo, json);
    }

    private static string GerarHash(string senha)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha));
        return Convert.ToHexString(bytes);
    }

    private static UsuarioSistema Clonar(UsuarioSistema usuario)
    {
        return new UsuarioSistema
        {
            NomeUsuario = usuario.NomeUsuario,
            SenhaHash = usuario.SenhaHash,
            EhAdmin = usuario.EhAdmin
        };
    }
}