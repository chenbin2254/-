using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using HR.Geometry;
using Microsoft.VisualBasic;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace CHXQ.XMManager
{
    public partial class XMManagerControl : UserControl
    {
        public AcadApplication AcadApp = null;
        private XMDirTreeView pXMWDDirTreeView = null;
        private FileListView pWDFileListView = null;
        private XMTreeview pXMTreeview = null;
      
        public XMManagerControl()
        {
            InitializeComponent();
            AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AutoCAD.Application");
            AcadApp.BeginOpen += new _DAcadApplicationEvents_BeginOpenEventHandler(AcadApp_BeginOpen);
            AcadApp.EndOpen += new _DAcadApplicationEvents_EndOpenEventHandler(AcadApp_EndOpen);

            BtnOpenCAD.Command = new CHXQ.XMManager.Command.OpenCADDocment();
            BtnOpenCAD.CommandParameter = this;
          

            BtnSave.Command = new CHXQ.XMManager.Command.SaveCommand();
            BtnSave.CommandParameter = this;
            BtnSave.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            BtnSave.UseSmallImage = true;      
            
            pXMWDDirTreeView = new XMDirTreeView();
            pXMWDDirTreeView.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(pXMWDDirTreeView);
            pXMWDDirTreeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(pXMDirTreeView_NodeMouseClick);

            pWDFileListView = new FileListView();
            pWDFileListView.Dock = DockStyle.Fill;
            panel3.Controls.Add(pWDFileListView);

            pXMTreeview = new XMTreeview();
            pXMTreeview.Dock = DockStyle.Fill;
            panelEx1.Controls.Add(pXMTreeview);            
            pXMTreeview.LoadData();
            pXMTreeview.ExpandAll();
            pXMTreeview.XMTreeViewOperEvent += new XMTreeViewEventHandler(pXMTreeview_XMTreeViewOperEvent);

            superTabControl1.SelectedTabIndex = 0;
        }

       private void pXMTreeview_XMTreeViewOperEvent(object sender, XMTreeViewEventArgs e)
        {
            IXMRK pXMRK = XMRKClass.GetXMRKByID(e.XMID);
            FillByXMRK(pXMRK);
        }

       

        void AcadApp_EndOpen(string FileName)
        {            
            this.Enabled = true;
        }

        void AcadApp_BeginOpen(ref string FileName)
        {
            this.Enabled = false;
        }

       private void pXMDirTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
           // throw new NotImplementedException();
            TreeNode pNode = e.Node;           
            pWDFileListView.LoadFileNamesByDir(pXMWDDirTreeView.GetNodeID(pNode));
        }
        public void NewXM()
        {
            this.m_XMRK = null;
          //  this.superTabItemFile.Enabled = false;
            //清空UI
            pXMWDDirTreeView.Nodes.Clear();
       

            superGridControlCoords.PrimaryGrid.Rows.Clear();
            superGridControlCoords.Tag = null;
           
       
            TxbTXMJ.Clear();
          
            pWDFileListView.Items.Clear();
       
        }
        private void BtnGetStr_Click(object sender, EventArgs e)
        {

            tableLayoutPanel1.Enabled = false;

            Button btn = sender as Button;
            if (btn == null) return;
            string str = btn.Name;
            switch (str)
            {
                case "BtnTZBH":
                    TxbTZBH.ResetText();
                    TxbTZBH.Text = GetTextString(btn.Text);
                    break;
                case "BtnXMMC":
                    TxbXMMC.ResetText();
                    TxbXMMC.Text = GetTextString(btn.Text);
                    break;
                case "BtnDM":
                    TxbDM.ResetText();
                    TxbDM.Text = GetTextString(btn.Text);
                    break;
                case "BtnBLC":
                    string BLC = GetTextString(btn.Text);
                    BLC = Regex.Replace(BLC, " ", "");
                    int BLCNum = 0;
                    if (!int.TryParse(BLC, out BLCNum))
                    {
                        BLC = BLC.Substring(BLC.IndexOf('1') + 2);
                        if (!int.TryParse(BLC, out BLCNum))
                        {
                            MessageBox.Show("值选择不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (BLCNum < 100)
                    {
                        MessageBox.Show("值选择不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    TxbBLC.Text = BLC;
                    break;
                case "BtnXMMJ":
                    TxbXMMJ.ResetText();
                    TxbXMMJ.Text = GetTextString(btn.Text);
                    break;
                case "BtnZDMJ":
                    TxbZDMJ.ResetText();
                    TxbZDMJ.Text = GetTextString(btn.Text);
                    break;
                case "BtnCLSJ":
                    DtpCLSJ.ResetText();
                    DtpCLSJ.Value = DateTime.ParseExact(GetTextString(btn.Text), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    break;
                case "BtnCTSJ":
                    DtpCTSJ.ResetText();
                    DtpCTSJ.Value = DateTime.ParseExact(GetTextString(btn.Text), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    break;
                case "BtnCLGS":
                    TxbCLGS.ResetText();
                    TxbCLGS.Text = GetTextString(btn.Text);
                    break;

            }
            tableLayoutPanel1.Enabled = true;

        }
        private bool GeoChange = false;
        private AcadObject GetHX()
        {
            
               // tableLayoutPanel2.Enabled = false;
                AcadDocument AcadDoc = AcadApp.ActiveDocument;
            
                Microsoft.VisualBasic.Interaction.AppActivate(AcadApp.Caption);
                object returnObj, pickPoint;
                string pickPrompt = "获取入库红线";
                try
                {
                    AcadDoc.Utility.GetEntity(out returnObj, out pickPoint, pickPrompt);
                }
                catch { return null; }
                AcadObject returnCADObj = (AcadObject)returnObj;

                if (returnCADObj.ObjectName == "AcDbPolyline")
                {
                    AcadLWPolyline Adlwp = (AcadLWPolyline)returnCADObj;
                    if (!Adlwp.Closed)
                    {
                        return null;
                    }
                    GeoChange = true;
                    return returnCADObj;
                }
                //Microsoft.VisualBasic.Interaction.AppActivate(this.Text);
                return null;
            
          


        }
        private IXMRK m_XMRK;
        public IXMRK XMRK
        {
            get { return m_XMRK; }
            set { m_XMRK = value; GeoChange = false; }
        }
        public IXMRK GetXMRK()
        {
            if(m_XMRK==null)
                m_XMRK = new XMRKClass();
            //添加获取属性信息代码
       
           
            if (GeoChange)//如果重新设置了图形
            {
                AcadObject pAcadObject = superGridControlCoords.Tag as AcadObject;
                if (m_XMRK.ID==-1&& pAcadObject == null)
                    throw new Exception("未获取入库图形");
                m_XMRK.STGeometry = ConvertDBObject(pAcadObject);
                //m_XMRK.STGeometry.SRID = 300000;
                //if (!m_XMRK.STGeometry.IsSimplify())
                //{
              m_XMRK.STGeometry=  m_XMRK.STGeometry.Simplify();
             //   m_XMRK.CADPath=
                //}

                ISTPointCollection pSTPointCollection = new STPointCollectionClass();              
                double[] AdlwpPoint = (double[])(pAcadObject as AcadLWPolyline).Coordinates;
                int PointCount = AdlwpPoint.Length / 2;
                for (int j = 0; j < PointCount; j++)
                {
                    ISTPoint pSTPoint = new STPointClass() { X = AdlwpPoint[j * 2], Y = AdlwpPoint[j * 2 + 1] };
                    pSTPointCollection.AddPoint(pSTPoint);
                }
                m_XMRK.SoucePoints = pSTPointCollection;
            }
            return m_XMRK;
        }
        private ISTGeometry ConvertDBObject(AcadObject pAcadObject)
        {
            ISTPointCollection pSTPointCollection = new STPolygonClass();
            AcadLWPolyline Adlwp = (AcadLWPolyline)pAcadObject;
            double[] AdlwpPoint = (double[])Adlwp.Coordinates;
            int PointCount = AdlwpPoint.Length / 2;
            for (int j = 0; j < PointCount - 1; j++)
            {
                ISTPoint fromPoint = new STPointClass();
                fromPoint.X = AdlwpPoint[j * 2];
                fromPoint.Y = AdlwpPoint[j * 2 + 1];

                ISTPoint toPoint = new STPointClass();
                toPoint.X = AdlwpPoint[j * 2 + 2];
                toPoint.Y = AdlwpPoint[j * 2 + 3];

                if (Adlwp.GetBulge(j) != 0)  //添加弧线转折现处理代码
                {
                    double Angle = Math.Atan(Adlwp.GetBulge(j)) * 4;
                    string url = HR.Utility.CommonVariables.GetAppSetString("ConvertArcToPolyline");
                    string data = string.Format("Angle={0}&FromPointX={1}&FromPointY={2}&ToPointX={3}&ToPointY={4}&f=json"
                        , Angle, fromPoint.X, fromPoint.Y, toPoint.X, toPoint.Y);
                    string Request = PostData(url, data);
                    if (Request.Contains("error"))
                    {
                        throw new Exception("调用曲线转折线服务异常");
                    }
                    XmlNode pPointNode= Newtonsoft.Json.JsonConvert.DeserializeXmlNode(Request);
                    string[] XYs = pPointNode.FirstChild.InnerText.TrimEnd().Split(';');
                    foreach (string XY in XYs)
                    {
                        string[] pXY = XY.Split(',');
                        ISTPoint pPoint = new STPointClass();
                        try
                        {
                            pPoint.X = double.Parse(pXY[0]);
                            pPoint.Y = double.Parse(pXY[1]);
                            pSTPointCollection.AddPoint(pPoint);
                        }
                        catch { continue; }
                    }
                }
                else
                {
                    pSTPointCollection.AddPoint(fromPoint);
                }
                if (j == PointCount - 2)
                {
                    pSTPointCollection.AddPoint(toPoint);
                }
            }
            ISTPoint FPoint = new STPointClass();
            FPoint.X = AdlwpPoint[0];
            FPoint.Y = AdlwpPoint[1];
            pSTPointCollection.AddPoint(FPoint);
            return pSTPointCollection;
        }
        private string PostData(string url, string data)
        {

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=gb2312";
            byte[] bs = Encoding.ASCII.GetBytes(data);
            request.ContentLength = bs.Length;
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(bs, 0, bs.Length);
            reqStream.Close();

            string responseString = null;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                responseString = reader.ReadToEnd();
                reader.Close();
            }
            return responseString;
        }
        private string GetTextString(string title)
        {

            string str = string.Empty;
            AcadDocument AcadDoc = AcadApp.ActiveDocument;
            Microsoft.VisualBasic.Interaction.AppActivate(AcadApp.Caption);
            object returnObj, pickPoint;
            string pickPrompt = title;
            try
            {
                AcadDoc.Utility.GetEntity(out returnObj, out pickPoint, pickPrompt);
            }
            catch(Exception ) { return ""; }
            AcadObject returnCADObj = (AcadObject)returnObj;
            if (returnCADObj.ObjectName == "AcDbText")
            {
                str = ((AcadText)returnCADObj).TextString;
            }
            else if (returnCADObj.ObjectName == "AcDbMText")
            {
                string Text = ((AcadMText)returnCADObj).TextString;
                if (Text.Contains('*'))
                {
                    int FirstIndex = Text.IndexOf('*');
                    str = Text.Substring(FirstIndex, Text.IndexOf('*', FirstIndex + 1) - FirstIndex + 1);
                }
                else
                    str = Text;

            }
            return str;            
        }        
              

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (m_XMRK.ID != -1)
            {
              //  superTabItemFile.Enabled = true;
             
                FillByXMRK(m_XMRK);
            }
        }
        private void FillByXMRK(IXMRK pXMRK)
        {
            m_XMRK = pXMRK;
            TxbLABH.Text = pXMRK.LXBH;
            TxbXMMC.Text = pXMRK.XMMC;
            DTLASJ.Value = pXMRK.LASJ;
           /*
            string SavePath = HR.Utility.CommonConstString.STR_TempPath + string.Format("\\{0}.dwg", pXMRK.TZBH);
            try
            {
                if (!File.Exists(SavePath))
                pXMRK.DownloadAD(SavePath);
                AcadApp.Documents.Open(SavePath, null, null);

                if (pXMRK.SoucePoints != null)
                {
                    for (int j = 0; j < pXMRK.SoucePoints.PointCount; j++)
                    {
                        ISTPoint pSTPoint = pXMRK.SoucePoints.GetSTPoint(j);
                        superGridControlCoords.PrimaryGrid.Rows.Add(new DevComponents.DotNetBar.SuperGrid.GridRow(new object[] {j+1,
                            pSTPoint.X.ToString("0.000"), pSTPoint.Y.ToString("0.000") }));
                    }
                }
            }
            finally
            { }*/
        }
        
        private void BtnTX_Click(object sender, EventArgs e)
        {
              AcadDocument AcadDoc = AcadApp.ActiveDocument;
            AcadObject pSelectLineobj = pSelectLineobj = GetHX();
            if (pSelectLineobj == null) return;
            try
            {

                AcadLWPolyline Adlwp = (AcadLWPolyline)pSelectLineobj;
                double[] AdlwpPoint = (double[])Adlwp.Coordinates;
                int PointCount = AdlwpPoint.Length / 2;
                superGridControlCoords.PrimaryGrid.Rows.Clear();
                // ISTPoint[] pSTPoints = new ISTPoint[PointCount];
                for (int j = 0; j < PointCount; j++)
                {
                    ISTPoint pSTPoint = new STPointClass() { X = AdlwpPoint[j * 2], Y = AdlwpPoint[j * 2 + 1] };
                    superGridControlCoords.PrimaryGrid.Rows.Add(new DevComponents.DotNetBar.SuperGrid.GridRow(
                        new object[] { j + 1, Math.Round(AdlwpPoint[j * 2 + 1], 3).ToString("0.000"), Math.Round(AdlwpPoint[j * 2], 3).ToString("0.000") }) { Tag = pSTPoint });
                }
                superGridControlCoords.Tag = Adlwp;
                TxbTXMJ.Text = Adlwp.Area.ToString("0.00");
            }
            finally
            { }
        }

        private void TxbXMBH_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        

        private void BtnAddFile_Click(object sender, EventArgs e)
        {
            TreeNode pNode = pXMWDDirTreeView.SelectedNode;
            if (pNode == null) return;
              OpenFileDialog pDialog = new OpenFileDialog();
            pDialog.Multiselect = true;
            if (pDialog.ShowDialog() == DialogResult.OK)
            {
                string[] FileNames = pDialog.FileNames;
                pXMWDDirTreeView.UploadFiles(FileNames, pNode);
                pWDFileListView.LoadFileNamesByDir(pXMWDDirTreeView.GetNodeID(pNode));
            }
        }

        private void BtnDelFile_Click(object sender, EventArgs e)
        {
            ListViewItem pSelectItem = pWDFileListView.SelectedItems[0];
            if (pSelectItem == null) return;
            if (pSelectItem.Tag == null) return;
            IFile pYWFile = pSelectItem.Tag as IFile;
            if (MessageBox.Show("是否删除所选文件？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                pYWFile.Delete();
                pWDFileListView.Items.Remove(pSelectItem);

            }
        }

        private void superGridControlCoords_CellDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
        {
            DevComponents.DotNetBar.SuperGrid.GridRow pGridRow = e.GridCell.GridRow;
            if (pGridRow == null) return;
            int pPointY = Convert.ToInt32(Convert.ToDouble(pGridRow[1].Value.ToString()));
            int pPointX = Convert.ToInt32(Convert.ToDouble(pGridRow[2].Value.ToString()));
            Point3d pMin = new Point3d(pPointX - 10, pPointY + 5, 0);
            Point3d pMax = new Point3d(pPointX + 10, pPointY, 0);
            ZoomPoint.Zoom(pMin, pMax, new Point3d(), 1);
            DrawCircle(new Point3d(double.Parse(pGridRow[2].Value.ToString()), double.Parse(pGridRow[1].Value.ToString()), 0));
            AcadDocument AcadDoc = AcadApp.ActiveDocument;
            Microsoft.VisualBasic.Interaction.AppActivate(AcadApp.Caption);
        }
        private void DrawCircle(Point3d ZoomPoint)
        {
            DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument();
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            Entity entity = null;
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead) as LayerTable;
                string strLayerName = "Zoom";
                LayerTableRecord acLyrTblRec = new LayerTableRecord();

                if (acLyrTbl.Has(strLayerName) == false)
                {
                    acLyrTblRec.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 255, 255);
                    acLyrTblRec.Name = strLayerName;
                    acLyrTbl.UpgradeOpen();
                    acLyrTbl.Add(acLyrTblRec);
                    acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                }
                else
                {
                    TypedValue[] glq = new TypedValue[]
                {
                    new TypedValue((int)DxfCode.LayerName,strLayerName)
                };
                    SelectionFilter sf = new SelectionFilter(glq);
                    PromptSelectionResult SS = acDoc.Editor.SelectAll(sf);
                    Autodesk.AutoCAD.EditorInput.SelectionSet SSet = SS.Value;
                    if (SSet != null)
                        foreach (ObjectId id in SSet.GetObjectIds())
                        {

                            AcadObject Adobj = null;
                            entity = (Entity)acTrans.GetObject(id, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForWrite, true);
                            DBObject obj = (DBObject)entity;
                            Adobj = (AcadObject)obj.AcadObject;
                            Adobj.Delete();
                        }

                }

                BlockTable acBlkTbl;
                BlockTableRecord acBlkTblRec;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead) as BlockTable;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], Autodesk.AutoCAD.DatabaseServices.OpenMode.ForWrite) as BlockTableRecord;

                Circle acCirc = new Circle();
                acCirc.SetDatabaseDefaults();
                acCirc.Center = ZoomPoint;
                acCirc.Radius = 0.05;
                acCirc.Layer = strLayerName;

                acBlkTblRec.AppendEntity(acCirc);
                acTrans.AddNewlyCreatedDBObject(acCirc, true);

                ObjectIdCollection collection = new ObjectIdCollection();
                collection.Add(acCirc.ObjectId);

                Hatch hatch = new Hatch();
                hatch.HatchStyle = HatchStyle.Normal;
                hatch.Layer = strLayerName;
                hatch.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(0, 0, 255);
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                hatch.AppendLoop(HatchLoopTypes.Default, collection);
                hatch.EvaluateHatch(true);
                acBlkTblRec.AppendEntity(hatch);
                acTrans.AddNewlyCreatedDBObject(hatch, true);

                docLock.Dispose();
                acTrans.Commit();
            }
        }
      
        
    }
}
