using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using HR.Geometry;
using System.Windows.Forms;

namespace CHXQ.XMManager.Command
{
    class SaveCommand : DevComponents.DotNetBar.Command
    {
        public SaveCommand()
        {
            this.Text = "保存";
            this.ImageSmall = Properties.Resources.入库;
        }
        public override void Execute(DevComponents.DotNetBar.ICommandSource commandSource)
        {
            XMManagerControl pXMManagerControl = commandSource.CommandParameter as XMManagerControl;
            IXMRK pXMRK = pXMManagerControl.GetXMRK();
            bool IsNewXMYD = pXMRK.ID == -1;
            if (pXMRK.GetIsGeoCange() && !IsNewXMYD)
            {
                AcadApplication AcadApp = pXMManagerControl.AcadApp;
                UploadCurCAD(AcadApp, pXMRK);   
            }
            try
            {
                pXMRK.Save();
                if (IsNewXMYD)
                {
                    AcadApplication AcadApp = pXMManagerControl.AcadApp;
                    UploadCurCAD(AcadApp, pXMRK);
                }
                ISTPoint MinPoint = pXMRK.STGeometry.GetMinPoint();
                ISTPoint MaxPoint = pXMRK.STGeometry.GetMaxPoint();
                pXMRK.UpdateMapServerCache(MinPoint.X, MinPoint.Y, MaxPoint.X, MaxPoint.Y);
                MessageBox.Show("保存成功", "消息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        private void UploadCurCAD(AcadApplication AcadApp, IXMRK pXMRK)
        {
            AcadDocument AcadDoc = AcadApp.ActiveDocument;

            Microsoft.VisualBasic.Interaction.AppActivate(AcadApp.Caption);
            AcadDoc.Save();

            string path = AcadDoc.Path;
            string DocmantPath = AcadDoc.FullName;
            //string strPath = HR.Utility.CommonConstString.STR_TempPath + "\\" + pXMRK.ID + ".dwg";
            string TempstrPath = HR.Utility.CommonConstString.STR_TempPath + pXMRK.TZBH + ".dwg";
            AcadDoc.SaveAs(TempstrPath, AcSaveAsType.ac2000_dwg, null);
            AcadDoc = AcadApp.ActiveDocument;
            AcadDoc.Close();
            Application.DoEvents();
           /* if (System.IO.File.Exists(strPath))
            {
                System.IO.File.Delete(strPath);
            }
            System.IO.File.Copy(TempstrPath, strPath);*/

            pXMRK.UploadCAD(TempstrPath);    
 
        }
    }
}
