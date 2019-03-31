using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Geometry;
using HR.Data;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using System.Runtime.InteropServices;

namespace CHXQ.XMManager
{
    public partial class FLAPCtrl : UserControl, IPipeDataCtrl
    {
        public FLAPCtrl()
        {
            InitializeComponent();
            pFLAP = new FLAP();
        }
         public bool IsErrorData { get; set; }
         public IPipeData GetData()
         {
             if (pFLAP == null)
                 pFLAP = new FLAP();

             
             pFLAP.ID= TxbID.Text ;
             pFLAP.ACQUISITION_DATE= TxbACQUISITION_DATE.Text ;
             pFLAP.ACQUISITION_UNIT= TxbACQUISITION_UNIT.Text ;

             pFLAP.PROCESS_Date= TxbPROCESS_DATE.Text ;
             pFLAP.PROCESS_Unit= TxbPROCESS_UNIT.Text ;
             pFLAP.ROAD_NAME= TxbROAD_NAME.Text ;
             pFLAP.STATE= TxbSTATE.Text ;
             pFLAP.Flap_Type= TxbFLAP_TYPE.Text ;
             pFLAP.INVERT_LEVEL= TxbINVERT_LEVEL.Text ;
              pFLAP.Flap_number= TxbFLAP_NUMBER.Text ;
             pFLAP.DS_OBJECT_ID= TxbDS_OBJECT_ID.Text ;

             pFLAP.DS_SURVEY_ID= TxbDS_SURVEY_ID.Text ;

             pFLAP.US_OBJECT_ID= TxbUS_OBJECT_ID.Text ;
            
             pFLAP.US_SURVEY_ID= TxbUS_SURVEY_ID.Text ;
           
             pFLAP.Remark= TxbRemark.Text ;
             return pFLAP;
         }
         public void SetNewID()
         {
             if (pFLAP == null)
                 pFLAP = new FLAP();
             TxbID.Text = pFLAP.GetNewID();
         }
         public bool ShoudReDraw
         {
             get
             {
                 if (pFLAP.ID.Equals(TxbID.Text) && 
                     pFLAP.US_OBJECT_ID.Equals(TxbUS_OBJECT_ID.Text) && 
                     pFLAP.DS_OBJECT_ID.Equals(TxbDS_OBJECT_ID.Text))
                     return true;
                 return false;
             }
         }
         public string DataType { get { return "拍门"; } }
           public void ZoomTo()
        {
            
            string SPoint = TxbUS_OBJECT_ID.Text.Trim();
            string EPoint = TxbDS_OBJECT_ID.Text.Trim();

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
        private Point3d GetPointByID(string ObjectID)
        {
            string sql = string.Format("select CO_X,CO_Y from PointsTable where ID='{0}'", ObjectID);
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
        private IFLAP pFLAP = null;
        public void SetData(IPipeData pPipeData)
        {
            pFLAP = pPipeData as IFLAP;
            TxbID.Text = pFLAP.ID;
            TxbACQUISITION_DATE.Text = pFLAP.ACQUISITION_DATE;
            TxbACQUISITION_UNIT.Text = pFLAP.ACQUISITION_UNIT;

            TxbPROCESS_DATE.Text = pFLAP.PROCESS_Date;
            TxbPROCESS_UNIT.Text = pFLAP.PROCESS_Unit;
            TxbROAD_NAME.Text = pFLAP.ROAD_NAME;
            TxbSTATE.Text = pFLAP.STATE;
            TxbFLAP_TYPE.Text = pFLAP.Flap_Type;
            TxbINVERT_LEVEL.Text = pFLAP.INVERT_LEVEL;
            TxbFLAP_NUMBER.Text = pFLAP.Flap_number;
            TxbDS_OBJECT_ID.Text = pFLAP.DS_OBJECT_ID;
          
            TxbDS_SURVEY_ID.Text = pFLAP.DS_SURVEY_ID;
          
            TxbUS_OBJECT_ID.Text = pFLAP.US_OBJECT_ID;
          
            TxbUS_SURVEY_ID.Text = pFLAP.US_SURVEY_ID;
          
            TxbRemark.Text = pFLAP.Remark;


        }
        public void Clear()
        {
            pFLAP = new FLAP();
            TxbID.ResetText();
            TxbACQUISITION_DATE.ResetText();
            TxbACQUISITION_UNIT.ResetText();

            TxbPROCESS_DATE.ResetText();
            TxbPROCESS_UNIT.ResetText();
            TxbROAD_NAME.ResetText();
            TxbSTATE.Text = "运行";
            TxbFLAP_TYPE.ResetText();
            TxbINVERT_LEVEL.ResetText();
            TxbFLAP_NUMBER.ResetText();
            TxbDS_OBJECT_ID.ResetText();

            TxbDS_SURVEY_ID.ResetText();

            TxbUS_OBJECT_ID.ResetText();

            TxbUS_SURVEY_ID.ResetText();

            TxbRemark.ResetText();
        }
        private void BtnZoomStart_Click(object sender, EventArgs e)
        {
            ZoomByPointID(TxbUS_OBJECT_ID.Text);

        }

        private void BtnZoomEnd_Click(object sender, EventArgs e)
        {
            ZoomByPointID(TxbDS_OBJECT_ID.Text);
        }

        
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        private AcadObject PickObject()
        {
            try
            {
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

                return returnCADObj;
            }
            catch
            { return null; }
        }
        private IPipePoint GetPointObject()
        {
            IPipePoint pPipePoint = null;
            AcadObject pAcadObject = PickObject();
            if (pAcadObject == null) return null;
            if (pAcadObject.ObjectName.Equals("AcDbText") || pAcadObject.ObjectName.Equals("AcDbMInsertBlock"))
            {
                AcadDictionary pAcadDictionary = pAcadObject.GetExtensionDictionary();
                if (pAcadDictionary.Count > 1)
                {
                    AcadXRecord pAcadXRecord1 = pAcadDictionary.Item(1) as AcadXRecord;

                    AcadXRecord pAcadXRecord2 = pAcadDictionary.Item(0) as AcadXRecord;
                    string TableName, ID;
                    if (pAcadXRecord1.Name.StartsWith("PS_"))
                    {
                        TableName = pAcadXRecord1.Name;
                        ID = pAcadXRecord2.Name;
                    }
                    else
                    {
                        TableName = pAcadXRecord2.Name;
                        ID = pAcadXRecord1.Name;
                    }
                    //if( )
                    pPipePoint = Pipeobject.GetDataByID(ID, TableName) as IPipePoint;
                    return pPipePoint;
                }
            }
            return null;
        }
        private void BtnRePickStart_Click(object sender, EventArgs e)
        {

            IPipePoint pStartPoint = GetPointObject();
            if (pStartPoint == null) return;
            TxbUS_OBJECT_ID.Text = pStartPoint.ID;
           
            TxbUS_SURVEY_ID.Text = pStartPoint.SURVEY_ID;
            this.pFLAP.Lenght = CalcLen(TxbUS_OBJECT_ID.Text, TxbDS_OBJECT_ID.Text).ToString();
           
        }
        private void BtnRepickEnd_Click(object sender, EventArgs e)
        {
            IPipePoint pEndPoint = GetPointObject();
            if (pEndPoint == null) return;
            TxbDS_OBJECT_ID.Text = pEndPoint.ID;
           
            TxbDS_SURVEY_ID.Text = pEndPoint.SURVEY_ID;
            this.pFLAP.Lenght = CalcLen(TxbUS_OBJECT_ID.Text, TxbDS_OBJECT_ID.Text).ToString();
        }
        private double CalcLen(string SPoint, string EPoint)
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

     
    }
}
