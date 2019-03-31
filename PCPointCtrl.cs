using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Geometry;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop;
using aApp = Autodesk.AutoCAD.ApplicationServices.Application;
namespace CHXQ.XMManager
{
    public partial class PCPointCtrl : UserControl, IPipeDataCtrl
    {
        private CADObjectEditCtrl m_CADObjectEditCtrl = null;
        public PCPointCtrl(CADObjectEditCtrl pCADObjectEditCtrl)
        {
            InitializeComponent();
            m_PCPoint =new PCPoint();
            m_CADObjectEditCtrl = pCADObjectEditCtrl;
        }
        public void ZoomTo()
        {
            Zoom(GetPoint());

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
        private void Zoom(Point3d CurPoint)
        {
            Point3d pMin = new Point3d(CurPoint.X - 10, CurPoint.Y + 5, 0);
            Point3d pMax = new Point3d(CurPoint.X + 10, CurPoint.Y, 0);
            Point3d pPoint3D = new Point3d(CurPoint.X, CurPoint.Y, 0);
            ZoomPoint.DrawCircle(pPoint3D);
            ZoomPoint.Zoom(pMin, pMax, new Point3d(), 5);

        }
        public string DataType { get { return "点"; } }
        private bool m_ShoudReDraw = false;
        public bool ShoudReDraw
        {
            get
            {
                return m_ShoudReDraw;
            }
        }
        private IPCPoint m_PCPoint = null;
        public void SetData(IPipeData pPipeData)
        {
            // m_PipeData = pPipeData;
            m_PCPoint = pPipeData as IPCPoint;
            m_ShoudReDraw = false;
            TxbSURVEY_ID.Text = m_PCPoint.SURVEY_ID;

            TxbACQUISITION_DATE.Text = m_PCPoint.ACQUISITION_DATE;
            TxbACQUISITION_UNIT.Text = m_PCPoint.ACQUISITION_UNIT;
            TxbBOTTOM_TYPE.Text = m_PCPoint.BOTTOM_TYPE;
            TxbCo_X.Text = m_PCPoint.X;
            TxbCO_Y.Text = m_PCPoint.Y;
            TxbCOVER_MATERIAL.Text = m_PCPoint.COVER_MATERIAL;
            TxbCOVER_STATE.Text = m_PCPoint.COVER_STATE;
             TxbGULLY_NUMBER.Text= m_PCPoint.GULLY_NUMBER ;
            TxbGROUND_LEVEL.Text = m_PCPoint.GROUND_LEVEL;


            TxbMANHOLE_MATERIAL.Text = m_PCPoint.MANHOLE_MATERIAL;
            TxbMANHOLE_SHAPE.Text = m_PCPoint.MANHOLE_SHAPE;
            TxbMANHOLE_SIZE.Text = m_PCPoint.MANHOLE_SIZE;
            TxbMANHOLE_TYPE.Text = m_PCPoint.MANHOLE_TYPE;
            TxbPROCESS_DATE.Text = m_PCPoint.PROCESS_Date;
            TxbPROCESS_UNIT.Text = m_PCPoint.PROCESS_Unit;

            TxbROAD_NAME.Text = m_PCPoint.ROAD_NAME;
            TxbSEDIMENT_DEPTH.Text = m_PCPoint.SEDIMENT_DEPTH;
            TxbSTATE.Text = m_PCPoint.STATE;
            TxbDepth.Text = m_PCPoint.Depth;

            CmbPointType.Text = m_PCPoint.Type;
            TxbRemark.Text = m_PCPoint.Remark;
            AlterCoord = false;
        }
        public IPipeData GetData()
        {
            if (m_PCPoint == null) m_PCPoint = new PCPoint();
            m_PCPoint.ACQUISITION_DATE = TxbACQUISITION_DATE.Text;
            m_PCPoint.ACQUISITION_UNIT = TxbACQUISITION_UNIT.Text;
            m_PCPoint.X = TxbCo_X.Text;
            m_PCPoint.Y = TxbCO_Y.Text;
            m_PCPoint.GROUND_LEVEL = TxbGROUND_LEVEL.Text;
            //m_MANHOLE.INVERT_LEVEL = TxbINVERT_LEVEL.Text;
            m_PCPoint.PROCESS_Date = TxbPROCESS_DATE.Text;
            m_PCPoint.PROCESS_Unit = TxbPROCESS_UNIT.Text;
            m_PCPoint.ROAD_NAME = TxbROAD_NAME.Text;
            m_PCPoint.STATE = TxbSTATE.Text;
            m_PCPoint.SURVEY_ID = TxbSURVEY_ID.Text;
            //m_MANHOLE.SYSTEM_TYPE = TxbSYSTEM_TYPE.Text;

            m_PCPoint.SURVEY_ID = TxbSURVEY_ID.Text;
            m_PCPoint.BOTTOM_TYPE = TxbBOTTOM_TYPE.Text;

            m_PCPoint.COVER_MATERIAL = TxbCOVER_MATERIAL.Text;
            m_PCPoint.COVER_STATE = TxbCOVER_STATE.Text;
            //m_MANHOLE.FLOW_STATE = TxbFLOW_STATE.Text;
            m_PCPoint.GROUND_LEVEL = TxbGROUND_LEVEL.Text;
          
            m_PCPoint.MANHOLE_MATERIAL = TxbMANHOLE_MATERIAL.Text;
            m_PCPoint.MANHOLE_SHAPE = TxbMANHOLE_SHAPE.Text;
            m_PCPoint.MANHOLE_SIZE = TxbMANHOLE_SIZE.Text;
            m_PCPoint.MANHOLE_TYPE = TxbMANHOLE_TYPE.Text;
            m_PCPoint.SEDIMENT_DEPTH = TxbSEDIMENT_DEPTH.Text;
             m_PCPoint.Depth= TxbDepth.Text ;

            m_PCPoint.Type= CmbPointType.Text ;
            //m_PCPoint.WATER_LEVEL = TxbWATER_LEVEL.Text;
            //m_PCPoint.WATER_QUALITY = TxbWATER_QUALITY.Text;
            m_PCPoint.Remark = TxbRemark.Text;
            m_PCPoint.GULLY_NUMBER = TxbGULLY_NUMBER.Text;
            TxbDepth.Clear();
            CmbPointType.ResetText();
            return m_PCPoint;
        }
        public void Clear()
        {
            m_PCPoint = new PCPoint();
            AlterCoord = false;
            TxbSURVEY_ID.Clear();
            TxbGULLY_NUMBER.Clear();
            TxbACQUISITION_DATE.Clear();
            TxbACQUISITION_UNIT.Clear();
            TxbBOTTOM_TYPE.ResetText();
            TxbCo_X.Clear();
            TxbCO_Y.Clear();
            TxbCOVER_MATERIAL.ResetText();
            TxbCOVER_STATE.ResetText();
            //TxbFLOW_STATE.ResetText();
            TxbGROUND_LEVEL.Clear();
            //TxbID.Clear();
            //TxbINVERT_LEVEL.Clear();
            TxbMANHOLE_MATERIAL.ResetText();
            TxbMANHOLE_SHAPE.ResetText();
            TxbMANHOLE_SIZE.Clear();
            TxbMANHOLE_TYPE.ResetText();
            TxbPROCESS_DATE.Clear();
            TxbPROCESS_UNIT.Clear();

            TxbROAD_NAME.Clear();
            TxbSEDIMENT_DEPTH.Clear();
            TxbSTATE.ResetText();
            CmbPointType.ResetText();
            TxbRemark.Clear();

        }
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        private Point3d PickPoint()
        {
            try
            {
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                AcadDocument AcadDoc = acDoc.AcadDocument as AcadDocument;
                m_CADObjectEditCtrl.RemoveSelectionChangedEvent();
                Microsoft.VisualBasic.Interaction.AppActivate(AcadDoc.Application.Caption);
                keybd_event(Keys.Escape, 0, 0, 0);
                SendKeys.SendWait("{ESC}");
                System.Windows.Forms.Application.DoEvents();
                //aApp.DocumentManager.MdiActiveDocument.Editor.GetSelection();
                string pickPrompt = "拾取坐标";
                //   AcadDoc.Utility.GetEntity(out returnObj, out pickPoint, pickPrompt);
                m_CADObjectEditCtrl.AddSelectionChangedEvent();
                PromptPointResult pPromptEntityResult = aApp.DocumentManager.MdiActiveDocument.Editor.GetPoint(pickPrompt);
                return pPromptEntityResult.Value;
                
                // AcadPoint ReturnPoint = pickPoint as AcadPoint;
                //AcadObject returnCADObj = (AcadObject)returnObj;

                //return ReturnPoint;
            }
            catch
            { return new Point3d(); }
        }
        public bool AlterCoord = false;
        private void BtnPickCoord_Click(object sender, EventArgs e)
        {
            Point3d pPoint3d = PickPoint();
            TxbCo_X.Text = pPoint3d.X.ToString("0.00");
            TxbCO_Y.Text = pPoint3d.Y.ToString("0.00");
            ZoomPoint.DrawCircle(pPoint3d);
            AlterCoord = true;
        }
    }
}
