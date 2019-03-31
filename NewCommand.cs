using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Interop;
using System.ComponentModel;
using HR.Data;
using System.Data;
using System.Windows.Forms;
using Autodesk.AutoCAD.Interop.Common;
using System.IO;
using Autodesk.AutoCAD.Windows;
using OAUS.Core;
using Autodesk.AutoCAD.Geometry;
[assembly: CommandClass(typeof(CHXQ.XMManager.NewCommand))]
namespace CHXQ.XMManager
{
    public class NewCommand
    {
       //private CADObjectEditCtrl pCADObjectEditCtrl = null;
          [CommandMethod("SJDR")]
        public void New()
        {
            try
            {
                string serverIP = CIni.ReadINI("updateconfig", "ServerIP");
                int serverPort = int.Parse(CIni.ReadINI("updateconfig", "ServerPort"));
                if (VersionHelper.HasNewVersion(serverIP, serverPort))
                {
                    if (MessageBox.Show("服务器端发布了更新，请退出AutoCAD然后运行获取更新程序", "提示",
                        MessageBoxButtons.OKCancel
                        , MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        AcadApplication AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AutoCAD.Application");
                        AcadApp.Quit();

                        return;
                        //MessageBox.Show("完成关闭");
                    }
                }
            }
            finally
            { 

                ImportDataFrm pImportDataFrm = new ImportDataFrm();
                if (pImportDataFrm.ShowDialog() == DialogResult.OK)
                {
                    pWork = new BackgroundWorker();
                    pWork.WorkerReportsProgress = true;
                    pWork.WorkerSupportsCancellation = true;
                    pWork.DoWork += PWork_DoWork;
                    pWork.ProgressChanged += PWork_ProgressChanged;
                    
                  
                    pWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(pWork_RunWorkerCompleted);

                    pProgressFrm = HR.Controls.ProgressFrm.GetInstance();
                    pProgressFrm.CanCancle = false;
                    pProgressFrm.FormClosed += new FormClosedEventHandler(pProgressFrm_FormClosed);
                    pProgressFrm.SetProgressStyle(ProgressBarStyle.Blocks);
                    pProgressFrm.Text = "数据成图";
                    pWork.RunWorkerAsync(pImportDataFrm.pWorkArgument);
                    //pProgressFrm.FormClosing += new FormClosingEventHandler(pProgressFrm_FormClosing);

                /*    pCADObjectEditCtrl = new CADObjectEditCtrl();

                    Autodesk.AutoCAD.Windows.PaletteSet ps = new Autodesk.AutoCAD.Windows.PaletteSet("管网管理");

                    ps.Style = PaletteSetStyles.ShowTabForSingle;
                    ps.Style = PaletteSetStyles.NameEditable;
                    ps.Style = PaletteSetStyles.ShowPropertiesMenu;
                    ps.Style = PaletteSetStyles.ShowAutoHideButton;
                    ps.Style = PaletteSetStyles.ShowCloseButton;

                    ps.Dock = DockSides.Left;
                    ps.Visible = true;
                    ps.MinimumSize = new System.Drawing.Size(230, 490);
                    ps.Size = new System.Drawing.Size(230, 490);

                    ps.Add("管网管理", pCADObjectEditCtrl);
                    ps.Activate(0);*/
                }
            }
        }

          void pProgressFrm_FormClosed(object sender, FormClosedEventArgs e)
          {
              if (pWork.IsBusy)
              {
                  pWork.CancelAsync();
              }
          }

          void pWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
          {
              pProgressFrm.SafeCallCloseDialog();
              
          }

          private void pProgressFrm_FormClosing(object sender, FormClosingEventArgs e)
          {
              if (pWork != null)
              {
                  if (pWork.IsBusy)
                  {
                      pWork.CancelAsync();
                  }
              }
          }
          private HR.Controls.ProgressFrm pProgressFrm = null;
          private void PWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
          {
              Progressup(e.ProgressPercentage);
              if (e.UserState != null)
                  Labelup(e.UserState.ToString());
          }
          private BackgroundWorker pWork = null;
          delegate void Progressupdelegate(int value);
          delegate void Labeldelegate(string msg);
          private void Progressup(int value)
          {
              pProgressFrm.SafeCallDisplayProgress(value);

          }
        
          private void Labelup(string msg)
          {
              pProgressFrm.SafeCallDisplayText(msg);
              
          }
          
          private DataRow IsExistSURVEYID(string SURVEYID)
          {
              string sql = string.Format("select  * from Points where SURVEY_ID='{0}'", SURVEYID);
              IDataBase pDataBase = SysDBUnitiy.OleDataBase;
              System.Data.DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
              if (pTable.Rows.Count == 0) return null;
              return pTable.Rows[0];
 
          }
      
          private void PWork_DoWork(object sender, DoWorkEventArgs e)
          {
              //pProgressFrm.TopMost = true;
              pProgressFrm.ShowDialog();
              WorkArgument pWorkArgument = e.Argument as WorkArgument;


              string ExcelPath = pWorkArgument.ExcelPath;

              string ExcelName = System.IO.Path.GetFileNameWithoutExtension(ExcelPath);
              //string ProjectName = pWorkArgument.ProjectName;
              
              string DwgPath = pWorkArgument.DwgSavePath;
              SysDBUnitiy.MDBPath = Path.GetDirectoryName(DwgPath) + string.Format("\\{0}.db", Path.GetFileNameWithoutExtension(DwgPath));

              string TemplateDwg = SysDBUnitiy.RootDir + "\\Template\\template.dwg";
              string TemplateMdb = SysDBUnitiy.RootDir + "\\Template\\Template.db";
              AcadApplication  AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AutoCAD.Application");

              pWork.ReportProgress(0, "正在创建数据文件");
              if (File.Exists(SysDBUnitiy.MDBPath))
              {
                  File.Delete(SysDBUnitiy.MDBPath);
              }
              if (!File.Exists(SysDBUnitiy.MDBPath))
              File.Copy(TemplateMdb, SysDBUnitiy.MDBPath, false);

              if (File.Exists(DwgPath))              
              {
                  try
                  {
                      File.Delete(DwgPath);
                  }
                  catch (System.Exception ex)
                  {
                      MessageBox.Show("删除旧dwg文件失败，请检查该文件是否被占用");
                      return;
 
                  }
              }
              if (!File.Exists(DwgPath))
              File.Copy(TemplateDwg, DwgPath, false);
             

              //List<TableConfig> LineTableName = new List<TableConfig>();
              //List<TableConfig> PointTableName = new List<TableConfig>();                         
              pWork.ReportProgress(0, "正在准备写入dwg文件数据");
              Autodesk.AutoCAD.Interop.AcadDocument AcadDoc = AcadApp.Documents.Open(DwgPath);
              double MinX, MinY, MaxX, MaxY;
              //string[] MinPoint = System.Configuration.ConfigurationSettings.AppSettings["MinPoint"].Split(',');

              MinX = double.Parse(CIni.ReadINI("XYExtent", "MinX"));
              MinY = double.Parse(CIni.ReadINI("XYExtent", "MinY"));

              //string[] MaxPoint = System.Configuration.ConfigurationSettings.AppSettings["MaxPoint"].Split(',');
              MaxX = double.Parse(CIni.ReadINI("XYExtent", "MaxX"));
              MaxY = double.Parse(CIni.ReadINI("XYExtent", "MaxY"));
              pWork.ReportProgress(0, "正在读取表格数据");
              DataTable PCTable = ExcelClass.ReadExcelFile(ExcelPath);
              DataTable CoordTable = ExcelClass.ReadExcelFile(pWorkArgument.CoordExcelpath);
              CoordTable.Columns[0].ColumnName = "ID";
              //CoordTable.PrimaryKey = new DataColumn[] { CoordTable.Columns[0] };
              DataRow FirstRow = CoordTable.Rows[0];
              for (int j = 1; j < FirstRow.ItemArray.Length; j++)
              {
                  string value = FirstRow.ItemArray[j].ToString();
                  double V_num = 0;
                  if (double.TryParse(value, out V_num))
                  {
                      if (V_num > MinX && V_num < MaxX)
                      {
                          CoordTable.Columns[j].ColumnName = "CO_X"; 
                      }
                      else if (V_num > MinY && V_num < MaxY)
                      {
                          CoordTable.Columns[j].ColumnName = "CO_Y";
                      }
                      else
                          CoordTable.Columns[j].ColumnName = "CO_Z";
                  }
              }
              Dictionary<string, DataRow> CoordBHTable = new Dictionary<string, DataRow>();
              foreach (DataRow pCoordRow in CoordTable.Rows)
              {
                  string key = pCoordRow["ID"].ToString();
                  if (!CoordBHTable.ContainsKey(key))
                      CoordBHTable.Add(key, pCoordRow); 
              }
              DataTable ErrorTable = PCTable.Clone();
              if (!ErrorTable.Columns.Contains("错误消息"))
                  ErrorTable.Columns.Add("错误消息");
              #region 循环遍历表
              //int SumCount = PCTable.Rows.Count;
              //int CurNum =0;
              DataTable PCTab2 = PCTable.Copy();
              for (int i = 0; i < PCTable.Rows.Count; i++)
              {
                  //CurNum++;
                  DataRow pDataRow = PCTable.Rows[i];
                  try
                  {
                       pWork.ReportProgress(i*100/PCTable.Rows.Count,string.Format("正在导入第: {0}/{1}条", i+1, PCTable.Rows.Count));
                    
                      string ErrorMsg = string.Empty;
                  
                      string SPointID = GetValue(pDataRow, "起点点号");
                      if (string.IsNullOrEmpty(SPointID))
                          ErrorMsg += "起点号不能为空;";
                      IPipePoint SPoint = null;
                      DataRow SPointRow = IsExistSURVEYID(SPointID);
                      if (SPointRow == null)
                      {
                         
                          if(!CoordBHTable.ContainsKey(SPointID))
                          {
                              ErrorMsg += "起点对应的坐标不存在;";

                          }
                          
                          if (ErrorMsg != string.Empty)
                          {
                              goto a1;
                          }
                          //DataRow pCoordRow = CoordBHTable[SPointID];
                          SPoint = GetPipePoint(SPointID, pDataRow, CoordBHTable[SPointID]);
                          SPoint.AddNew();
                          SPoint.DrawCADObject(AcadDoc);
                          //CoordBHTable.ContainsKey(SPointID)
                          CoordBHTable.Remove(SPointID);
                      }
                      else
                      {
                          //string ClassName = SPointRow["ClassName"].ToString();
                          //string ID = SPointRow["ID"].ToString();
                          SPoint = new PCPoint();
                          //string sql = string.Format("select * from Points where ID='{0}'" , ID);

                          //DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];

                          //if (pTable.Rows.Count > 0)
                          //{
                          SPoint.FillValueByRow(SPointRow);
                          //}
                      }
                      string EPointID = GetValue(pDataRow, "终点点号");
                      if (EPointID == string.Empty)
                          goto a1;
                      IPipePoint EPoint = null;
                      DataRow EPointRow = IsExistSURVEYID(EPointID);

                     if (EPointRow==null)
                     {
                           EPointRow =FindRow( PCTable, EPointID,i+1);
                         if (EPointRow == null)
                         {
                             ErrorMsg += "终点对应的信息不存在;";
                             goto a1;
                         }
                      //   DataRow[] ECoordRows = CoordTable.Select(string.Format("ID='{0}'", EPointID));
                         if (!CoordBHTable.ContainsKey(EPointID))
                         {
                             ErrorMsg += "终点对应的坐标不存在;";
                             goto a1;
                         }

                      /*   string EPointType = GetValue(EPointRow, "起点类型");
                         if (EPointType == string.Empty)
                         {
                             ErrorMsg += "终点类型不能为空";
                         }
                         string ClassName = GetTrueType(EPointType);
                         if (ClassName == string.Empty)
                         {
                             ErrorMsg += "终点类型无法识别";

                         } */
                         if (ErrorMsg != string.Empty)
                         {
                             goto a1;
                         }
                         EPoint = GetPipePoint(EPointID, EPointRow, CoordBHTable[EPointID]);
                         EPoint.AddNew();
                         EPoint.DrawCADObject(AcadDoc);
                         CoordBHTable.Remove(EPointID);
                     }
                     else
                     {
                         //string ClassName = EPointRow["ClassName"].ToString();
                         //string ID = EPointRow["ID"].ToString();
                         EPoint = new PCPoint();
                         //string sql = string.Format("select * from Points where ID='{0}'", ID);

                         //DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];

                         //if (pTable.Rows.Count > 0)
                         //{
                         EPoint.FillValueByRow(EPointRow);
                         //}
 
                     }
                      string GJ = GetValue(pDataRow, "管径");
                      IPIPELine pPIPELine = new PIPELineClass();
                      if (!pPIPELine.isExistSURVEY_ID(SPointID, EPointID))
                      {
                          
                          pPIPELine.Width = GJ;
                          pPIPELine.US_SURVEY_ID = SPointID;
                          pPIPELine.DS_SURVEY_ID = EPointID;
                          //pPIPELine.ID = pPIPELine.GetHead() + SPointID.Substring(2, 3) + pPIPELine.GetNextNO();
                          pPIPELine.ID = Guid.NewGuid().ToString("N");
                          pPIPELine.US_OBJECT_ID = SPoint.ID;
                          pPIPELine.DS_OBJECT_ID = EPoint.ID;
                          //pPIPELine.SYSTEM_TYPE = SPoint.SYSTEM_TYPE;

                          double D_X = double.Parse( EPoint.X) -double.Parse( SPoint.X);
                          double D_Y = double.Parse(EPoint.Y) -double.Parse( EPoint.Y);
                          double Len = Math.Round(Math.Sqrt(D_X * D_X + D_Y * D_Y), 2);

                          pPIPELine.Pipe_Length = Len.ToString();
                          //pPIPELine.US_POINT_INVERT_LEVEL = SPoint.INVERT_LEVEL;

                         /* string US_NS = GetValue(pDataRow, "起点管口泥深");
                          if (US_NS != string.Empty)
                              pPIPELine.US_INVERT_LEVEL = (double.Parse(SPoint.GROUND_LEVEL) - double.Parse(US_NS)/100).ToString("0.00");
                          string DS_NS = GetValue(pDataRow, "终点管口泥深");
                          if (DS_NS != string.Empty)
                              pPIPELine.DS_INVERT_LEVEL = (double.Parse(EPoint.GROUND_LEVEL) - double.Parse(DS_NS)/100).ToString("0.00");
                          */
                          pPIPELine.SEDIMENT_DEPTH = GetValue(pDataRow, "泥深");
                          pPIPELine.MATERIAL = GetValue(pDataRow, "材质");
                          pPIPELine.PRESSURE = GetValue(pDataRow, "管道形式");
                          pPIPELine.STATE = GetValue(pDataRow, "设施状态");
                          pPIPELine.ROAD_NAME = GetValue(pDataRow, "所在道路");
                          pPIPELine.Remark = GetValue(pDataRow, "线备注");

                          pPIPELine.WATER_LEVEL = GetValue(pDataRow, "泥深");
                          pPIPELine.WATER_QUALITY = GetValue(pDataRow, "水质");
                          pPIPELine.WATER_State = GetValue(pDataRow, "水体状态");
                          pPIPELine.Dirtcion = GetValue(pDataRow, "流向");

                          pPIPELine.AddNew();
                          pPIPELine.DrawCADObject(AcadDoc);
                      }
                      a1:
                      if (ErrorMsg != string.Empty)
                      {
                          DataRow ErrorRow = ErrorTable.NewRow();

                          List<object> ItemList = new List<object>();
                       
                          if (pDataRow.Table.Columns.Contains("错误消息"))
                          {
                              pDataRow["错误消息"] = ErrorMsg;
                              ItemList.AddRange(pDataRow.ItemArray);
                          }
                          else
                          {
                              ItemList.AddRange(pDataRow.ItemArray);
                              ItemList.Add(ErrorMsg);
                          }

                          ErrorRow.ItemArray = ItemList.ToArray();
                          ErrorTable.Rows.Add(ErrorRow);
                          
                      }
                      //PCTable.Rows.RemoveAt(i);
                  }
                  catch (System.Exception ex)
                  {
                      DataRow ErrorRow = ErrorTable.NewRow();

                      List<object> ItemList = new List<object>();

                      if (pDataRow.Table.Columns.Contains("错误消息"))
                      {
                          pDataRow["错误消息"] = ex.Message;
                          ItemList.AddRange(pDataRow.ItemArray);
                      }
                      else
                      {
                          ItemList.AddRange(pDataRow.ItemArray);
                          ItemList.Add(ex.Message);
                      }

                      ErrorRow.ItemArray = ItemList.ToArray();
                      ErrorTable.Rows.Add(ErrorRow);
 
                  }
                  pWork.ReportProgress((i+1) * 100 / PCTable.Rows.Count);
              }
             int   n=0;
                //pProgressFrm.SafeCallDisplayProgress(0);

              foreach (KeyValuePair<string, DataRow> CoordBH in CoordBHTable)
              {
                  pWork.ReportProgress(n * 100 / CoordBHTable.Count, string.Format("正在导入第: {0}/{1}个多余点", n, CoordBHTable.Count));
                  n++;
                   
               //   pProgressFrm.SafeCallDisplayText(string.Format("正在导入第: {0}/{1}个多余点", n, CoordBHTable.Count));
                
                  IPCPoint pPCPoint = new PCPoint();
                  if (IsExistSURVEYID(CoordBH.Key) == null)
                  {
                      try
                      {
                          pPCPoint.ID = Guid.NewGuid().ToString("N");
                          pPCPoint.SURVEY_ID = CoordBH.Key;
                          pPCPoint.X = CoordBH.Value["CO_X"].ToString();
                          pPCPoint.Y = CoordBH.Value["CO_Y"].ToString();
                          pPCPoint.GROUND_LEVEL = CoordBH.Value["CO_Z"].ToString();
                          pPCPoint.AddNew();
                          pPCPoint.DrawCADObject(AcadDoc);
                      }
                      catch (System.Exception ex)
                      {
 
                      }

                  }
                  pWork.ReportProgress(n * 100 / CoordBHTable.Count);
                 // pProgressFrm.SafeCallDisplayProgress(n * 100 / CoordBHTable.Count);
              }
              //pProgressFrm.SafeCallCloseDialog();
              MessageBox.Show(string.Format("总共导入{0}条管线数据，{3}个多余点，其中成功{1}条，失败{2}条。",
                      PCTab2.Rows.Count, PCTab2.Rows.Count - ErrorTable.Rows.Count, ErrorTable.Rows.Count, CoordBHTable.Count
                      ), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
              if (ErrorTable.Rows.Count > 0)
              {
                  string ReportXls = System.IO.Path.GetDirectoryName(pWorkArgument.DwgSavePath) + string.Format("\\未导入{0}.xls", DateTime.Now.ToString("MMddHHmm"));
                  ExcelClass.ExpReport(ErrorTable, ReportXls);
                  if (MessageBox.Show("已导出失败记录到输出目录，是否查看","提示",
                      MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
                  {
                      System.Diagnostics.Process pExecuteEXE = new System.Diagnostics.Process();
                      pExecuteEXE.StartInfo.FileName = ReportXls;
                      pExecuteEXE.Start();
 
                  }
              }
              #endregion 
          }
          private DataRow FindRow(DataTable pTable, string PointID, int StartIndex)
          {
              for (int i = StartIndex; i < pTable.Rows.Count; i++)
              {
                  if (pTable.Rows[i]["起点点号"].ToString().Equals(PointID))
                  {
                      return pTable.Rows[i];
                  }
                  
 
              }
              return null;
          }
          private IPipePoint GetPipePoint(string SPointID, DataRow ValueRow, DataRow CoordRow)
          {
              double X = double.Parse(CoordRow["CO_X"].ToString());
              double Y = double.Parse(CoordRow["CO_Y"].ToString());
              double Z = double.Parse(CoordRow["CO_Z"].ToString());


              IPipePoint SPoint = new PCPoint();
 
              (SPoint as IPCPoint).MANHOLE_TYPE = GetValue(ValueRow, "井类型");
              (SPoint as IPCPoint).SEDIMENT_DEPTH = GetValue(ValueRow, "起点管口泥深");
              (SPoint as IPCPoint).FLOW_STATE = GetValue(ValueRow, "水体状态");
              (SPoint as IPCPoint).BOTTOM_TYPE = GetValue(ValueRow, "井底形式");
              (SPoint as IPCPoint).MANHOLE_MATERIAL = GetValue(ValueRow, "井室材质");
              (SPoint as IPCPoint).MANHOLE_SHAPE = GetValue(ValueRow, "井室类型");
              (SPoint as IPCPoint).MANHOLE_SIZE = GetValue(ValueRow, "井室尺寸");
              (SPoint as IPCPoint).COVER_MATERIAL = GetValue(ValueRow, "井盖材质");
              (SPoint as IPCPoint).COVER_STATE = GetValue(ValueRow, "井盖是否损坏");
              //(SPoint as IPCPoint).WATER_QUALITY = GetValue(ValueRow, "水质");
              (SPoint as IPCPoint).Type =GetValue(ValueRow, "起点类型");
              (SPoint as IPCPoint).Depth = GetValue(ValueRow, "起点埋深");

              SPoint.SURVEY_ID = SPointID;

              SPoint.ROAD_NAME = GetValue(ValueRow, "所在道路");
              SPoint.STATE = GetValue(ValueRow, "设施状态");
              SPoint.Remark = GetValue(ValueRow, "点备注");
              
              SPoint.GROUND_LEVEL = Z.ToString();
             
              SPoint.ID = Guid.NewGuid().ToString("N");
              SPoint.X = X.ToString();
              SPoint.Y = Y.ToString();


              return SPoint;
          }
          private string GetTrueType(string pType)
          {
              string sql = string.Format("select TableName from tabledict where ClassValue='{0}'", pType);
              DataTable pTable= SysDBUnitiy.SysDataBase.ExecuteQuery(sql).Tables[0];
              if (pTable.Rows.Count == 0) 
                  return string.Empty;
              else
                 return pTable.Rows[0][0].ToString();
          }
          private string GetValue(DataRow ReadRow, string FieldName)
          {
              if (ReadRow.Table.Columns.Contains(FieldName))
              {
                  string CurValue = ReadRow[FieldName].ToString();
                  if (CurValue.Equals("/"))
                      CurValue = string.Empty;
                  return CurValue;
              }
              else
              {
                  foreach (DataColumn Column in ReadRow.Table.Columns)
                  {
                      if(Column.ColumnName.Contains(FieldName) )
                      {
                          string CurValue = ReadRow[Column.ColumnName].ToString();
                          if (CurValue.Equals("/"))
                              CurValue = string.Empty;
                          return CurValue;
                      }
                  }
                  return string.Empty;
              }
          }
         
     
         
    }
}
