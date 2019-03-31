using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD.ApplicationServices;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.Interop;
using aApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.EditorInput;
using System;
namespace CHXQ.XMManager
{
    public partial class PUMPCtrl : UserControl, IPipeDataCtrl
    {
        public PUMPCtrl()
        {
            InitializeComponent();
            TxbSURVEY_ID.CharacterCasing = CharacterCasing.Upper;
            pPUMP = new PUMP();
            TxbSURVEY_ID.TextChanged += new EventHandler(TxbSURVEY_ID_TextChanged);
        }
        void TxbSURVEY_ID_TextChanged(object sender, EventArgs e)
        {
            if (TxbSURVEY_ID.Text.StartsWith("YS"))
                TxbSYSTEM_TYPE.Text = "雨水";
            else if (TxbSURVEY_ID.Text.StartsWith("WS"))
                TxbSYSTEM_TYPE.Text = "污水";
            else
                TxbSYSTEM_TYPE.ResetText();
            if (TxbSURVEY_ID.Text.Length == 10)
            {
                if (!this.pPUMP.IsExistSURVEYID(TxbSURVEY_ID.Text))
                {
                    TxbID.Text = this.pPUMP.GetHead() + TxbSURVEY_ID.Text.Substring(2);
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
        public bool IsErrorData { get; set; }
        public void ZoomTo()
        {
            Zoom(GetPoint());

        }
        public string DataType { get { return "泵站前池"; } }
        public bool ShoudReDraw
        {
            get
            {
                if (pPUMP.ID.Equals(TxbID.Text) && pPUMP.X.Equals(TxbCo_X.Text) && pPUMP.Y.Equals(TxbCO_Y.Text))
                    return true;
                return false;
            }
        }
        public IPipeData GetData()
        {
            if (pPUMP == null) pPUMP = new PUMP();
             pPUMP.ID= TxbID.Text ;
             pPUMP.ACQUISITION_DATE = TxbACQUISITION_DATE.Text;

            pPUMP.X= TxbCo_X.Text ;
            pPUMP.Y= TxbCO_Y.Text ;
            pPUMP.GROUND_LEVEL= TxbGROUND_LEVEL.Text ;
            pPUMP.BOTTOM_LEVEL= TxbBOTTOM_LEVEL.Text;
            // TxbPROCESS_DATE.Text = pPipePoint.PROCESS_Date;
            //TxbPROCESS_UNIT.Text = pPipePoint.PROCESS_Unit;
            pPUMP.ROAD_NAME= TxbROAD_NAME.Text ;
            pPUMP.STATE= TxbSTATE.Text ;
            pPUMP.SURVEY_ID= TxbSURVEY_ID.Text ;
            pPUMP.SYSTEM_TYPE= TxbSYSTEM_TYPE.Text ;

            pPUMP.BOTTOM_AREA= TxbBOTTOM_AREA.Text;
            pPUMP.TOP_LEVEL= TxbTOP_LEVEL.Text  ;
             pPUMP.TOP_AREA= TxbTOP_AREA.Text ;
            pPUMP.WATER_LEVEL= TxbWATER_LEVEL.Text ;
            pPUMP.Remark= TxbRemark.Text ;
            return pPUMP;
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
                Y = double.Parse(TxbCO_Y.Text);
            }
            return new Point3d(X, Y, 0);
        }
        public void SetNewID()
        {
            if (pPUMP == null) pPUMP = new PUMP();
            TxbID.Text = pPUMP.GetNewID();
        }
        private IPUMP pPUMP = null;
        public void Clear()
        {
            pPUMP = new PUMP();
            TxbID.Clear();
            TxbACQUISITION_DATE.Clear();

            TxbCo_X.Clear();
            TxbCO_Y.Clear();
            TxbGROUND_LEVEL.Clear();
            TxbBOTTOM_LEVEL.Clear();
            // TxbPROCESS_DATE.Text = pPipePoint.PROCESS_Date;
            //TxbPROCESS_UNIT.Text = pPipePoint.PROCESS_Unit;
            TxbROAD_NAME.Clear();
            TxbSTATE.ResetText();
            TxbSURVEY_ID.Clear();
            TxbSYSTEM_TYPE.ResetText();

            TxbBOTTOM_AREA.Clear();
            TxbTOP_LEVEL.Clear();
            TxbTOP_AREA.Clear();
            TxbWATER_LEVEL.Clear();
            TxbRemark.Clear();

        }
        public  void SetData(IPipeData pPipeData)
        {

             pPUMP = pPipeData as IPUMP;
            TxbID.Text = pPUMP.ID;
            TxbACQUISITION_DATE.Text = pPipeData.ACQUISITION_DATE;
           
            TxbCo_X.Text = pPUMP.X;
            TxbCO_Y.Text = pPUMP.Y;
            TxbGROUND_LEVEL.Text = pPUMP.GROUND_LEVEL;
            TxbBOTTOM_LEVEL.Text = pPUMP.BOTTOM_LEVEL;
           // TxbPROCESS_DATE.Text = pPipePoint.PROCESS_Date;
            //TxbPROCESS_UNIT.Text = pPipePoint.PROCESS_Unit;
            TxbROAD_NAME.Text = pPUMP.ROAD_NAME;
            TxbSTATE.Text = pPUMP.STATE;
            TxbSURVEY_ID.TextChanged -= new EventHandler(TxbSURVEY_ID_TextChanged);
            TxbSURVEY_ID.Text = pPUMP.SURVEY_ID;
            TxbSURVEY_ID.TextChanged += new EventHandler(TxbSURVEY_ID_TextChanged);
            TxbSYSTEM_TYPE.Text = pPUMP.SYSTEM_TYPE;

            TxbBOTTOM_AREA.Text = pPUMP.BOTTOM_AREA;
            TxbTOP_LEVEL.Text = pPUMP.TOP_LEVEL;
            TxbTOP_AREA.Text = pPUMP.TOP_AREA;
            TxbWATER_LEVEL.Text = pPUMP.WATER_LEVEL;
            TxbRemark.Text = pPUMP.Remark;
        }

        private void BtnPickcoord_Click(object sender, EventArgs e)
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
