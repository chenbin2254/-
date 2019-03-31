using System;
using System.Collections.Generic;
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

namespace CHXQ.XMManager
{
    public partial class MANHOLECtrl : UserControl, IPipeDataCtrl
    {
        public MANHOLECtrl()
        {
            InitializeComponent();
            TxbSURVEY_ID.CharacterCasing = CharacterCasing.Upper;
            TxbSURVEY_ID.TextChanged += new EventHandler(TxbSURVEY_ID_TextChanged);
            m_MANHOLE=new  MANHOLE();
        }

        void TxbSURVEY_ID_TextChanged(object sender, EventArgs e)
        {
            if (TxbSURVEY_ID.Text.StartsWith("YS"))
                TxbSYSTEM_TYPE.Text = "雨水";
            else if (TxbSURVEY_ID.Text.StartsWith("WS"))
                TxbSYSTEM_TYPE.Text = "污水";
            else
                TxbSYSTEM_TYPE.Clear();
            if (TxbSURVEY_ID.Text.Length == 10)
            {
                if (!this.m_MANHOLE.IsExistSURVEYID(TxbSURVEY_ID.Text))
                {
                    TxbID.Text = this.m_MANHOLE.GetHead() + TxbSURVEY_ID.Text.Substring(2);
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
      
        public string DataType { get { return "检查井"; } }

        public void ZoomTo()
        {
            Zoom(GetPoint());

        }
        private void Zoom(Point3d CurPoint)
        {
            Point3d pMin = new Point3d(CurPoint.X - 10, CurPoint.Y + 5, 0);
            Point3d pMax = new Point3d(CurPoint.X + 10, CurPoint.Y, 0);
            Point3d pPoint3D = new Point3d(CurPoint.X, CurPoint.Y, 0);
            ZoomPoint.DrawCircle(pPoint3D);
            ZoomPoint.Zoom(pMin, pMax, new Point3d(), 5);

        }
        public void SetNewID()
        {
            if (m_MANHOLE == null) m_MANHOLE = new MANHOLE();
            //TxbID.Text = m_MANHOLE.GetNewID();
        }
        public bool ShoudReDraw
        {
            get
            {
                if (m_MANHOLE.ID.Equals(TxbID.Text) && m_MANHOLE.X.Equals(TxbCo_X.Text) && m_MANHOLE.Y.Equals(TxbCO_Y.Text))
                    return true;
                return false;
            }
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
        IMANHOLE m_MANHOLE = null;
        public IPipeData GetData()
        {
            if (m_MANHOLE == null) m_MANHOLE = new MANHOLE();
            m_MANHOLE.ACQUISITION_DATE = TxbACQUISITION_DATE.Text;
            m_MANHOLE.ACQUISITION_UNIT = TxbACQUISITION_UNIT.Text;
            m_MANHOLE.X = TxbCo_X.Text;
            m_MANHOLE.Y = TxbCO_Y.Text;
            m_MANHOLE.GROUND_LEVEL = TxbGROUND_LEVEL.Text;
            m_MANHOLE.INVERT_LEVEL = TxbINVERT_LEVEL.Text;
            m_MANHOLE.PROCESS_Date = TxbPROCESS_DATE.Text;
            m_MANHOLE.PROCESS_Unit = TxbPROCESS_UNIT.Text;
            m_MANHOLE.ROAD_NAME = TxbROAD_NAME.Text;
            m_MANHOLE.STATE = TxbSTATE.Text;
            m_MANHOLE.SURVEY_ID = TxbSURVEY_ID.Text;
            m_MANHOLE.SYSTEM_TYPE = TxbSYSTEM_TYPE.Text;

            m_MANHOLE.SURVEY_ID = TxbSURVEY_ID.Text;
            m_MANHOLE.BOTTOM_TYPE = TxbBOTTOM_TYPE.Text;

            m_MANHOLE.COVER_MATERIAL = TxbCOVER_MATERIAL.Text;
            m_MANHOLE.COVER_STATE = TxbCOVER_STATE.Text;
            m_MANHOLE.FLOW_STATE = TxbFLOW_STATE.Text;
            m_MANHOLE.GROUND_LEVEL=TxbGROUND_LEVEL.Text;
            m_MANHOLE.ID = TxbID.Text;
            m_MANHOLE.MANHOLE_MATERIAL = TxbMANHOLE_MATERIAL.Text;
            m_MANHOLE.MANHOLE_SHAPE = TxbMANHOLE_SHAPE.Text;
            m_MANHOLE.MANHOLE_SIZE = TxbMANHOLE_SIZE.Text;
            m_MANHOLE.MANHOLE_TYPE= TxbMANHOLE_TYPE.Text ;
            m_MANHOLE.SEDIMENT_DEPTH = TxbSEDIMENT_DEPTH.Text;

            m_MANHOLE.WATER_LEVEL = TxbWATER_LEVEL.Text;
            m_MANHOLE.WATER_QUALITY = TxbWATER_QUALITY.Text;
            m_MANHOLE.Remark= TxbRemark.Text  ;
            return m_MANHOLE;
        }
        public void Clear()
        {
            m_MANHOLE = new MANHOLE();
            TxbSURVEY_ID.Clear();
            TxbACQUISITION_DATE.Clear();
            TxbACQUISITION_UNIT.Clear();
            TxbBOTTOM_TYPE.ResetText();
            TxbCo_X.Clear();
            TxbCO_Y.Clear();
            TxbCOVER_MATERIAL.ResetText();
            TxbCOVER_STATE.ResetText();
            TxbFLOW_STATE.ResetText();
            TxbGROUND_LEVEL.Clear();
            TxbID.Clear();
            TxbINVERT_LEVEL.Clear();
            TxbMANHOLE_MATERIAL.ResetText();
            TxbMANHOLE_SHAPE.ResetText();
            TxbMANHOLE_SIZE.Clear();
            TxbMANHOLE_TYPE.ResetText();
            TxbPROCESS_DATE.Clear();
            TxbPROCESS_UNIT.Clear();

            TxbROAD_NAME.Clear();
            TxbSEDIMENT_DEPTH.Clear();
            TxbSTATE.ResetText();
            TxbSYSTEM_TYPE.ResetText();
            TxbWATER_LEVEL.Clear();
            TxbWATER_QUALITY.Clear();

            TxbRemark.Clear();
 
        }
        public  void SetData(IPipeData pPipeData)
        {
           // m_PipeData = pPipeData;
            m_MANHOLE = pPipeData as IMANHOLE;
            TxbSURVEY_ID.TextChanged -= new EventHandler(TxbSURVEY_ID_TextChanged);
            TxbSURVEY_ID.Text = m_MANHOLE.SURVEY_ID;
            TxbSURVEY_ID.TextChanged += new EventHandler(TxbSURVEY_ID_TextChanged);
            TxbACQUISITION_DATE.Text = m_MANHOLE.ACQUISITION_DATE;
            TxbACQUISITION_UNIT.Text = m_MANHOLE.ACQUISITION_UNIT;
            TxbBOTTOM_TYPE.Text = m_MANHOLE.BOTTOM_TYPE;
            TxbCo_X.Text = m_MANHOLE.X;
            TxbCO_Y.Text = m_MANHOLE.Y;
            TxbCOVER_MATERIAL.Text = m_MANHOLE.COVER_MATERIAL;
            TxbCOVER_STATE.Text = m_MANHOLE.COVER_STATE;
            TxbFLOW_STATE.Text = m_MANHOLE.FLOW_STATE;
            TxbGROUND_LEVEL.Text = m_MANHOLE.GROUND_LEVEL;
            TxbID.Text = m_MANHOLE.ID;
            TxbINVERT_LEVEL.Text = m_MANHOLE.INVERT_LEVEL;
            TxbMANHOLE_MATERIAL.Text = m_MANHOLE.MANHOLE_MATERIAL;
            TxbMANHOLE_SHAPE.Text = m_MANHOLE.MANHOLE_SHAPE;
            TxbMANHOLE_SIZE.Text = m_MANHOLE.MANHOLE_SIZE;
            TxbMANHOLE_TYPE.Text = m_MANHOLE.MANHOLE_TYPE;
            TxbPROCESS_DATE.Text = m_MANHOLE.PROCESS_Date;
            TxbPROCESS_UNIT.Text = m_MANHOLE.PROCESS_Unit;

            TxbROAD_NAME.Text = m_MANHOLE.ROAD_NAME;
            TxbSEDIMENT_DEPTH.Text = m_MANHOLE.SEDIMENT_DEPTH;
            TxbSTATE.Text = m_MANHOLE.STATE;
            TxbSYSTEM_TYPE.Text = m_MANHOLE.SYSTEM_TYPE;
            TxbWATER_LEVEL.Text = m_MANHOLE.WATER_LEVEL;
            TxbWATER_QUALITY.Text = m_MANHOLE.WATER_QUALITY;

            TxbRemark.Text = m_MANHOLE.Remark;
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
                System.Windows.Forms.Application.DoEvents();
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
        private void BtnPickCoord_Click(object sender, EventArgs e)
        {
            Point3d pPoint3d = PickObject();
            TxbCo_X.Text = pPoint3d.X.ToString("0.00");
            TxbCO_Y.Text = pPoint3d.Y.ToString("0.00");
            ZoomPoint.DrawCircle(pPoint3d);
        }
    }
}
