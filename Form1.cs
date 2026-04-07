using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace CrudItensWinForms;

public partial class Form1 : Form
{
    private readonly List<ItemCadastro> _itens = [];
    private readonly List<MovimentoEstoque> _movimentos = [];
    private readonly BindingSource _bindingSource = new();

    private readonly string _caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "itens.json");
    private readonly string _caminhoMovimentos = Path.Combine(AppContext.BaseDirectory, "movimentos.json");
    private readonly UsuarioStore _usuarioStore = new(Path.Combine(AppContext.BaseDirectory, "usuarios.json"));

    private UsuarioSistema? _usuarioLogado;
    private int _proximoId = 1;
    private int _proximoMovimentoId = 1;

    private readonly Panel _panelTelaLista = new();
    private readonly Label _lblTelaListaTitulo = new();
    private readonly DataGridView _dgvTelaLista = new();
    private readonly Button _btnVoltarItens = new();

    public Form1()
    {
        InitializeComponent();
        InicializarTelaLista();
        ConfigurarEventosNavegacaoMenu();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        ConfigurarGrade();
        CarregarDados();
        CarregarMovimentos();
        AtualizarGrade();

        if (!RealizarLoginObrigatorio())
        {
            Close();
            return;
        }

        ExibirDashboard();
    }

    private void InicializarTelaLista()
    {
        _panelTelaLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _panelTelaLista.BackColor = Color.WhiteSmoke;
        _panelTelaLista.Location = new Point(12, 36);
        _panelTelaLista.Name = "panelTelaLista";
        _panelTelaLista.Size = new Size(860, 503);
        _panelTelaLista.Visible = false;

        _lblTelaListaTitulo.AutoSize = true;
        _lblTelaListaTitulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        _lblTelaListaTitulo.Location = new Point(16, 14);
        _lblTelaListaTitulo.Text = "Tela";

        _btnVoltarItens.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnVoltarItens.BackColor = Color.SteelBlue;
        _btnVoltarItens.FlatStyle = FlatStyle.Flat;
        _btnVoltarItens.ForeColor = Color.White;
        _btnVoltarItens.Location = new Point(714, 12);
        _btnVoltarItens.Size = new Size(130, 32);
        _btnVoltarItens.Text = "Voltar p/ Itens";
        _btnVoltarItens.UseVisualStyleBackColor = false;
        _btnVoltarItens.Click += (_, _) => MostrarTelaCrudItens();

        _dgvTelaLista.AllowUserToAddRows = false;
        _dgvTelaLista.AllowUserToDeleteRows = false;
        _dgvTelaLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _dgvTelaLista.BackgroundColor = Color.White;
        _dgvTelaLista.Location = new Point(16, 58);
        _dgvTelaLista.MultiSelect = false;
        _dgvTelaLista.ReadOnly = true;
        _dgvTelaLista.RowHeadersVisible = false;
        _dgvTelaLista.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvTelaLista.Size = new Size(828, 429);

        _panelTelaLista.Controls.Add(_lblTelaListaTitulo);
        _panelTelaLista.Controls.Add(_btnVoltarItens);
        _panelTelaLista.Controls.Add(_dgvTelaLista);

        Controls.Add(_panelTelaLista);
    }

    private void ConfigurarEventosNavegacaoMenu()
    {
        menuCadastros.Click += (_, _) => ExibirCategorias();
        menuMovimentacoes.Click += (_, _) => ExibirMovimentacoes();
        menuFinanceiro.Click += (_, _) => ExibirFinanceiro();
        menuRelatorios.Click += (_, _) => ExibirInventarioCompleto();
        menuUsuario.Click += (_, _) => ExibirSessaoUsuario();
        menuAdministracao.Click += (_, _) => ExibirUsuariosAdmin();
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

    private string UsuarioAtual => _usuarioLogado?.NomeUsuario ?? "sistema";

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
        RegistrarMovimento("Cadastro", novoItem, novoItem.Quantidade);
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

        RegistrarMovimento("Edicao", itemSelecionado, itemSelecionado.Quantidade);
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

        RegistrarMovimento("Exclusao", itemSelecionado, itemSelecionado.Quantidade);
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
        RegistrarMovimento("Duplicacao", copia, copia.Quantidade);
        SalvarDados();
        AtualizarGrade(copia.Id);
    }

    private void btnExportarCsv_Click(object sender, EventArgs e)
    {
        ExportarCsv(ObterItensFiltrados().ToList(), "itens");
    }

    private void menuDashboard_Click(object sender, EventArgs e)
    {
        ExibirDashboard();
    }

    private void menuCadastrosItens_Click(object sender, EventArgs e)
    {
        MostrarTelaCrudItens();
    }

    private void menuCadastrosCategorias_Click(object sender, EventArgs e)
    {
        ExibirCategorias();
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
        RegistrarMovimento("Entrada", itemSelecionado, quantidade);
        SalvarDados();
        AtualizarGrade(itemSelecionado.Id);
        ExibirMovimentacoes();
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
        RegistrarMovimento("Saida", itemSelecionado, quantidade);
        SalvarDados();
        AtualizarGrade(itemSelecionado.Id);
        ExibirMovimentacoes();
    }

    private void menuFinanceiroResumo_Click(object sender, EventArgs e)
    {
        ExibirFinanceiro();
    }

    private void menuRelEstoqueBaixo_Click(object sender, EventArgs e)
    {
        ExibirEstoqueBaixo();
    }

    private void menuRelExportarCsv_Click(object sender, EventArgs e)
    {
        ExibirInventarioCompleto();
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
            return;
        }

        ExibirDashboard();
    }

    private void menuAdmUsuarios_Click(object sender, EventArgs e)
    {
        ExibirUsuariosAdmin();
    }

    private void ExibirDashboard()
    {
        var quantidadeTotal = _itens.Sum(item => item.Quantidade);
        var valorTotal = _itens.Sum(item => item.Quantidade * item.Preco);

        var dados = new List<DashboardLinha>
        {
            new() { Indicador = "Itens cadastrados", Valor = _itens.Count.ToString(CultureInfo.CurrentCulture) },
            new() { Indicador = "Quantidade em estoque", Valor = quantidadeTotal.ToString(CultureInfo.CurrentCulture) },
            new() { Indicador = "Valor total em estoque", Valor = valorTotal.ToString("C2", CultureInfo.CurrentCulture) },
            new() { Indicador = "Itens com estoque baixo (<=5)", Valor = _itens.Count(item => item.Quantidade <= 5).ToString(CultureInfo.CurrentCulture) },
            new() { Indicador = "Ultimo acesso", Valor = DateTime.Now.ToString("g", CultureInfo.CurrentCulture) }
        };

        MostrarTelaLista("Dashboard", dados);
    }

    private void ExibirCategorias()
    {
        var dados = _itens
            .GroupBy(item => string.IsNullOrWhiteSpace(item.Categoria) ? "(sem categoria)" : item.Categoria)
            .Select(grupo => new CategoriaLinha
            {
                Categoria = grupo.Key,
                Itens = grupo.Count(),
                QuantidadeTotal = grupo.Sum(i => i.Quantidade),
                ValorTotal = grupo.Sum(i => i.Quantidade * i.Preco)
            })
            .OrderBy(linha => linha.Categoria)
            .ToList();

        MostrarTelaLista("Cadastros - Categorias", dados);
    }

    private void ExibirMovimentacoes()
    {
        var dados = _movimentos
            .OrderByDescending(m => m.DataHora)
            .Select(m => new MovimentoLinha
            {
                Id = m.Id,
                DataHora = m.DataHora,
                Tipo = m.Tipo,
                ItemId = m.ItemId,
                Item = m.ItemNome,
                Quantidade = m.Quantidade,
                Usuario = m.Usuario
            })
            .ToList();

        MostrarTelaLista("Movimentacoes", dados);
    }

    private void ExibirFinanceiro()
    {
        var dados = _itens
            .OrderBy(item => item.Nome)
            .Select(item => new FinanceiroLinha
            {
                Id = item.Id,
                Item = item.Nome,
                Categoria = item.Categoria,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.Preco,
                ValorTotal = item.Quantidade * item.Preco
            })
            .ToList();

        MostrarTelaLista("Financeiro - Custos por Item", dados);
    }

    private void ExibirEstoqueBaixo()
    {
        var dados = _itens
            .Where(item => item.Quantidade <= 5)
            .OrderBy(item => item.Quantidade)
            .ThenBy(item => item.Nome)
            .Select(item => new EstoqueBaixoLinha
            {
                Id = item.Id,
                Item = item.Nome,
                Categoria = item.Categoria,
                Quantidade = item.Quantidade,
                Preco = item.Preco,
                ValorTotal = item.Quantidade * item.Preco
            })
            .ToList();

        MostrarTelaLista("Relatorio - Estoque Baixo", dados);
    }

    private void ExibirInventarioCompleto()
    {
        var dados = _itens
            .OrderBy(item => item.Id)
            .Select(item => new InventarioLinha
            {
                Id = item.Id,
                Item = item.Nome,
                Categoria = item.Categoria,
                Quantidade = item.Quantidade,
                Preco = item.Preco,
                ValorTotal = item.Quantidade * item.Preco
            })
            .ToList();

        MostrarTelaLista("Relatorio - Inventario Completo", dados);
    }

    private void ExibirSessaoUsuario()
    {
        var usuario = _usuarioLogado;

        var dados = new List<SessaoUsuarioLinha>
        {
            new() { Campo = "Usuario", Valor = usuario?.NomeUsuario ?? "(nao autenticado)" },
            new() { Campo = "Perfil", Valor = usuario?.EhAdmin == true ? "Administrador" : "Operador" },
            new() { Campo = "Data/Hora", Valor = DateTime.Now.ToString("g", CultureInfo.CurrentCulture) },
            new() { Campo = "Acoes", Valor = "Use o menu Usuario para trocar senha ou logout" }
        };

        MostrarTelaLista("Usuario - Sessao", dados);
    }

    private void ExibirUsuariosAdmin()
    {
        if (_usuarioLogado?.EhAdmin != true)
        {
            MessageBox.Show("Apenas administradores podem visualizar a lista de usuarios.", "Permissao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var dados = _usuarioStore
            .ListarUsuarios()
            .Select(usuario => new UsuarioLinha
            {
                NomeUsuario = usuario.NomeUsuario,
                Perfil = usuario.EhAdmin ? "Administrador" : "Operador"
            })
            .ToList();

        MostrarTelaLista("Administracao - Usuarios", dados);
    }

    private void MostrarTelaCrudItens()
    {
        _panelTelaLista.Visible = false;
        panelFormulario.Visible = true;
        dgvItens.Visible = true;
        panelFormulario.BringToFront();
        dgvItens.BringToFront();
    }

    private void MostrarTelaLista<T>(string titulo, IReadOnlyCollection<T> dados)
    {
        _lblTelaListaTitulo.Text = $"{titulo} - {dados.Count} registro(s)";

        _dgvTelaLista.DataSource = null;
        _dgvTelaLista.Columns.Clear();
        _dgvTelaLista.AutoGenerateColumns = true;
        _dgvTelaLista.DataSource = dados.ToList();

        FormatarColunasTabela(_dgvTelaLista);

        panelFormulario.Visible = false;
        dgvItens.Visible = false;
        _panelTelaLista.Visible = true;
        _panelTelaLista.BringToFront();
    }

    private static void FormatarColunasTabela(DataGridView grid)
    {
        foreach (DataGridViewColumn coluna in grid.Columns)
        {
            var nome = coluna.Name.ToLowerInvariant();

            if (nome.Contains("preco") || nome.Contains("valor") || nome.Contains("total"))
            {
                coluna.DefaultCellStyle.Format = "C2";
                coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (nome.Contains("data"))
            {
                coluna.DefaultCellStyle.Format = "g";
            }

            if (nome.Contains("quantidade"))
            {
                coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
    }

    private void RegistrarMovimento(string tipo, ItemCadastro item, int quantidade)
    {
        _movimentos.Add(new MovimentoEstoque
        {
            Id = _proximoMovimentoId++,
            DataHora = DateTime.Now,
            Tipo = tipo,
            ItemId = item.Id,
            ItemNome = item.Nome,
            Quantidade = quantidade,
            Usuario = UsuarioAtual
        });

        SalvarMovimentos();
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

    private void CarregarMovimentos()
    {
        if (!File.Exists(_caminhoMovimentos))
        {
            return;
        }

        try
        {
            var json = File.ReadAllText(_caminhoMovimentos);
            var movimentos = JsonSerializer.Deserialize<List<MovimentoEstoque>>(json) ?? [];

            _movimentos.Clear();
            _movimentos.AddRange(movimentos);
            _proximoMovimentoId = _movimentos.Count == 0 ? 1 : _movimentos.Max(m => m.Id) + 1;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Falha ao carregar movimentacoes: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SalvarMovimentos()
    {
        try
        {
            var json = JsonSerializer.Serialize(_movimentos.OrderBy(m => m.Id), new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_caminhoMovimentos, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Falha ao salvar movimentacoes: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private sealed class DashboardLinha
    {
        public string Indicador { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
    }

    private sealed class CategoriaLinha
    {
        public string Categoria { get; set; } = string.Empty;
        public int Itens { get; set; }
        public int QuantidadeTotal { get; set; }
        public decimal ValorTotal { get; set; }
    }

    private sealed class MovimentoLinha
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int ItemId { get; set; }
        public string Item { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public string Usuario { get; set; } = string.Empty;
    }

    private sealed class FinanceiroLinha
    {
        public int Id { get; set; }
        public string Item { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }

    private sealed class EstoqueBaixoLinha
    {
        public int Id { get; set; }
        public string Item { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal ValorTotal { get; set; }
    }

    private sealed class InventarioLinha
    {
        public int Id { get; set; }
        public string Item { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal ValorTotal { get; set; }
    }

    private sealed class SessaoUsuarioLinha
    {
        public string Campo { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
    }

    private sealed class UsuarioLinha
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
    }
}