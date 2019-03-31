using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HR.Data;
using System.Data.OleDb;
using System.Data;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop;
using System.Windows.Forms;
using System.Data.SQLite;
using aApp = Autodesk.AutoCAD.ApplicationServices.Application;
namespace CHXQ.XMManager
{

    public interface IPipeData
    {
        //int ObjectID { get; set; }
        string ID { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        string SYSTEM_TYPE
        {
            get;

            set;
        }

        string ROAD_NAME
        {
            get;

            set;
        }

        string ACQUISITION_DATE
        {
            get;

            set;
        }

        string ACQUISITION_UNIT
        {
            get;

            set;
        }

        string PROCESS_Unit
        {
            get;

            set;
        }

        string PROCESS_Date
        {
            get;

            set;
        }
        string STATE
        {
            get;

            set;
        }
        string Remark
        {
            get;

            set;
        }
        void FillValueByRow(System.Data.DataRow pDataRow);

        void Update();
        void AddNew();
        string[] Verification();
        void Delete();
        void DrawCADObject(Autodesk.AutoCAD.Interop.AcadDocument AcadDoc);
        void DeleteCADObject(Autodesk.AutoCAD.ApplicationServices.Document doc);
        //string CurClass{get;}
        string GetNewID();
        string GetHead();
        string GetNextNO();
        bool IsExistSURVEYID(string SURVEYID);
        //bool IsDelete{get;set;}
        IPipeData GetDataByID(string ID);
    }
    public abstract class PipeData : IPipeData
    {
        //public bool IsDelete { get; set; }
        //public int ObjectID { get; set; }
        public string ID { get; set; }
        public string SYSTEM_TYPE { get; set; }
        public abstract IPipeData GetDataByID(string ID);
        public string ROAD_NAME { get; set; }
        private string m_ACQUISITION_DATE = string.Empty;
        public string ACQUISITION_DATE { get { return m_ACQUISITION_DATE; } set { m_ACQUISITION_DATE = value; } }
        private string m_ACQUISITION_UNIT = string.Empty;
        public string ACQUISITION_UNIT { get { return m_ACQUISITION_UNIT; } set { m_ACQUISITION_UNIT = value; } }
        private string m_PROCESS_Unit = string.Empty;
        public string PROCESS_Unit { get { return m_PROCESS_Unit; } set { m_PROCESS_Unit = value; } }
        private string m_PROCESS_Date = string.Empty;
        public string PROCESS_Date { get { return m_PROCESS_Date; } set { m_PROCESS_Date = value; } }

        public string Remark { get; set; }
        public string STATE { get; set; }
      //  public abstract Point GetPoint();
        //public string CurClass 
        //{
        //    get { return CurClassName; }
        //}
        public bool IsExistSURVEYID(string SURVEYID)
        {
            string sql = string.Format("select  * from Points where SURVEY_ID='{0}'", SURVEYID);
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            System.Data.DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
            return !(pTable.Rows.Count == 0);
            

        }
        //protected string CurClassName;
        public string GetHead()
        {
            string sql = string.Format("select Head from TableConfig where TableClassName='{0}'", ClassName);
            System.Data.DataTable ptable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
            return ptable.Rows[0][0].ToString();
        }
        public virtual void DeleteCADObject(Autodesk.AutoCAD.ApplicationServices.Document doc)
        {

          /*  string[] Layers = null;
            if (this is IPCPoint)
            {

                if ((this as IPCPoint).SURVEY_ID.StartsWith("WS"))
                    Layers = new string[] { "0", "WSPoint", "WSText" };
                else
                    Layers = new string[] { "0", "YSPoint", "YSText" };

            }
            else
            {
                if ((this as IPIPELine).US_SURVEY_ID.StartsWith("WS"))
                    Layers = new string[] { "WSLine", "WSZJ" };
                else
                    Layers = new string[] { "YSLine", "YSZJ" };

            }
            //Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.Interop.AcadDocument AcadDoc = doc.AcadDocument as Autodesk.AutoCAD.Interop.AcadDocument;
            
            Editor ed = doc.Editor;
            int count = 0;
            foreach (string Layer in Layers)
            {
                TypedValue[] tvs = new TypedValue[] 

            {

                new TypedValue((int)DxfCode.LayerName,Layer)

            };



                SelectionFilter sf = new SelectionFilter(tvs);

                PromptSelectionResult psr = ed.SelectAll(sf);

                if (psr.Status == PromptStatus.OK)
                {

                    SelectionSet SS = psr.Value;

                    ObjectId[] idArray = SS.GetObjectIds();

                    for (int i = 0; i < idArray.Length; i++)
                    {

                        if (this is IPipePoint && count == 2)
                        {
                            break;
                        }
                        else if (this is IPIPELine && count == 3)
                            break;
                        //Entity ent = (Entity)Tools.GetDBObject(idArray[i]);
                        AcadEntity pAcadEntity = AcadDoc.ObjectIdToObject(idArray[i].OldIdPtr.ToInt64()) as AcadEntity;

                        string ObjectID = GetPointObjectID(pAcadEntity);
                        if (ObjectID.Equals(this.ID))
                        {
                           
                            pAcadEntity.Delete();
                            count++;
                        }
                        //ent.Highlight();

                        //Tools.WriteMessage(i + ":" + ent.ObjectId.ToString() + "," + ent.GetType().Name);

                    }
                }

            }
            AcadDoc.Save();
            */

        }




        
        /*
        public void DeleteCADObject()
        {
          
                string[] Layers = null;
                if (this is IPipePoint)
                {

                    if ((this as IPipePoint).SURVEY_ID.StartsWith("WS"))
                        Layers = new string[] {"0", "WSPoint", "WSText" };
                    else
                        Layers = new string[] {"0", "YSPoint", "YSText" };

                }
                else
                {
                    if ((this as IPIPELine).US_SURVEY_ID.StartsWith("WS"))
                        Layers = new string[] { "WSLine", "WSZJ" };
                    else
                        Layers = new string[] { "YSLine", "YSZJ" };

                }
               
                DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument();
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                Database acCurDb = acDoc.Database;
               
                Entity entity = null;
             
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    int count = 0;
                    LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead) as LayerTable;
                    foreach (string Layer in Layers)
                    {
                        LayerTableRecord acLyrTblRec = new LayerTableRecord();
                        TypedValue[] glq = new TypedValue[]
                {
                    new TypedValue((int)DxfCode.LayerName,Layer)
                };
                        SelectionFilter sf = new SelectionFilter(glq);
                        PromptSelectionResult SS = acDoc.Editor.SelectAll(sf);
                        Autodesk.AutoCAD.EditorInput.SelectionSet SSet = SS.Value;
                        if (SSet != null)
                            foreach (ObjectId id in SSet.GetObjectIds())
                            {

                                AcadObject Adobj = null;
                                entity = (Entity)acTrans.GetObject(id, OpenMode.ForWrite, true);
                                DBObject obj = (DBObject)entity;
                                Adobj = (AcadObject)obj.AcadObject;
                                string ObjectID = GetPointObjectID(Adobj);
                                if (ObjectID.Equals(this.ID))
                                {
                                    if (this is IPipePoint && count == 2)
                                    {
                                        break;

                                    }
                                    else if (this is IPIPELine && count == 3)
                                        break;
                                    Adobj.Delete();
                                    count++;
                                }
                            }
                        if (this is IPipePoint && count == 2)
                        {
                            break;
                        }
                        else if (this is IPIPELine && count == 3)
                            break;
                    }
                    docLock.Dispose();
                    acTrans.Commit();
                }
                (acDoc.AcadDocument as AcadDocument).Save();
          
 
        }*/
        public virtual void Delete()
        {

            string sql = string.Format("delete from {0}   where ID='{1}'", this.ClassName, this.ID);
                IDataBase pDataBase = SysDBUnitiy.OleDataBase;
                pDataBase.OpenConnection();
                try
                {
                    pDataBase.ExecuteNonQuery(sql);
                }
                finally
                {
                    pDataBase.CloseConnection();
                }        
           

        }
        protected string GetPointObjectID(AcadEntity pAcadObject)
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
        public virtual void FillValueByRow(System.Data.DataRow pDataRow )
        {
            //CurClassName = Tablename;
            //if (pDataRow.Table.Columns.Contains("OBJECTID"))
            //    this.ObjectID = int.Parse(pDataRow["OBJECTID"].ToString());

            if (pDataRow.Table.Columns.Contains("ID"))
                this.ID = pDataRow["ID"].ToString();

            //if (pDataRow.Table.Columns.Contains("SYSTEM_TYPE"))
            //    this.SYSTEM_TYPE = pDataRow["SYSTEM_TYPE"].ToString();

            if (pDataRow.Table.Columns.Contains("ROAD_NAME"))
                this.ROAD_NAME = pDataRow["ROAD_NAME"].ToString();

            if (pDataRow.Table.Columns.Contains("ACQUISITION_DATE"))
                this.ACQUISITION_DATE = pDataRow["ACQUISITION_DATE"].ToString();

            if (pDataRow.Table.Columns.Contains("ACQUISITION_UNIT"))
                this.ACQUISITION_UNIT = pDataRow["ACQUISITION_UNIT"].ToString();

            if (pDataRow.Table.Columns.Contains("PROCESS_UNIT"))
                this.PROCESS_Unit = pDataRow["PROCESS_UNIT"].ToString();

            if (pDataRow.Table.Columns.Contains("PROCESS_DATE"))
                this.PROCESS_Date = pDataRow["PROCESS_DATE"].ToString();
            if (pDataRow.Table.Columns.Contains("STATE"))
                this.STATE = pDataRow["STATE"].ToString();
            if (pDataRow.Table.Columns.Contains("REMARK"))
                this.Remark = pDataRow["REMARK"].ToString();

            //if(pDataRow.Table.Columns.Contains("IsDelete") )
            //    this.IsDelete = pDataRow["IsDelete"].ToString().Equals("1");
        }
        public abstract void Update();
         
        public abstract void AddNew();
        public abstract string[] Verification();
        protected string ClassName;
        protected int GetLayerIndex(string LayerName, Autodesk.AutoCAD.Interop.AcadDocument AcadDoc)
        {
            for (int i = 0; i < AcadDoc.Layers.Count; i++)
            {
                string pLayerName = AcadDoc.Layers.Item(i).Name;
                if (pLayerName.Equals(LayerName, StringComparison.InvariantCultureIgnoreCase))
                    return i;

            }
            return -1;
        }
        public string GetNextNO()
        {
            string sql = "select  max(cast( substr(id,7,5) as int))+1  as num    from " + ClassName;
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;

            //string No = pDataBase.ExecuteScalar(sql).ToString().PadLeft(5, '0');
            System.Data.DataTable pTab = pDataBase.ExecuteQuery(sql).Tables[0];
            string Curvalue = pDataBase.ExecuteQuery(sql).Tables[0].Rows[0][0].ToString();
            if (pTab.Rows.Count == 0)
                Curvalue = "1";
            return Curvalue.PadLeft(5, '0');
 
        }
        public string GetNewID()
        {

           // string sql = "select  (max( cint( mid(id,6)))+1) as num   from " + ClassName;
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            string No = GetNextNO();
            string Head =string.Empty;
            if (string.IsNullOrEmpty(this.ID))
            {
                string sql = "select   top 1 substr(ID,1,5) as head  from  " + ClassName;
              System.Data.DataTable pTab = pDataBase.ExecuteQuery(sql).Tables[0];
                Head = pTab.Rows[0][0].ToString();
            }
            else
            {
                Head = this.ID.Substring(0, 5);
            }
            return Head + No;
        }

        public abstract void DrawCADObject(Autodesk.AutoCAD.Interop.AcadDocument AcadDoc);
       
        protected bool IsExistID(string UsID,string DsID)          
        {
            string sql = string.Format("select 1 from PS_PIPE where US_SURVEY_ID='{0}' and DS_SURVEY_ID='{1}'", UsID, DsID);
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            System.Data.DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
            return pTable.Rows.Count != 0;
        }

    }
    public interface IPipePoint : IPipeData
    {
        string SURVEY_ID { get; set; }
        string X { get; set; }

        string Y { get; set; }
        /// <summary>
        /// 流向
        /// </summary>
        string Dirction { get; set; }
        /// <summary>
        /// 井盖高程
        /// </summary>
        string GROUND_LEVEL { get; set; }

        /// <summary>
        /// 井底高程
        /// </summary>
        string INVERT_LEVEL { get; set; }
        Point GetPoint();
        IPIPELine[] GetConnLines();
    }

    public class PipePoint : PipeData,IPipePoint
    {
        public override IPipeData GetDataByID(string ID)
        {
            string sql = string.Format("select * from Points where ID='{0}'", ID);
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            System.Data.DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
            if (pTable.Rows.Count == 0) return null;
            IPCPoint pPCPoint = new PCPoint();
            pPCPoint.FillValueByRow(pTable.Rows[0]);
            return pPCPoint;
        }
       public PipePoint() 
       {
           ClassName = "PS_VIRTUAL_POINT";
       }
       /// <summary>
       /// 流向
       /// </summary>
     public  string Dirction { get; set; }
       public string SURVEY_ID
       {
           get;

           set;
       }
      
       
       public string X { get; set; }

       public string Y { get; set; }
       public IPIPELine[] GetConnLines()
       {
           string sql = string.Format("select  *  from  ps_pipe t  where  t.US_OBJECT_ID='{0}' or t.DS_OBJECT_ID='{0}'", this.ID);
           IDataBase pDataBase = SysDBUnitiy.OleDataBase;
           System.Data.DataTable ptable = pDataBase.ExecuteQuery(sql).Tables[0];

            IPIPELine[] pLines=new IPIPELine[ptable.Rows.Count];
            for (int i = 0; i < pLines.Length; i++)
            {
                IPIPELine pPIPELine = new PIPELineClass();
                pPIPELine.FillValueByRow(ptable.Rows[i]);
                pLines[i] = pPIPELine;
            }
            return pLines;
       }
       /// <summary>
       /// 井底高程
       /// </summary>
       public string INVERT_LEVEL { get; set; }

       public string GROUND_LEVEL { get; set; }

       private bool VerificationXY()
       {
           double MinX, MinY, MaxX, MaxY;
           //string[] MinPoint = System.Configuration.ConfigurationSettings.AppSettings["MinPoint"].Split(',');

           MinX = double.Parse(CIni.ReadINI("XYExtent", "MinX"));
           MinY = double.Parse(CIni.ReadINI("XYExtent", "MinY"));

           //string[] MaxPoint = System.Configuration.ConfigurationSettings.AppSettings["MaxPoint"].Split(',');
           MaxX = double.Parse(CIni.ReadINI("XYExtent", "MaxX"));
           MaxY = double.Parse(CIni.ReadINI("XYExtent", "MaxY"));

           double C_X = double.Parse(this.X);
           double C_Y = double.Parse(this.Y);
           if (C_X < MinX || C_X > MaxX || C_Y < MinY || C_Y > MaxY)
           {
               return false;

           }
           return true;
       }
       public  Point GetPoint()
       {
           double pX, pY;
           if (!double.TryParse(this.X, out pX))
               return new Point();

           if (!double.TryParse(this.Y, out pY))
               return new Point();
           return new Point() { X = (int)pX, Y = (int)pY };
       }

       public override void DrawCADObject(Autodesk.AutoCAD.Interop.AcadDocument AcadDoc)
       {
        
           /*
           double pX = double.Parse(this.X);
           double pY = double.Parse(this.Y);
           double[] InsertPoint = new double[] { pX, pY, 0 };

           string BlockName = "一般管线点";
                   
        
           if (this is ICOMB)
               BlockName = "雨篦";
           else if (this is IMANHOLE)
               BlockName = "排水检修井";
           else if (this is IOUTFALL)
               BlockName = "出水口";
           else
               BlockName = "一般管线点";


           int LayerID = GetLayerIndex("YSPoint", AcadDoc);
           if (this.SYSTEM_TYPE == "污水")
               LayerID = GetLayerIndex("WSPoint", AcadDoc);
           AcadDoc.ActiveLayer = AcadDoc.Layers.Item(LayerID);

           AcadMInsertBlock pAcadMInsertBlock = AcadDoc.ModelSpace.AddMInsertBlock(InsertPoint, BlockName, 1, 1, 1, 0, 1, 1, 1, 1);
           AcadDictionary pAcadDictionary = pAcadMInsertBlock.GetExtensionDictionary();
           //pAcadDictionary.AddXRecord(ClassName);
           pAcadDictionary.AddXRecord(this.ID);

           if (this.SYSTEM_TYPE == "污水")
               LayerID = GetLayerIndex("WSText", AcadDoc);
           else
           LayerID = GetLayerIndex("YSText", AcadDoc);
           AcadDoc.ActiveLayer = AcadDoc.Layers.Item(LayerID);

           AcadText pAcadText = AcadDoc.ModelSpace.AddText(this.SURVEY_ID, InsertPoint, 2.0);
           pAcadDictionary = pAcadText.GetExtensionDictionary();
           //pAcadDictionary.AddXRecord(ClassName);
           pAcadDictionary.AddXRecord(this.ID);
           AcadDoc.Save();*/
       }
       public override string[] Verification()
       {
           List<string> ErrorItems = new List<string>();
           if (string.IsNullOrEmpty(this.SURVEY_ID))
           {
               ErrorItems.Add("物探点号不能为空");
           }
            
           if (string.IsNullOrEmpty(this.X))
           {
               ErrorItems.Add("X坐标为空");
 
           }
           if (string.IsNullOrEmpty(this.Y))
           {
               ErrorItems.Add("Y坐标为空");

           }
           if (!string.IsNullOrEmpty(this.X) && !string.IsNullOrEmpty(this.Y))
           {
               if (!VerificationXY())
               {
                   ErrorItems.Add("坐标超出范围");
 
               }
           }
           return ErrorItems.ToArray();
       }

       public override void FillValueByRow(System.Data.DataRow pDataRow)
       {

           base.FillValueByRow(pDataRow);
           if (pDataRow.Table.Columns.Contains("GROUND_LEVEL"))
               this.GROUND_LEVEL = pDataRow["GROUND_LEVEL"].ToString();
           if(pDataRow.Table.Columns.Contains("CO_X"))
               this.X = pDataRow["CO_X"].ToString();

           if (pDataRow.Table.Columns.Contains("CO_Y"))
               this.Y = pDataRow["CO_Y"].ToString();

           if (pDataRow.Table.Columns.Contains("SURVEY_ID"))
               this.SURVEY_ID = pDataRow["SURVEY_ID"].ToString();

           if (pDataRow.Table.Columns.Contains("INVERT_LEVEL"))
               this.INVERT_LEVEL = pDataRow["INVERT_LEVEL"].ToString();
       }
       public override void Update()
       {

           SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE},
               new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME},
               new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE},
               new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT},
               new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit},
               new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date},
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE},
               new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark},
               new SQLiteParameter(){ ParameterName="@GROUND_LEVEL" ,Value=this.GROUND_LEVEL},
                new SQLiteParameter(){ ParameterName="@CO_X" ,Value=this.X},
                new SQLiteParameter(){ ParameterName="@CO_Y" ,Value=this.Y},
                 new SQLiteParameter(){ ParameterName="@SURVEY_ID" ,Value=this.SURVEY_ID },
                   new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=this.INVERT_LEVEL },
               //new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID},
                   new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID}
               
           };
           string sql = "update PS_VIRTUAL_POINT set SYSTEM_TYPE=@SYSTEM_TYPE,ROAD_NAME=@ROAD_NAME,"
               + "ACQUISITION_DATE=@ACQUISITION_DATE,ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_UNIT=@PROCESS_UNIT,"
                + "PROCESS_DATE=@PROCESS_DATE,STATE=@STATE,Remark=@Remark,"
                + "GROUND_LEVEL=@GROUND_LEVEL,CO_X=@CO_X,CO_Y=@CO_Y,SURVEY_ID=@SURVEY_ID,INVERT_LEVEL=@INVERT_LEVEL"
                + " where  ID=@ID ";
                
           IDataBase pDataBase = SysDBUnitiy.OleDataBase;
           pDataBase.OpenConnection();
           try
           {
               pDataBase.ExecuteNonQuery(sql, Parameters);

           }
           finally
           {
               pDataBase.CloseConnection();

           }
       }
       public override void AddNew()
       {
           SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
               new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID},
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE},
               new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME},
               new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE},
               new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT},
               new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit},
               new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date},
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE},
               new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark},
               new SQLiteParameter(){ ParameterName="@GROUND_LEVEL" ,Value=string.IsNullOrEmpty( this.GROUND_LEVEL)?DBNull.Value:(object)double.Parse(this.GROUND_LEVEL)},
               new SQLiteParameter(){ ParameterName="@CO_X" ,Value=this.X},
               new SQLiteParameter(){ ParameterName="@CO_Y" ,Value=this.Y},
               new SQLiteParameter(){ ParameterName="@SURVEY_ID" ,Value=this.SURVEY_ID },
               new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty( this.INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.INVERT_LEVEL) }
                
             //  new SQLiteParameter(){ ParameterName="@ObjectID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer}
               
           };
           string sql = "insert into PS_VIRTUAL_POINT(ID,SYSTEM_TYPE,ROAD_NAME,AQCUISITION_DATE,ACQUISITION_UNIT"
               + ",PROCESS_UNIT,PROCESS_DATE,STATE,Remark,GROUND_LEVEL,CO_X,CO_Y,SURVEY_ID,INVERT_LEVEL) values(@ID, "
               + " @SYSTEM_TYPE,@ROAD_NAME,@ACQUISITION_DATE,@ACQUISITION_UNIT,@PROCESS_UNIT,@PROCESS_DATE,@STATE,"
               + " @Remark,@GROUND_LEVEL,@CO_X,@CO_Y,@SURVEY_ID,@INVERT_LEVEL)";
           IDataBase pDataBase = SysDBUnitiy.OleDataBase;
           pDataBase.OpenConnection();
           try
           {
               pDataBase.ExecuteNonQuery(sql, Parameters);
               sql = string.Format("insert into points (ID,CO_X,CO_Y,ClassName,SURVEY_ID) values('{0}',{1},{2},'{3}','{4}')",
           ID, this.X, this.Y, this.ClassName, this.SURVEY_ID);
               pDataBase.ExecuteNonQuery(sql);
               //sql = "select max(objectid) from PS_VIRTUAL_POINT";
               //this.ObjectID = int.Parse(pDataBase.ExecuteScalar(sql).ToString());
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               pDataBase.CloseConnection();

           }
       }
     
    }
}
