namespace CrudItensWinForms;

public static class QuantidadeDialog
{
    public static bool TryObterQuantidade(IWin32Window owner, string titulo, string mensagem, out int quantidade)
    {
        quantidade = 0;

        using var form = new Form
        {
            Text = titulo,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false,
            ShowInTaskbar = false,
            ClientSize = new Size(320, 145)
        };

        var lblMensagem = new Label
        {
            Text = mensagem,
            Location = new Point(20, 18),
            AutoSize = true
        };

        var nudQuantidade = new NumericUpDown
        {
            Location = new Point(20, 44),
            Size = new Size(130, 23),
            Minimum = 1,
            Maximum = 100000,
            Value = 1
        };

        var btnOk = new Button
        {
            Text = "OK",
            Location = new Point(144, 94),
            Size = new Size(75, 30),
            DialogResult = DialogResult.OK
        };

        var btnCancelar = new Button
        {
            Text = "Cancelar",
            Location = new Point(225, 94),
            Size = new Size(75, 30),
            DialogResult = DialogResult.Cancel
        };

        form.AcceptButton = btnOk;
        form.CancelButton = btnCancelar;

        form.Controls.Add(lblMensagem);
        form.Controls.Add(nudQuantidade);
        form.Controls.Add(btnOk);
        form.Controls.Add(btnCancelar);

        if (form.ShowDialog(owner) != DialogResult.OK)
        {
            return false;
        }

        quantidade = (int)nudQuantidade.Value;
        return true;
    }
}