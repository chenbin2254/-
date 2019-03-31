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
using Autodesk.AutoCAD.Geometry;
using DevComponents.DotNetBar.SuperGrid;
using System.Runtime.InteropServices;
using DevComponents.DotNetBar;
using aApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.EditorInput;
using HR.Data;
namespace CHXQ.XMManager
{
    public partial class CADObjectEditCtrl : UserControl
    {
        private AcadApplication AcadApp = null;
        private IPipeDataCtrl pPipeDataCtrl = null;
        public CADObjectEditCtrl()
        {
            InitializeComponent();
            AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AutoCAD.Application");
            //AcadApp.BeginOpen += new _DAcadApplicationEvents_BeginOpenEventHandler(AcadApp_BeginOpen);
            //AcadApp.EndOpen += new _DAcadApplicationEvents_EndOpenEventHandler(AcadApp_EndOpen);
            //aApp.DocumentManager.MdiActiveDocument.Editor.PointMonitor += new Autodesk.AutoCAD.EditorInput.PointMonitorEventHandler(Editor_PointMonitor);
           // AcadApp.AppActivate += new _DAcadApplicationEvents_AppActivateEventHandler(AcadApp_AppActivate);
            
            //CmbDataType.SelectedIndexChanged += new EventHandler(CmbDataType_SelectedIndexChanged);
            //CmbDataBase.SelectedIndexChanged += new EventHandler(CmbDataBase_SelectedIndexChanged);
            //DgvImportError.CellClick += new EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs>(DgvImportError_CellClick);
            //DgvQueryResult.CellClick += new EventHandler<GridCellClickEventArgs>(DgvQueryResult_CellClick);
            //DgvImportError.CellClick += new DataGridViewCellEventHandler(DgvImportError_CellClick);
            //bar1.Text = "导入不成功数据";
            //DgvImportError.AllowUserToResizeRows = false;


            /*    QueryItem ManholeItem2 = new QueryItem() { QueryItemName = "检查井", QueryClassName = "PS_Manhole" };
                ManholeItem2.QueryFields = new QueryField[] {                   
                    new QueryField() { FieldName = "ID", ItemName = "序号" , pDataType= DataType.text},
                    //new QueryField() { FieldName = "ID", ItemName = "编码" , pDataType= DataType.text},
                    new QueryField() { FieldName = "SURVEY_ID", ItemName = "物探点号" , pDataType= DataType.text}              
                 
                };
                //BtnQueryError_Click
                BtnManholeItem2.Tag = ManholeItem2;
                BtnManholeItem2.Click += new EventHandler(BtnQueryError_Click);
                QueryItem PipePointItem2 = new QueryItem() { QueryItemName = "管线点", QueryClassName = "PS_VIRTUAL_POINT" };
                PipePointItem2.QueryFields = new QueryField[] {
                      new QueryField() { FieldName = "ID", ItemName = "序号" , pDataType= DataType.text},
                   // new QueryField() { FieldName = "ID", ItemName = "编码" , pDataType= DataType.text},
                    new QueryField() { FieldName = "SURVEY_ID", ItemName = "物探点号" , pDataType= DataType.text}   
                };
                BtnPipePointItem2.Tag = PipePointItem2;
                BtnPipePointItem2.Click += new EventHandler(BtnQueryError_Click);

                QueryItem COMBItem2 = new QueryItem() { QueryItemName = "雨水口", QueryClassName = "PS_COMB" };
                COMBItem2.QueryFields = new QueryField[] {
                        new QueryField() { FieldName = "ID", ItemName = "序号" , pDataType= DataType.text},
                    //new QueryField() { FieldName = "ID", ItemName = "编码" , pDataType= DataType.text},
                     new QueryField() { FieldName = "SURVEY_ID", ItemName = "物探点号" , pDataType= DataType.text}   
                };
                BtnCOMBItem2.Tag = COMBItem2;
                QueryItem OUTFALLItem2 = new QueryItem() { QueryItemName = "排放口", QueryClassName = "PS_OUTFALL" };
                OUTFALLItem2.QueryFields = new QueryField[] {
                        new QueryField() { FieldName = "ID", ItemName = "序号" , pDataType= DataType.text},
                   // new QueryField() { FieldName = "ID", ItemName = "编码" , pDataType= DataType.text},
                     new QueryField() { FieldName = "SURVEY_ID", ItemName = "物探点号" , pDataType= DataType.text}   
                };
                BtnOUTFALLItem2.Tag = OUTFALLItem2;
                BtnOUTFALLItem2.Click += new EventHandler(BtnQueryError_Click);

                QueryItem PUMPItem2 = new QueryItem() { QueryItemName = "泵站前池", QueryClassName = "PS_PUMP_STORAGE" };
                PUMPItem2.QueryFields = new QueryField[] {
                        new QueryField() { FieldName = "ID", ItemName = "序号" , pDataType= DataType.text},
                   // new QueryField() { FieldName = "ID", ItemName = "编码" , pDataType= DataType.text},
                   new QueryField() { FieldName = "SURVEY_ID", ItemName = "物探点号" , pDataType= DataType.text}   
                };
                BtnPUMPItem2.Tag = PUMPItem2;
                BtnPUMPItem2.Click += new EventHandler(BtnQueryError_Click);

                QueryItem PipeItem2 = new QueryItem() { QueryItemName = "圆管", QueryClassName = "PS_Pipe" };
                PipeItem2.QueryFields = new QueryField[] {
                     new QueryField() { FieldName = "ID", ItemName = "序号" , pDataType= DataType.text},
                 new QueryField() { FieldName = "US_SURVEY_ID", ItemName = "起点点号" , pDataType= DataType.text},
                 new QueryField() { FieldName = "DS_SURVEY_ID", ItemName = "终点点号" , pDataType= DataType.text},
                 new QueryField() { FieldName = "WIDTH", ItemName = "管径" , pDataType= DataType.text}
                };
                BtnPipeItem2.Tag = PipeItem2;
                BtnPipeItem2.Click += new EventHandler(BtnQueryError_Click);

                QueryItem CanalItem2 = new QueryItem() { QueryItemName = "渠箱", QueryClassName = "PS_CANAL" };
                CanalItem2.QueryFields = new QueryField[] {
                     new QueryField() { FieldName = "ID", ItemName = "序号" , pDataType= DataType.text},
                 new QueryField() { FieldName = "US_SURVEY_ID", ItemName = "起点点号" , pDataType= DataType.text},
                 new QueryField() { FieldName = "DS_SURVEY_ID", ItemName = "终点点号" , pDataType= DataType.text},
                 new QueryField() { FieldName = "WIDTH", ItemName = "管宽" , pDataType= DataType.text},
                 new QueryField() { FieldName = "HEIGHT", ItemName = "管高" , pDataType= DataType.text}
                };
                BtnCanal2.Tag = CanalItem2;
                BtnCanal2.Click += new EventHandler(BtnQueryError_Click);
        
          
                BtnNewPipe.Tag = new PipeLineCtrl();
                BtnCanal.Tag = new CANALCtrl();
                BtnFLAP.Tag=new  FLAPCtrl();
                BtnWEIR.Tag=new WEIRCtrl();
                BtnSLUICE.Tag = new SLUICECtrl();

                BtnManhole.Tag = new MANHOLECtrl();
                BtnComb.Tag = new COMBCtrl();
                BtnOutFall.Tag = new OUTFALLCtrl();
                BtnVPipePoint.Tag = new PipePointCtrl();
                BtnPUMP.Tag = new PUMPCtrl();  */
            AcadDoc = AcadApp.ActiveDocument;
            acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            //bar1.Text = "数据查询";
            AcadDoc.SelectionChanged += new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);
            AcadDoc.ObjectErased += new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
             
            //aApp.DocumentManager.MdiActiveDocument.Editor.PromptedForSelection += new Autodesk.AutoCAD.EditorInput.PromptSelectionResultEventHandler(Editor_PromptedForSelection);
            //SelectionAdded
            //aApp.DocumentManager.MdiActiveDocument.Editor.SelectionAdded += new SelectionAddedEventHandler(Editor_SelectionAdded);
          
           
        }
        public void AddSelectionChangedEvent()
        {
            AcadDoc.SelectionChanged += new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);

        }
        public void RemoveSelectionChangedEvent()
        {
            AcadDoc.SelectionChanged -= new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);
 
        }
       private void AcadDoc_ObjectErased(int ObjectId)
        {
            /*
            if (pPipeDataCtrl == null) return;
            IPipeData CPipeData = pPipeDataCtrl.GetData();
            try
            {
                //  aApp.DocumentManager.MdiActiveDocument.Editor.SelectionAdded -= new SelectionAddedEventHandler(Editor_SelectionAdded);
                AcadDoc.SelectionChanged -= new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);

                CPipeData.Delete();
                AcadDoc.ObjectErased -= new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                //Autodesk.AutoCAD.ApplicationServices.Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                CPipeData.DeleteCADObject(acDoc);
                AcadDoc.ObjectErased += new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                //AcadDoc.Save();
                pPipeDataCtrl.Clear();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                AcadDoc.SelectionChanged += new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);
                AcadDoc.ObjectErased += new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                //aApp.DocumentManager.MdiActiveDocument.Editor.SelectionAdded += new SelectionAddedEventHandler(Editor_SelectionAdded);
            } */
        }
       private string GetPointObjectID(AcadEntity pAcadObject)
       {

           if (pAcadObject == null) return "";

           AcadDictionary pAcadDictionary = pAcadObject.GetExtensionDictionary();
           if (pAcadDictionary.Count == 1)
           {

               AcadXRecord pAcadXRecord = pAcadDictionary.Item(0) as AcadXRecord;
               string ID = pAcadXRecord.Name;

               return ID;
               //}
           }
           return string.Empty;
       }
       

       

   /*     void Editor_SelectionAdded(object sender, SelectionAddedEventArgs e)
        {
            //SelectedCADObj = null;
            //if (!BtnPick.Checked) return;
            try
            {
                AcadDocument AcadDoc = AcadApp.ActiveDocument;
                if (e.AddedObjects.Count == 0) return;
                AcadEntity pAcadEntity = AcadDoc.ObjectIdToObject(e.AddedObjects[e.AddedObjects.Count - 1].ObjectId.OldId) as AcadEntity;
                if (pAcadEntity == null) return;

                if (SelectedCADObj != null && SelectedCADObj.Equals(pAcadEntity)) return;
                SelectedCADObj = pAcadEntity;

                AcadDictionary pAcadDictionary = SelectedCADObj.GetExtensionDictionary();
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
                    string sql = string.Format("select * from {0} where ID='{1}'", TableName, ID);
                    string ItemName = GetItemNameByClassname(TableName);
                    if (ItemName == string.Empty) return;
                    if (pPipeDataCtrl == null)
                        SwitchControl(ItemName);
                    else if (pPipeDataCtrl != null && !pPipeDataCtrl.DataType.Equals(ItemName))
                        SwitchControl(ItemName);
                    DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];

                    if (pTable.Rows.Count > 0 && pPipeData != null && pPipeDataCtrl != null)
                    {
                        pPipeData.FillValueByRow(pTable.Rows[0], TableName);
                        pPipeDataCtrl.SetData(pPipeData);
                        this.Refresh();
                    }
                }
            }
            catch (Exception ex)            
            {
            }
        }
        */
     

       

      void AcadApp_EndOpen(string FileName)
        {
            //AcadDoc = AcadApp.ActiveDocument;
            //AcadDoc.SelectionChanged += new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);
            //aApp.DocumentManager.MdiActiveDocument.Editor.SelectionAdded += new SelectionAddedEventHandler(Editor_SelectionAdded);
        }
       
        
    
        
        //void AcadApp_AppActivate()
        //{
        //    AcadDoc = AcadApp.ActiveDocument;
        //    AcadDoc.SelectionChanged += new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);
        //}
        /*
       private void DgvQueryResult_CellClick(object sender, GridCellClickEventArgs e)
        {
            GridRow pGridRow = e.GridCell.GridRow;
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            if (pGridRow == null) return;

            //QueryItem pQueryItem = DgvQueryResult.Tag as QueryItem;
            string TableName = (DgvQueryResult.PrimaryGrid.Tag as DataTable).TableName;
            string ID = pGridRow.Cells["序号"].Value.ToString();
            try
            {
                string sql = string.Format("select * from {0} where objectID={1}", TableName, ID);
                DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
                string ItemName = DgvQueryResult.Text;
                if (pPipeDataCtrl == null)
                    SwitchControl(ItemName);
                if (pPipeDataCtrl != null && !pPipeDataCtrl.DataType.Equals(ItemName))
                    SwitchControl(ItemName);

                if (pTable.Rows.Count > 0 && pPipeData != null)
                {

                    pPipeData.FillValueByRow(pTable.Rows[0], TableName);
                    pPipeDataCtrl.SetData(pPipeData);
                    pPipeDataCtrl.ZoomTo();
                    pPipeDataCtrl.IsErrorData = false;
                    this.Refresh();
                }
            }
            finally
            { }
        }
        */
        /*
        private void DgvImportError_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            GridRow pGridRow = e.GridCell.GridRow;
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            if (pGridRow == null) return;

            string ID = pGridRow.Cells["序号"].Value.ToString();

            //QueryItem pQueryItem = DgvImportError.Tag as QueryItem;
            DataTable pTable = DgvImportError.PrimaryGrid.Tag as DataTable;
            string TableName = pGridRow.Tag.ToString();
            //if (pTable.Columns.Contains("ClassName"))
            //{
            //    TableName = pGridRow.Cells["ClassName"].Value.ToString();
            //}
            try
            {
                string sql = string.Format("select * from {0} where ID='{1}'", TableName, ID);
                 pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
                string ItemName = GetItemNameByClassname(TableName);
                //string ItemName = pQueryItem.QueryItemName;

                if (pPipeDataCtrl == null)
                    SwitchControl(ItemName);
                if (pPipeDataCtrl != null && !pPipeDataCtrl.DataType.Equals(ItemName))
                    SwitchControl(ItemName);

                if (pTable.Rows.Count > 0 && pPipeData != null)
                {
                    pPipeData.FillValueByRow(pTable.Rows[0], TableName);
                    pPipeDataCtrl.SetData(pPipeData);
                    this.Refresh();
                    pPipeDataCtrl.ZoomTo();
                    pPipeDataCtrl.IsErrorData = true;
                }
            }
            catch (Exception ex)
            { }
            finally
            { }
        }*/
        private void BindTableToGrid(GridPanel DataGrid, DataTable pTable)
        {
            DataTable ntable = pTable;
          
            pTable = ntable.DefaultView.ToTable();
            DataGrid.Columns.Clear();
            DataGrid.Rows.Clear();
            DataGrid.Tag = pTable;
            foreach (DataColumn pColumn in pTable.Columns)
            {
                DataGrid.Columns.Add(new GridColumn(pColumn.ColumnName));
            }
            foreach (DataRow pRow in pTable.Rows)
            {
                GridRow pGridRow = new GridRow(pRow.ItemArray);
                //if (pTable.Columns)
                pGridRow.Tag = pTable.TableName;
                DataGrid.Rows.Add(pGridRow);

            }
        }
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        const int MOUSEEVENTF_LEFTUP = 0x0004;
   
        private IPipeData pPipeData = null;
        

        private void SwitchControl(string ObjectType)
        {
            if (pPipeDataCtrl != null)
            {
                if (pPipeDataCtrl is PCPointCtrl && ObjectType.Equals("点"))
                    return;
                else if (pPipeDataCtrl is PipeLineCtrl && ObjectType.Equals("线"))
                    return;
            }
            panelEx3.SuspendLayout();
            panelEx3.Controls.Clear();
           // string TableName = (CmbDataType.SelectedItem as ErrorTable).Name;
            if (ObjectType.Equals("点") )
            {
                pPipeDataCtrl = new PCPointCtrl(this);
                //pPipeDataCtrl.DataType = ObjectType;
             //   panelEx3.Controls.Add(pPipeDataCtrl as UserControl);
                //pPipeData = new MANHOLE();
            }
            else if (ObjectType.Equals("线") )
            {
                pPipeDataCtrl = new PipeLineCtrl(this);
                //pPipeDataCtrl.DataType = ObjectType;
                //pPipeData = new PIPELineClass();
            }         
            else
            {
                pPipeDataCtrl = null;
                //pPipeData = null;

            }
            if (pPipeDataCtrl != null)
            {
                (pPipeDataCtrl as UserControl).Dock = DockStyle.Fill;
                panelEx3.Controls.Add(pPipeDataCtrl as UserControl);
            }
            panelEx3.ResumeLayout();
 
        }

    
       
        private AcadDocument AcadDoc = null;
        Autodesk.AutoCAD.ApplicationServices.Document acDoc =null;
        protected override void OnLoad(EventArgs e)
        {
       
          /*  ErrorTable[] ErrorTables = new ErrorTable[] 
            { new ErrorTable() { Name = "检查井", TableName = "PS_MANHOLE" },
            new ErrorTable(){Name = "雨水口", TableName = "PS_COMB"},
            new ErrorTable(){Name = "排放口", TableName = "PS_OUTFALL"},
            new ErrorTable(){Name = "管线点", TableName = "PS_VIRTUAL_POINT"},
            new ErrorTable(){Name = "泵站前池", TableName = "PS_PUMP_STORAGE"},
            new ErrorTable(){Name = "圆管", TableName = "PS_PIPE"}
            };*/
            QueryItem ManholeItem = new QueryItem() { QueryItemName = "检查井", QueryClassName = "PS_Manhole" };
            ManholeItem.QueryFields = new QueryField[] {                   
                new QueryField() { FieldName = "ObjectID", ItemName = "序号" , pDataType= DataType.number},
                new QueryField() { FieldName = "ID", ItemName = "检查井编码" , pDataType= DataType.text},
                new QueryField() { FieldName = "SURVEY_ID", ItemName = "摸查点号" , pDataType= DataType.text},
               new QueryField() { FieldName = "SYSTEM_TYPE", ItemName = "属性" , pDataType= DataType.text},
                new QueryField() { FieldName = "GROUND_LEVEL", ItemName = "井盖高程" , pDataType= DataType.number},
                new QueryField() { FieldName = "INVERT_LEVEL", ItemName = "井底高程" , pDataType= DataType.number},
                new QueryField() { FieldName = "WATER_LEVEL", ItemName = "水位高程" , pDataType= DataType.number},
                new QueryField() { FieldName = "SEDIMENT_DEPTH", ItemName = "淤积深度" , pDataType= DataType.number},
                 new QueryField() { FieldName = "COVER_MATERIAL", ItemName = "井盖材质" , pDataType= DataType.text},
                 new QueryField() { FieldName = "MANHOLE_MATERIAL", ItemName = "井室材质" , pDataType= DataType.text},
                 new QueryField() { FieldName = "BOTTOM_TYPE", ItemName = "井底形式" , pDataType= DataType.text},
                 new QueryField() { FieldName = "MANHOLE_SHAPE", ItemName = "井室类型" , pDataType= DataType.text},
                 new QueryField() { FieldName = "MANHOLE_SIZE", ItemName = "井室尺寸" , pDataType= DataType.text},
                 new QueryField() { FieldName = "ROAD_NAME", ItemName = "所在道路" , pDataType= DataType.text}
                 
            };

            QueryItem PipePointItem = new QueryItem() { QueryItemName = "管线点", QueryClassName = "PS_VIRTUAL_POINT" };
            PipePointItem.QueryFields = new QueryField[] {
                new QueryField() { FieldName = "ObjectID", ItemName = "序号" , pDataType= DataType.number},
                new QueryField() { FieldName = "ID", ItemName = "管线点编码" , pDataType= DataType.text},
                new QueryField() { FieldName = "SYSTEM_TYPE", ItemName = "属性" , pDataType= DataType.text},
                new QueryField() { FieldName = "SURVEY_ID", ItemName = "摸查点号" , pDataType= DataType.text},
                 new QueryField() { FieldName = "GROUND_LEVEL", ItemName = "地面高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "INVERT_LEVEL", ItemName = "底部高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "ROAD_NAME", ItemName = "所在道路" , pDataType= DataType.text}
            };

            QueryItem COMBItem = new QueryItem() { QueryItemName = "雨水口", QueryClassName = "PS_COMB" };
            COMBItem.QueryFields = new QueryField[] {
                new QueryField() { FieldName = "ObjectID", ItemName = "序号" , pDataType= DataType.number},
                new QueryField() { FieldName = "ID", ItemName = "雨水口编码" , pDataType= DataType.text},
                new QueryField() { FieldName = "SURVEY_ID", ItemName = "摸查点号" , pDataType= DataType.text},
                
                 new QueryField() { FieldName = "GROUND_LEVEL", ItemName = "地面高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "INVERT_LEVEL", ItemName = "底部高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "ROAD_NAME", ItemName = "所在道路" , pDataType= DataType.text}
            };

            QueryItem OUTFALLItem = new QueryItem() { QueryItemName = "排放口", QueryClassName = "PS_OUTFALL" };
            OUTFALLItem.QueryFields = new QueryField[] {
                new QueryField() { FieldName = "ObjectID", ItemName = "序号" , pDataType= DataType.number},
                new QueryField() { FieldName = "ID", ItemName = "排放口编码" , pDataType= DataType.text},
                new QueryField() { FieldName = "SURVEY_ID", ItemName = "摸查点号" , pDataType= DataType.text},
                new QueryField() { FieldName = "SYSTEM_TYPE", ItemName = "属性" , pDataType= DataType.text},
                 new QueryField() { FieldName = "GROUND_LEVEL", ItemName = "地面高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "INVERT_LEVEL", ItemName = "底部高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "ROAD_NAME", ItemName = "所在道路" , pDataType= DataType.text}
            };
            QueryItem PUMPItem = new QueryItem() { QueryItemName = "泵站前池", QueryClassName = "PS_PUMP_STORAGE" };
            PUMPItem.QueryFields = new QueryField[] {
                new QueryField() { FieldName = "ObjectID", ItemName = "序号" , pDataType= DataType.number},
                new QueryField() { FieldName = "ID", ItemName = "泵站前池编码" , pDataType= DataType.text},
              //  new QueryField() { FieldName = "SURVEY_ID", ItemName = "摸查点号" , pDataType= DataType.text},
                new QueryField() { FieldName = "SYSTEM_TYPE", ItemName = "属性" , pDataType= DataType.text},
                 new QueryField() { FieldName = "GROUND_LEVEL", ItemName = "地面高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "Bottom_LEVEL", ItemName = "池底高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "Bottom_AREA", ItemName = "池底面积" , pDataType= DataType.number},
                 new QueryField() { FieldName = "Top_LEVEL", ItemName = "池顶高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "Top_AREA", ItemName = "池顶面积" , pDataType= DataType.number},
                 new QueryField() { FieldName = "ROAD_NAME", ItemName = "所在道路" , pDataType= DataType.text}
            };

            QueryItem PipeItem = new QueryItem() { QueryItemName = "圆管", QueryClassName = "PS_Pipe" };
            PipeItem.QueryFields = new QueryField[] {
                new QueryField() { FieldName = "ObjectID", ItemName = "序号" , pDataType= DataType.number},
                new QueryField() { FieldName = "ID", ItemName = "圆管编码" , pDataType= DataType.text},
              //  new QueryField() { FieldName = "SURVEY_ID", ItemName = "摸查点号" , pDataType= DataType.text},
                new QueryField() { FieldName = "SYSTEM_TYPE", ItemName = "属性" , pDataType= DataType.text},
                new QueryField() { FieldName = "US_OBJECT_ID", ItemName = "上游点编码" , pDataType= DataType.text},
                 new QueryField() { FieldName = "DS_OBJECT_ID", ItemName = "下游点编码" , pDataType= DataType.text},
                 new QueryField() { FieldName = "Width", ItemName = "管径" , pDataType= DataType.number},
                 new QueryField() { FieldName = "PIPE_LENGTH", ItemName = "管长" , pDataType= DataType.number},
                 new QueryField() { FieldName = "US_POINT_INVERT_LEVEL", ItemName = "上游井底高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "US_INVERT_LEVEL", ItemName = "上游管底高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "DS_POINT_INVERT_LEVEL", ItemName = "下游井底高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "DS_INVERT_LEVEL", ItemName = "下游管底高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "SEDIMENT_DEPTH", ItemName = "淤积深度" , pDataType= DataType.number},
                 new QueryField() { FieldName = "MATERIAL", ItemName = "材质" , pDataType= DataType.text},
                 new QueryField() { FieldName = "ROAD_NAME", ItemName = "所在道路" , pDataType= DataType.text}
            };
            //CmbDataBase.Items.AddRange(new QueryItem[] { ManholeItem, PipePointItem, COMBItem, OUTFALLItem, PUMPItem, PipeItem });

          
            //CmbDataType.Items.AddRange(new QueryItem[] { ManholeItem2, PipePointItem2, COMBItem2, OUTFALLItem2, PUMPItem2, PipeItem2 });
            
           /* foreach (ErrorTable pErrorTable in ErrorTables)
            {                
                string sql = string.Format("select ObjectID as 序号,ID as 编号,ErrorMsg as 错误信息 from {0}", pErrorTable.TableName);
                DataTable pTable= SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
                if (pTable.Rows.Count > 0)
                {
                    CmbDataType.Items.Add(new ErrorTable() { Name = pErrorTable.Name, TableName = pErrorTable.TableName, Table = pTable });
                }
            }*/

            //CmbDataType.SelectedIndex = 0;
           // CmbDataBase.SelectedIndex = 0;
        }
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);



        private AcadEntity SelectedCADObj = null;
        private bool IsPointID(string ID)
        {
            
            string sql = string.Format("select CO_X,CO_Y from Points where ID='{0}'", ID);
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
            return pTable.Rows.Count > 0; 
          /*  if (ID.StartsWith("YS") || ID.StartsWith("WS"))
                return true;
            else
                return false;*/
        }
      private void AcadDoc_SelectionChanged()
        {
             SelectedCADObj = null;
            //if (!BtnPick.Checked) return;
            // AcadDocument AcadDoc = AcadApp.ActiveDocument;
             AcadSelectionSet pAcadSelectionSets = AcadDoc.PickfirstSelectionSet;
            if (pAcadSelectionSets.Count == 0)
            {
                pPipeDataCtrl.Clear();
                return;
            }
            SelectedCADObj = pAcadSelectionSets.Item(pAcadSelectionSets.Count-1);

           AcadDictionary pAcadDictionary = SelectedCADObj.GetExtensionDictionary();
           if (pAcadDictionary.Count == 1)
           {
             
               AcadXRecord pAcadXRecord  = pAcadDictionary.Item(0) as AcadXRecord;
               string ID = pAcadXRecord.Name;
               string ClassName = "Points";
               string sql = string.Format("select * from {0} where ID='{1}'", ClassName, ID);
               string ControlType;
               if (!IsPointID(ID))
               {
                   ClassName = "PS_PIPE";
                   sql = string.Format("select * from {0} where  ID='{1}'", ClassName, ID);
                   pPipeData = new PIPELineClass();
                   ControlType = "线";
                  // SwitchControl("线");
               }
               else
               {
                   pPipeData = new PCPoint();
                   ControlType = "点";
                   //SwitchControl("点");
               }
              // string ItemName = GetItemNameByClassname(TableName);
            
               DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];

               if (pTable.Rows.Count > 0)
               {
                   SwitchControl(ControlType);
                   pPipeData.FillValueByRow(pTable.Rows[0]);
                   pPipeDataCtrl.SetData(pPipeData);
                   this.Refresh();
               }
               else
               {
                   MessageBox.Show("该图元信息数据库中不存在","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   panelEx3.Controls.Clear(); 
               }
           }
  
        } 
       private string GetItemNameByClassname(string ClassName)
       {
           string sql = string.Format("select TableName from tableconfig where TableClassName='{0}'", ClassName.ToUpper());
           DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
           if (pTable.Rows.Count == 0) return string.Empty;
           else
               return pTable.Rows[0][0].ToString();
       }
        private void BtnZoom_Click(object sender, EventArgs e)
        {
            if (pPipeDataCtrl != null)
            {
                //Point pPoint = pPipeDataCtrl.GetPoint();
                try
                {
                    pPipeDataCtrl.ZoomTo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("定位失败", ex.Message);
                }
            }
        }

        

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (pPipeDataCtrl == null) return;
            IPipeData pPipeData = pPipeDataCtrl.GetData();
            //string ClassName = pPipeData.CurClass;
            if (string.IsNullOrEmpty(pPipeData.ID))
            {
                string[] Errors= pPipeData.Verification();
                if (Errors.Length == 0)
                {
                    pPipeData.ID = Guid.NewGuid().ToString("N");
                    try
                    {
                        pPipeData.AddNew();
                        pPipeData.DrawCADObject(AcadDoc);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"保存出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //AcadDoc.Save();
                    pPipeDataCtrl.SetData(pPipeData);
                    
                    //pPipeData.Delete();
                    //GridRow pGridRow = DgvImportError.ActiveRow as GridRow;
                    //DgvImportError.PrimaryGrid.Rows.Remove(pGridRow);
                    MessageBox.Show("保存成功", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                }
                else
                {
                    ErrorItemFrm pDlg = new ErrorItemFrm(Errors);
                    pDlg.ShowDialog();
                    return;
                }
            }
            else
            {
                string[] Errors = pPipeData.Verification();
                if (Errors.Length == 0)
                {
                 
                    
                        //if (SelectedCADObj != null)
                        //    SelectedCADObj.Delete();
                        try
                        {
                            //aApp.DocumentManager.MdiActiveDocument.Editor.SelectionAdded -= new SelectionAddedEventHandler(Editor_SelectionAdded);
                            AcadDoc.SelectionChanged -= new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);
                            //Autodesk.AutoCAD.ApplicationServices.Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                            if (pPipeData is IPCPoint)
                            {
                                IPCPoint NewPoint=pPipeData as IPCPoint;
                                IPCPoint OldPoint = new PCPoint();
                                OldPoint.GetDataByID(pPipeData.ID);
                                OldPoint.DeleteCADObject(acDoc);
                                pPipeData.Update();
                               
                                NewPoint.DrawCADObject(AcadDoc);

                                IPIPELine[] PipeLines = (pPipeData as IPipePoint).GetConnLines();
                                foreach (IPIPELine pPIPELine in PipeLines)
                                {
                                    if (pPIPELine.US_OBJECT_ID.Equals(pPipeData.ID))
                                    {
                                        string OldUSType = pPIPELine.US_SURVEY_ID.Substring(0, 1);
                                        string NewUSType = (pPipeData as IPCPoint).SURVEY_ID.Substring(0, 1);
                                        pPIPELine.US_SURVEY_ID = (pPipeData as IPCPoint).SURVEY_ID;
                                        if (!OldUSType.Equals(NewUSType))
                                        {
                                            IPIPELine pOldLine = new PIPELineClass();
                                            pOldLine.GetDataByID(pPIPELine.ID);
                                            pOldLine.DeleteCADObject(acDoc);
                                            pPIPELine.DrawCADObject(AcadDoc);

                                        }
                                    }
                                    else if (pPIPELine.DS_OBJECT_ID.Equals(pPipeData.ID))
                                        pPIPELine.DS_SURVEY_ID = (pPipeData as IPCPoint).SURVEY_ID;
                                    pPIPELine.Update();
                                }

                                if (OldPoint.X != NewPoint.X||OldPoint.Y!=NewPoint.Y)
                                {
                                    if (PipeLines.Length == 0) return;                                   

                                    foreach (IPIPELine pPIPELine in PipeLines)
                                    {
                                       
                                        pPIPELine.DeleteCADObject(acDoc);                                       
                                        pPIPELine.DrawCADObject(AcadDoc);

                                    }
                                    
                                }
                            }
                            else
                            {
                                IPIPELine pOldLine = new PIPELineClass();
                                pOldLine.GetDataByID(pPipeData.ID);
                                pOldLine.DeleteCADObject(acDoc);
                        
                                pPipeData.Update();                                
                                
                                pPipeData.DrawCADObject(AcadDoc);
                            }
                            MessageBox.Show("保存成功", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            if (pPipeDataCtrl is PCPointCtrl)
                            {
                               
                               
 
                            }
                             
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            AcadDoc.SelectionChanged += new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);
                          //  aApp.DocumentManager.MdiActiveDocument.Editor.SelectionAdded += new SelectionAddedEventHandler(Editor_SelectionAdded);
                        }

                   
                  
                }
                else
                {
                    ErrorItemFrm pDlg = new ErrorItemFrm(Errors);
                    pDlg.ShowDialog();
                    return;
                }
            }
        }

       

        private void BtnQueryError_Click(object sender, EventArgs e)
        {
            ButtonItem pButtonItem = sender as ButtonItem;           
            if (pButtonItem.Tag == null) return;
            //DgvImportError.Text = pButtonItem.Text;
            QueryItem pQueryItem = pButtonItem.Tag as QueryItem;
            string ItemName = pQueryItem.QueryItemName;
            string TableName = pQueryItem.QueryClassName;
            string Fields = string.Empty;
            QueryField[] QueryFields = pQueryItem.QueryFields;
            foreach (QueryField pQueryField in QueryFields)
            {
                if (!pQueryField.FieldName.Equals(pQueryField.ItemName))
                    Fields += string.Format("{0} as {1},", pQueryField.FieldName, pQueryField.ItemName);
                else
                    Fields += string.Format(" {0},", pQueryField.FieldName);
            }
            Fields = Fields.TrimEnd(',');
            //string sql = string.Format("select ObjectID as 序号,ID as 编号,ErrorMsg as 错误信息 from {0}_2", TableName);
            string sql = string.Format("select {0} from {1}   ", Fields, TableName);
            /*  DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
              DgvQueryResult.PrimaryGrid.DataSource = pTable;*/

            // Fields += ",ErrorMsg as 错误信息";
            DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
            pTable.TableName = TableName;
            //DgvImportError.Text = ItemName;
            //DgvImportError.Tag = pQueryItem;
            //DgvImportError.PrimaryGrid.Rows.Clear();
            //DgvImportError.PrimaryGrid.Columns.Clear();

            //DgvImportError.PrimaryGrid.DataSource = pTable;
            //BindTableToGrid(DgvImportError.PrimaryGrid, pTable);

        }

        private void BtnNewLine_Click(object sender, EventArgs e)
        {


            SwitchControl("线");
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (pPipeDataCtrl == null) return;
           
            IPipeData pPipeData = pPipeDataCtrl.GetData();
            if (MessageBox.Show("是否删除当前要素", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.Cancel)
                return;
            try
            {
              //  aApp.DocumentManager.MdiActiveDocument.Editor.SelectionAdded -= new SelectionAddedEventHandler(Editor_SelectionAdded);
                AcadDoc.SelectionChanged -= new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);
                pPipeData.Delete();
                //Autodesk.AutoCAD.ApplicationServices.Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                AcadDoc.ObjectErased -= new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                pPipeData.DeleteCADObject(acDoc);
                AcadDoc.ObjectErased += new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                //AcadDoc.Save();
                pPipeDataCtrl.Clear();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                AcadDoc.SelectionChanged += new _DAcadDocumentEvents_SelectionChangedEventHandler(AcadDoc_SelectionChanged);
                //aApp.DocumentManager.MdiActiveDocument.Editor.SelectionAdded += new SelectionAddedEventHandler(Editor_SelectionAdded);
            }
        }

        private void BtnNewPoint_Click(object sender, EventArgs e)
        {
            //ButtonItem pButtonItem = sender as ButtonItem;
            //if (pButtonItem.Tag == null) return;

           /* panelEx3.SuspendLayout();
            panelEx3.Controls.Clear();
            pPipeDataCtrl = new PCPointCtrl();

            
                (pPipeDataCtrl as UserControl).Dock = DockStyle.Fill;
                panelEx3.Controls.Add(pPipeDataCtrl as UserControl);
                pPipeDataCtrl.Clear();
              */
                //pPipeDataCtrl.SetNewID();
            //pPipeData = new PCPoint();
            SwitchControl("点");
            pPipeDataCtrl.Clear();

        }

        private void BtnLineLenght_Click(object sender, EventArgs e)
        {
            SetLenhtDlg pSetLenhtDlg = new SetLenhtDlg();
            if (pSetLenhtDlg.ShowDialog() == DialogResult.OK)
            {
                double lenght = pSetLenhtDlg.Lenght;
                string sql = string.Format("select ID as 编码,lenght as 长度,类别,ClassName from V_Line_US_DS where lenght>{0}", lenght);
                DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
                               
                //BindTableToGrid(DgvImportError.PrimaryGrid, pTable);
            }
        }

        private void BtnDataImport_Click(object sender, EventArgs e)
        {

            AddDataFrm pAddDataFrm = new AddDataFrm();
            if (pAddDataFrm.ShowDialog() != DialogResult.OK) return;
            //pWork.RunWorkerAsync(pAddDataFrm.pWorkArgument);
           

            pProgressFrm = HR.Controls.ProgressFrm.GetInstance();
            pProgressFrm.CanCancle = false;
            //pProgressFrm.FormClosed += new FormClosedEventHandler(pProgressFrm_FormClosed);
            pProgressFrm.SetProgressStyle(ProgressBarStyle.Blocks);
            pProgressFrm.Text = "数据导入";

            PWork_DoWork(pAddDataFrm.pWorkArgument);
        }

      

        void pWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pProgressFrm.SafeCallCloseDialog();

        }

        
        private HR.Controls.ProgressFrm pProgressFrm = null;
        private void PWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progressup(e.ProgressPercentage);
        }
        //private BackgroundWorker pWork = null;
        delegate void Progressupdelegate(int value);
        delegate void Labeldelegate(string msg);
        private void Progressup(int value)
        {
            pProgressFrm.SafeCallDisplayProgress(value);

        }
      
      
        private void Labelup(string msg)
        {
            pProgressFrm.SafeCallDisplayText(msg);

        }

        private DataRow IsExistSURVEYID(string SURVEYID)
        {
            string sql = string.Format("select  * from Points where SURVEY_ID='{0}'", SURVEYID);
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            System.Data.DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
            if (pTable.Rows.Count == 0) return null;
            return pTable.Rows[0];

        }

        private void PWork_DoWork(WorkArgument pWorkArgument)
        {
            //pProgressFrm.TopMost = true;
            pProgressFrm.ShowDialog();
            //WorkArgument pWorkArgument = e.Argument as WorkArgument;


            string ExcelPath = pWorkArgument.ExcelPath;

         
            double MinX, MinY, MaxX, MaxY;
            //string[] MinPoint = System.Configuration.ConfigurationSettings.AppSettings["MinPoint"].Split(',');

            MinX = double.Parse(CIni.ReadINI("XYExtent", "MinX"));
            MinY = double.Parse(CIni.ReadINI("XYExtent", "MinY"));

            //string[] MaxPoint = System.Configuration.ConfigurationSettings.AppSettings["MaxPoint"].Split(',');
            MaxX = double.Parse(CIni.ReadINI("XYExtent", "MaxX"));
            MaxY = double.Parse(CIni.ReadINI("XYExtent", "MaxY"));
            DataTable PCTable = null;
            if (!string.IsNullOrEmpty(ExcelPath))
                PCTable = ExcelClass.ReadExcelFile(ExcelPath);           

            Dictionary<string, DataRow> CoordBHTable = new Dictionary<string, DataRow>();
            if (!string.IsNullOrEmpty(pWorkArgument.CoordExcelpath))
            {
                DataTable CoordTable = ExcelClass.ReadExcelFile(pWorkArgument.CoordExcelpath);
                CoordTable.Columns[0].ColumnName = "ID";
                //CoordTable.PrimaryKey = new DataColumn[] { CoordTable.Columns[0] };
                DataRow FirstRow = CoordTable.Rows[0];
                for (int j = 1; j < FirstRow.ItemArray.Length; j++)
                {
                    string value = FirstRow.ItemArray[j].ToString();
                    double V_num = 0;
                    if (double.TryParse(value, out V_num))
                    {
                        if (V_num > MinX && V_num < MaxX)
                        {
                            CoordTable.Columns[j].ColumnName = "CO_X";
                        }
                        else if (V_num > MinY && V_num < MaxY)
                        {
                            CoordTable.Columns[j].ColumnName = "CO_Y";
                        }
                        else
                            CoordTable.Columns[j].ColumnName = "CO_Z";
                    }
                }


                foreach (DataRow pCoordRow in CoordTable.Rows)
                {
                    string key = pCoordRow["ID"].ToString();
                    if (!CoordBHTable.ContainsKey(key))
                        CoordBHTable.Add(key, pCoordRow);
                }
            }
           
            try
            {
                if (PCTable != null)
                {
                    DataTable ErrorTable = PCTable.Clone();
                    if (!ErrorTable.Columns.Contains("错误消息"))
                        ErrorTable.Columns.Add("错误消息");
                    #region 循环遍历表
                    //int SumCount = PCTable.Rows.Count;
                    //int CurNum =0;
                    DataTable PCTab2 = PCTable.Copy();
                    for (int i = 0; i < PCTable.Rows.Count; i++)
                    {
                        //CurNum++;
                        DataRow pDataRow = PCTable.Rows[i];
                        try
                        {
                            //Labelup(string.Format("正在导入第: {0}/{1}条", i, PCTable.Rows.Count));
                            pProgressFrm.SafeCallDisplayText(string.Format("正在导入第: {0}/{1}条管段", i, PCTable.Rows.Count));
                            pProgressFrm.SafeCallDisplayProgress(i * 100 / PCTable.Rows.Count);
                            //pWork.ReportProgress(i * 100 / PCTable.Rows.Count);
                            string ErrorMsg = string.Empty;

                            string SPointID = GetValue(pDataRow, "起点点号");
                            if (string.IsNullOrEmpty(SPointID))
                                ErrorMsg += "起点号不能为空;";
                            IPCPoint SPoint = null;
                            DataRow SPointRow = IsExistSURVEYID(SPointID);
                            if (SPointRow == null)
                            {

                                if (!CoordBHTable.ContainsKey(SPointID))
                                {

                                    ErrorMsg += "起点对应的坐标不存在;";

                                }

                                if (ErrorMsg != string.Empty)
                                {
                                    goto a1;
                                }
                                //DataRow pCoordRow = CoordBHTable[SPointID];
                                SPoint = GetPipePoint(SPointID, pDataRow, CoordBHTable[SPointID]);
                                SPoint.AddNew();
                                SPoint.DrawCADObject(AcadDoc);
                                //CoordBHTable.ContainsKey(SPointID)
                                //CoordBHTable.Remove(SPointID);
                            }
                            else
                            {
                                //string ClassName = SPointRow["ClassName"].ToString();
                                //string ID = SPointRow["ID"].ToString();
                                SPoint = new PCPoint();
                                //string sql = string.Format("select * from Points where ID='{0}'", ID);

                                //DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];


                                SPoint.FillValueByRow(SPointRow);
                                //if (string.IsNullOrEmpty(SPoint.Type))
                                //{

                                //}

                                UpdatePipePoint(SPoint, pDataRow);
                                bool IsRedraw = false;
                                if (SPoint.Type.Equals(pDataRow["起点类型"].ToString()))
                                    IsRedraw = true;

                                if (CoordBHTable.ContainsKey(SPoint.SURVEY_ID))
                                {
                                    if (!SPoint.X.Equals(CoordBHTable[SPoint.SURVEY_ID]["CO_X"].ToString()) || !SPoint.Y.Equals(CoordBHTable[SPoint.SURVEY_ID]["CO_Y"].ToString()))
                                    {
                                        IsRedraw = true;
                                        SPoint.X = CoordBHTable[SPoint.SURVEY_ID]["CO_X"].ToString();
                                        SPoint.Y = CoordBHTable[SPoint.SURVEY_ID]["CO_Y"].ToString();
                                        SPoint.GROUND_LEVEL = CoordBHTable[SPoint.SURVEY_ID]["CO_Z"].ToString();
                                    }
                                }
                                SPoint.Update();
                                if (IsRedraw)
                                {
                                    AcadDoc.ObjectErased -= new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                                    SPoint.DeleteCADObject(acDoc);
                                    AcadDoc.ObjectErased += new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                                    SPoint.DrawCADObject(AcadDoc);

                                    IPIPELine[] PipeLines = SPoint.GetConnLines();
                                    foreach (IPIPELine pPIPELine in PipeLines)
                                    {
                                        //Autodesk.AutoCAD.ApplicationServices.Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                                        AcadDoc.ObjectErased -= new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                                        pPIPELine.DeleteCADObject(acDoc);
                                        AcadDoc.ObjectErased += new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                                        pPIPELine.DrawCADObject(AcadDoc);
                                    }
                                }




                            }
                            if (CoordBHTable.ContainsKey(SPointID))
                                CoordBHTable.Remove(SPointID);
                            string EPointID = GetValue(pDataRow, "终点点号");
                            if (EPointID == string.Empty)
                                goto a1;
                            IPCPoint EPoint = null;
                            DataRow EPointRow = IsExistSURVEYID(EPointID);

                            if (EPointRow == null)
                            {
                                EPointRow = FindRow(PCTable, EPointID, i + 1);
                                if (EPointRow == null)
                                {
                                    ErrorMsg += "终点对应的信息不存在;";
                                    goto a1;
                                }
                                //   DataRow[] ECoordRows = CoordTable.Select(string.Format("ID='{0}'", EPointID));
                                if (!CoordBHTable.ContainsKey(EPointID))
                                {
                                    ErrorMsg += "终点对应的坐标不存在;";
                                    goto a1;
                                }
 
                                if (ErrorMsg != string.Empty)
                                {
                                    goto a1;
                                }
                                EPoint = GetPipePoint(EPointID, EPointRow, CoordBHTable[EPointID]);
                                EPoint.AddNew();
                                EPoint.DrawCADObject(AcadDoc);
                               
                            }
                            else
                            {
                                //string ClassName = EPointRow["ClassName"].ToString();
                                //string ID = EPointRow["ID"].ToString();
                                EPoint = new PCPoint();
                                //string sql = string.Format("select * from Points where ID='{0}'", ID);

                                //DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];

                                //if (pTable.Rows.Count > 0)
                                //{
                                EPoint.FillValueByRow(EPointRow);
                                UpdatePipePoint(EPoint, pDataRow);
                                bool IsRedraw = false;
                                if (EPoint.Type.Equals(pDataRow["起点类型"].ToString()))
                                    IsRedraw = true;

                                if (CoordBHTable.ContainsKey(EPoint.SURVEY_ID))
                                {
                                    if (!EPoint.X.Equals(CoordBHTable[EPoint.SURVEY_ID]["CO_X"].ToString()) || !EPoint.Y.Equals(CoordBHTable[EPoint.SURVEY_ID]["CO_Y"].ToString()))
                                    {
                                        IsRedraw = true;
                                        EPoint.X = CoordBHTable[EPoint.SURVEY_ID]["CO_X"].ToString();
                                        EPoint.Y = CoordBHTable[EPoint.SURVEY_ID]["CO_Y"].ToString();
                                        EPoint.GROUND_LEVEL = CoordBHTable[EPoint.SURVEY_ID]["CO_Z"].ToString();
                                    }
                                }
                                EPoint.Update();
                                if (IsRedraw)
                                {
                                    AcadDoc.ObjectErased -= new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                                    EPoint.DeleteCADObject(acDoc);
                                    AcadDoc.ObjectErased += new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                                    EPoint.DrawCADObject(AcadDoc);

                                    IPIPELine[] PipeLines = EPoint.GetConnLines();
                                    foreach (IPIPELine pPIPELine in PipeLines)
                                    {
                                        //Autodesk.AutoCAD.ApplicationServices.Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                                        AcadDoc.ObjectErased -= new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                                        pPIPELine.DeleteCADObject(acDoc);
                                        AcadDoc.ObjectErased += new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                                        pPIPELine.DrawCADObject(AcadDoc);
                                    }
                                }
                               
                            }
                            if (CoordBHTable.ContainsKey(EPointID))
                            CoordBHTable.Remove(EPointID);
                            string GJ = GetValue(pDataRow, "管径");
                            IPIPELine CurPIPELine = new PIPELineClass();
                            if (!CurPIPELine.isExistSURVEY_ID(SPointID, EPointID))
                            {

                                CurPIPELine.Width = GJ;
                                CurPIPELine.US_SURVEY_ID = SPointID;
                                CurPIPELine.DS_SURVEY_ID = EPointID;
                                //pPIPELine.ID = pPIPELine.GetHead() + SPointID.Substring(2, 3) + pPIPELine.GetNextNO();
                                CurPIPELine.ID = Guid.NewGuid().ToString("N");
                                CurPIPELine.US_OBJECT_ID = SPoint.ID;
                                CurPIPELine.DS_OBJECT_ID = EPoint.ID;
                                //pPIPELine.SYSTEM_TYPE = SPoint.SYSTEM_TYPE;

                                double D_X = double.Parse(EPoint.X) - double.Parse(SPoint.X);
                                double D_Y = double.Parse(EPoint.Y) - double.Parse(EPoint.Y);
                                double Len = Math.Round(Math.Sqrt(D_X * D_X + D_Y * D_Y), 2);

                                CurPIPELine.Pipe_Length = Len.ToString();
                                
                                CurPIPELine.SEDIMENT_DEPTH = GetValue(pDataRow, "泥深");
                                CurPIPELine.MATERIAL = GetValue(pDataRow, "材质");
                                CurPIPELine.PRESSURE = GetValue(pDataRow, "管道形式");
                                CurPIPELine.STATE = GetValue(pDataRow, "设施状态");
                                CurPIPELine.ROAD_NAME = GetValue(pDataRow, "所在道路");
                                CurPIPELine.Remark = GetValue(pDataRow, "线备注");

                                CurPIPELine.WATER_LEVEL = GetValue(pDataRow, "泥深");
                                CurPIPELine.WATER_QUALITY = GetValue(pDataRow, "水质");
                                CurPIPELine.WATER_State = GetValue(pDataRow, "水体状态");
                                CurPIPELine.Dirtcion = GetValue(pDataRow, "流向");

                                CurPIPELine.AddNew();
                                CurPIPELine.DrawCADObject(AcadDoc);
                            }
                        a1:
                            if (ErrorMsg != string.Empty)
                            {
                                DataRow ErrorRow = ErrorTable.NewRow();

                                List<object> ItemList = new List<object>();

                                if (pDataRow.Table.Columns.Contains("错误消息"))
                                {
                                    pDataRow["错误消息"] = ErrorMsg;
                                    ItemList.AddRange(pDataRow.ItemArray);
                                }
                                else
                                {
                                    ItemList.AddRange(pDataRow.ItemArray);
                                    ItemList.Add(ErrorMsg);
                                }

                                ErrorRow.ItemArray = ItemList.ToArray();
                                ErrorTable.Rows.Add(ErrorRow);

                            }
                            //PCTable.Rows.RemoveAt(i);
                        }
                        catch (System.Exception ex)
                        {
                            DataRow ErrorRow = ErrorTable.NewRow();

                            List<object> ItemList = new List<object>();

                            if (pDataRow.Table.Columns.Contains("错误消息"))
                            {
                                pDataRow["错误消息"] = ex.Message;
                                ItemList.AddRange(pDataRow.ItemArray);
                            }
                            else
                            {
                                ItemList.AddRange(pDataRow.ItemArray);
                                ItemList.Add(ex.Message);
                            }

                            ErrorRow.ItemArray = ItemList.ToArray();
                            ErrorTable.Rows.Add(ErrorRow);

                        }
                    }

                    MessageBox.Show(string.Format("总共导入{0}条管线数据 ，其中成功{1}条，失败{2}条。",
                       PCTab2.Rows.Count, PCTab2.Rows.Count - ErrorTable.Rows.Count, ErrorTable.Rows.Count 
                       ), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (ErrorTable.Rows.Count > 0)
                    {
                        string ReportXls = System.IO.Path.GetDirectoryName(pWorkArgument.DwgSavePath) + string.Format("\\未导入{0}.xls", DateTime.Now.ToString("MMddHHmm"));
                        ExcelClass.ExpReport(ErrorTable, ReportXls);
                        if (MessageBox.Show("已导出失败记录到输出目录，是否查看", "提示",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            System.Diagnostics.Process pExecuteEXE = new System.Diagnostics.Process();
                            pExecuteEXE.StartInfo.FileName = ReportXls;
                            pExecuteEXE.Start();

                        }
                    }
                }
                int n = 0;
                foreach (KeyValuePair<string, DataRow> CoordBH in CoordBHTable)
                {
                    n++;
                    pProgressFrm.SafeCallDisplayText(string.Format("正在导入第: {0}/{1}个多余点", n, CoordBHTable.Count));
                    IPCPoint pPCPoint = new PCPoint();
                    DataRow pRestRow = IsExistSURVEYID(CoordBH.Key);
                    if (pRestRow == null) //库中不存在
                    {
                        pPCPoint.ID = Guid.NewGuid().ToString("N");
                        pPCPoint.SURVEY_ID = CoordBH.Key;
                        pPCPoint.X = CoordBH.Value["CO_X"].ToString();
                        pPCPoint.Y = CoordBH.Value["CO_Y"].ToString();
                        pPCPoint.GROUND_LEVEL = CoordBH.Value["CO_Z"].ToString();
                        pPCPoint.DrawCADObject(AcadDoc);
                        pPCPoint.AddNew();

                    }
                    else
                    {
                        pPCPoint.FillValueByRow(pRestRow);
                       string X = CoordBH.Value["CO_X"].ToString();
                       string Y = CoordBH.Value["CO_Y"].ToString();
                       string Z = CoordBH.Value["CO_Z"].ToString();
                       if (!pPCPoint.X.Equals(X) || !pPCPoint.Y.Equals(Y) || !pPCPoint.GROUND_LEVEL.Equals(Z))
                       {
                           pPCPoint.Update();
                           AcadDoc.ObjectErased -= new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                           pPCPoint.DeleteCADObject(acDoc);
                           AcadDoc.ObjectErased += new _DAcadDocumentEvents_ObjectErasedEventHandler(AcadDoc_ObjectErased);
                           pPCPoint.DrawCADObject(AcadDoc);
                       }
 
                    }
                    pProgressFrm.SafeCallDisplayProgress(n * 100 / CoordBHTable.Count);
                }
                pProgressFrm.SafeCallCloseDialog();
               
                #endregion
            }
            finally
            {

                //pProgressFrm.SafeCallCloseDialog();
 
            }
        }
        private DataRow FindRow(DataTable pTable, string PointID, int StartIndex)
        {
            for (int i = StartIndex; i < pTable.Rows.Count; i++)
            {
                if (pTable.Rows[i]["起点点号"].ToString().Equals(PointID))
                {
                    return pTable.Rows[i];
                }


            }
            return null;
        }
        private void UpdatePipePoint(IPCPoint pPCPoint, DataRow ValueRow)
        {

            pPCPoint.MANHOLE_TYPE = GetValue(ValueRow, "井类型");
            pPCPoint.SEDIMENT_DEPTH = GetValue(ValueRow, "起点管口泥深");
            pPCPoint.FLOW_STATE = GetValue(ValueRow, "水体状态");
            pPCPoint.BOTTOM_TYPE = GetValue(ValueRow, "井底形式");
            pPCPoint.MANHOLE_MATERIAL = GetValue(ValueRow, "井室材质");
            pPCPoint.MANHOLE_SHAPE = GetValue(ValueRow, "井室类型");
            pPCPoint.MANHOLE_SIZE = GetValue(ValueRow, "井室尺寸");
            pPCPoint.COVER_MATERIAL = GetValue(ValueRow, "井盖材质");
            pPCPoint.COVER_STATE = GetValue(ValueRow, "井盖是否损坏");
            //(SPoint as IPCPoint).WATER_QUALITY = GetValue(ValueRow, "水质");
            pPCPoint.Type = GetValue(ValueRow, "起点类型");
            pPCPoint.Depth = GetValue(ValueRow, "起点埋深");

           

            pPCPoint.ROAD_NAME = GetValue(ValueRow, "所在道路");
            pPCPoint.STATE = GetValue(ValueRow, "设施状态");
            pPCPoint.Remark = GetValue(ValueRow, "点备注");
            
 
        }
        private IPCPoint GetPipePoint(string SPointID, DataRow ValueRow, DataRow CoordRow)
        {

            double X = double.Parse(CoordRow["CO_X"].ToString());
            double Y = double.Parse(CoordRow["CO_Y"].ToString());
            double Z = double.Parse(CoordRow["CO_Z"].ToString());


            IPCPoint SPoint = new PCPoint();

            SPoint = new PCPoint();
            (SPoint as IPCPoint).MANHOLE_TYPE = GetValue(ValueRow, "井类型");
            (SPoint as IPCPoint).SEDIMENT_DEPTH = GetValue(ValueRow, "起点管口泥深");
            (SPoint as IPCPoint).FLOW_STATE = GetValue(ValueRow, "水体状态");
            (SPoint as IPCPoint).BOTTOM_TYPE = GetValue(ValueRow, "井底形式");
            (SPoint as IPCPoint).MANHOLE_MATERIAL = GetValue(ValueRow, "井室材质");
            (SPoint as IPCPoint).MANHOLE_SHAPE = GetValue(ValueRow, "井室类型");
            (SPoint as IPCPoint).MANHOLE_SIZE = GetValue(ValueRow, "井室尺寸");
            (SPoint as IPCPoint).COVER_MATERIAL = GetValue(ValueRow, "井盖材质");
            (SPoint as IPCPoint).COVER_STATE = GetValue(ValueRow, "井盖是否损坏");
            //(SPoint as IPCPoint).WATER_QUALITY = GetValue(ValueRow, "水质");
            (SPoint as IPCPoint).Type = GetValue(ValueRow, "起点类型");
            (SPoint as IPCPoint).Depth = GetValue(ValueRow, "起点埋深");

            SPoint.SURVEY_ID = SPointID;

            SPoint.ROAD_NAME = GetValue(ValueRow, "所在道路");
            SPoint.STATE = GetValue(ValueRow, "设施状态");
            SPoint.Remark = GetValue(ValueRow, "点备注");
            //SPoint.INVERT_LEVEL =  Z-;
            SPoint.GROUND_LEVEL = Z.ToString();

            SPoint.ID = Guid.NewGuid().ToString("N");
            SPoint.X = X.ToString();
            SPoint.Y = Y.ToString();


            return SPoint;
        }
        private string GetTrueType(string pType)
        {
            string sql = string.Format("select TableName from tabledict where ClassValue='{0}'", pType);
            DataTable pTable = SysDBUnitiy.SysDataBase.ExecuteQuery(sql).Tables[0];
            if (pTable.Rows.Count == 0)
                return string.Empty;
            else
                return pTable.Rows[0][0].ToString();
        }
        private string GetValue(DataRow ReadRow, string FieldName)
        {
            if (ReadRow.Table.Columns.Contains(FieldName))
            {
                string CurValue = ReadRow[FieldName].ToString();
                if (CurValue.Equals("/"))
                    CurValue = string.Empty;
                return CurValue;
            }
            else
            {
                foreach (DataColumn Column in ReadRow.Table.Columns)
                {
                    if (Column.ColumnName.Contains(FieldName))
                    {
                        string CurValue = ReadRow[Column.ColumnName].ToString();
                        if (CurValue.Equals("/"))
                            CurValue = string.Empty;
                        return CurValue;
                    }
                }
                return string.Empty;
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            SURVEYIDQueryFrm pSURVEYIDQueryFrm = new SURVEYIDQueryFrm();
            if (pSURVEYIDQueryFrm.ShowDialog() == DialogResult.OK)
            {
                string SURVEYID = pSURVEYIDQueryFrm.SURVEYID;

                DataRow PointRow = IsExistSURVEYID(SURVEYID);
                if (PointRow == null)
                {
                    MessageBox.Show(string.Format("数据库中没有点号为{0}的数据", SURVEYID), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //string TableName = PointRow["ClassName"].ToString();
                    //string ItemName = GetItemNameByClassname(TableName);
                    string ID = PointRow["ID"].ToString();
                    pPipeData =new PCPoint();
                    pPipeData.FillValueByRow(PointRow);
                     
                        SwitchControl("点");
                    //pPipeData.FillValueByRow(PointRow, TableName);
                    pPipeDataCtrl.SetData(pPipeData);
                    this.Refresh();
                    pPipeDataCtrl.ZoomTo();
                }
            }
        }

        private void BtnExpReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog pSaveFileDialog = new SaveFileDialog();
            pSaveFileDialog.Filter = "普查记录表|*.xls";
            string sql = "select t2.SURVEY_ID  as 起点号,t3.SURVEY_ID as 终点号,t2.PointType as 起点类型,t2.CO_X as X,t2.CO_Y as Y,t2.GROUND_LEVEL as 高程,t2.Depth as 起点埋深,t3.Depth as 终点埋深,"
                + "t1.WIDTH as 管径,t1.Dirtcion as 流向,t1.MATERIAL as 材质,t2.COVER_MATERIAL as 井盖材质,t2.MANHOLE_MATERIAL as 井室材质,t2.Depth as 井深,t1.WATER_LEVEL as 水深,t1.SEDIMENT_DEPTH as 泥深,"
                + " t1.SEDIMENT_DEPTH as 起点管口泥深,t3.SEDIMENT_DEPTH as 终点管口泥深,t2.BOTTOM_TYPE as 井底形式,t2.COVER_STATE as 井盖是否损坏,t1.Water_State as 水体状态,t1.WATER_QUALITY as 水质,"
                + " t2.MANHOLE_SHAPE as 井室类型,t2.MANHOLE_SIZE as 井室尺寸,t1.STATE as 设施状态,t1.ROAD_NAME as 所在道路,t1.PRESSURE as 管道形式,t2.GULLY_NUMBER as 篦子个数,t2.REMARK as 点备注,"
                + " t1.REMARK as 线备注,t2.PROCESS_UNIT as 检测员,t2.PROCESS_DATE as 检测日期,t2.ACQUISITION_UNIT as 编制员,t2.ACQUISITION_DATE as 编制日期   from ps_pipe t1 left join Points t2 on"
                + " t1.US_OBJECT_ID=t2.ID  left join  Points t3 on t1.DS_OBJECT_ID=t3.ID " + "union all"
                + " select   t.SURVEY_ID,null,t.PointType,t.CO_X,t.CO_Y,t.GROUND_LEVEL,t.Depth,null,null,null,null,t.COVER_MATERIAL,t.MANHOLE_MATERIAL,t.Depth ,null,null,"
                + " t.SEDIMENT_DEPTH,null,t.BOTTOM_TYPE,t.COVER_STATE,null,null,t.MANHOLE_SHAPE,t.MANHOLE_SIZE,null,t.ROAD_NAME,null,t.GULLY_NUMBER,t.REMARK,null,t.PROCESS_UNIT,"
                + " t.PROCESS_DATE,t.ACQUISITION_UNIT,t.ACQUISITION_DATE   from   points t where t.ID not in  (select  t1.US_OBJECT_ID   from  ps_pipe  t1   )";
               
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            if (pSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string TemplateXls = SysDBUnitiy.RootDir + "\\Template\\普查表模板.xls";
                System.Data.DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
                ExcelClass.ExpReport(pTable, pSaveFileDialog.FileName, false, TemplateXls);
                if (MessageBox.Show("已输出到指定路径，是否查看", "提示",
                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    System.Diagnostics.Process pExecuteEXE = new System.Diagnostics.Process();
                    pExecuteEXE.StartInfo.FileName = pSaveFileDialog.FileName;
                    pExecuteEXE.Start();

                }
            }
        }
        
     


       
    }
    class ErrorTable
    {
       public string Name;
       public string TableName;
       public DataTable Table;
       public override string ToString()
       {
           return Name;
       }
    }
}
