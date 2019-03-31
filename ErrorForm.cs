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
    public partial class ErrorForm : Form
    {
        public ErrorForm(List<IError> ListError)
        {
            InitializeComponent();
            SetDataView(ListError);
        }
        public void SetDataView(List<IError> ListError)
        {
            DataTable pTable = new DataTable("ErrorTable");
            pTable.Columns.Add("图层名", Type.GetType("System.String"));
            pTable.Columns.Add("错误信息", Type.GetType("System.String"));
            foreach (IError pError in ListError)
            {
                DataRow pRow = pTable.NewRow();
                pRow["图层名"] = pError.LayerName;
                pRow["错误信息"] = pError.MSG;
                pTable.Rows.Add(pRow);
            }
            dataGridView1.AutoSize = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = pTable;
           
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult=DialogResult.Cancel;
            this.Close();
        }
    }
}
