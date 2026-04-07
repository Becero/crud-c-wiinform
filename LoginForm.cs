namespace CrudItensWinForms;

public class LoginForm : Form
{
    private readonly UsuarioStore _usuarioStore;

    private readonly TextBox _txtUsuario = new();
    private readonly TextBox _txtSenha = new();
    private readonly Button _btnEntrar = new();
    private readonly Button _btnCancelar = new();

    public UsuarioSistema? UsuarioAutenticado { get; private set; }

    public LoginForm(UsuarioStore usuarioStore)
    {
        _usuarioStore = usuarioStore;

        Text = "Login";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(360, 190);

        var lblUsuario = new Label
        {
            Text = "Usuario",
            Location = new Point(24, 24),
            AutoSize = true
        };

        _txtUsuario.Location = new Point(24, 44);
        _txtUsuario.Size = new Size(300, 23);

        var lblSenha = new Label
        {
            Text = "Senha",
            Location = new Point(24, 78),
            AutoSize = true
        };

        _txtSenha.Location = new Point(24, 98);
        _txtSenha.Size = new Size(300, 23);
        _txtSenha.UseSystemPasswordChar = true;

        _btnEntrar.Text = "Entrar";
        _btnEntrar.Location = new Point(168, 136);
        _btnEntrar.Size = new Size(75, 30);
        _btnEntrar.Click += BtnEntrar_Click;

        _btnCancelar.Text = "Cancelar";
        _btnCancelar.Location = new Point(249, 136);
        _btnCancelar.Size = new Size(75, 30);
        _btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel;

        AcceptButton = _btnEntrar;
        CancelButton = _btnCancelar;

        Controls.Add(lblUsuario);
        Controls.Add(_txtUsuario);
        Controls.Add(lblSenha);
        Controls.Add(_txtSenha);
        Controls.Add(_btnEntrar);
        Controls.Add(_btnCancelar);

        Shown += (_, _) => _txtUsuario.Focus();
    }

    private void BtnEntrar_Click(object? sender, EventArgs e)
    {
        var usuario = _txtUsuario.Text.Trim();
        var senha = _txtSenha.Text;

        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
        {
            MessageBox.Show("Informe usuario e senha.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!_usuarioStore.ValidarCredenciais(usuario, senha, out var usuarioAutenticado))
        {
            MessageBox.Show("Credenciais invalidas.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _txtSenha.Clear();
            _txtSenha.Focus();
            return;
        }

        UsuarioAutenticado = usuarioAutenticado;
        DialogResult = DialogResult.OK;
        Close();
    }
}