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
    public partial class SURVEYIDQueryFrm : Form
    {
        public SURVEYIDQueryFrm()
        {
            InitializeComponent();
        }
        public string SURVEYID
        {
            get { return TxbSURVEYID.Text.Trim(); }
        }
    }
}
