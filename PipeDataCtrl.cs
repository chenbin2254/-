using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CHXQ.XMManager
{
    public partial class PipeDataCtrl : UserControl, IPipeDataCtrl
    {
        protected IPipeData m_PipeData = null;
        public PipeDataCtrl()
        {
            InitializeComponent();
        }
        public virtual void SetData(IPipeData pPipeData) { }
        public virtual IPipeData GetData() { return m_PipeData; }
        public virtual void Reset() { }
        public virtual Point GetPoint() { return new Point(); }

    }
}
