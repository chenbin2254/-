using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using HR.Data;
using System.Data.SQLite;

namespace CHXQ.XMManager
{
  public  interface IMANHOLE : IPipePoint
    {
      
        /// <summary>
        /// 检查井类型
        /// </summary>
        string MANHOLE_TYPE { get; set; }
        /// <summary>
        /// 淤积深度
        /// </summary>
        string SEDIMENT_DEPTH { get; set; }
        /// <summary>
        /// 水流状态
        /// </summary>
        string FLOW_STATE { get; set; }
        /// <summary>
        /// 井底形式
        /// </summary>
        string BOTTOM_TYPE { get; set; }
        /// <summary>
        /// 井体材质
        /// </summary>
        string MANHOLE_MATERIAL { get; set; }
        /// <summary>
        /// 井体形式
        /// </summary>
        string MANHOLE_SHAPE { get; set; }

        string MANHOLE_SIZE { get; set; }
        /// <summary>
        /// 井盖材质
        /// </summary>
        string COVER_MATERIAL { get; set; }
        /// <summary>
        /// 井盖状况
        /// </summary>
        string COVER_STATE { get; set; }
        /// <summary>
        /// 水质状况
        /// </summary>
        string WATER_QUALITY { get; set; }
        /// <summary>
        /// 水位高程
        /// </summary>
        string WATER_LEVEL { get; set; }
    }
  public  class MANHOLE :PipePoint, IMANHOLE
    {
        public MANHOLE()
        {
            ClassName = "PS_MANHOLE";
        }

        /// <summary>
        /// 检查井类型
        /// </summary>
        public string MANHOLE_TYPE
        {
            get;

            set;
        }
        /// <summary>
        /// 淤积深度
        /// </summary>
        public string SEDIMENT_DEPTH
        {
            get;

            set;
        }
        /// <summary>
        /// 水流状态
        /// </summary>
        public string FLOW_STATE
        {
            get;

            set;
        }
        /// <summary>
        /// 井底形式
        /// </summary>
        public string BOTTOM_TYPE
        {
            get;

            set;
        }
        /// <summary>
        /// 井体材质
        /// </summary>
        public string MANHOLE_MATERIAL
        {
            get;

            set;
        }
        /// <summary>
        /// 井体形式
        /// </summary>
        public string MANHOLE_SHAPE
        {
            get;

            set;
        }
        public string MANHOLE_SIZE
        {
            get;

            set;
        }
        /// <summary>
        /// 井盖材质
        /// </summary>
        public string COVER_MATERIAL
        {
            get;

            set;
        }
        /// <summary>
        /// 井盖状况
        /// </summary>
        public string COVER_STATE
        {
            get;

            set;
        }
        /// <summary>
        /// 水质状况
        /// </summary>
        public string WATER_QUALITY
        {
            get;

            set;
        }
        /// <summary>
        /// 水位高程
        /// </summary>
        public string WATER_LEVEL
        {
            get;

            set;
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

               new SQLiteParameter(){ ParameterName="@GROUND_LEVEL" ,Value=string.IsNullOrEmpty( this.GROUND_LEVEL)?DBNull.Value:(object) this.GROUND_LEVEL},
               new SQLiteParameter(){ ParameterName="@CO_X" ,Value=this.X},
               new SQLiteParameter(){ ParameterName="@CO_Y" ,Value=this.Y},
               new SQLiteParameter(){ ParameterName="@SURVEY_ID" ,Value=this.SURVEY_ID },
               new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.INVERT_LEVEL)?DBNull.Value: (object) this.INVERT_LEVEL },
               new SQLiteParameter(){ ParameterName="@MANHOLE_TYPE" ,Value=this.MANHOLE_TYPE },
               new SQLiteParameter(){ ParameterName="@SEDIMENT_DEPTH" ,Value=string.IsNullOrEmpty( this.SEDIMENT_DEPTH)?DBNull.Value:(object) this.SEDIMENT_DEPTH },
               new SQLiteParameter(){ ParameterName="@FLOW_STATE" ,Value=this.FLOW_STATE },
               new SQLiteParameter(){ ParameterName="@BOTTOM_TYPE" ,Value=this.BOTTOM_TYPE },
               new SQLiteParameter(){ ParameterName="@MANHOLE_MATERIAL" ,Value=this.MANHOLE_MATERIAL },
               new SQLiteParameter(){ ParameterName="@MANHOLE_SHAPE" ,Value=this.MANHOLE_SHAPE },
               new SQLiteParameter(){ ParameterName="@COVER_MATERIAL" ,Value=this.COVER_MATERIAL },
               new SQLiteParameter(){ ParameterName="@COVER_STATE" ,Value=this.COVER_STATE },
               new SQLiteParameter(){ ParameterName="@WATER_QUALITY" ,Value=this.WATER_QUALITY },
               new SQLiteParameter(){ ParameterName="@WATER_LEVEL" ,Value=string.IsNullOrEmpty( this.WATER_LEVEL)?DBNull.Value:(object)this.WATER_LEVEL }
              
               // new SQLiteParameter(){ ParameterName="@ObjectID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer}

           };
          string sql = "insert into PS_MANHOLE(ID,SYSTEM_TYPE,ROAD_NAME,ACQUISITION_DATE,ACQUISITION_UNIT"
               + ",PROCESS_UNIT,PROCESS_DATE,STATE,Remark,GROUND_LEVEL,CO_X,CO_Y,SURVEY_ID,INVERT_LEVEL,"
               + " MANHOLE_TYPE,SEDIMENT_DEPTH,FLOW_STATE,BOTTOM_TYPE,MANHOLE_MATERIAL,MANHOLE_SHAPE,COVER_MATERIAL,"
               + "COVER_STATE,WATER_QUALITY,WATER_LEVEL"
               +") values( "
               + " @ID,@SYSTEM_TYPE,@ROAD_NAME,@ACQUISITION_DATE,@ACQUISITION_UNIT,@PROCESS_UNIT,@PROCESS_DATE,@STATE,"
               + " @Remark,@GROUND_LEVEL,@CO_X,@CO_Y,@SURVEY_ID,@INVERT_LEVEL,@MANHOLE_TYPE,@SEDIMENT_DEPTH,@FLOW_STATE,"
               + "@BOTTOM_TYPE,@MANHOLE_MATERIAL,@MANHOLE_SHAPE,@COVER_MATERIAL,@COVER_STATE,@WATER_QUALITY,@WATER_LEVEL)";
          IDataBase pDataBase = SysDBUnitiy.OleDataBase;
          pDataBase.OpenConnection();
          try
          {
              pDataBase.ExecuteNonQuery(sql, Parameters);
              sql = string.Format("insert into pointstable(ID,CO_X,CO_Y,ClassName,SURVEY_ID) values('{0}',{1},{2},'{3}','{4}')",
            ID, this.X, this.Y, this.ClassName,this.SURVEY_ID);
              pDataBase.ExecuteNonQuery(sql);
              //this.CurClassName = "PS_MANHOLE";
              //sql = "select max(objectid) from PS_MANHOLE";
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
                    
            if (pDataRow.Table.Columns.Contains("MANHOLE_TYPE"))
                this.MANHOLE_TYPE = pDataRow["MANHOLE_TYPE"].ToString();

            if (pDataRow.Table.Columns.Contains("SEDIMENT_DEPTH"))
                this.SEDIMENT_DEPTH = pDataRow["SEDIMENT_DEPTH"].ToString();

            if (pDataRow.Table.Columns.Contains("FLOW_STATE"))
                this.FLOW_STATE = pDataRow["FLOW_STATE"].ToString();

            if (pDataRow.Table.Columns.Contains("BOTTOM_TYPE"))
                this.BOTTOM_TYPE = pDataRow["BOTTOM_TYPE"].ToString();

            if (pDataRow.Table.Columns.Contains("MANHOLE_MATERIAL"))
                this.MANHOLE_MATERIAL = pDataRow["MANHOLE_MATERIAL"].ToString();

            if (pDataRow.Table.Columns.Contains("MANHOLE_SHAPE"))
                this.MANHOLE_SHAPE = pDataRow["MANHOLE_SHAPE"].ToString();

            if (pDataRow.Table.Columns.Contains("COVER_MATERIAL"))
                this.COVER_MATERIAL = pDataRow["COVER_MATERIAL"].ToString();

            if (pDataRow.Table.Columns.Contains("COVER_STATE"))
                this.COVER_STATE = pDataRow["COVER_STATE"].ToString();

            if (pDataRow.Table.Columns.Contains("WATER_QUALITY"))
                this.WATER_QUALITY = pDataRow["WATER_QUALITY"].ToString();

            if (pDataRow.Table.Columns.Contains("WATER_LEVEL"))
                this.WATER_LEVEL = pDataRow["WATER_LEVEL"].ToString();
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
               new SQLiteParameter(){ ParameterName="@REMARK" ,Value=this.Remark},

               new SQLiteParameter(){ ParameterName="@GROUND_LEVEL" ,Value=string.IsNullOrEmpty( this.GROUND_LEVEL)?DBNull.Value:(object) this.GROUND_LEVEL},
               new SQLiteParameter(){ ParameterName="@CO_X" ,Value=this.X},
               new SQLiteParameter(){ ParameterName="@CO_Y" ,Value=this.Y},
               new SQLiteParameter(){ ParameterName="@SURVEY_ID" ,Value=this.SURVEY_ID },
               new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.INVERT_LEVEL)?DBNull.Value: (object) this.INVERT_LEVEL },
               new SQLiteParameter(){ ParameterName="@MANHOLE_TYPE" ,Value=this.MANHOLE_TYPE },
               new SQLiteParameter(){ ParameterName="@SEDIMENT_DEPTH" ,Value=string.IsNullOrEmpty( this.SEDIMENT_DEPTH)?DBNull.Value:(object) this.SEDIMENT_DEPTH },
               new SQLiteParameter(){ ParameterName="@FLOW_STATE" ,Value=this.FLOW_STATE },
               new SQLiteParameter(){ ParameterName="@BOTTOM_TYPE" ,Value=this.BOTTOM_TYPE },
               new SQLiteParameter(){ ParameterName="@MANHOLE_MATERIAL" ,Value=this.MANHOLE_MATERIAL },
               new SQLiteParameter(){ ParameterName="@MANHOLE_SHAPE" ,Value=this.MANHOLE_SHAPE },
               new SQLiteParameter(){ ParameterName="@COVER_MATERIAL" ,Value=this.COVER_MATERIAL },
               new SQLiteParameter(){ ParameterName="@COVER_STATE" ,Value=this.COVER_STATE },
               new SQLiteParameter(){ ParameterName="@WATER_QUALITY" ,Value=this.WATER_QUALITY },
               new SQLiteParameter(){ ParameterName="@WATER_LEVEL" ,Value=string.IsNullOrEmpty( this.WATER_LEVEL)?DBNull.Value:(object)this.WATER_LEVEL },
               new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID}
                //new SQLiteParameter(){ ParameterName="@ObjectID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer}

           };
          string sql = string.Format("update PS_MANHOLE set SYSTEM_TYPE=@SYSTEM_TYPE,ROAD_NAME=@ROAD_NAME,"
              + "ACQUISITION_DATE=@ACQUISITION_DATE,ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_UNIT=@PROCESS_UNIT,"
               + "PROCESS_DATE=@PROCESS_DATE,STATE=@STATE,REMARK=@REMARK,"
               + "GROUND_LEVEL=@GROUND_LEVEL,CO_X=@CO_X,CO_Y=@CO_Y,SURVEY_ID=@SURVEY_ID,INVERT_LEVEL=@INVERT_LEVEL,"
               + " MANHOLE_TYPE=@MANHOLE_TYPE,SEDIMENT_DEPTH=@SEDIMENT_DEPTH,FLOW_STATE=@FLOW_STATE,BOTTOM_TYPE=@BOTTOM_TYPE,"
               + " MANHOLE_MATERIAL=@MANHOLE_MATERIAL,MANHOLE_SHAPE=@MANHOLE_SHAPE,COVER_MATERIAL=@COVER_MATERIAL,COVER_STATE=@COVER_STATE,"
               + " WATER_QUALITY=@WATER_QUALITY"
               + " where  ID=@ID ");

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
    }
}
