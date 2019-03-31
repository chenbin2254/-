using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HR.Data;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Interop;
using System.Runtime.InteropServices;

namespace CHXQ.XMManager
{
    public partial class PipeLineCtrl : UserControl, IPipeDataCtrl
    {
        private CADObjectEditCtrl m_CADObjectEditCtrl = null;
        public PipeLineCtrl(CADObjectEditCtrl pCADObjectEditCtrl)
        {
            InitializeComponent();
            pPIPELine = new PIPELineClass();
            m_CADObjectEditCtrl = pCADObjectEditCtrl;
        }
        public bool IsErrorData { get; set; }
        public IPipeData GetData()
        {
            if (pPIPELine == null)
                pPIPELine = new PIPELineClass();
            //pPIPELine.ID = TxbID.Text;
            pPIPELine.ACQUISITION_DATE = TxbACQUISITION_DATE.Text;
            pPIPELine.ACQUISITION_UNIT = TxbACQUISITION_UNIT.Text;

            pPIPELine.PROCESS_Date = TxbPROCESS_DATE.Text;
            pPIPELine.PROCESS_Unit = TxbPROCESS_UNIT.Text;
            pPIPELine.ROAD_NAME = TxbROAD_NAME.Text;
            pPIPELine.STATE = TxbSTATE.Text;
            //pPIPELine.SYSTEM_TYPE= TxbSYSTEM_TYPE.Text;

            //pPIPELine.DS_INVERT_LEVEL= TxbDS_INVERT_LEVEL.Text ;
            //pPIPELine.DS_OBJECT_ID= TxbDS_OBJECT_ID.Text ;
            //pPIPELine.DS_POINT_INVERT_LEVEL= TxbDS_POINT_INVERT_LEVEL.Text ;
            pPIPELine.DS_SURVEY_ID = TxbDS_SURVEY_ID.Text;
            pPIPELine.MATERIAL = TxbMATERIAL.Text;
            pPIPELine.Pipe_Length = TxbPipe_Length.Text;
            pPIPELine.PRESSURE = TxbPRESSURE.Text;

            pPIPELine.SEDIMENT_DEPTH = TxbSEDIMENT_DEPTH.Text;
            //pPIPELine.US_INVERT_LEVEL= TxbUS_INVERT_LEVEL.Text ;
            //pPIPELine.US_OBJECT_ID= TxbUS_OBJECT_ID.Text ;
            //pPIPELine.US_POINT_INVERT_LEVEL= TxbUS_POINT_INVERT_LEVEL.Text ;
            pPIPELine.US_SURVEY_ID = TxbUS_SURVEY_ID.Text;
            pPIPELine.Width = TxbWidth.Text;
            pPIPELine.Remark = TxbRemark.Text;           

            pPIPELine.WATER_LEVEL = TxbWATER_LEVEL.Text;
            pPIPELine.WATER_QUALITY = TxbWATER_QUALITY.Text;
            pPIPELine.WATER_State = TxbWATER_State.Text;
            pPIPELine.Dirtcion = CmbDirction.Text;
            return pPIPELine;
        }
        
        public bool ShoudReDraw
        {
            get
            {
                if (!pPIPELine.US_SURVEY_ID.Equals(TxbUS_SURVEY_ID.Text)
                    || !pPIPELine.DS_SURVEY_ID.Equals(TxbDS_SURVEY_ID.Text)
                    ||! pPIPELine.Width.Equals(TxbWidth.Text)
                    ||! pPIPELine.MATERIAL.Equals(TxbMATERIAL.Text ) )
                    
                    return true;
                return false;
            }
        }
        public string DataType { get { return "线"; } }
        public void ZoomTo()
        {

            string SPoint = TxbUS_SURVEY_ID.Text.Trim();
            string EPoint = TxbDS_SURVEY_ID.Text.Trim();

            if (SPoint == string.Empty || EPoint == string.Empty)
                return;
            Point3d S_P = GetPointByID(SPoint);
            Point3d E_P = GetPointByID(EPoint);
            if (S_P.X == 0 || S_P.Y == 0)
            {
                throw new Exception("起点不存在");
                
                //MessageBox.Show("起点不存在");
                //return;
            }
            if (E_P.X == 0 || E_P.Y == 0)
            {
                throw new Exception("终点不存在");
               
                //MessageBox.Show("终点不存在");
                //return;
            }
            if (S_P.X != 0 && S_P.Y != 0 && E_P.X != 0 && E_P.Y != 0)
            {
                Point3d Mid_P = new Point3d((S_P.X + E_P.X) / 2, (S_P.Y + E_P.Y) / 2, 0);
               // ZoomPoint.Zoom(S_P, E_P,new Point3d(), 5);
                ZoomPoint.DrawCircle(Mid_P);
                Zoom(Mid_P);
            }

        }
        private void ZoomByPointID(string PointID)
        {
            Point3d pPoint = GetPointByID(PointID);
            if (pPoint.X == 0 || pPoint.Y == 0)
            {
                MessageBox.Show("该点号不存在");
                return;
            }
            ZoomPoint.DrawCircle(pPoint);
            Zoom(pPoint);
 
        }
        private void Zoom(Point3d CurPoint)
        {
            Point3d pMin = new Point3d(CurPoint.X - 10, CurPoint.Y + 5, 0);
            Point3d pMax = new Point3d(CurPoint.X + 10, CurPoint.Y, 0);
            Point3d pPoint3D = new Point3d(CurPoint.X, CurPoint.Y, 0);
            ZoomPoint.DrawCircle(pPoint3D);
            ZoomPoint.Zoom(pMin, pMax, new Point3d(), 5);

        }
        private string GetPointSURVEYID(string ID)
        {

            string sql = string.Format("select SURVEY_ID from Points where ID='{0}'", ID);
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
            return pTable.Rows[0][0].ToString();
          /*  if (ID.StartsWith("YS") || ID.StartsWith("WS"))
                return true;
            else
                return false;*/
        }
        private Point3d GetPointByID(string ObjectID)
        {
            string sql = string.Format("select CO_X,CO_Y from Points where SURVEY_ID='{0}'", ObjectID);
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
            if (pTable.Rows.Count > 0)
            {
                double X = double.Parse(pTable.Rows[0]["CO_X"].ToString());
                double Y = double.Parse(pTable.Rows[0]["CO_Y"].ToString());
                return new Point3d(X, Y,0);
            }
            else
                return new Point3d();
        }
        private IPIPELine pPIPELine = null;
        public void SetData(IPipeData pPipeData)
        {
             pPIPELine = pPipeData as IPIPELine;
            //TxbID.Text = pPipeData.ID;
            TxbACQUISITION_DATE.Text = pPIPELine.ACQUISITION_DATE;
            TxbACQUISITION_UNIT.Text = pPIPELine.ACQUISITION_UNIT;

            TxbPROCESS_DATE.Text = pPIPELine.PROCESS_Date;
            TxbPROCESS_UNIT.Text = pPIPELine.PROCESS_Unit;
            TxbROAD_NAME.Text = pPIPELine.ROAD_NAME;
            TxbSTATE.Text = pPIPELine.STATE;
            //TxbSYSTEM_TYPE.Text = pPIPELine.SYSTEM_TYPE;

            //TxbDS_INVERT_LEVEL.Text = pPIPELine.DS_INVERT_LEVEL;
            //TxbDS_OBJECT_ID.Text = pPIPELine.DS_OBJECT_ID;
            //TxbDS_POINT_INVERT_LEVEL.Text = pPIPELine.DS_POINT_INVERT_LEVEL;
            TxbDS_SURVEY_ID.Text = pPIPELine.DS_SURVEY_ID;
            TxbMATERIAL.Text = pPIPELine.MATERIAL;
            TxbPipe_Length.Text = pPIPELine.Pipe_Length;
            TxbPRESSURE.Text = pPIPELine.PRESSURE;

            TxbSEDIMENT_DEPTH.Text = pPIPELine.SEDIMENT_DEPTH;
            //TxbUS_INVERT_LEVEL.Text = pPIPELine.US_INVERT_LEVEL;
            //TxbUS_OBJECT_ID.Text = pPIPELine.US_OBJECT_ID;
            //TxbUS_POINT_INVERT_LEVEL.Text = pPIPELine.US_POINT_INVERT_LEVEL;
            TxbUS_SURVEY_ID.Text = pPIPELine.US_SURVEY_ID;
            TxbWidth.Text = pPIPELine.Width;
            TxbRemark.Text = pPIPELine.Remark;
            CmbDirction.Text= pPIPELine.Dirtcion ;
            TxbWATER_LEVEL.Text = pPIPELine.WATER_LEVEL;
            TxbWATER_QUALITY.Text = pPIPELine.WATER_QUALITY;
            TxbWATER_State.Text = pPIPELine.WATER_State;
            //if (pPIPELine.IsDelete)
            //    LableIsD.Text = "已删除";
            //else
            //    LableIsD.Text = "";
        }
        public void Clear()
        {
            pPIPELine = new PIPELineClass();
            //TxbID.Clear();
            TxbACQUISITION_DATE.Clear();
            TxbACQUISITION_UNIT.Clear();

            TxbPROCESS_DATE.Clear();
            TxbPROCESS_UNIT.Clear();
            TxbROAD_NAME.Clear();
            TxbSTATE.ResetText();
            //TxbSYSTEM_TYPE.ResetText();
            //LableIsD.Text = "";
            //TxbDS_INVERT_LEVEL.Clear();
            //TxbDS_OBJECT_ID.Clear();
            //TxbDS_POINT_INVERT_LEVEL.Clear();
            TxbDS_SURVEY_ID.Clear();
            TxbMATERIAL.ResetText();
            TxbPipe_Length.Clear();
            TxbPRESSURE.Text = "自流";

            TxbSEDIMENT_DEPTH.Clear();
            //TxbUS_INVERT_LEVEL.Clear();
            //TxbUS_OBJECT_ID.Clear();
            //TxbUS_POINT_INVERT_LEVEL.Clear();
            TxbUS_SURVEY_ID.Clear();
            TxbWidth.Clear();
            TxbRemark.Clear();

            TxbWATER_LEVEL.Clear();
            TxbWATER_QUALITY.ResetText();
            TxbWATER_State.ResetText();
 
        }
        private void BtnZoomStart_Click(object sender, EventArgs e)
        {
            ZoomByPointID(TxbUS_SURVEY_ID.Text);

        }

        private void BtnZoomEnd_Click(object sender, EventArgs e)
        {
            ZoomByPointID(TxbDS_SURVEY_ID.Text);
        }
        private string GetPointID()
        {
            //IPipePoint pPipePoint = null;
             AcadObject pAcadObject = PickObject();
             if (pAcadObject == null) return null;
             if (pAcadObject.ObjectName.Equals("AcDbText") || pAcadObject.ObjectName.Equals("AcDbMInsertBlock"))
             {
                 AcadDictionary pAcadDictionary = pAcadObject.GetExtensionDictionary();
                 if (pAcadDictionary.Count == 1)
                 {
                     string ID = (pAcadDictionary.Item(0) as AcadXRecord).Name;
                     return ID;
                   /*  string SURVEYID = GetPointSURVEYID(ID);
                     if (SURVEYID != string.Empty)
                     {

                         return SURVEYID;
                     }
                     else return string.Empty;*/
                 }
             }
             return string.Empty;
        }
       
        private void BtnRePickStart_Click(object sender, EventArgs e)
        {

            string  pStartID = GetPointID();
            string SURVEYID = GetPointSURVEYID(pStartID);
            this.pPIPELine.US_OBJECT_ID = pStartID;
            this.pPIPELine.US_SURVEY_ID = SURVEYID;
            if (pStartID == string.Empty) return;
            //TxbUS_OBJECT_ID.Text = pStartPoint.ID;
            //TxbUS_INVERT_LEVEL.Text = pStartPoint.INVERT_LEVEL;
            TxbUS_SURVEY_ID.Text = pStartID;
            TxbPipe_Length.Text = CalcLen(TxbUS_SURVEY_ID.Text, TxbDS_SURVEY_ID.Text).ToString();
            this.pPIPELine.Lenght = TxbPipe_Length.Text;
            //TxbSYSTEM_TYPE.Text = pStartPoint.SYSTEM_TYPE;
            
        }
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        private AcadObject PickObject()
        {
            try
            {
                m_CADObjectEditCtrl.RemoveSelectionChangedEvent();
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                AcadDocument AcadDoc = acDoc.AcadDocument as AcadDocument;

                Microsoft.VisualBasic.Interaction.AppActivate(AcadDoc.Application.Caption);
                keybd_event(Keys.Escape, 0, 0, 0);
                SendKeys.SendWait("{ESC}");
                System.Windows.Forms.Application.DoEvents();
                object returnObj, pickPoint;               
                string pickPrompt = "选择对象";
                AcadDoc.Utility.GetEntity(out returnObj, out pickPoint, pickPrompt);
                AcadObject returnCADObj = (AcadObject)returnObj;
                m_CADObjectEditCtrl.AddSelectionChangedEvent();
                return returnCADObj;
            }
            catch
            { return null; }
        }

        private void BtnRepickEnd_Click(object sender, EventArgs e)
        {
            string  pEndID = GetPointID();
            if (pEndID == string.Empty) return;

            string SURVEYID = GetPointSURVEYID(pEndID);
            this.pPIPELine.DS_OBJECT_ID = pEndID;
            this.pPIPELine.DS_SURVEY_ID = SURVEYID;

            //TxbDS_OBJECT_ID.Text = pEndPoint.ID;
            //TxbDS_INVERT_LEVEL.Text = pEndPoint.INVERT_LEVEL;
            TxbDS_SURVEY_ID.Text = pEndID;
            TxbPipe_Length.Text = CalcLen(TxbUS_SURVEY_ID.Text, TxbDS_SURVEY_ID.Text).ToString();
            this.pPIPELine.Lenght = TxbPipe_Length.Text;
        }

        private double CalcLen (string SPoint, string EPoint)
        {
            if (string.IsNullOrEmpty(SPoint) || string.IsNullOrEmpty(EPoint)) return 0;
            Point3d SPoint3D = GetPointByID(SPoint);
            if (SPoint3D.X == 0 || SPoint3D.Y == 0)
            {
                MessageBox.Show("起点号不存在");
                return 0;
            }
          
            Point3d EPoint3D = GetPointByID(EPoint);
            if (SPoint3D.X == 0 || SPoint3D.Y == 0)
            {
                MessageBox.Show("终点号不存在");
                return 0;
            }
            return GetLen(SPoint3D, EPoint3D);
            //TxbPipe_Length.Text = GetLen(SPoint3D, EPoint3D).ToString();
        }
        double GetLen(Point3d SPoint, Point3d EPoint)
        {
            double D_X = EPoint.X - SPoint.X;
            double D_Y = EPoint.Y - EPoint.Y;
            double Len = Math.Round(Math.Sqrt(D_X * D_X + D_Y * D_Y), 2);
            return Len;
        }

        private void BtnSiwch_Click(object sender, EventArgs e)
        {
            string SpointID = TxbUS_SURVEY_ID.Text;
            TxbUS_SURVEY_ID.Text = TxbDS_SURVEY_ID.Text;
            TxbDS_SURVEY_ID.Text = SpointID;
        }
    }

}
