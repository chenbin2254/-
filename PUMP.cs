using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using HR.Data;
using System.Data.SQLite;

namespace CHXQ.XMManager
{
    interface IPUMP : IPipePoint
    {
        /// <summary>
        /// 池底高程
        /// </summary>
        string BOTTOM_LEVEL
        {
            get;

            set;
        }
        /// <summary>
        /// 池底面积
        /// </summary>
        string BOTTOM_AREA
        {
            get;

            set;
        }
        /// <summary>
        /// 池顶高程
        /// </summary>
        string TOP_LEVEL
        {
            get;

            set;
        }
        /// <summary>
        /// 池顶面积
        /// </summary>
        string TOP_AREA
        {
            get;

            set;
        }
        /// <summary>
        /// 日常运行水位
        /// </summary>
        string WATER_LEVEL
        {
            get;

            set;
        }
    }
    class PUMP : PipePoint, IPUMP
    {
        public PUMP()
        {
            ClassName = "PS_PUMP_STORAGE";
        }
        /// <summary>
        /// 池底高程
        /// </summary>
      public  string BOTTOM_LEVEL { get; set; }
        /// <summary>
        /// 池底面积
        /// </summary>
      public string BOTTOM_AREA { get; set; }
        /// <summary>
        /// 池顶高程
        /// </summary>
      public string TOP_LEVEL { get; set; }
        /// <summary>
        /// 池顶面积
        /// </summary>
      public string TOP_AREA { get; set; }
        /// <summary>
        /// 日常运行水位
        /// </summary>
      public string WATER_LEVEL { get; set; }

      public override void FillValueByRow(System.Data.DataRow pDataRow, string Tablename)
      {
          base.FillValueByRow(pDataRow,Tablename);
          if (pDataRow.Table.Columns.Contains("BOTTOM_LEVEL"))
              this.BOTTOM_LEVEL = pDataRow["BOTTOM_LEVEL"].ToString();

          if (pDataRow.Table.Columns.Contains("BOTTOM_AREA"))
              this.BOTTOM_AREA = pDataRow["BOTTOM_AREA"].ToString();

          if (pDataRow.Table.Columns.Contains("TOP_LEVEL"))
              this.TOP_LEVEL = pDataRow["TOP_LEVEL"].ToString();

          if (pDataRow.Table.Columns.Contains("TOP_AREA"))
              this.TOP_AREA = pDataRow["TOP_AREA"].ToString();

          if (pDataRow.Table.Columns.Contains("WATER_LEVEL"))
              this.WATER_LEVEL = pDataRow["WATER_LEVEL"].ToString();
      }
      public override void AddNew()
      {
          SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
               new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID },
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE },
               new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME },
               new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE },
               new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT },
               new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit },
               new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date },
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE },
               new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark },

               new SQLiteParameter(){ ParameterName="@GROUND_LEVEL" ,Value=string.IsNullOrEmpty( this.GROUND_LEVEL)?DBNull.Value:(object) this.GROUND_LEVEL },
               new SQLiteParameter(){ ParameterName="@CO_X" ,Value=this.X },
               new SQLiteParameter(){ ParameterName="@CO_Y" ,Value=this.Y},
               new SQLiteParameter(){ ParameterName="@SURVEY_ID" ,Value=this.SURVEY_ID },
               new SQLiteParameter(){ ParameterName="@BOTTOM_AREA" ,Value=string.IsNullOrEmpty(this.BOTTOM_AREA)?DBNull.Value: (object) this.BOTTOM_AREA },
               
               new SQLiteParameter(){ ParameterName="@BOTTOM_LEVEL" ,Value=string.IsNullOrEmpty(this.BOTTOM_LEVEL)?DBNull.Value: (object) this.BOTTOM_LEVEL },
               new SQLiteParameter(){ ParameterName="@TOP_LEVEL" ,Value=string.IsNullOrEmpty(this.TOP_LEVEL)?DBNull.Value: (object) this.TOP_LEVEL },
                new SQLiteParameter(){ ParameterName="@TOP_AREA" ,Value=string.IsNullOrEmpty(this.TOP_AREA)?DBNull.Value: (object) this.TOP_AREA },                
               new SQLiteParameter(){ ParameterName="@WATER_LEVEL" ,Value=string.IsNullOrEmpty( this.WATER_LEVEL)?DBNull.Value:(object)this.WATER_LEVEL }
              
               // new SQLiteParameter(){ ParameterName="@ObjectID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer}

           };
          string sql = "insert into PS_PUMP_STORAGE(ID,SYSTEM_TYPE,ROAD_NAME,ACQUISITION_DATE,ACQUISITION_UNIT"
               + ",PROCESS_UNIT,PROCESS_DATE,STATE,Remark,GROUND_LEVEL,CO_X,CO_Y,SURVEY_ID,BOTTOM_AREA,"
               + "BOTTOM_LEVEL,TOP_LEVEL,TOP_AREA,WATER_LEVEL"
               + ") values( "
               + " @ID,@SYSTEM_TYPE,@ROAD_NAME,@ACQUISITION_DATE,@ACQUISITION_UNIT,@PROCESS_UNIT,@PROCESS_DATE,@STATE,"
               + " @Remark,@GROUND_LEVEL,@CO_X,@CO_Y,@SURVEY_ID,@BOTTOM_AREA,"
               + "@BOTTOM_LEVEL,@TOP_LEVEL,@TOP_AREA,@WATER_LEVEL)";
          IDataBase pDataBase = SysDBUnitiy.OleDataBase;
          pDataBase.OpenConnection();
          try
          {
              pDataBase.ExecuteNonQuery(sql, Parameters);
              sql = string.Format("insert into pointstable(ID,CO_X,CO_Y,ClassName,SURVEY_ID) values('{0}',{1},{2},'{3}','{4}')",
           ID, this.X, this.Y, this.ClassName, this.SURVEY_ID);
              pDataBase.ExecuteNonQuery(sql);
              this.CurClassName = "PS_PUMP_STORAGE";
              //sql = "select max(objectid) from PS_PUMP_STORAGE";
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
               new SQLiteParameter(){ ParameterName="@BOTTOM_AREA" ,Value=string.IsNullOrEmpty(this.INVERT_LEVEL)?DBNull.Value: (object) this.INVERT_LEVEL },
               
               new SQLiteParameter(){ ParameterName="@BOTTOM_LEVEL" ,Value=string.IsNullOrEmpty(this.BOTTOM_LEVEL)?DBNull.Value: (object) this.BOTTOM_LEVEL },
               new SQLiteParameter(){ ParameterName="@TOP_LEVEL" ,Value=string.IsNullOrEmpty(this.TOP_LEVEL)?DBNull.Value: (object) this.TOP_LEVEL },
                new SQLiteParameter(){ ParameterName="@TOP_AREA" ,Value=string.IsNullOrEmpty(this.TOP_AREA)?DBNull.Value: (object) this.TOP_AREA },                
               new SQLiteParameter(){ ParameterName="@WATER_LEVEL" ,Value=string.IsNullOrEmpty( this.WATER_LEVEL)?DBNull.Value:(object)this.WATER_LEVEL },
                new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID} 
               

           };
          string sql = string.Format("update PS_PUMP_STORAGE set SYSTEM_TYPE=@SYSTEM_TYPE,ROAD_NAME=@ROAD_NAME,"
              + "ACQUISITION_DATE=@ACQUISITION_DATE,ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_UNIT=@PROCESS_UNIT,"
               + "PROCESS_DATE=@PROCESS_DATE,STATE=@STATE,REMARK=@REMARK,"
               + "GROUND_LEVEL=@GROUND_LEVEL,CO_X=@CO_X,CO_Y=@CO_Y,SURVEY_ID=@SURVEY_ID,BOTTOM_AREA=@BOTTOM_AREA,"
               + " BOTTOM_LEVEL=@BOTTOM_LEVEL,TOP_LEVEL=@TOP_LEVEL,TOP_AREA=@TOP_AREA,WATER_LEVEL=@WATER_LEVEL "              
               + "  where  ID=@ID ");

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
    }
}
