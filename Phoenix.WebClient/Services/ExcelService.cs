using ClosedXML.Excel;

namespace Phoenix.WebClient.Services
{
    public static class ExcelService
    {
        public static byte[] GenerateExcelWorkbook<TEntity>(List<TEntity> dataToExport)
        {
            var stream = new MemoryStream();

            var fieldNames =
                typeof(TEntity).GetProperties().Select(x => x.Name);

            using (var wbook = new XLWorkbook())
            {
                var ws = wbook.AddWorksheet();
                ws.Cell("A1").InsertData(fieldNames, true);
                ws.Cell("A2").InsertData(dataToExport);
                wbook.SaveAs(stream);
            }
            stream.Seek(0, SeekOrigin.Begin);
            byte[] b;

            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes((int)stream.Length);
            }
            return b;
        }
    }
}
