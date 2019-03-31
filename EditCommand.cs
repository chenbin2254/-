using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using OAUS.Core;
using Autodesk.AutoCAD.Interop;

[assembly: CommandClass(typeof(CHXQ.XMManager.EditCommand))]
namespace CHXQ.XMManager
{

    public class EditCommand
    {
        [CommandMethod("edit")]
        public void Edit()
        {
                      
            try
            {
                string serverIP = CIni.ReadINI("updateconfig", "ServerIP");
                int serverPort = int.Parse(CIni.ReadINI("updateconfig", "ServerPort"));
                if (VersionHelper.HasNewVersion(serverIP, serverPort))
                {
                    if (MessageBox.Show("服务器端发布了更新，请退出AutoCAD然后运行获取更新程序", "提示",
                        MessageBoxButtons.OKCancel
                        , MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        AcadApplication AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AutoCAD.Application");
                        AcadApp.Quit();

                        return;
                       
                    }
                } 
            }
             finally
                { 
                    CADObjectEditCtrl pCADObjectEditCtrl = new CADObjectEditCtrl();

                    Autodesk.AutoCAD.Windows.PaletteSet ps = new Autodesk.AutoCAD.Windows.PaletteSet("管网管理");

                    ps.Style = PaletteSetStyles.ShowTabForSingle;
                    ps.Style = PaletteSetStyles.NameEditable;
                    ps.Style = PaletteSetStyles.ShowPropertiesMenu;
                    ps.Style = PaletteSetStyles.ShowAutoHideButton;
                    ps.Style = PaletteSetStyles.ShowCloseButton;

                    ps.Dock = DockSides.Left;
                    ps.Visible = true;
                    ps.MinimumSize = new System.Drawing.Size(556, 490);
                    ps.Size = new System.Drawing.Size(556, 490);

                    AcadApplication AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AutoCAD.Application");
                    Autodesk.AutoCAD.Interop.AcadDocument AcadDoc = AcadApp.ActiveDocument;
                    string CurCadfile = AcadDoc.FullName;
                    string CurMdbName = System.IO.Path.GetDirectoryName(CurCadfile) + "\\" + System.IO.Path.GetFileNameWithoutExtension(CurCadfile) + ".db";
                    //MessageBox.Show(CurMdbName);
                    if (!System.IO.File.Exists(CurMdbName))
                    {
                        System.Windows.Forms.OpenFileDialog pDlg = new System.Windows.Forms.OpenFileDialog();
                        pDlg.Filter = "数据库文件|*.db|所有文件(*.*)|*.*";
                        pDlg.Multiselect = false;
                        if (pDlg.ShowDialog() == DialogResult.OK)
                        {
                            SysDBUnitiy.MDBPath = pDlg.FileName;
                            ps.Add("管网管理", pCADObjectEditCtrl);
                            ps.Activate(0);
                        }
                       
                    }
                    else
                    {
                        SysDBUnitiy.MDBPath = CurMdbName;
                    
                        ps.Add("管网管理", pCADObjectEditCtrl);
                        ps.Activate(0);
                    }
                     
                }
            
           
          
        }
    }
}
