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
    public partial class ErrorItemFrm : Form
    {
        public ErrorItemFrm(string[] ErrorItems)
        {
            InitializeComponent();
            LstErrorItems.Items.AddRange(ErrorItems);
        }
    }
}
