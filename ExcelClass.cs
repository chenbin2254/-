using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Aspose.Cells;
//using Excel = NetOffice.ExcelApi;

namespace CHXQ.XMManager
{
    class ExcelClass
    {
        public static DataTable ReadExcelFile(string ExcelPath, int firstRow = 0, int firstColumn = 0, int sheetIndex = 0,bool NeedHead=true,int maxRow=-1)
        {
            Workbook pWorkbook = new Workbook();
            pWorkbook.Open(ExcelPath);
            Cells pCells = pWorkbook.Worksheets[sheetIndex].Cells;
            if (maxRow == -1)
                maxRow = pCells.MaxDataRow;
            DataTable pDataTable = pCells.ExportDataTableAsString(firstRow, firstColumn, maxRow + 1, pCells.MaxColumn + 1, NeedHead);
            return pDataTable;
        }
        public static DataTable ReadExcelFile(string ExcelPath, string SheetName, int firstRow = 0, int firstColumn = 0, bool NeedHead = true, int maxRow = -1)
        {
            Workbook pWorkbook = new Workbook();
            pWorkbook.Open(ExcelPath);
            int sheetIndex = FindSheetByName(ExcelPath, SheetName);
            Cells pCells = pWorkbook.Worksheets[sheetIndex].Cells;
            if (maxRow == -1)
                maxRow = pCells.MaxDataRow;
            DataTable pDataTable = pCells.ExportDataTableAsString(firstRow, firstColumn, maxRow + 1, pCells.MaxColumn + 1, NeedHead);
            return pDataTable;
        }
        public static string[] GetExcelSheetNames(string ExcelPath)
        {
            Workbook pWorkbook = new Workbook();
            pWorkbook.Open(ExcelPath);
            string[] SheetNames = new string[pWorkbook.Worksheets.Count];
            for (int i = 0; i < pWorkbook.Worksheets.Count; i++)
            {
                SheetNames[i] = pWorkbook.Worksheets[i].Name;
 
            }
            return SheetNames;
        }

        public static void ExpReport(DataTable Reporttable, string SavePath, bool isFieldNameShown = true, string TemplateXls=null)
        {
            Workbook pWorkbook = new Workbook();        
          
            if (!string.IsNullOrEmpty(TemplateXls))
            {
                if( System.IO.File.Exists(TemplateXls) )
                    pWorkbook.Open(TemplateXls);
            }
            Worksheet psheet = pWorkbook.Worksheets[0];
            string StatrCell = "A1";
            if (!isFieldNameShown) StatrCell = "A2";
            psheet.Cells.ImportDataTable(Reporttable, isFieldNameShown, StatrCell);
            psheet.AutoFitRows();
            if (System.IO.File.Exists(SavePath))
            {
                try
                {
                    System.IO.File.Delete(SavePath);
                }
                finally { }

            }
            pWorkbook.Save(SavePath);
        }
        public static int FindSheetByName(string ExcelPath,string SheetName)
        {
            Workbook pWorkbook = new Workbook();
            pWorkbook.Open(ExcelPath);
            
            for (int i = 0; i < pWorkbook.Worksheets.Count; i++)
            {
                if (pWorkbook.Worksheets[i].Name.Equals(SheetName))
                    return i;

            }
            return -1;
        }
    }
}
