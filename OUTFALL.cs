using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using HR.Data;
using System.Data.SQLite;

namespace CHXQ.XMManager
{
    interface IOUTFALL : IPipePoint
    {
        /// <summary>
        /// 宽度
        /// </summary>
        string WIDTH
        {
            get;

            set;
        }
        /// <summary>
        /// 高度
        /// </summary>
        string HEIGHT
        {
            get;

            set;
        }
        /// <summary>
        /// 出流形式
        /// </summary>
        string EFFLUENT_TYPE
        {
            get;

            set;
        }
        /// <summary>
        /// 旱天状态
        /// </summary>
        string DRYWEATHER_STATE
        {
            get;

            set;
        }
        /// <summary>
        /// 水流颜色
        /// </summary>
        string WATER_QUALITY
        {
            get;
            set;
        }
 
    }
    class OUTFALL : PipePoint, IOUTFALL
    {
        public OUTFALL()
        {
            ClassName = "PS_OUTFALL";
        }
        /// <summary>
        /// 宽度
        /// </summary>
       public string WIDTH { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
       public string HEIGHT { get; set; }
        /// <summary>
        /// 出流形式
        /// </summary>
       public string EFFLUENT_TYPE { get; set; }
        /// <summary>
        /// 旱天状态
        /// </summary>
       public string DRYWEATHER_STATE { get; set; }

       /// <summary>
       /// 水流颜色
       /// </summary>
       public string WATER_QUALITY { get; set; }
       public override void FillValueByRow(System.Data.DataRow pDataRow, string Tablename)
       {
           base.FillValueByRow(pDataRow, Tablename);
           if (pDataRow.Table.Columns.Contains("WIDTH"))
               this.WIDTH = pDataRow["WIDTH"].ToString();

           if (pDataRow.Table.Columns.Contains("HEIGHT"))
               this.HEIGHT = pDataRow["HEIGHT"].ToString();

           if (pDataRow.Table.Columns.Contains("EFFLUENT_TYPE"))
               this.EFFLUENT_TYPE = pDataRow["EFFLUENT_TYPE"].ToString();

           if (pDataRow.Table.Columns.Contains("DRYWEATHER_STATE"))
               this.DRYWEATHER_STATE = pDataRow["DRYWEATHER_STATE"].ToString();

           if (pDataRow.Table.Columns.Contains("DRYWEATHER_WATER_QUALITY"))
               this.WATER_QUALITY = pDataRow["DRYWEATHER_WATER_QUALITY"].ToString();
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
                
               new SQLiteParameter(){ ParameterName="@WIDTH" ,Value=string.IsNullOrEmpty(this.WIDTH)?DBNull.Value: (object) this.WIDTH },
               new SQLiteParameter(){ ParameterName="@HEIGHT" ,Value=string.IsNullOrEmpty(this.HEIGHT)?DBNull.Value: (object) this.HEIGHT },
                new SQLiteParameter(){ ParameterName="@EFFLUENT_TYPE" ,Value=this.EFFLUENT_TYPE },
                new SQLiteParameter(){ ParameterName="@DRYWEATHER_STATE" ,Value=this.DRYWEATHER_STATE },
                new SQLiteParameter(){ ParameterName="@DRYWEATHER_WATER_QUALITY" ,Value=this.WATER_QUALITY },
                //new SQLiteParameter(){ ParameterName="@ObjectID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer}
                  new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID}

           };
           string sql = string.Format("update PS_OUTFALL set SYSTEM_TYPE=@SYSTEM_TYPE,ROAD_NAME=@ROAD_NAME,"
               + "ACQUISITION_DATE=@ACQUISITION_DATE,ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_UNIT=@PROCESS_UNIT,"
                + "PROCESS_DATE=@PROCESS_DATE,STATE=@STATE,REMARK=@REMARK,"
                + "GROUND_LEVEL=@GROUND_LEVEL,CO_X=@CO_X,CO_Y=@CO_Y,SURVEY_ID=@SURVEY_ID,INVERT_LEVEL=@INVERT_LEVEL,"
                + " WIDTH=@WIDTH,HEIGHT=@HEIGHT,EFFLUENT_TYPE=@EFFLUENT_TYPE,DRYWEATHER_STATE=@DRYWEATHER_STATE,"
                + " DRYWEATHER_WATER_QUALITY=@DRYWEATHER_WATER_QUALITY"
                + " where ID=@ID ");

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

               new SQLiteParameter(){ ParameterName="@GROUND_LEVEL" ,Value=string.IsNullOrEmpty( this.GROUND_LEVEL)?DBNull.Value:(object) this.GROUND_LEVEL},
               new SQLiteParameter(){ ParameterName="@CO_X" ,Value=this.X},
               new SQLiteParameter(){ ParameterName="@CO_Y" ,Value=this.Y},
               new SQLiteParameter(){ ParameterName="@SURVEY_ID" ,Value=this.SURVEY_ID },
               new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.INVERT_LEVEL)?DBNull.Value: (object) this.INVERT_LEVEL },
                
               new SQLiteParameter(){ ParameterName="@WIDTH" ,Value=string.IsNullOrEmpty(this.WIDTH)?DBNull.Value: (object) this.WIDTH },
               new SQLiteParameter(){ ParameterName="@HEIGHT" ,Value=string.IsNullOrEmpty(this.HEIGHT)?DBNull.Value: (object) this.HEIGHT },
                new SQLiteParameter(){ ParameterName="@EFFLUENT_TYPE" ,Value=this.EFFLUENT_TYPE },
                new SQLiteParameter(){ ParameterName="@DRYWEATHER_STATE" ,Value=this.DRYWEATHER_STATE },
                new SQLiteParameter(){ ParameterName="@DRYWEATHER_WATER_QUALITY" ,Value=this.WATER_QUALITY },
               // new SQLiteParameter(){ ParameterName="@ObjectID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer}

           };
           string sql = "insert into PS_OUTFALL(ID,SYSTEM_TYPE,ROAD_NAME,ACQUISITION_DATE,ACQUISITION_UNIT"
                + ",PROCESS_UNIT,PROCESS_DATE,STATE,Remark,GROUND_LEVEL,CO_X,CO_Y,SURVEY_ID,INVERT_LEVEL,"
                + " WIDTH,HEIGHT,EFFLUENT_TYPE,DRYWEATHER_STATE,DRYWEATHER_WATER_QUALITY"                
                + ") values( "
                + " @ID,@SYSTEM_TYPE,@ROAD_NAME,@ACQUISITION_DATE,@ACQUISITION_UNIT,@PROCESS_UNIT,@PROCESS_DATE,@STATE,"
                + " @Remark,@GROUND_LEVEL,@CO_X,@CO_Y,@SURVEY_ID,@INVERT_LEVEL,@WIDTH,@HEIGHT,@EFFLUENT_TYPE,"
                + "@DRYWEATHER_STATE,@DRYWEATHER_WATER_QUALITY)";
           IDataBase pDataBase = SysDBUnitiy.OleDataBase;
           pDataBase.OpenConnection();
           try
           {
               pDataBase.ExecuteNonQuery(sql, Parameters);
               sql = string.Format("insert into pointstable(ID,CO_X,CO_Y,ClassName,SURVEY_ID) values('{0}',{1},{2},'{3}','{4}')",
           ID, this.X, this.Y, this.ClassName, this.SURVEY_ID);
               pDataBase.ExecuteNonQuery(sql);
               this.CurClassName = "PS_OUTFALL";
               //sql = "select max(objectid) from PS_OUTFALL";
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
