using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace CrudItensWinForms;

public partial class Form1 : Form
{
    private readonly List<ItemCadastro> _itens = [];
    private readonly BindingSource _bindingSource = new();
    private readonly string _caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "itens.json");
    private readonly UsuarioStore _usuarioStore = new(Path.Combine(AppContext.BaseDirectory, "usuarios.json"));
    private UsuarioSistema? _usuarioLogado;
    private int _proximoId = 1;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        ConfigurarGrade();
        CarregarDados();
        AtualizarGrade();

        if (!RealizarLoginObrigatorio())
        {
            Close();
        }
    }

    private bool RealizarLoginObrigatorio()
    {
        using var loginForm = new LoginForm(_usuarioStore);
        var resultado = loginForm.ShowDialog(this);

        if (resultado != DialogResult.OK || loginForm.UsuarioAutenticado is null)
        {
            return false;
        }

        _usuarioLogado = loginForm.UsuarioAutenticado;
        AtualizarContextoUsuario();
        return true;
    }

    private void AtualizarContextoUsuario()
    {
        if (_usuarioLogado is null)
        {
            lblUsuarioLogado.Text = "Usuario: nao autenticado";
            menuAdministracao.Enabled = false;
            return;
        }

        lblUsuarioLogado.Text = $"Usuario: {_usuarioLogado.NomeUsuario} ({(_usuarioLogado.EhAdmin ? "admin" : "operador")})";
        menuAdministracao.Enabled = _usuarioLogado.EhAdmin;
    }

    private void ConfigurarGrade()
    {
        dgvItens.AutoGenerateColumns = false;
        dgvItens.Columns.Clear();

        dgvItens.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ItemCadastro.Id),
            HeaderText = "ID",
            Width = 70
        });

        dgvItens.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ItemCadastro.Nome),
            HeaderText = "Nome",
            Width = 220
        });

        dgvItens.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ItemCadastro.Categoria),
            HeaderText = "Categoria",
            Width = 170
        });

        dgvItens.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ItemCadastro.Quantidade),
            HeaderText = "Quantidade",
            Width = 120
        });

        dgvItens.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ItemCadastro.Preco),
            HeaderText = "Preco",
            Width = 140,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
        });

        _bindingSource.DataSource = new List<ItemCadastro>();
        dgvItens.DataSource = _bindingSource;
    }

    private void btnAdicionar_Click(object sender, EventArgs e)
    {
        if (!ValidarFormulario())
        {
            return;
        }

        var novoItem = new ItemCadastro
        {
            Id = _proximoId++,
            Nome = txtNome.Text.Trim(),
            Categoria = txtCategoria.Text.Trim(),
            Quantidade = (int)nudQuantidade.Value,
            Preco = nudPreco.Value
        };

        _itens.Add(novoItem);
        SalvarDados();
        AtualizarGrade();
        LimparFormulario();
    }

    private void btnAtualizar_Click(object sender, EventArgs e)
    {
        if (!TryGetItemSelecionado(out var itemSelecionado))
        {
            MessageBox.Show("Selecione um item na grade para atualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!ValidarFormulario())
        {
            return;
        }

        itemSelecionado.Nome = txtNome.Text.Trim();
        itemSelecionado.Categoria = txtCategoria.Text.Trim();
        itemSelecionado.Quantidade = (int)nudQuantidade.Value;
        itemSelecionado.Preco = nudPreco.Value;

        SalvarDados();
        AtualizarGrade(itemSelecionado.Id);
    }

    private void btnExcluir_Click(object sender, EventArgs e)
    {
        if (!TryGetItemSelecionado(out var itemSelecionado))
        {
            MessageBox.Show("Selecione um item na grade para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirmacao = MessageBox.Show(
            $"Deseja realmente excluir o item '{itemSelecionado.Nome}'?",
            "Confirmar exclusao",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmacao != DialogResult.Yes)
        {
            return;
        }

        _itens.Remove(itemSelecionado);
        SalvarDados();
        AtualizarGrade();
        LimparFormulario();
    }

    private void btnNovo_Click(object sender, EventArgs e)
    {
        LimparFormulario();
    }

    private void btnDuplicar_Click(object sender, EventArgs e)
    {
        if (!TryGetItemSelecionado(out var itemSelecionado))
        {
            MessageBox.Show("Selecione um item na grade para duplicar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var copia = new ItemCadastro
        {
            Id = _proximoId++,
            Nome = $"{itemSelecionado.Nome} (Copia)",
            Categoria = itemSelecionado.Categoria,
            Quantidade = itemSelecionado.Quantidade,
            Preco = itemSelecionado.Preco
        };

        _itens.Add(copia);
        SalvarDados();
        AtualizarGrade(copia.Id);
    }

    private void btnExportarCsv_Click(object sender, EventArgs e)
    {
        ExportarCsv(ObterItensFiltrados().ToList(), "itens");
    }

    private void menuDashboard_Click(object sender, EventArgs e)
    {
        var quantidadeTotal = _itens.Sum(item => item.Quantidade);
        var valorTotal = _itens.Sum(item => item.Quantidade * item.Preco);

        MessageBox.Show(
            $"Itens cadastrados: {_itens.Count}\nQuantidade em estoque: {quantidadeTotal}\nValor total em estoque: {valorTotal:C2}",
            "Dashboard",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void menuCadastrosItens_Click(object sender, EventArgs e)
    {
        txtNome.Focus();
    }

    private void menuCadastrosCategorias_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Cadastro de categorias pode ser o proximo modulo a implementar.", "Cadastros", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void menuMovEntrada_Click(object sender, EventArgs e)
    {
        if (!TryGetItemSelecionado(out var itemSelecionado))
        {
            MessageBox.Show("Selecione um item na grade para registrar entrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!QuantidadeDialog.TryObterQuantidade(this, "Entrada de estoque", "Quantidade a adicionar:", out var quantidade))
        {
            return;
        }

        itemSelecionado.Quantidade += quantidade;
        SalvarDados();
        AtualizarGrade(itemSelecionado.Id);
    }

    private void menuMovSaida_Click(object sender, EventArgs e)
    {
        if (!TryGetItemSelecionado(out var itemSelecionado))
        {
            MessageBox.Show("Selecione um item na grade para registrar saida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!QuantidadeDialog.TryObterQuantidade(this, "Saida de estoque", "Quantidade a retirar:", out var quantidade))
        {
            return;
        }

        if (quantidade > itemSelecionado.Quantidade)
        {
            MessageBox.Show("A quantidade informada e maior que o estoque atual.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        itemSelecionado.Quantidade -= quantidade;
        SalvarDados();
        AtualizarGrade(itemSelecionado.Id);
    }

    private void menuFinanceiroResumo_Click(object sender, EventArgs e)
    {
        using var form = new FinanceiroForm(_itens);
        form.ShowDialog(this);
    }

    private void menuRelEstoqueBaixo_Click(object sender, EventArgs e)
    {
        var itensBaixoEstoque = _itens
            .Where(item => item.Quantidade <= 5)
            .OrderBy(item => item.Quantidade)
            .ThenBy(item => item.Nome)
            .ToList();

        using var form = new RelatorioEstoqueBaixoForm(itensBaixoEstoque);
        form.ShowDialog(this);
    }

    private void menuRelExportarCsv_Click(object sender, EventArgs e)
    {
        ExportarCsv(_itens.OrderBy(item => item.Id).ToList(), "inventario_completo");
    }

    private void menuUsuarioTrocarSenha_Click(object sender, EventArgs e)
    {
        if (_usuarioLogado is null)
        {
            MessageBox.Show("Nenhum usuario autenticado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var form = new TrocaSenhaForm(_usuarioStore, _usuarioLogado.NomeUsuario);
        form.ShowDialog(this);
    }

    private void menuUsuarioLogout_Click(object sender, EventArgs e)
    {
        _usuarioLogado = null;
        AtualizarContextoUsuario();

        if (!RealizarLoginObrigatorio())
        {
            Close();
        }
    }

    private void menuAdmUsuarios_Click(object sender, EventArgs e)
    {
        var usuarios = _usuarioStore.ListarUsuarios();

        if (usuarios.Count == 0)
        {
            MessageBox.Show("Nenhum usuario cadastrado.", "Administracao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var resumo = string.Join(Environment.NewLine, usuarios.Select(usuario =>
            $"- {usuario.NomeUsuario} ({(usuario.EhAdmin ? "admin" : "operador")})"));

        MessageBox.Show($"Usuarios cadastrados:\n\n{resumo}", "Administracao", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ExportarCsv(IReadOnlyCollection<ItemCadastro> itensParaExportar, string prefixo)
    {
        if (itensParaExportar.Count == 0)
        {
            MessageBox.Show("Nao ha itens para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Filter = "Arquivo CSV (*.csv)|*.csv",
            FileName = $"{prefixo}_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
            Title = "Exportar itens"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        try
        {
            var linhas = new List<string> { "Id;Nome;Categoria;Quantidade;Preco" };
            linhas.AddRange(itensParaExportar.Select(item =>
                $"{item.Id};{EscaparCsv(item.Nome)};{EscaparCsv(item.Categoria)};{item.Quantidade};{item.Preco.ToString("0.00", CultureInfo.CurrentCulture)}"));

            File.WriteAllLines(dialog.FileName, linhas, Encoding.UTF8);
            MessageBox.Show($"Exportacao concluida:\n{dialog.FileName}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Falha ao exportar CSV: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static string EscaparCsv(string valor)
    {
        if (valor.Contains(';') || valor.Contains('"') || valor.Contains('\n') || valor.Contains('\r'))
        {
            return $"\"{valor.Replace("\"", "\"\"")}\"";
        }

        return valor;
    }

    private void txtBusca_TextChanged(object sender, EventArgs e)
    {
        AtualizarGrade();
    }

    private void btnLimparBusca_Click(object sender, EventArgs e)
    {
        txtBusca.Clear();
        txtBusca.Focus();
    }

    private void dgvItens_SelectionChanged(object sender, EventArgs e)
    {
        if (!TryGetItemSelecionado(out var itemSelecionado))
        {
            return;
        }

        txtNome.Text = itemSelecionado.Nome;
        txtCategoria.Text = itemSelecionado.Categoria;
        nudQuantidade.Value = itemSelecionado.Quantidade;
        nudPreco.Value = itemSelecionado.Preco;
    }

    private void dgvItens_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvItens.Rows.Count)
        {
            return;
        }

        if (dgvItens.Rows[e.RowIndex].DataBoundItem is not ItemCadastro item)
        {
            return;
        }

        dgvItens.Rows[e.RowIndex].DefaultCellStyle.BackColor = item.Quantidade <= 5 ? Color.MistyRose : Color.White;
    }

    private bool ValidarFormulario()
    {
        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            MessageBox.Show("Informe o nome do item.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtNome.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtCategoria.Text))
        {
            MessageBox.Show("Informe a categoria.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtCategoria.Focus();
            return false;
        }

        return true;
    }

    private bool TryGetItemSelecionado([NotNullWhen(true)] out ItemCadastro? item)
    {
        item = null;

        if (dgvItens.CurrentRow?.DataBoundItem is ItemCadastro selecionado)
        {
            item = selecionado;
            return true;
        }

        return false;
    }

    private void LimparFormulario()
    {
        txtNome.Clear();
        txtCategoria.Clear();
        nudQuantidade.Value = 0;
        nudPreco.Value = 0;
        dgvItens.ClearSelection();
        txtNome.Focus();
    }

    private IEnumerable<ItemCadastro> ObterItensFiltrados()
    {
        var filtro = txtBusca.Text.Trim();
        IEnumerable<ItemCadastro> consulta = _itens;

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            consulta = consulta.Where(item =>
                item.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                item.Categoria.Contains(filtro, StringComparison.OrdinalIgnoreCase));
        }

        return consulta.OrderBy(item => item.Id);
    }

    private void AtualizarGrade(int? idSelecionado = null)
    {
        var itensFiltrados = ObterItensFiltrados().ToList();

        _bindingSource.DataSource = itensFiltrados;
        AtualizarResumo(itensFiltrados);

        if (idSelecionado.HasValue)
        {
            SelecionarItemPorId(idSelecionado.Value);
        }
    }

    private void AtualizarResumo(IReadOnlyCollection<ItemCadastro> itensFiltrados)
    {
        var quantidadeTotal = _itens.Sum(item => item.Quantidade);
        var quantidadeFiltrada = itensFiltrados.Sum(item => item.Quantidade);

        var valorTotal = _itens.Sum(item => item.Quantidade * item.Preco);
        var valorFiltrado = itensFiltrados.Sum(item => item.Quantidade * item.Preco);

        lblResumoItens.Text = $"Itens: {_itens.Count} (exibidos: {itensFiltrados.Count})";
        lblResumoQuantidade.Text = $"Qtd estoque: {quantidadeTotal} (filtro: {quantidadeFiltrada})";
        lblResumoValor.Text = $"Valor estoque: {valorTotal:C2} (filtro: {valorFiltrado:C2})";
    }

    private void SelecionarItemPorId(int id)
    {
        foreach (DataGridViewRow row in dgvItens.Rows)
        {
            if (row.DataBoundItem is ItemCadastro item && item.Id == id)
            {
                row.Selected = true;
                dgvItens.CurrentCell = row.Cells[0];
                return;
            }
        }
    }

    private void CarregarDados()
    {
        if (!File.Exists(_caminhoArquivo))
        {
            return;
        }

        try
        {
            var json = File.ReadAllText(_caminhoArquivo);
            var itensCarregados = JsonSerializer.Deserialize<List<ItemCadastro>>(json) ?? [];

            _itens.Clear();
            _itens.AddRange(itensCarregados);
            _proximoId = _itens.Count == 0 ? 1 : _itens.Max(i => i.Id) + 1;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Falha ao carregar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SalvarDados()
    {
        try
        {
            var json = JsonSerializer.Serialize(_itens.OrderBy(item => item.Id), new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_caminhoArquivo, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Falha ao salvar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}