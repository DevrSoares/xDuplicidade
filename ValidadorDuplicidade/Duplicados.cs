using System;
using System.Windows.Forms;

namespace ValidadorDuplicidade
{
    public partial class Duplicados : Form

    {
        private List<Registro> DuplicadosLista;
        private List<Registro> List1;
        private List<Registro> List2;

        public int contador;
        public int contador1;
        public int contador2;

        Filtros f = new Filtros();
        Excel excel = new Excel();




        public Duplicados()
        {


            InitializeComponent();

            ResultadoLista1.ClearSelection();
            ResultadoLista2.ClearSelection();


            init();


            ResultadoLista2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TabelaResultado.Sort(TabelaResultado.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
            ResultadoLista1.Sort(ResultadoLista1.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
            ResultadoLista2.Sort(ResultadoLista2.Columns[1], System.ComponentModel.ListSortDirection.Ascending);

            foreach (DataGridViewRow rowL2 in ResultadoLista2.Rows)
            {
                string Nome = rowL2.Cells[1].Value?.ToString();
                string Valor = rowL2.Cells[3].Value?.ToString();

                foreach (DataGridViewRow row in TabelaResultado.Rows)
                {
                    bool nomeIgual = row.Cells[1].Value?.ToString() == Nome;
                    bool valorIgual = row.Cells[3].Value?.ToString() == Valor;

                    if (nomeIgual && valorIgual)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }


                }
            }

            foreach (DataGridViewRow rowL1 in ResultadoLista1.Rows)
            {
                string Nome = rowL1.Cells[1].Value?.ToString();
                string Valor = rowL1.Cells[3].Value?.ToString();
                foreach (DataGridViewRow row in TabelaResultado.Rows)
                {
                    bool nomeIgual = row.Cells[1].Value?.ToString() == Nome;
                    bool valorIgual = row.Cells[3].Value?.ToString() == Valor;
                    if (nomeIgual && valorIgual)
                    {
                        row.DefaultCellStyle.BackColor = Color.Orange;
                    }
                }


            }

        }

        private void FiltroValor_CheckedChanged(object sender, EventArgs e)
        {

            //Os contadores marcam a quantidade de registro dentro das tabelas;
            //São zerados quando o filtro é aplicado para recomeçar a contagem;

            f.Valor = FiltroValor.Checked;
            contador = 0;
            contador1 = 0;
            contador2 = 0;

            init();


            TabelaResultado.Sort(TabelaResultado.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
            ResultadoLista1.Sort(ResultadoLista1.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
            ResultadoLista2.Sort(ResultadoLista2.Columns[1], System.ComponentModel.ListSortDirection.Ascending);

            foreach (DataGridViewRow rowL2 in ResultadoLista2.Rows)
            {
                string NomeA = rowL2.Cells[1].Value?.ToString();
                string ValorA = rowL2.Cells[3].Value?.ToString();

                foreach (DataGridViewRow row in TabelaResultado.Rows)
                {
                    bool nomeIgual = row.Cells[1].Value?.ToString() == NomeA;
                    bool valorIgual = row.Cells[3].Value?.ToString() == ValorA;

                    if (nomeIgual && valorIgual)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }


                }
            }

            foreach (DataGridViewRow rowL1 in ResultadoLista1.Rows)
            {
                string NomeB = rowL1.Cells[1].Value?.ToString();
                string ValorB = rowL1.Cells[3].Value?.ToString();
                foreach (DataGridViewRow row in TabelaResultado.Rows)
                {
                    bool nomeIgual = row.Cells[1].Value?.ToString() == NomeB;
                    bool valorIgual = row.Cells[3].Value?.ToString() == ValorB;
                    if (nomeIgual && valorIgual)
                    {
                        row.DefaultCellStyle.BackColor = Color.Orange;
                    }
                }


            }




        }

        public void init()
        {
            try
            {
                TabelaResultado.Rows.Clear();
                ResultadoLista1.Rows.Clear();
                ResultadoLista2.Rows.Clear();

                var retorno = excel.abrirDocumento(Properties.Settings.Default.Caminho_arq, f);


                foreach (var dado in retorno.Duplicados["DuplicadosTodos"])
                {
                    TabelaResultado.Rows.Add(
                        dado.NumeroRegistro,
                        dado.NomeRegistro,
                        dado.DataRegistro,
                        dado.ValorRegistro
                    );
                    contador++;

                }

                QtdResultado.Text = contador.ToString();

                foreach (var dado in retorno.Duplicados["RegistrosLista1"])
                {
                    ResultadoLista1.Rows.Add(
                        dado.NumeroRegistro,
                        dado.NomeRegistro,
                        dado.DataRegistro,
                        dado.ValorRegistro
                    );
                    contador1++;
                }
                QtdTabela1.Text = contador1.ToString();

                foreach (var dado in retorno.Duplicados["RegistrosLista2"])
                {
                    ResultadoLista2.Rows.Add(
                        dado.NumeroRegistro,
                        dado.NomeRegistro,
                        dado.DataRegistro,
                        dado.ValorRegistro
                    );
                    contador2++;
                }
                QtdTabela2.Text = contador2.ToString();


                DataUltimaSemana.Text = retorno.nomes["nome1"] + " Tabela 1";
                DataPenultimaSemana.Text = retorno.nomes["nome2"] + " Tabela 2";
            }catch(Exception ex)
            {
                MessageBox.Show("Erro ao abrir o arquivo: " + ex.Message + "\nNão existe planilhas suficientes para serem comparadas", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            }

        private void Duplicados_Load(object? sender, EventArgs e)
        {

        }

        private void moonLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private async void TabelaResultado_CellClick(object sender, EventArgs e)
        {


            if (TabelaResultado.CurrentRow == null) return;



            string Nome = TabelaResultado.CurrentRow.Cells[1].Value?.ToString();
            string Valor = TabelaResultado.CurrentRow.Cells[3].Value?.ToString();

            foreach (DataGridViewRow r in ResultadoLista1.Rows)
                r.DefaultCellStyle.BackColor = Color.Empty;

            ResultadoLista1.ClearSelection();
            ResultadoLista2.ClearSelection();

            foreach (DataGridViewRow row in ResultadoLista1.Rows)
            {
                bool nomeIgual = row.Cells[1].Value?.ToString() == Nome;
                bool valorIgual = row.Cells[3].Value?.ToString() == Valor;

                if (nomeIgual && valorIgual)
                {

                    for (int i = 0; i < 3; i++)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        await Task.Delay(200);
                        row.DefaultCellStyle.BackColor = Color.Orange;
                        await Task.Delay(200);
                        row.DefaultCellStyle.BackColor = Color.White;

                    }

                    row.Selected = true;
                    row.DefaultCellStyle.SelectionBackColor = Color.DimGray;
                    ResultadoLista1.CurrentCell = row.Cells[0];
                    break;
                }

            }

            foreach (DataGridViewRow row in ResultadoLista2.Rows)
            {
                bool nomeIgual = row.Cells[1].Value?.ToString() == Nome;
                bool valorIgual = row.Cells[3].Value?.ToString() == Valor;

                if (nomeIgual && valorIgual)
                {

                    for (int i = 0; i < 3; i++)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        await Task.Delay(200);
                        row.DefaultCellStyle.BackColor = Color.Orange;
                        await Task.Delay(200);
                        row.DefaultCellStyle.BackColor = Color.White;


                    }
                    row.Selected = true;
                    row.DefaultCellStyle.SelectionBackColor = Color.DimGray;
                    ResultadoLista2.CurrentCell = row.Cells[0];
                    break;
                }
            }

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ResultadoLista2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ResultadoLista1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


}
