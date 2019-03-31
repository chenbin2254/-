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
    public partial class SetLenhtDlg : Form
    {
        public SetLenhtDlg()
        {
            InitializeComponent();
        }
        private double m_Lenght;
        public double Lenght
        {
            get
            {
                return m_Lenght; 
            }
        }
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            if (TxbLenght.Text != string.Empty)
            {
                m_Lenght = double.Parse(TxbLenght.Text);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
