using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HR.Data;
using System.Data;


namespace CHXQ.XMManager
{
    static class YingSheiniRW
    {
        public static Dictionary<string, FieldInfo> ReadYingShes(string YingSheFilePath, string ClassName)
        {

            Dictionary<string, FieldInfo> ExcelColNameYS = new Dictionary<string, FieldInfo>();
           
            //string sql = string.Format("select * from [{0}]", ClassName);
            int SheetIndex = ExcelClass.FindSheetByName(YingSheFilePath,ClassName);
            if (SheetIndex == -1) return null;
            DataTable pDataTable = ExcelClass.ReadExcelFile(YingSheFilePath, 0, 0, SheetIndex, true);
            foreach (DataRow pRow in pDataTable.Rows)
            {
                if (pRow[1].ToString().Trim() != string.Empty)
                {
                    ExcelColNameYS.Add(pRow[0].ToString(), new FieldInfo() { ExcelName = pRow[1].ToString(), FieldName = pRow[0].ToString(), IsNeed = pRow[3].ToString().Equals("是"), FieldType = pRow[2].ToString() });
                }
            }
            return ExcelColNameYS;
        }
        
    }
    class FieldInfo
    {
        public string ExcelName;
        public string FieldName;
        public bool IsNeed = false;
        public string FieldType;
    }
}
         
     

