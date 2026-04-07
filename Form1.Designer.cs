namespace CrudItensWinForms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        menuPrincipal = new MenuStrip();
        menuDashboard = new ToolStripMenuItem();
        menuCadastros = new ToolStripMenuItem();
        menuCadastrosItens = new ToolStripMenuItem();
        menuCadastrosCategorias = new ToolStripMenuItem();
        menuMovimentacoes = new ToolStripMenuItem();
        menuMovEntrada = new ToolStripMenuItem();
        menuMovSaida = new ToolStripMenuItem();
        menuFinanceiro = new ToolStripMenuItem();
        menuFinanceiroResumo = new ToolStripMenuItem();
        menuRelatorios = new ToolStripMenuItem();
        menuRelEstoqueBaixo = new ToolStripMenuItem();
        menuRelExportarCsv = new ToolStripMenuItem();
        menuUsuario = new ToolStripMenuItem();
        menuUsuarioTrocarSenha = new ToolStripMenuItem();
        menuUsuarioLogout = new ToolStripMenuItem();
        menuAdministracao = new ToolStripMenuItem();
        menuAdmUsuarios = new ToolStripMenuItem();
        panelFormulario = new Panel();
        lblUsuarioLogado = new Label();
        lblResumoValor = new Label();
        lblResumoQuantidade = new Label();
        lblResumoItens = new Label();
        btnLimparBusca = new Button();
        txtBusca = new TextBox();
        lblBusca = new Label();
        btnExportarCsv = new Button();
        btnDuplicar = new Button();
        btnNovo = new Button();
        btnExcluir = new Button();
        btnAtualizar = new Button();
        btnAdicionar = new Button();
        nudPreco = new NumericUpDown();
        nudQuantidade = new NumericUpDown();
        txtCategoria = new TextBox();
        txtNome = new TextBox();
        lblPreco = new Label();
        lblQuantidade = new Label();
        lblCategoria = new Label();
        lblNome = new Label();
        lblTitulo = new Label();
        dgvItens = new DataGridView();
        menuPrincipal.SuspendLayout();
        panelFormulario.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudPreco).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudQuantidade).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvItens).BeginInit();
        SuspendLayout();
        // 
        // menuPrincipal
        // 
        menuPrincipal.Items.AddRange(new ToolStripItem[] { menuDashboard, menuCadastros, menuMovimentacoes, menuFinanceiro, menuRelatorios, menuUsuario, menuAdministracao });
        menuPrincipal.Location = new Point(0, 0);
        menuPrincipal.Name = "menuPrincipal";
        menuPrincipal.Size = new Size(884, 24);
        menuPrincipal.TabIndex = 0;
        menuPrincipal.Text = "menuStrip1";
        // 
        // menuDashboard
        // 
        menuDashboard.Name = "menuDashboard";
        menuDashboard.Size = new Size(76, 20);
        menuDashboard.Text = "Dashboard";
        menuDashboard.Click += menuDashboard_Click;
        // 
        // menuCadastros
        // 
        menuCadastros.DropDownItems.AddRange(new ToolStripItem[] { menuCadastrosItens, menuCadastrosCategorias });
        menuCadastros.Name = "menuCadastros";
        menuCadastros.Size = new Size(71, 20);
        menuCadastros.Text = "Cadastros";
        // 
        // menuCadastrosItens
        // 
        menuCadastrosItens.Name = "menuCadastrosItens";
        menuCadastrosItens.Size = new Size(129, 22);
        menuCadastrosItens.Text = "Itens";
        menuCadastrosItens.Click += menuCadastrosItens_Click;
        // 
        // menuCadastrosCategorias
        // 
        menuCadastrosCategorias.Name = "menuCadastrosCategorias";
        menuCadastrosCategorias.Size = new Size(129, 22);
        menuCadastrosCategorias.Text = "Categorias";
        menuCadastrosCategorias.Click += menuCadastrosCategorias_Click;
        // 
        // menuMovimentacoes
        // 
        menuMovimentacoes.DropDownItems.AddRange(new ToolStripItem[] { menuMovEntrada, menuMovSaida });
        menuMovimentacoes.Name = "menuMovimentacoes";
        menuMovimentacoes.Size = new Size(96, 20);
        menuMovimentacoes.Text = "Movimentacoes";
        // 
        // menuMovEntrada
        // 
        menuMovEntrada.Name = "menuMovEntrada";
        menuMovEntrada.Size = new Size(111, 22);
        menuMovEntrada.Text = "Entrada";
        menuMovEntrada.Click += menuMovEntrada_Click;
        // 
        // menuMovSaida
        // 
        menuMovSaida.Name = "menuMovSaida";
        menuMovSaida.Size = new Size(111, 22);
        menuMovSaida.Text = "Saida";
        menuMovSaida.Click += menuMovSaida_Click;
        // 
        // menuFinanceiro
        // 
        menuFinanceiro.DropDownItems.AddRange(new ToolStripItem[] { menuFinanceiroResumo });
        menuFinanceiro.Name = "menuFinanceiro";
        menuFinanceiro.Size = new Size(72, 20);
        menuFinanceiro.Text = "Financeiro";
        // 
        // menuFinanceiroResumo
        // 
        menuFinanceiroResumo.Name = "menuFinanceiroResumo";
        menuFinanceiroResumo.Size = new Size(122, 22);
        menuFinanceiroResumo.Text = "Resumo";
        menuFinanceiroResumo.Click += menuFinanceiroResumo_Click;
        // 
        // menuRelatorios
        // 
        menuRelatorios.DropDownItems.AddRange(new ToolStripItem[] { menuRelEstoqueBaixo, menuRelExportarCsv });
        menuRelatorios.Name = "menuRelatorios";
        menuRelatorios.Size = new Size(71, 20);
        menuRelatorios.Text = "Relatorios";
        // 
        // menuRelEstoqueBaixo
        // 
        menuRelEstoqueBaixo.Name = "menuRelEstoqueBaixo";
        menuRelEstoqueBaixo.Size = new Size(196, 22);
        menuRelEstoqueBaixo.Text = "Estoque Baixo";
        menuRelEstoqueBaixo.Click += menuRelEstoqueBaixo_Click;
        // 
        // menuRelExportarCsv
        // 
        menuRelExportarCsv.Name = "menuRelExportarCsv";
        menuRelExportarCsv.Size = new Size(196, 22);
        menuRelExportarCsv.Text = "Exportar Inventario CSV";
        menuRelExportarCsv.Click += menuRelExportarCsv_Click;
        // 
        // menuUsuario
        // 
        menuUsuario.DropDownItems.AddRange(new ToolStripItem[] { menuUsuarioTrocarSenha, menuUsuarioLogout });
        menuUsuario.Name = "menuUsuario";
        menuUsuario.Size = new Size(59, 20);
        menuUsuario.Text = "Usuario";
        // 
        // menuUsuarioTrocarSenha
        // 
        menuUsuarioTrocarSenha.Name = "menuUsuarioTrocarSenha";
        menuUsuarioTrocarSenha.Size = new Size(140, 22);
        menuUsuarioTrocarSenha.Text = "Trocar Senha";
        menuUsuarioTrocarSenha.Click += menuUsuarioTrocarSenha_Click;
        // 
        // menuUsuarioLogout
        // 
        menuUsuarioLogout.Name = "menuUsuarioLogout";
        menuUsuarioLogout.Size = new Size(140, 22);
        menuUsuarioLogout.Text = "Logout";
        menuUsuarioLogout.Click += menuUsuarioLogout_Click;
        // 
        // menuAdministracao
        // 
        menuAdministracao.DropDownItems.AddRange(new ToolStripItem[] { menuAdmUsuarios });
        menuAdministracao.Name = "menuAdministracao";
        menuAdministracao.Size = new Size(96, 20);
        menuAdministracao.Text = "Administracao";
        // 
        // menuAdmUsuarios
        // 
        menuAdmUsuarios.Name = "menuAdmUsuarios";
        menuAdmUsuarios.Size = new Size(116, 22);
        menuAdmUsuarios.Text = "Usuarios";
        menuAdmUsuarios.Click += menuAdmUsuarios_Click;
        // 
        // panelFormulario
        // 
        panelFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelFormulario.BackColor = Color.WhiteSmoke;
        panelFormulario.Controls.Add(lblUsuarioLogado);
        panelFormulario.Controls.Add(lblResumoValor);
        panelFormulario.Controls.Add(lblResumoQuantidade);
        panelFormulario.Controls.Add(lblResumoItens);
        panelFormulario.Controls.Add(btnLimparBusca);
        panelFormulario.Controls.Add(txtBusca);
        panelFormulario.Controls.Add(lblBusca);
        panelFormulario.Controls.Add(btnExportarCsv);
        panelFormulario.Controls.Add(btnDuplicar);
        panelFormulario.Controls.Add(btnNovo);
        panelFormulario.Controls.Add(btnExcluir);
        panelFormulario.Controls.Add(btnAtualizar);
        panelFormulario.Controls.Add(btnAdicionar);
        panelFormulario.Controls.Add(nudPreco);
        panelFormulario.Controls.Add(nudQuantidade);
        panelFormulario.Controls.Add(txtCategoria);
        panelFormulario.Controls.Add(txtNome);
        panelFormulario.Controls.Add(lblPreco);
        panelFormulario.Controls.Add(lblQuantidade);
        panelFormulario.Controls.Add(lblCategoria);
        panelFormulario.Controls.Add(lblNome);
        panelFormulario.Controls.Add(lblTitulo);
        panelFormulario.Location = new Point(12, 36);
        panelFormulario.Name = "panelFormulario";
        panelFormulario.Size = new Size(860, 245);
        panelFormulario.TabIndex = 1;
        // 
        // lblUsuarioLogado
        // 
        lblUsuarioLogado.AutoSize = true;
        lblUsuarioLogado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblUsuarioLogado.Location = new Point(590, 26);
        lblUsuarioLogado.Name = "lblUsuarioLogado";
        lblUsuarioLogado.Size = new Size(138, 15);
        lblUsuarioLogado.TabIndex = 21;
        lblUsuarioLogado.Text = "Usuario: nao autenticado";
        // 
        // lblResumoValor
        // 
        lblResumoValor.AutoSize = true;
        lblResumoValor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblResumoValor.Location = new Point(500, 211);
        lblResumoValor.Name = "lblResumoValor";
        lblResumoValor.Size = new Size(147, 15);
        lblResumoValor.TabIndex = 20;
        lblResumoValor.Text = "Valor estoque: R$ 0,00";
        // 
        // lblResumoQuantidade
        // 
        lblResumoQuantidade.AutoSize = true;
        lblResumoQuantidade.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblResumoQuantidade.Location = new Point(500, 191);
        lblResumoQuantidade.Name = "lblResumoQuantidade";
        lblResumoQuantidade.Size = new Size(111, 15);
        lblResumoQuantidade.TabIndex = 19;
        lblResumoQuantidade.Text = "Qtd estoque: 0";
        // 
        // lblResumoItens
        // 
        lblResumoItens.AutoSize = true;
        lblResumoItens.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblResumoItens.Location = new Point(500, 171);
        lblResumoItens.Name = "lblResumoItens";
        lblResumoItens.Size = new Size(48, 15);
        lblResumoItens.TabIndex = 18;
        lblResumoItens.Text = "Itens: 0";
        // 
        // btnLimparBusca
        // 
        btnLimparBusca.BackColor = Color.DimGray;
        btnLimparBusca.FlatStyle = FlatStyle.Flat;
        btnLimparBusca.ForeColor = Color.White;
        btnLimparBusca.Location = new Point(348, 170);
        btnLimparBusca.Name = "btnLimparBusca";
        btnLimparBusca.Size = new Size(130, 29);
        btnLimparBusca.TabIndex = 17;
        btnLimparBusca.Text = "Limpar Busca";
        btnLimparBusca.UseVisualStyleBackColor = false;
        btnLimparBusca.Click += btnLimparBusca_Click;
        // 
        // txtBusca
        // 
        txtBusca.Location = new Point(70, 173);
        txtBusca.Name = "txtBusca";
        txtBusca.PlaceholderText = "Buscar por nome ou categoria...";
        txtBusca.Size = new Size(270, 23);
        txtBusca.TabIndex = 16;
        txtBusca.TextChanged += txtBusca_TextChanged;
        // 
        // lblBusca
        // 
        lblBusca.AutoSize = true;
        lblBusca.Location = new Point(20, 176);
        lblBusca.Name = "lblBusca";
        lblBusca.Size = new Size(41, 15);
        lblBusca.TabIndex = 15;
        lblBusca.Text = "Busca";
        // 
        // btnExportarCsv
        // 
        btnExportarCsv.BackColor = Color.Teal;
        btnExportarCsv.FlatStyle = FlatStyle.Flat;
        btnExportarCsv.ForeColor = Color.White;
        btnExportarCsv.Location = new Point(590, 121);
        btnExportarCsv.Name = "btnExportarCsv";
        btnExportarCsv.Size = new Size(130, 36);
        btnExportarCsv.TabIndex = 14;
        btnExportarCsv.Text = "Exportar CSV";
        btnExportarCsv.UseVisualStyleBackColor = false;
        btnExportarCsv.Click += btnExportarCsv_Click;
        // 
        // btnDuplicar
        // 
        btnDuplicar.BackColor = Color.DarkGoldenrod;
        btnDuplicar.FlatStyle = FlatStyle.Flat;
        btnDuplicar.ForeColor = Color.White;
        btnDuplicar.Location = new Point(476, 121);
        btnDuplicar.Name = "btnDuplicar";
        btnDuplicar.Size = new Size(108, 36);
        btnDuplicar.TabIndex = 13;
        btnDuplicar.Text = "Duplicar";
        btnDuplicar.UseVisualStyleBackColor = false;
        btnDuplicar.Click += btnDuplicar_Click;
        // 
        // btnNovo
        // 
        btnNovo.BackColor = Color.SlateGray;
        btnNovo.FlatStyle = FlatStyle.Flat;
        btnNovo.ForeColor = Color.White;
        btnNovo.Location = new Point(362, 121);
        btnNovo.Name = "btnNovo";
        btnNovo.Size = new Size(108, 36);
        btnNovo.TabIndex = 12;
        btnNovo.Text = "Limpar";
        btnNovo.UseVisualStyleBackColor = false;
        btnNovo.Click += btnNovo_Click;
        // 
        // btnExcluir
        // 
        btnExcluir.BackColor = Color.Firebrick;
        btnExcluir.FlatStyle = FlatStyle.Flat;
        btnExcluir.ForeColor = Color.White;
        btnExcluir.Location = new Point(248, 121);
        btnExcluir.Name = "btnExcluir";
        btnExcluir.Size = new Size(108, 36);
        btnExcluir.TabIndex = 11;
        btnExcluir.Text = "Excluir";
        btnExcluir.UseVisualStyleBackColor = false;
        btnExcluir.Click += btnExcluir_Click;
        // 
        // btnAtualizar
        // 
        btnAtualizar.BackColor = Color.SteelBlue;
        btnAtualizar.FlatStyle = FlatStyle.Flat;
        btnAtualizar.ForeColor = Color.White;
        btnAtualizar.Location = new Point(134, 121);
        btnAtualizar.Name = "btnAtualizar";
        btnAtualizar.Size = new Size(108, 36);
        btnAtualizar.TabIndex = 10;
        btnAtualizar.Text = "Atualizar";
        btnAtualizar.UseVisualStyleBackColor = false;
        btnAtualizar.Click += btnAtualizar_Click;
        // 
        // btnAdicionar
        // 
        btnAdicionar.BackColor = Color.SeaGreen;
        btnAdicionar.FlatStyle = FlatStyle.Flat;
        btnAdicionar.ForeColor = Color.White;
        btnAdicionar.Location = new Point(20, 121);
        btnAdicionar.Name = "btnAdicionar";
        btnAdicionar.Size = new Size(108, 36);
        btnAdicionar.TabIndex = 9;
        btnAdicionar.Text = "Adicionar";
        btnAdicionar.UseVisualStyleBackColor = false;
        btnAdicionar.Click += btnAdicionar_Click;
        // 
        // nudPreco
        // 
        nudPreco.DecimalPlaces = 2;
        nudPreco.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
        nudPreco.Location = new Point(633, 80);
        nudPreco.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
        nudPreco.Name = "nudPreco";
        nudPreco.Size = new Size(163, 23);
        nudPreco.TabIndex = 8;
        nudPreco.ThousandsSeparator = true;
        // 
        // nudQuantidade
        // 
        nudQuantidade.Location = new Point(456, 80);
        nudQuantidade.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
        nudQuantidade.Name = "nudQuantidade";
        nudQuantidade.Size = new Size(140, 23);
        nudQuantidade.TabIndex = 7;
        // 
        // txtCategoria
        // 
        txtCategoria.Location = new Point(258, 80);
        txtCategoria.MaxLength = 80;
        txtCategoria.Name = "txtCategoria";
        txtCategoria.Size = new Size(165, 23);
        txtCategoria.TabIndex = 6;
        // 
        // txtNome
        // 
        txtNome.Location = new Point(20, 80);
        txtNome.MaxLength = 120;
        txtNome.Name = "txtNome";
        txtNome.Size = new Size(210, 23);
        txtNome.TabIndex = 5;
        // 
        // lblPreco
        // 
        lblPreco.AutoSize = true;
        lblPreco.Location = new Point(633, 62);
        lblPreco.Name = "lblPreco";
        lblPreco.Size = new Size(38, 15);
        lblPreco.TabIndex = 4;
        lblPreco.Text = "Preco";
        // 
        // lblQuantidade
        // 
        lblQuantidade.AutoSize = true;
        lblQuantidade.Location = new Point(456, 62);
        lblQuantidade.Name = "lblQuantidade";
        lblQuantidade.Size = new Size(69, 15);
        lblQuantidade.TabIndex = 3;
        lblQuantidade.Text = "Quantidade";
        // 
        // lblCategoria
        // 
        lblCategoria.AutoSize = true;
        lblCategoria.Location = new Point(258, 62);
        lblCategoria.Name = "lblCategoria";
        lblCategoria.Size = new Size(58, 15);
        lblCategoria.TabIndex = 2;
        lblCategoria.Text = "Categoria";
        // 
        // lblNome
        // 
        lblNome.AutoSize = true;
        lblNome.Location = new Point(20, 62);
        lblNome.Name = "lblNome";
        lblNome.Size = new Size(40, 15);
        lblNome.TabIndex = 1;
        lblNome.Text = "Nome";
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(14, 19);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(221, 25);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "CRUD - Cadastro de Itens";
        // 
        // dgvItens
        // 
        dgvItens.AllowUserToAddRows = false;
        dgvItens.AllowUserToDeleteRows = false;
        dgvItens.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvItens.BackgroundColor = Color.White;
        dgvItens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvItens.Location = new Point(12, 287);
        dgvItens.MultiSelect = false;
        dgvItens.Name = "dgvItens";
        dgvItens.ReadOnly = true;
        dgvItens.RowHeadersVisible = false;
        dgvItens.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvItens.Size = new Size(860, 252);
        dgvItens.TabIndex = 2;
        dgvItens.CellFormatting += dgvItens_CellFormatting;
        dgvItens.SelectionChanged += dgvItens_SelectionChanged;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(884, 551);
        Controls.Add(dgvItens);
        Controls.Add(panelFormulario);
        Controls.Add(menuPrincipal);
        MainMenuStrip = menuPrincipal;
        MinimumSize = new Size(940, 640);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Cadastro de Itens";
        Load += Form1_Load;
        menuPrincipal.ResumeLayout(false);
        menuPrincipal.PerformLayout();
        panelFormulario.ResumeLayout(false);
        panelFormulario.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudPreco).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudQuantidade).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvItens).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuPrincipal;
    private ToolStripMenuItem menuDashboard;
    private ToolStripMenuItem menuCadastros;
    private ToolStripMenuItem menuCadastrosItens;
    private ToolStripMenuItem menuCadastrosCategorias;
    private ToolStripMenuItem menuMovimentacoes;
    private ToolStripMenuItem menuMovEntrada;
    private ToolStripMenuItem menuMovSaida;
    private ToolStripMenuItem menuFinanceiro;
    private ToolStripMenuItem menuFinanceiroResumo;
    private ToolStripMenuItem menuRelatorios;
    private ToolStripMenuItem menuRelEstoqueBaixo;
    private ToolStripMenuItem menuRelExportarCsv;
    private ToolStripMenuItem menuUsuario;
    private ToolStripMenuItem menuUsuarioTrocarSenha;
    private ToolStripMenuItem menuUsuarioLogout;
    private ToolStripMenuItem menuAdministracao;
    private ToolStripMenuItem menuAdmUsuarios;
    private Panel panelFormulario;
    private Label lblUsuarioLogado;
    private Label lblTitulo;
    private Label lblNome;
    private Label lblCategoria;
    private Label lblQuantidade;
    private Label lblPreco;
    private TextBox txtNome;
    private TextBox txtCategoria;
    private NumericUpDown nudQuantidade;
    private NumericUpDown nudPreco;
    private Button btnAdicionar;
    private Button btnAtualizar;
    private Button btnExcluir;
    private Button btnNovo;
    private Button btnDuplicar;
    private Button btnExportarCsv;
    private Label lblBusca;
    private TextBox txtBusca;
    private Button btnLimparBusca;
    private Label lblResumoItens;
    private Label lblResumoQuantidade;
    private Label lblResumoValor;
    private DataGridView dgvItens;
}