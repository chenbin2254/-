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
    public partial class PipePointCtrl : UserControl, IPipeDataCtrl
    {
        public PipePointCtrl()
        {
            InitializeComponent();
            TxbSURVEY_ID.CharacterCasing = CharacterCasing.Upper;
            TxbSURVEY_ID.TextChanged += new EventHandler(TxbSURVEY_ID_TextChanged);
            m_PipePoint = new PipePoint();
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
                if (!this.m_PipePoint.IsExistSURVEYID(TxbSURVEY_ID.Text))
                {
                    TxbID.Text = this.m_PipePoint.GetHead() + TxbSURVEY_ID.Text.Substring(2);
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
        public string DataType { get { return "管线点"; } }
      
        private IPipePoint m_PipePoint;
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
            TxbID.Text = m_PipePoint.GetNewID();
        }
        public bool ShoudReDraw
        {
            get
            {
                if (m_PipePoint.ID.Equals(TxbID.Text) && m_PipePoint.X.Equals(TxbCo_X.Text) && m_PipePoint.Y.Equals(TxbCO_Y.Text))
                    return true;
                return false;
            }
        }
        public void Clear()
        {
            m_PipePoint = new PipePoint();

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
            TxbSYSTEM_TYPE.ResetText();
 
        }
        public  void SetData(IPipeData pPipeData)
        {

            m_PipePoint = pPipeData as IPipePoint;
            IPipePoint pPipePoint = pPipeData as IPipePoint;
            TxbID.Text = pPipePoint.ID;
            TxbACQUISITION_DATE.Text = pPipeData.ACQUISITION_DATE;
            TxbACQUISITION_UNIT.Text = pPipeData.ACQUISITION_UNIT;
            TxbCo_X.Text = pPipePoint.X;
            TxbCO_Y.Text = pPipePoint.Y;
            TxbGROUND_LEVEL.Text = pPipePoint.GROUND_LEVEL;
            TxbINVERT_LEVEL.Text = pPipePoint.INVERT_LEVEL;
            TxbPROCESS_DATE.Text = pPipePoint.PROCESS_Date;
            TxbPROCESS_UNIT.Text = pPipePoint.PROCESS_Unit;
            TxbROAD_NAME.Text = pPipePoint.ROAD_NAME;
            TxbSTATE.Text = pPipePoint.STATE;
            TxbSURVEY_ID.TextChanged -= new EventHandler(TxbSURVEY_ID_TextChanged);
            TxbSURVEY_ID.Text = pPipePoint.SURVEY_ID;
            TxbSURVEY_ID.TextChanged += new EventHandler(TxbSURVEY_ID_TextChanged);
            TxbSYSTEM_TYPE.Text = pPipePoint.SYSTEM_TYPE;
        }
       public IPipeData GetData()
        {
            if (m_PipePoint == null) m_PipePoint = new PipePoint();
            m_PipePoint.ID = TxbID.Text;
            m_PipePoint.ACQUISITION_DATE = TxbACQUISITION_DATE.Text;
            m_PipePoint.ACQUISITION_UNIT = TxbACQUISITION_UNIT.Text;
            m_PipePoint.X = TxbCo_X.Text;
            m_PipePoint.Y=TxbCO_Y.Text;
            m_PipePoint.GROUND_LEVEL = TxbGROUND_LEVEL.Text;
            m_PipePoint.INVERT_LEVEL= TxbINVERT_LEVEL.Text ;
            m_PipePoint.PROCESS_Date = TxbPROCESS_DATE.Text;
            m_PipePoint.PROCESS_Unit= TxbPROCESS_UNIT.Text;
            m_PipePoint.ROAD_NAME= TxbROAD_NAME.Text;
            m_PipePoint.STATE= TxbSTATE.Text ;
            m_PipePoint.SURVEY_ID= TxbSURVEY_ID.Text  ;
            m_PipePoint.SYSTEM_TYPE= TxbSYSTEM_TYPE.Text ;
            return m_PipePoint;
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
       private void btnPickCoord_Click(object sender, EventArgs e)
       {
           Point3d pPoint3d = PickObject();
           TxbCo_X.Text = pPoint3d.X.ToString();
           TxbCO_Y.Text = pPoint3d.Y.ToString();
           ZoomPoint.DrawCircle(pPoint3d);
       }
    }
}
