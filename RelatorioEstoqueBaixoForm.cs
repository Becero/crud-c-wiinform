using System.Globalization;
using System.Text;

namespace CrudItensWinForms;

public class RelatorioEstoqueBaixoForm : Form
{
    public RelatorioEstoqueBaixoForm(IReadOnlyCollection<ItemCadastro> itensBaixoEstoque)
    {
        Text = "Relatorio - Estoque Baixo";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(680, 420);

        var lblTitulo = new Label
        {
            Text = "Itens com estoque baixo (<= 5)",
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(16, 14)
        };

        var lblResumo = new Label
        {
            Text = $"Total de itens no relatorio: {itensBaixoEstoque.Count}",
            AutoSize = true,
            Location = new Point(18, 40)
        };

        var dgv = new DataGridView
        {
            Location = new Point(16, 66),
            Size = new Size(648, 300),
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            RowHeadersVisible = false,
            AutoGenerateColumns = true,
            DataSource = itensBaixoEstoque
                .OrderBy(item => item.Quantidade)
                .ThenBy(item => item.Nome)
                .Select(item => new
                {
                    item.Id,
                    item.Nome,
                    item.Categoria,
                    item.Quantidade,
                    Preco = item.Preco.ToString("C2", CultureInfo.CurrentCulture)
                })
                .ToList()
        };

        var btnExportar = new Button
        {
            Text = "Exportar CSV",
            Location = new Point(478, 378),
            Size = new Size(90, 30)
        };
        btnExportar.Click += (_, _) => ExportarCsv(itensBaixoEstoque);

        var btnFechar = new Button
        {
            Text = "Fechar",
            Location = new Point(574, 378),
            Size = new Size(90, 30)
        };
        btnFechar.Click += (_, _) => Close();

        Controls.Add(lblTitulo);
        Controls.Add(lblResumo);
        Controls.Add(dgv);
        Controls.Add(btnExportar);
        Controls.Add(btnFechar);
    }

    private static void ExportarCsv(IReadOnlyCollection<ItemCadastro> itens)
    {
        if (itens.Count == 0)
        {
            MessageBox.Show("Nao ha itens de estoque baixo para exportar.", "Relatorio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Filter = "Arquivo CSV (*.csv)|*.csv",
            FileName = $"estoque_baixo_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
            Title = "Exportar relatorio"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        try
        {
            var linhas = new List<string> { "Id;Nome;Categoria;Quantidade;Preco" };
            linhas.AddRange(itens.Select(item =>
                $"{item.Id};{Escapar(item.Nome)};{Escapar(item.Categoria)};{item.Quantidade};{item.Preco.ToString("0.00", CultureInfo.CurrentCulture)}"));

            File.WriteAllLines(dialog.FileName, linhas, Encoding.UTF8);
            MessageBox.Show("Relatorio exportado com sucesso.", "Relatorio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Falha ao exportar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static string Escapar(string valor)
    {
        if (valor.Contains(';') || valor.Contains('"') || valor.Contains('\n') || valor.Contains('\r'))
        {
            return $"\"{valor.Replace("\"", "\"\"")}\"";
        }

        return valor;
    }
}