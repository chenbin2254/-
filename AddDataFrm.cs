using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CHXQ.XMManager
{
    public partial class AddDataFrm : Form
    {
        public AddDataFrm()
        {
            InitializeComponent();
        }
        public WorkArgument pWorkArgument;
        private void BtnStart_Click(object sender, EventArgs e)
        {
            /*   if (txbExcelPath.Text == string.Empty)
             {
                 MessageBox.Show("请选择普查数据表格");
                 return;
             }
           if (TxbCoordFile.Text == string.Empty)
             {
                 MessageBox.Show("请选择坐标数据表格");
                 return;
             }
             */
            pWorkArgument = new WorkArgument();
            pWorkArgument.ExcelPath = txbExcelPath.Text;
            
            pWorkArgument.CoordExcelpath = TxbCoordFile.Text;
          

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

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
                    MessageBox.Show("选择的表格不符合格式要求", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txbExcelPath.Clear();
                }

            }
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
    }
}
