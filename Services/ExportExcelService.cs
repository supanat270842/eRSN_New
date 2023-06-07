using OfficeOpenXml.Table;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace eRSN_New.Services
{
    public class ExportExcelService
    {
        public bool ExportXLSX(DataTable dt, List<string> col, string path)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                FileInfo file = new FileInfo(path);

                if (file.Exists)
                {
                    file.Delete();
                } // end if: file is exists will delete first

                using (ExcelPackage excel = new ExcelPackage(file))
                {
                    ExcelWorksheet ws = excel.Workbook.Worksheets.Add("Sheet1");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.None);
                    ws.Cells[1, 1].LoadFromText(string.Join(",", col));

                    ws.Cells.AutoFitColumns();
                    ws.Row(1).Style.Font.Bold = true;
                    excel.Save();

                    return true;
                } // end using: excel
            } // end try

            catch
            {
                return false;
            } // end catch:
        } // end ExcelPackage: ExportXLSX
    } // end class: ExportExcelService
} // end namespace: eRSN_New.Services