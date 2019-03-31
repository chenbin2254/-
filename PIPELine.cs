using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using HR.Data;
using System.Data;
using Autodesk.AutoCAD.Interop.Common;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data.SQLite;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
 

namespace CHXQ.XMManager
{
    /// <summary>
    /// 圆管
    /// </summary>
   public interface IPIPELine : IPipeData
    {
        /// <summary>
        /// 上游点编码
        /// </summary>
        string US_OBJECT_ID { get; set; }
        /// <summary>
        /// 下游点编码
        /// </summary>
        string DS_OBJECT_ID { get; set; }
        /// <summary>
        /// 管径
        /// </summary>
        string Width { get; set; }
        /// <summary>
        /// 管长
        /// </summary>
        string Pipe_Length { get; set; }
        /// <summary>
        /// 上游井底高程(m)
        /// </summary>
        string US_POINT_INVERT_LEVEL { get; set; }
        /// <summary>
        /// 下游井底高程(m)
        /// </summary>
        string DS_POINT_INVERT_LEVEL { get; set; }
        /// <summary>
        /// 上游管底高程（m)
        /// </summary>
        string US_INVERT_LEVEL { get; set; }

        /// <summary>
        /// 下游管底高程（m)
        /// </summary>
        string DS_INVERT_LEVEL { get; set; }
        /// <summary>
        /// 淤积深度
        /// </summary>
        string SEDIMENT_DEPTH { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        string MATERIAL { get; set; }
        /// <summary>
        /// 上游点号
        /// </summary>
        string US_SURVEY_ID { get; set; }

        /// <summary>
        /// 下游点号
        /// </summary>
        string DS_SURVEY_ID { get; set; }

        /// <summary>
        /// 管道形式
        /// </summary>
        string PRESSURE { get; set; }
       
        /// <summary>
        /// 水质状况
        /// </summary>
          string WATER_QUALITY
        {
            get;

            set;
        }
       /// <summary>
       /// 水体状态
       /// </summary>
          string WATER_State
          {
              get;

              set;
          }
        string Lenght { get; set; }
       /// <summary>
       /// 水位
       /// </summary>
        string WATER_LEVEL
        {
            get;

            set;
        }

        string Dirtcion { get; set; }
        bool isExistSURVEY_ID(string SPointID, string EPointID);
    }

   public class PIPELineClass : PipeData, IPIPELine
    {
       public override IPipeData GetDataByID(string ID)
       {
           string sql = string.Format("select * from PS_PIPE where ID='{0}'", ID);
           IDataBase pDataBase = SysDBUnitiy.OleDataBase;
           System.Data.DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
           if (pTable.Rows.Count == 0) return null;
           IPIPELine pPCPoint = new PIPELineClass();
           pPCPoint.FillValueByRow(pTable.Rows[0]);
           return pPCPoint;
       }
        /// <summary>
        /// 水质状况
        /// </summary>
      public string WATER_QUALITY
        {
            get;

            set;
        }
      public override void DeleteCADObject(Autodesk.AutoCAD.ApplicationServices.Document doc)
      {

          string[] Layers = null;
          if (this.US_SURVEY_ID.StartsWith("WS"))
              Layers = new string[] { "WSLine", "WSZJ" };
          else
              Layers = new string[] { "YSLine", "YSZJ" };

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
                     

                  }
              }

          }
          AcadDoc.Save();

      }

        /// <summary>
        /// 水体状态
        /// </summary>
      public string WATER_State
        {
            get;
            set;
        }
       public bool isExistSURVEY_ID(string SPointID, string EPointID)
       {
           string sql = string.Format("select 1 from {0} where US_SURVEY_ID='{1}' and DS_SURVEY_ID='{2}'",ClassName, SPointID, EPointID);
           IDataBase pDataBase = SysDBUnitiy.OleDataBase;
           System.Data.DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
           return pTable.Rows.Count != 0;
       }
       public string Dirtcion { get; set; }
        public override string[] Verification()
        {
          
            List<string> ErrorItems = new List<string>();
            string USPointID = this.US_SURVEY_ID;
            string DsPintID = this.DS_SURVEY_ID;
            if (IsExistID(this.US_SURVEY_ID,this.DS_SURVEY_ID))
            {
                
                    ErrorItems.Add("库中已存在相同起止点编号的数据");
            }
            
           
            return ErrorItems.ToArray(); 
        }
        protected IPCPoint GetPointByID(string ObjectID)
        {
            string sql = string.Format("select * from Points where SURVEY_ID='{0}'", ObjectID);
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            System.Data.DataTable pTable = pDataBase.ExecuteQuery(sql).Tables[0];
            if (pTable.Rows.Count > 0)
            {
                //double X = double.Parse(pTable.Rows[0]["CO_X"].ToString());
                //double Y = double.Parse(pTable.Rows[0]["CO_Y"].ToString());
                IPCPoint pPCPoint = new PCPoint();
                pPCPoint.FillValueByRow(pTable.Rows[0]);
                return pPCPoint;
            }
            else
                return null;
        }


        public override void DrawCADObject(Autodesk.AutoCAD.Interop.AcadDocument AcadDoc)
        {
            string Linetype = "合流";
            int LayerID = GetLayerIndex("YSLine", AcadDoc);
            if (this.US_SURVEY_ID.StartsWith("WS"))
            {
                LayerID = GetLayerIndex("WSLine", AcadDoc);
                Linetype = "污水";
            }
            else if (this.US_SURVEY_ID.StartsWith("YS"))
            {
                Linetype = "雨水";
            }
            AcadDoc.ActiveLayer = AcadDoc.Layers.Item(LayerID);

            IPCPoint SPoint = GetPointByID(this.US_SURVEY_ID);
            IPCPoint EPoint = GetPointByID(this.DS_SURVEY_ID);

            double[] StartPoint = new double[3] {double.Parse( SPoint.X), double.Parse( SPoint.Y), 0 };
            double[] EndPoint = new double[3] { double.Parse( EPoint.X), double.Parse( EPoint.Y), 0 };
            AcadLine pAcadLine = AcadDoc.ModelSpace.AddLine(StartPoint, EndPoint);
            AcadDictionary pAcadDictionary = pAcadLine.GetExtensionDictionary();
            //pAcadDictionary.AddXRecord(ClassName);
            pAcadDictionary.AddXRecord(ID);

            string MinArrowVal = CIni.ReadINI("DrawCAD", "ArrowMin");
            bool IsDrawArrow = false;
            if (string.IsNullOrEmpty(MinArrowVal))
                IsDrawArrow = true;
            else
            {
                double MinArrow = double.Parse(MinArrowVal);
                if (pAcadLine.Length < MinArrow)
                {
                    IsDrawArrow = false;
                }
                else
                    IsDrawArrow = true;
            }
            double[] MidPoint = new double[3] { (double.Parse(SPoint.X) + double.Parse(EPoint.X)) / 2, (double.Parse(SPoint.Y) + double.Parse(EPoint.Y)) / 2, 0 };
            if (IsDrawArrow)
            {
             
                string WidthValue = this.Width;
                AcadMInsertBlock pBlock = AcadDoc.ModelSpace.AddMInsertBlock(MidPoint, "GP4", 1, 1, 1, 0, 1, 1, 1, 1);
                pBlock.Rotate(MidPoint, pAcadLine.Angle);
                pAcadDictionary = pBlock.GetExtensionDictionary();
                pAcadDictionary.AddXRecord(ID);
            }
            string MinLableVal = CIni.ReadINI("DrawCAD", "LableMin");
            if (!string.IsNullOrEmpty(MinLableVal))
            {
                double MinLable = double.Parse(MinLableVal);
                if (pAcadLine.Length < MinLable)
                {
                    return;
                }
            }
            string pUS_INVERT_LEVEL = double.Parse(SPoint.INVERT_LEVEL).ToString("0.000");
            string pDS_INVERT_LEVEL = double.Parse(EPoint.INVERT_LEVEL).ToString("0.000");
            string LineLable = string.Format("{0} {1}m {2}Φ{3} {4}m", Linetype, pUS_INVERT_LEVEL, this.MATERIAL, this.Width, pDS_INVERT_LEVEL);

            LayerID = GetLayerIndex("YSZJ", AcadDoc);
            if (this.US_SURVEY_ID.StartsWith("WS"))
                LayerID = GetLayerIndex("WSZJ", AcadDoc);
            else
                LayerID = GetLayerIndex("YSZJ", AcadDoc);
            AcadDoc.ActiveLayer = AcadDoc.Layers.Item(LayerID);

            AcadText pAcadText = AcadDoc.ModelSpace.AddText(LineLable, MidPoint, 2.0);
            double LineAngle = pAcadLine.Angle;
            if (LineAngle > Math.PI / 2 && LineAngle < 3 * Math.PI / 2)
                LineAngle = LineAngle - Math.PI;
            pAcadText.Rotate(MidPoint, LineAngle);
            pAcadDictionary = pAcadText.GetExtensionDictionary();
            //pAcadDictionary.AddXRecord(ClassName);
            pAcadDictionary.AddXRecord(ID);
            //}

            AcadDoc.Save();
        }
        public PIPELineClass()
        {
            ClassName = "PS_PIPE";
        }
       
 
        /// <summary>
        /// 上游点编码
        /// </summary>
        public string US_OBJECT_ID { get; set; }
        /// <summary>
        /// 下游点编码
        /// </summary>
        public string DS_OBJECT_ID { get; set; }
        /// <summary>
        /// 管径
        /// </summary>
        public string Width { get; set; }
        /// <summary>
        /// 管长
        /// </summary>
        public string Pipe_Length { get; set; }
        /// <summary>
        /// 上游井底高程(m)
        /// </summary>
        public string US_POINT_INVERT_LEVEL { get; set; }
        /// <summary>
        /// 下游井底高程(m)
        /// </summary>
        public string DS_POINT_INVERT_LEVEL { get; set; }
        /// <summary>
        /// 上游管底高程（m)
        /// </summary>
        public string US_INVERT_LEVEL { get; set; }

        /// <summary>
        /// 下游管底高程（m)
        /// </summary>
        public string DS_INVERT_LEVEL { get; set; }
        /// <summary>
        /// 淤积深度
        /// </summary>
        public string SEDIMENT_DEPTH { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        public string MATERIAL { get; set; }
        /// <summary>
        /// 上游点号
        /// </summary>
        public string US_SURVEY_ID { get; set; }

        /// <summary>
        /// 下游点号
        /// </summary>
        public string DS_SURVEY_ID { get; set; }
        /// <summary>
        /// 水位高程
        /// </summary>
        public string WATER_LEVEL
        {
            get;

            set;
        }
        /// <summary>
        /// 管道形式
        /// </summary>
        public string PRESSURE { get; set; }

        public string Lenght { get; set; }
        public override void Update()
        {

            string sql = "update  PS_PIPE  set   US_OBJECT_ID=@US_OBJECT_ID,DS_OBJECT_ID=@DS_OBJECT_ID,SYSTEM_TYPE=@SYSTEM_TYPE,Width=@Width,PIPE_LENGTH=@PIPE_LENGTH,"
              + "US_POINT_INVERT_LEVEL=@US_POINT_INVERT_LEVEL,US_INVERT_LEVEL=@US_INVERT_LEVEL,DS_POINT_INVERT_LEVEL=@DS_POINT_INVERT_LEVEL,DS_INVERT_LEVEL=@DS_INVERT_LEVEL,SEDIMENT_DEPTH=@SEDIMENT_DEPTH,"
              + "MATERIAL=@MATERIAL,STATE=@STATE,PRESSURE=@PRESSURE,ROAD_NAME=@ROAD_NAME,ACQUISITION_DATE=@ACQUISITION_DATE,Remark=@Remark,US_SURVEY_ID=@US_SURVEY_ID,DS_SURVEY_ID=@DS_SURVEY_ID,"
              + "ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_DATE=@PROCESS_DATE,PROCESS_UNIT=@PROCESS_UNIT,SHAPE_Length=@SHAPE_Length,Water_State=@Water_State,WATER_QUALITY=@WATER_QUALITY,WATER_LEVEL=@WATER_LEVEL,Dirtcion=@Dirtcion  where ID=@ID  ";
                
            SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
               
               new SQLiteParameter(){ ParameterName="@US_OBJECT_ID" ,Value=this.US_OBJECT_ID},
               new SQLiteParameter(){ ParameterName="@DS_OBJECT_ID" ,Value=this.DS_OBJECT_ID},
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE},
               new SQLiteParameter(){ ParameterName="@Width" ,Value=string.IsNullOrEmpty( this.Width)?DBNull.Value:(object)int.Parse(this.Width) },
               new SQLiteParameter(){ ParameterName="@PIPE_LENGTH" ,Value=string.IsNullOrEmpty(this.Pipe_Length)?DBNull.Value:(object)double.Parse( this.Pipe_Length)},
               new SQLiteParameter(){ ParameterName="@US_POINT_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.US_POINT_INVERT_LEVEL)?DBNull.Value:(object)double.Parse( this.US_POINT_INVERT_LEVEL)},
               new SQLiteParameter(){ ParameterName="@US_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.US_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.US_INVERT_LEVEL) },
               new SQLiteParameter(){ ParameterName="@DS_POINT_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.DS_POINT_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.DS_POINT_INVERT_LEVEL) },
               new SQLiteParameter(){ ParameterName="@DS_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.DS_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.DS_INVERT_LEVEL) },
              new SQLiteParameter(){ ParameterName="@SEDIMENT_DEPTH" ,Value=string.IsNullOrEmpty(this.SEDIMENT_DEPTH)?DBNull.Value:(object)double.Parse(this.SEDIMENT_DEPTH) },

               new SQLiteParameter(){ ParameterName="@MATERIAL" ,Value=this.MATERIAL},
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE},
              new SQLiteParameter(){ ParameterName="@PRESSURE" ,Value=this.PRESSURE},
              new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME },
              new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE },
              new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark },
              new SQLiteParameter(){ ParameterName="@US_SURVEY_ID" ,Value=this.US_SURVEY_ID },
              new SQLiteParameter(){ ParameterName="@DS_SURVEY_ID" ,Value=this.DS_SURVEY_ID },
              new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT },
              new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date },
              new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit },
              new SQLiteParameter(){ ParameterName="@SHAPE_Length" ,Value=string.IsNullOrEmpty(this.Lenght)?DBNull.Value:(object)double.Parse(this.Lenght) },
               new SQLiteParameter(){ ParameterName="@Water_State" ,Value=this.WATER_State },
                 new SQLiteParameter(){ ParameterName="@WATER_QUALITY" ,Value=this.WATER_QUALITY },
              new SQLiteParameter(){ ParameterName="@WATER_LEVEL" ,Value=string.IsNullOrEmpty(this.WATER_LEVEL)?DBNull.Value:(object)double.Parse(this.WATER_LEVEL)  },
               new SQLiteParameter(){ ParameterName="@Dirtcion" ,Value=this.Dirtcion },
              //new SQLiteParameter(){ ParameterName="@OBJECTID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer }
               new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID}

           };
             IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            pDataBase.OpenConnection();
            try
            {
                pDataBase.ExecuteNonQuery(sql, Parameters);
             
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
      /* public override void Delete()
        {

            string sql = string.Format("update {0} set IsDelete=0 where ID='{1}'", this.ClassName, this.ID);
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


        }*/
        public override void AddNew()
        {
            string sql = "insert into PS_PIPE(ID,US_OBJECT_ID,DS_OBJECT_ID,SYSTEM_TYPE,Width,PIPE_LENGTH,"
                + "US_POINT_INVERT_LEVEL,US_INVERT_LEVEL,DS_POINT_INVERT_LEVEL,DS_INVERT_LEVEL,SEDIMENT_DEPTH,"
                + "MATERIAL,STATE,PRESSURE,ROAD_NAME,ACQUISITION_DATE,Remark,US_SURVEY_ID,DS_SURVEY_ID,"
                + "ACQUISITION_UNIT,PROCESS_DATE,PROCESS_UNIT,SHAPE_Length,Water_State,WATER_QUALITY,WATER_LEVEL,Dirtcion) "
                 +" values(@ID,@US_OBJECT_ID,@DS_OBJECT_ID,@SYSTEM_TYPE,@Width,@PIPE_LENGTH,"
                  + "@US_POINT_INVERT_LEVEL,@US_INVERT_LEVEL,@DS_POINT_INVERT_LEVEL,@DS_INVERT_LEVEL,@SEDIMENT_DEPTH,"
                  + "@MATERIAL,@STATE,@PRESSURE,@ROAD_NAME,@ACQUISITION_DATE,@Remark,@US_SURVEY_ID,@DS_SURVEY_ID,"
                  + "@ACQUISITION_UNIT,@PROCESS_DATE,@PROCESS_UNIT,@SHAPE_Length,@Water_State,@WATER_QUALITY,@WATER_LEVEL,@Dirtcion)";
            SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
                new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID},
               new SQLiteParameter(){ ParameterName="@US_OBJECT_ID" ,Value=this.US_OBJECT_ID},
               new SQLiteParameter(){ ParameterName="@DS_OBJECT_ID" ,Value=this.DS_OBJECT_ID},
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE},
               new SQLiteParameter(){ ParameterName="@Width" ,Value=this.Width },
               new SQLiteParameter(){ ParameterName="@PIPE_LENGTH" ,Value=string.IsNullOrEmpty(this.Pipe_Length)?DBNull.Value:(object)double.Parse( this.Pipe_Length)},
               new SQLiteParameter(){ ParameterName="@US_POINT_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.US_POINT_INVERT_LEVEL)?DBNull.Value:(object)double.Parse( this.US_POINT_INVERT_LEVEL)},
               new SQLiteParameter(){ ParameterName="@US_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.US_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.US_INVERT_LEVEL) },
               new SQLiteParameter(){ ParameterName="@DS_POINT_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.DS_POINT_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.DS_POINT_INVERT_LEVEL) },
               new SQLiteParameter(){ ParameterName="@DS_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.DS_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.DS_INVERT_LEVEL) },
                new SQLiteParameter(){ ParameterName="@SEDIMENT_DEPTH" ,Value=string.IsNullOrEmpty(this.SEDIMENT_DEPTH)?DBNull.Value:(object)double.Parse(this.SEDIMENT_DEPTH) },

               new SQLiteParameter(){ ParameterName="@MATERIAL" ,Value=this.MATERIAL},
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE},
                new SQLiteParameter(){ ParameterName="@PRESSURE" ,Value=this.PRESSURE},
                 new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME },
                   new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE },
                     new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark },
                       new SQLiteParameter(){ ParameterName="@US_SURVEY_ID" ,Value=this.US_SURVEY_ID },
                            new SQLiteParameter(){ ParameterName="@DS_SURVEY_ID" ,Value=this.DS_SURVEY_ID },
                            new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT },
                               new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date },
                               new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit },
                               new SQLiteParameter(){ ParameterName="@SHAPE_Length" ,Value=string.IsNullOrEmpty(this.Lenght)?DBNull.Value:(object)double.Parse(this.Lenght) },
                                 new SQLiteParameter(){ ParameterName="@Water_State" ,Value=this.WATER_State },
                 new SQLiteParameter(){ ParameterName="@WATER_QUALITY" ,Value=this.WATER_QUALITY },
              new SQLiteParameter(){ ParameterName="@WATER_LEVEL" ,Value=string.IsNullOrEmpty(this.WATER_LEVEL)?DBNull.Value:(object)double.Parse(this.WATER_LEVEL)  },
               new SQLiteParameter(){ ParameterName="@Dirtcion" ,Value=this.Dirtcion } 
           };
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            pDataBase.OpenConnection();
            try
            {
                pDataBase.ExecuteNonQuery(sql, Parameters);
                //this.CurClassName = "PS_PIPE";
                //sql = "select max(objectid) from PS_PIPE";
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
        public override void FillValueByRow(System.Data.DataRow pDataRow )
        {
            base.FillValueByRow(pDataRow);
            if (pDataRow.Table.Columns.Contains("US_OBJECT_ID"))
                this.US_OBJECT_ID = pDataRow["US_OBJECT_ID"].ToString();

            if (pDataRow.Table.Columns.Contains("DS_OBJECT_ID"))
                this.DS_OBJECT_ID = pDataRow["DS_OBJECT_ID"].ToString();

            if (pDataRow.Table.Columns.Contains("Width"))
                this.Width = pDataRow["Width"].ToString();

            if (pDataRow.Table.Columns.Contains("Pipe_Length"))
                this.Pipe_Length = pDataRow["Pipe_Length"].ToString();

            if (pDataRow.Table.Columns.Contains("US_POINT_INVERT_LEVEL"))
                this.US_POINT_INVERT_LEVEL = pDataRow["US_POINT_INVERT_LEVEL"].ToString();

            if (pDataRow.Table.Columns.Contains("DS_POINT_INVERT_LEVEL"))
                this.DS_POINT_INVERT_LEVEL = pDataRow["DS_POINT_INVERT_LEVEL"].ToString();

            if (pDataRow.Table.Columns.Contains("US_INVERT_LEVEL"))
                this.US_INVERT_LEVEL = pDataRow["US_INVERT_LEVEL"].ToString();

            if (pDataRow.Table.Columns.Contains("DS_INVERT_LEVEL"))
                this.DS_INVERT_LEVEL = pDataRow["DS_INVERT_LEVEL"].ToString();

            if (pDataRow.Table.Columns.Contains("SEDIMENT_DEPTH"))
                this.SEDIMENT_DEPTH = pDataRow["SEDIMENT_DEPTH"].ToString();

            if (pDataRow.Table.Columns.Contains("MATERIAL"))
                this.MATERIAL = pDataRow["MATERIAL"].ToString();

            if (pDataRow.Table.Columns.Contains("US_SURVEY_ID"))
                this.US_SURVEY_ID = pDataRow["US_SURVEY_ID"].ToString();

            if (pDataRow.Table.Columns.Contains("DS_SURVEY_ID"))
                this.DS_SURVEY_ID = pDataRow["DS_SURVEY_ID"].ToString();

            if (pDataRow.Table.Columns.Contains("PRESSURE"))
                this.PRESSURE = pDataRow["PRESSURE"].ToString();

            if(pDataRow.Table.Columns.Contains("SHAPE_Length") )
                this.Lenght = pDataRow["SHAPE_Length"].ToString();

            if (pDataRow.Table.Columns.Contains("WATER_LEVEL"))
                this.WATER_LEVEL = pDataRow["WATER_LEVEL"].ToString();
            if (pDataRow.Table.Columns.Contains("Dirtcion"))
                this.Dirtcion = pDataRow["Dirtcion"].ToString();
            if (pDataRow.Table.Columns.Contains("Water_State"))
                this.WATER_State = pDataRow["Water_State"].ToString();
            if (pDataRow.Table.Columns.Contains("WATER_QUALITY"))
                this.WATER_QUALITY = pDataRow["WATER_QUALITY"].ToString();
          
        }
    }
 

}
