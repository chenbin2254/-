using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Interop;
using System.Runtime.InteropServices;
using aApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.EditorInput;
namespace CHXQ.XMManager
{
    public partial class COMBCtrl : UserControl, IPipeDataCtrl
    {
        public bool IsErrorData { get; set; }
        public COMBCtrl()
        {
            InitializeComponent();
            pCOMB = new COMB();
            TxbSURVEY_ID.CharacterCasing = CharacterCasing.Upper;
            TxbSURVEY_ID.TextChanged += new EventHandler(TxbSURVEY_ID_TextChanged);
        }
        void TxbSURVEY_ID_TextChanged(object sender, EventArgs e)
        {

            if (TxbSURVEY_ID.Text.Length == 10)
            {
                if (!this.pCOMB.IsExistSURVEYID(TxbSURVEY_ID.Text))
                {
                    TxbID.Text = this.pCOMB.GetHead() + TxbSURVEY_ID.Text.Substring(2);
                }
                else
                {
                    MessageBox.Show("已存在该物探点号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxbSURVEY_ID.Select();
                    return;
                }
            }
            else
                TxbID.Clear();
        }
        public void ZoomTo()
        {
            Zoom(GetPoint());
 
        }
        public string DataType { get { return "雨水口"; } }
        public bool ShoudReDraw 
        {
            get
            {
                if (pCOMB.ID.Equals(TxbID.Text) && pCOMB.X.Equals(TxbCo_X.Text) && pCOMB.Y.Equals(TxbCO_Y.Text))
                    return true;
                return false;
            }
        }
        public void SetNewID()
        {
            if (pCOMB == null) pCOMB = new COMB();
            TxbID.Text = pCOMB.GetNewID();
        }
        public void Clear()
        {
            pCOMB = new COMB();
            TxbID.Clear();
            TxbACQUISITION_DATE.Clear();
            TxbACQUISITION_UNIT.Clear();
            TxbCo_X.Clear();
            TxbCO_Y.Clear();
            TxbGROUND_LEVEL.Clear();
            TxbINVERT_LEVEL.Clear();
            TxbPROCESS_DATE.Clear();
            TxbPROCESS_UNIT.Clear();
            TxbROAD_NAME.Clear();
            TxbSTATE.Text = "运行";
            TxbSURVEY_ID.Clear();

            TxbSEDIMENT_DEPTH.Clear();
            TxbOBJECT_TYPE.Clear();
            TxbGULLY_NUMBER.Clear();

 
        }
        public IPipeData GetData()
        {
            if (pCOMB == null) pCOMB = new COMB();
           pCOMB.ID= TxbID.Text;
            pCOMB.ACQUISITION_DATE= TxbACQUISITION_DATE.Text ;
           pCOMB.ACQUISITION_UNIT= TxbACQUISITION_UNIT.Text  ;
             pCOMB.X= TxbCo_X.Text ;
            pCOMB.Y= TxbCO_Y.Text ;
            pCOMB.GROUND_LEVEL= TxbGROUND_LEVEL.Text;
             pCOMB.INVERT_LEVEL= TxbINVERT_LEVEL.Text;
            pCOMB.PROCESS_Date= TxbPROCESS_DATE.Text;
             pCOMB.PROCESS_Unit= TxbPROCESS_UNIT.Text;
            pCOMB.ROAD_NAME= TxbROAD_NAME.Text ;
            pCOMB.STATE= TxbSTATE.Text;
            pCOMB.SURVEY_ID= TxbSURVEY_ID.Text  ;

             pCOMB.SEDIMENT_DEPTH= TxbSEDIMENT_DEPTH.Text ;
            pCOMB.OBJECT_TYPE= TxbOBJECT_TYPE.Text ;
            pCOMB.GULLY_NUMBER= TxbGULLY_NUMBER.Text ;
            pCOMB.Remark = txbRemark.Text;
            return pCOMB;
        }
        private void Zoom(Point3d CurPoint)
        {
            Point3d pMin = new Point3d(CurPoint.X - 10, CurPoint.Y + 5, 0);
            Point3d pMax = new Point3d(CurPoint.X + 10, CurPoint.Y, 0);
            Point3d pPoint3D = new Point3d(CurPoint.X, CurPoint.Y, 0);
            ZoomPoint.DrawCircle(pPoint3D);
            ZoomPoint.Zoom(pMin, pMax, new Point3d(), 5);

        }
        private Point3d GetPoint()
        {
            double X, Y;
            X = 0;
            Y = 0;
            if (TxbCo_X.Text.Trim() != string.Empty)
            {
                X = double.Parse(TxbCo_X.Text);
            }
            if (TxbCO_Y.Text.Trim() != string.Empty)
            {
                Y =  double.Parse(TxbCO_Y.Text);
            }
            return new Point3d(X, Y,0);
        }
        private ICOMB pCOMB = null;
        public  void SetData(IPipeData pPipeData)
        {

             pCOMB = pPipeData as ICOMB;
            TxbID.Text = pCOMB.ID;
            TxbACQUISITION_DATE.Text = pPipeData.ACQUISITION_DATE;
            TxbACQUISITION_UNIT.Text = pPipeData.ACQUISITION_UNIT;
            TxbCo_X.Text = pCOMB.X;
            TxbCO_Y.Text = pCOMB.Y;
            TxbGROUND_LEVEL.Text = pCOMB.GROUND_LEVEL;
            TxbINVERT_LEVEL.Text = pCOMB.INVERT_LEVEL;
            TxbPROCESS_DATE.Text = pCOMB.PROCESS_Date;
            TxbPROCESS_UNIT.Text = pCOMB.PROCESS_Unit;
            TxbROAD_NAME.Text = pCOMB.ROAD_NAME;
            TxbSTATE.Text = pCOMB.STATE;
            TxbSURVEY_ID.TextChanged -= new EventHandler(TxbSURVEY_ID_TextChanged);
            TxbSURVEY_ID.Text = pCOMB.SURVEY_ID;
            TxbSURVEY_ID.TextChanged += new EventHandler(TxbSURVEY_ID_TextChanged);
            TxbSEDIMENT_DEPTH.Text = pCOMB.SEDIMENT_DEPTH;
            TxbOBJECT_TYPE.Text = pCOMB.OBJECT_TYPE;
            TxbGULLY_NUMBER.Text = pCOMB.GULLY_NUMBER;
            txbRemark.Text = pCOMB.Remark;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point3d pPoint3d = PickObject();
            TxbCo_X.Text = pPoint3d.X.ToString("0.00");
            TxbCO_Y.Text = pPoint3d.Y.ToString("0.00");
            ZoomPoint.DrawCircle(pPoint3d);
        }
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        private Point3d PickObject()
        {
            try
            {
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                AcadDocument AcadDoc = acDoc.AcadDocument as AcadDocument;

                Microsoft.VisualBasic.Interaction.AppActivate(AcadDoc.Application.Caption);
                keybd_event(Keys.Escape, 0, 0, 0);
                SendKeys.SendWait("{ESC}");
                //System.Windows.Forms.Application.DoEvents();
                //aApp.DocumentManager.MdiActiveDocument.Editor.GetSelection();
                string pickPrompt = "拾取坐标";
                //   AcadDoc.Utility.GetEntity(out returnObj, out pickPoint, pickPrompt);
                PromptPointResult pPromptEntityResult = aApp.DocumentManager.MdiActiveDocument.Editor.GetPoint(pickPrompt);
                return pPromptEntityResult.Value;
                // AcadPoint ReturnPoint = pickPoint as AcadPoint;
                //AcadObject returnCADObj = (AcadObject)returnObj;

                //return ReturnPoint;
            }
            catch
            { return new Point3d(); }
        }
    }
}
