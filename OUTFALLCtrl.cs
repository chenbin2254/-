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
    public partial class OUTFALLCtrl : UserControl, IPipeDataCtrl
    {
        public OUTFALLCtrl()
        {
            InitializeComponent();
            TxbSURVEY_ID.CharacterCasing = CharacterCasing.Upper;
            pOUTFALL = new OUTFALL();
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
                if (!this.pOUTFALL.IsExistSURVEYID(TxbSURVEY_ID.Text))
                {
                    TxbID.Text = this.pOUTFALL.GetHead() + TxbSURVEY_ID.Text.Substring(2);
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
        public void SetNewID()
        {
            if (pOUTFALL == null) pOUTFALL = new OUTFALL();
            TxbID.Text = pOUTFALL.GetNewID();
        }
        public bool ShoudReDraw
        {
            get
            {
                if (pOUTFALL.ID.Equals(TxbID.Text) && pOUTFALL.X.Equals(TxbCo_X.Text) && pOUTFALL.Y.Equals(TxbCO_Y.Text))
                    return true;
                return false;
            }
        }
        public string DataType { get { return "排放口"; } }
        public IPipeData GetData()
        {
            if (pOUTFALL == null) pOUTFALL = new OUTFALL();
          pOUTFALL.ID=  TxbID.Text;
          pOUTFALL.ACQUISITION_DATE = TxbACQUISITION_DATE.Text;
          pOUTFALL.ACQUISITION_UNIT = TxbACQUISITION_UNIT.Text;
            pOUTFALL.X= TxbCo_X.Text ;
            pOUTFALL.Y= TxbCO_Y.Text ;
            pOUTFALL.GROUND_LEVEL= TxbGROUND_LEVEL.Text ;
            pOUTFALL.INVERT_LEVEL= TxbINVERT_LEVEL.Text ;
            pOUTFALL.PROCESS_Date= TxbPROCESS_DATE.Text ;
            pOUTFALL.PROCESS_Unit= TxbPROCESS_UNIT.Text ;
            pOUTFALL.ROAD_NAME= TxbROAD_NAME.Text;
            pOUTFALL.STATE= TxbSTATE.Text ;
            pOUTFALL.SURVEY_ID= TxbSURVEY_ID.Text;
            pOUTFALL.SYSTEM_TYPE= TxbSYSTEM_TYPE.Text;

            pOUTFALL.WIDTH= TxbWIDTH.Text;
            pOUTFALL.HEIGHT= TxbHeight.Text ;
            pOUTFALL.EFFLUENT_TYPE= TxbEFFLUENT_TYPE.Text;
            pOUTFALL.DRYWEATHER_STATE= TxbDRYWEATHER_STATE.Text;
            pOUTFALL.WATER_QUALITY= TxbWATER_QUALITY.Text ;
            pOUTFALL.Remark = TxbRemark.Text;
            return pOUTFALL;
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
        private IOUTFALL pOUTFALL;
        public void Clear()
        {
            pOUTFALL = new OUTFALL();

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
            TxbSTATE.ResetText();
            TxbSURVEY_ID.Clear();
            TxbSYSTEM_TYPE.ResetText();

            TxbWIDTH.Clear();
            TxbHeight.Clear();
            TxbEFFLUENT_TYPE.Clear();
            TxbDRYWEATHER_STATE.Clear();
            TxbWATER_QUALITY.Clear();
        }
        public  void SetData(IPipeData pPipeData)
        {

             pOUTFALL = pPipeData as IOUTFALL;
            TxbID.Text = pOUTFALL.ID;
            TxbACQUISITION_DATE.Text = pPipeData.ACQUISITION_DATE;
            TxbACQUISITION_UNIT.Text = pPipeData.ACQUISITION_UNIT;
            TxbCo_X.Text = pOUTFALL.X;
            TxbCO_Y.Text = pOUTFALL.Y;
            TxbGROUND_LEVEL.Text = pOUTFALL.GROUND_LEVEL;
            TxbINVERT_LEVEL.Text = pOUTFALL.INVERT_LEVEL;
            TxbPROCESS_DATE.Text = pOUTFALL.PROCESS_Date;
            TxbPROCESS_UNIT.Text = pOUTFALL.PROCESS_Unit;
            TxbROAD_NAME.Text = pOUTFALL.ROAD_NAME;
            TxbSTATE.Text = pOUTFALL.STATE;
            TxbSURVEY_ID.TextChanged -= new EventHandler(TxbSURVEY_ID_TextChanged);
            TxbSURVEY_ID.Text = pOUTFALL.SURVEY_ID;
            TxbSURVEY_ID.TextChanged += new EventHandler(TxbSURVEY_ID_TextChanged);
            TxbSYSTEM_TYPE.Text = pOUTFALL.SYSTEM_TYPE;

            TxbWIDTH.Text = pOUTFALL.WIDTH;
            TxbHeight.Text = pOUTFALL.HEIGHT;
            TxbEFFLUENT_TYPE.Text = pOUTFALL.EFFLUENT_TYPE;
            TxbDRYWEATHER_STATE.Text = pOUTFALL.DRYWEATHER_STATE;
            TxbWATER_QUALITY.Text = pOUTFALL.WATER_QUALITY;
             TxbRemark.Text= pOUTFALL.Remark ;
        }

        private void BtnPickCoord_Click(object sender, EventArgs e)
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
