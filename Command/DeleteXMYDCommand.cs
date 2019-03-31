using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HR.Geometry;

namespace CHXQ.XMManager.Command
{
    class DeleteXMYDCommand : DevComponents.DotNetBar.Command
    {
        public DeleteXMYDCommand()
        {
            this.Text = "删除";
            this.ImageSmall = Properties.Resources.DeleteXMYD; 
        }
        public override void Execute(DevComponents.DotNetBar.ICommandSource commandSource)
        {
            XMManagerControl pXMManagerControl = commandSource.CommandParameter as XMManagerControl;
            IXMRK pXMRK= pXMManagerControl.XMRK;
            if (pXMRK != null && pXMRK.ID != -1)
            {
                if (MessageBox.Show(string.Format("是否删除项目{0}", pXMRK.XMBH), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.Cancel) return;
                ISTPointCollection pSTPointCollection = pXMRK.SoucePoints;
                ISTPoint MinPoint = pSTPointCollection.GetMinPoint();
                ISTPoint MaxPoint = pSTPointCollection.GetMaxPoint();
                pXMRK.Delete();
                pXMRK.UpdateMapServerCache(MinPoint.X, MinPoint.Y, MaxPoint.X, MaxPoint.Y);
                MessageBox.Show("删除成功", "消息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
