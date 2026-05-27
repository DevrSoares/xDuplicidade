using OfficeOpenXml;

namespace ValidadorDuplicidade
{
    internal class Excel
    {
        bool Data;
        bool Nome = true;


        public (List<Registro> Lista1,List<Registro> Lista2,List<Registro> Lista3,Dictionary<string, List<Registro>> Duplicados,Dictionary<string, string> nomes)abrirDocumento(string caminho, Filtros filtro)
        {




            try
            {
                List<Registro> Lista1 = new();
                List<Registro> Lista2 = new();
                List<Registro> Lista3 = new();
                List<Registro> duplicadosTodos = new();

                string filePath = caminho;

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    MessageBox.Show(
                        "Nenhum arquivo selecionado.",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return (
                        new List<Registro>(),
                        new List<Registro>(),
                        new List<Registro>(),
                        new Dictionary<string, List<Registro>>(),
                        new Dictionary<string, string>()
                    );
                }

                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {

                    if (package?.Workbook?.Worksheets == null || package.Workbook.Worksheets.Count < 2)
                    {
                        return (
                            new List<Registro>(),
                            new List<Registro>(),
                            new List<Registro>(),
                            new Dictionary<string, List<Registro>>(),
                            new Dictionary<string, string>()
                        );
                    }

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets[1];
                    ExcelWorksheet worksheet3 = package.Workbook.Worksheets[2];

                    if (worksheet.Dimension == null ||
                        worksheet2.Dimension == null ||
                        worksheet3.Dimension == null)
                    {
                        return (
                            new List<Registro>(),
                            new List<Registro>(),
                            new List<Registro>(),
                            new Dictionary<string, List<Registro>>(),
                            new Dictionary<string, string>()
                        );
                    }

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        Registro registro = new Registro
                        {
                            NumeroRegistro = worksheet.Cells[row, 1].Text,
                            NomeRegistro = worksheet.Cells[row, 2].Text,
                            DataRegistro =  worksheet.Cells[row, 3].Text,
                            ValorRegistro =  worksheet.Cells[row, 4].Text
                        };

                        Lista1.Add(registro);
                    }

                    for (int row = 2; row <= worksheet2.Dimension.End.Row; row++)
                    {
                        Registro registro = new Registro
                        {
                            NumeroRegistro = worksheet2.Cells[row, 1].Text ,
                            NomeRegistro = worksheet2.Cells[row, 2].Text ,
                            DataRegistro = worksheet2.Cells[row, 3].Text ,
                            ValorRegistro =  worksheet2.Cells[row, 4].Text
                        };

                        Lista2.Add(registro);
                    }

                    for (int row = 2; row <= worksheet3.Dimension.End.Row; row++)
                    {
                        Registro registro = new Registro
                        {
                            NumeroRegistro =  worksheet3.Cells[row, 1].Text,
                            NomeRegistro =worksheet3.Cells[row, 2].Text ,
                            DataRegistro =  worksheet3.Cells[row, 3].Text ,
                            ValorRegistro =  worksheet3.Cells[row, 4].Text 
                        };

                        Lista3.Add(registro);
                    }

                    string nome = worksheet.Name;
                    string nome2 = worksheet2.Name;
                    string nome3 = worksheet3.Name;

                    //var duplicadosDentroda1 = Lista1
                    //    .Where(x => Lista1.Count(y => x.NumeroRegistro == y.NumeroRegistro) > 1).ToList();



                    var duplicadosDentroda2 = Lista2
                        .Where(x => Lista2.Count(y => x.NumeroRegistro == y.NumeroRegistro) > 1)
                        .Distinct()
                        .ToList();



                    duplicadosTodos = Lista3
                    .Where(x =>
                        Lista2.Any(y => x.NomeRegistro == y.NomeRegistro)
                        ||
                        Lista1.Any(z => x.NomeRegistro == z.NomeRegistro)
                    )
                    .GroupBy(x => x.NomeRegistro)
                    .Select(g => g.First())
                    .ToList();


                    Dictionary<string, string> nomes = new()
                    {
                        { "nome1", nome },
                        { "nome2", nome2 },
                        { "nome3", nome3 }
                    };


                    Dictionary<string, List<Registro>> duplicados = new()
                    {
                        { "DentroDa2", duplicadosDentroda2 },
                        { "DuplicadosTodos", duplicadosTodos }
                    };

                    return (Lista1, Lista2, Lista3, duplicados, nomes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao abrir o documento: " + ex.Message);


                return (
                    new List<Registro>(),
                    new List<Registro>(),
                    new List<Registro>(),
                    new Dictionary<string, List<Registro>>(),
                    new Dictionary<string, string>()
                );
            }
        }
    }
}