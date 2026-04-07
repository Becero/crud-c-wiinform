namespace CrudItensWinForms;

public class TrocaSenhaForm : Form
{
    private readonly UsuarioStore _usuarioStore;
    private readonly string _nomeUsuario;

    private readonly TextBox _txtSenhaAtual = new();
    private readonly TextBox _txtNovaSenha = new();
    private readonly TextBox _txtConfirmarSenha = new();

    public TrocaSenhaForm(UsuarioStore usuarioStore, string nomeUsuario)
    {
        _usuarioStore = usuarioStore;
        _nomeUsuario = nomeUsuario;

        Text = "Trocar Senha";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(390, 255);

        var lblUsuario = new Label
        {
            Text = $"Usuario: {_nomeUsuario}",
            Location = new Point(24, 18),
            AutoSize = true
        };

        var lblAtual = new Label
        {
            Text = "Senha atual",
            Location = new Point(24, 52),
            AutoSize = true
        };

        _txtSenhaAtual.Location = new Point(24, 72);
        _txtSenhaAtual.Size = new Size(340, 23);
        _txtSenhaAtual.UseSystemPasswordChar = true;

        var lblNova = new Label
        {
            Text = "Nova senha",
            Location = new Point(24, 104),
            AutoSize = true
        };

        _txtNovaSenha.Location = new Point(24, 124);
        _txtNovaSenha.Size = new Size(340, 23);
        _txtNovaSenha.UseSystemPasswordChar = true;

        var lblConfirmar = new Label
        {
            Text = "Confirmar nova senha",
            Location = new Point(24, 156),
            AutoSize = true
        };

        _txtConfirmarSenha.Location = new Point(24, 176);
        _txtConfirmarSenha.Size = new Size(340, 23);
        _txtConfirmarSenha.UseSystemPasswordChar = true;

        var btnSalvar = new Button
        {
            Text = "Salvar",
            Location = new Point(208, 213),
            Size = new Size(75, 30)
        };
        btnSalvar.Click += BtnSalvar_Click;

        var btnCancelar = new Button
        {
            Text = "Cancelar",
            Location = new Point(289, 213),
            Size = new Size(75, 30)
        };
        btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel;

        AcceptButton = btnSalvar;
        CancelButton = btnCancelar;

        Controls.Add(lblUsuario);
        Controls.Add(lblAtual);
        Controls.Add(_txtSenhaAtual);
        Controls.Add(lblNova);
        Controls.Add(_txtNovaSenha);
        Controls.Add(lblConfirmar);
        Controls.Add(_txtConfirmarSenha);
        Controls.Add(btnSalvar);
        Controls.Add(btnCancelar);
    }

    private void BtnSalvar_Click(object? sender, EventArgs e)
    {
        var senhaAtual = _txtSenhaAtual.Text;
        var novaSenha = _txtNovaSenha.Text;
        var confirmarSenha = _txtConfirmarSenha.Text;

        if (string.IsNullOrWhiteSpace(senhaAtual) || string.IsNullOrWhiteSpace(novaSenha) || string.IsNullOrWhiteSpace(confirmarSenha))
        {
            MessageBox.Show("Preencha todos os campos.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (novaSenha.Length < 4)
        {
            MessageBox.Show("A nova senha deve ter no minimo 4 caracteres.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!string.Equals(novaSenha, confirmarSenha, StringComparison.Ordinal))
        {
            MessageBox.Show("A confirmacao da senha nao confere.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!_usuarioStore.TrocarSenha(_nomeUsuario, senhaAtual, novaSenha))
        {
            MessageBox.Show("Senha atual invalida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        MessageBox.Show("Senha alterada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        DialogResult = DialogResult.OK;
        Close();
    }
}