using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using HR.Data;

using System.Configuration;
using System.Threading;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD.Interop;
using System.Reflection;

namespace CHXQ.XMManager
{
    public partial class ImportDataFrm : Form
    {
        public ImportDataFrm()
        {
            InitializeComponent();
            TxbLableMin.Text = CIni.ReadINI("DrawCAD", "LableMin");
            TxbArrowMin.Text = CIni.ReadINI("DrawCAD", "ArrowMin");
            //this.FormClosing += ImportDataFrm_FormClosing;
            //AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AutoCAD.Application");
            //AcadApp.Application.Visible = false;
            //AcadDoc = AcadApp.ActiveDocument;
            //AcadDoc.Close(false);
        }

         
        
        
      

        
    /*    private void ImportDataFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pWork != null)
            {
                if (pWork.IsBusy)
                {
                    pWork.CancelAsync();
                }
            }
        }
        */
    
        //delegate void tabControldelegate(bool value);
        
        private void BtnOpenExcel_Click(object sender, EventArgs e)
        {
             OpenFileDialog pDialog = new OpenFileDialog();
            pDialog.Filter = "Excel文件|*.xls;*.xlsx|所有文件(*.*)|*.*";
            pDialog.Title = "选择入库文件";
            if (pDialog.ShowDialog() == DialogResult.OK)
            {
                txbExcelPath.Text = pDialog.FileName;
                DataTable CurTable = ExcelClass.ReadExcelFile(pDialog.FileName, 0, 0, 0, true, 1);
                if (!CurTable.Columns.Contains("起点点号") || !CurTable.Columns.Contains("终点点号"))
                {
                    MessageBox.Show("选择的表格不符合格式要求","错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txbExcelPath.Clear();
                }
               
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog pDialog = new FolderBrowserDialog();
            SaveFileDialog pDialog = new SaveFileDialog();
            pDialog.Filter = "CAD文件|*.dwg";
            pDialog.Title = "选择保存路径";
            if (pDialog.ShowDialog() == DialogResult.OK)
            {
                TxbSaveDir.Text = pDialog.FileName;
                

            }
        }
        public WorkArgument pWorkArgument;
        private void BtnStart_Click(object sender, EventArgs e)
        {

            if (txbExcelPath.Text == string.Empty)
            {
                MessageBox.Show("请选择普查数据表格");
                return;
            }
            if (TxbCoordFile.Text == string.Empty)
            {
                MessageBox.Show("请选择坐标数据表格");
                return;
            }
           

            CIni.WriterINI("DrawCAD", "LableMin",TxbLableMin.Text);
            CIni.WriterINI("DrawCAD", "ArrowMin", TxbArrowMin.Text);
             pWorkArgument = new WorkArgument();
            pWorkArgument.ExcelPath = txbExcelPath.Text;
            pWorkArgument.DwgSavePath = TxbSaveDir.Text;
            pWorkArgument.CoordExcelpath = TxbCoordFile.Text;
            //pWorkArgument.ProjectName = txbProject.Text;
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
             

        }

   /*     private void PWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progressup(e.ProgressPercentage);
        }
        
     */
       

      

        private void BtnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void BtnOpenCoordExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog pDialog = new OpenFileDialog();
            pDialog.Filter = "Excel文件|*.xls;*.xlsx|所有文件(*.*)|*.*";
            pDialog.Title = "选择入库文件";
            if (pDialog.ShowDialog() == DialogResult.OK)
            {
                TxbCoordFile.Text = pDialog.FileName;
                
            }
        }

        private void BtnOnline_Click(object sender, EventArgs e)
        {
            OnlineTable pOnlineTable = new OnlineTable();
            pOnlineTable.Show();
        }

       

    }
    public class WorkArgument
    {
        public string ExcelPath;
        public string DwgSavePath;
        public string CoordExcelpath;
        //public string ProjectName;
        //public string[] ImportTableNames;
        //public string FieldYSFile;
 
    }
 
}
