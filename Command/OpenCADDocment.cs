using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Interop;

namespace CHXQ.XMManager.Command
{
    class OpenCADDocment : DevComponents.DotNetBar.Command
    {
        public OpenCADDocment()
        {
            this.Text = "打开";
            this.ImageSmall = Properties.Resources.打开;
        }
        public override void Execute(DevComponents.DotNetBar.ICommandSource commandSource)
        {

            OpenFileDialog opoFile = new OpenFileDialog();
            opoFile.Filter = "CAD文件(*.dwg)|*.dwg|CAD图形文件(*.dxf)|*.dxf";
            opoFile.Title = "打开入库文件";
            if (opoFile.ShowDialog() == DialogResult.OK)
            {
                string strFileName = opoFile.FileName;
                try
                {
                    AcadApplication AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AutoCAD.Application");
                    AcadDocument AcadDoc = AcadApp.Documents.Open(strFileName, null, null);
                    XMManagerControl pXMManagerControl = commandSource.CommandParameter as XMManagerControl;
                    pXMManagerControl.NewXM();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
