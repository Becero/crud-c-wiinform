namespace CrudItensWinForms;

public class FinanceiroForm : Form
{
    public FinanceiroForm(IReadOnlyCollection<ItemCadastro> itens)
    {
        Text = "Financeiro - Resumo";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(430, 240);

        var quantidadeTotal = itens.Sum(item => item.Quantidade);
        var valorTotal = itens.Sum(item => item.Quantidade * item.Preco);
        var custoMedio = quantidadeTotal == 0 ? 0 : valorTotal / quantidadeTotal;

        var titulo = new Label
        {
            Text = "Resumo Financeiro do Estoque",
            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 20)
        };

        var lblItens = new Label
        {
            Text = $"Itens cadastrados: {itens.Count}",
            AutoSize = true,
            Location = new Point(22, 70)
        };

        var lblQuantidade = new Label
        {
            Text = $"Quantidade total: {quantidadeTotal}",
            AutoSize = true,
            Location = new Point(22, 95)
        };

        var lblValor = new Label
        {
            Text = $"Valor total em estoque: {valorTotal:C2}",
            AutoSize = true,
            Location = new Point(22, 120)
        };

        var lblCustoMedio = new Label
        {
            Text = $"Custo medio por unidade: {custoMedio:C2}",
            AutoSize = true,
            Location = new Point(22, 145)
        };

        var lblObservacao = new Label
        {
            Text = "Dica: use Relatorios > Estoque Baixo para identificar risco de ruptura.",
            AutoSize = true,
            Location = new Point(22, 182)
        };

        var btnFechar = new Button
        {
            Text = "Fechar",
            Location = new Point(330, 197),
            Size = new Size(75, 30)
        };
        btnFechar.Click += (_, _) => Close();

        Controls.Add(titulo);
        Controls.Add(lblItens);
        Controls.Add(lblQuantidade);
        Controls.Add(lblValor);
        Controls.Add(lblCustoMedio);
        Controls.Add(lblObservacao);
        Controls.Add(btnFechar);
    }
}