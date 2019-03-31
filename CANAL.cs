using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using HR.Data;
using System.Data.SQLite;
namespace CHXQ.XMManager
{
    interface ICANAL : IPIPELine
    {
        /// <summary>
        /// 渠高
        /// </summary>
        string Height { get; set; }


    }
    class CANAL : PIPELineClass, ICANAL
    {
        public CANAL()
        {
            ClassName = "PS_CANAL";
        }
        /// <summary>
        /// 渠高
        /// </summary>
        public string Height { get; set; }

        public override void FillValueByRow(System.Data.DataRow pDataRow, string Tablename)
        {
            base.FillValueByRow(pDataRow, Tablename);

            if (pDataRow.Table.Columns.Contains("Height"))
                this.Height = pDataRow["Height"].ToString();

            if (pDataRow.Table.Columns.Contains("CANAL_LENGTH"))
                this.Width = pDataRow["CANAL_LENGTH"].ToString();
        }
        public override void Update()
        {  
            string sql = "update  PS_CANAL  set   US_OBJECT_ID=@US_OBJECT_ID,DS_OBJECT_ID=@DS_OBJECT_ID,SYSTEM_TYPE=@SYSTEM_TYPE,Height=@Height,Width=@Width,CANAL_LENGTH=@CANAL_LENGTH,"
              + "US_POINT_INVERT_LEVEL=@US_POINT_INVERT_LEVEL,US_INVERT_LEVEL=@US_INVERT_LEVEL,DS_POINT_INVERT_LEVEL=@DS_POINT_INVERT_LEVEL,DS_INVERT_LEVEL=@DS_INVERT_LEVEL,SEDIMENT_DEPTH=@SEDIMENT_DEPTH,"
              + "MATERIAL=@MATERIAL,STATE=@STATE,PRESSURE=@PRESSURE,ROAD_NAME=@ROAD_NAME,ACQUISITION_DATE=@ACQUISITION_DATE,Remark=@Remark,US_SURVEY_ID=@US_SURVEY_ID,DS_SURVEY_ID=@DS_SURVEY_ID,"
              + "ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_DATE=@PROCESS_DATE,PROCESS_UNIT=@PROCESS_UNIT,SHAPE_Length=@SHAPE_Length  where ID=@ID  ";
            
            SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
              
               new SQLiteParameter(){ ParameterName="@US_OBJECT_ID" ,Value=this.US_OBJECT_ID, DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@DS_OBJECT_ID" ,Value=this.DS_OBJECT_ID, DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE, DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@Height" ,Value=string.IsNullOrEmpty( this.Height)?DBNull.Value:(object)double.Parse(this.Height) , DbType= System.Data.DbType.UInt32},
               new SQLiteParameter(){ ParameterName="@Width" ,Value=string.IsNullOrEmpty( this.Width)?DBNull.Value:(object)double.Parse(this.Width) ,DbType= System.Data.DbType.UInt32},
               new SQLiteParameter(){ ParameterName="@CANAL_LENGTH" ,Value=string.IsNullOrEmpty(this.Pipe_Length)?DBNull.Value:(object)double.Parse( this.Pipe_Length),DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@US_POINT_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.US_POINT_INVERT_LEVEL)?DBNull.Value:(object)double.Parse( this.US_POINT_INVERT_LEVEL),DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@US_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.US_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.US_INVERT_LEVEL) ,DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@DS_POINT_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.DS_POINT_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.DS_POINT_INVERT_LEVEL) ,DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@DS_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.DS_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.DS_INVERT_LEVEL) ,DbType= System.Data.DbType.Double},
                new SQLiteParameter(){ ParameterName="@SEDIMENT_DEPTH" ,Value=string.IsNullOrEmpty(this.SEDIMENT_DEPTH)?DBNull.Value:(object)double.Parse(this.SEDIMENT_DEPTH) ,DbType= System.Data.DbType.Double},

               new SQLiteParameter(){ ParameterName="@MATERIAL" ,Value=this.MATERIAL, DbType= System.Data.DbType.String },
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE , DbType= System.Data.DbType.String},
                new SQLiteParameter(){ ParameterName="@PRESSURE" ,Value=this.PRESSURE , DbType= System.Data.DbType.String},
                 new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME  , DbType= System.Data.DbType.String},
                   new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE , DbType= System.Data.DbType.String},
                     new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark, DbType= System.Data.DbType.String},
                       new SQLiteParameter(){ ParameterName="@US_SURVEY_ID" ,Value=this.US_SURVEY_ID, DbType= System.Data.DbType.String },
                            new SQLiteParameter(){ ParameterName="@DS_SURVEY_ID" ,Value=this.DS_SURVEY_ID, DbType= System.Data.DbType.String},
                            new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT, DbType= System.Data.DbType.String },
              new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date, DbType= System.Data.DbType.String },
              new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit, DbType= System.Data.DbType.String},
              new SQLiteParameter(){ ParameterName="@SHAPE_Length" ,Value=string.IsNullOrEmpty(this.Lenght)?DBNull.Value:(object)double.Parse(this.Lenght)  ,DbType= System.Data.DbType.Double},
                new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID, DbType= System.Data.DbType.String}
              //new SQLiteParameter(){ ParameterName="@OBJECTID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer }

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
        public override void AddNew()
        {
            string sql = "insert into PS_CANAL(ID,US_OBJECT_ID,DS_OBJECT_ID,SYSTEM_TYPE,Height,Width,CANAL_LENGTH,"
                + "US_POINT_INVERT_LEVEL,US_INVERT_LEVEL,DS_POINT_INVERT_LEVEL,DS_INVERT_LEVEL,SEDIMENT_DEPTH,"
                + "MATERIAL,STATE,PRESSURE,ROAD_NAME,ACQUISITION_DATE,Remark,US_SURVEY_ID,DS_SURVEY_ID,"
                + "ACQUISITION_UNIT,PROCESS_DATE,PROCESS_UNIT,SHAPE_Length)  values(@ID,@US_OBJECT_ID,@DS_OBJECT_ID,@SYSTEM_TYPE,@Height,@Width,@CANAL_LENGTH,"
                  + "@US_POINT_INVERT_LEVEL,@US_INVERT_LEVEL,@DS_POINT_INVERT_LEVEL,@DS_INVERT_LEVEL,@SEDIMENT_DEPTH,"
                  + "@MATERIAL,@STATE,@PRESSURE,@ROAD_NAME,@ACQUISITION_DATE,@Remark,@US_SURVEY_ID,@DS_SURVEY_ID,"
                  + "@ACQUISITION_UNIT,@PROCESS_DATE,@PROCESS_UNIT,@SHAPE_Length)";
            SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
                new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID, DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@US_OBJECT_ID" ,Value=this.US_OBJECT_ID, DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@DS_OBJECT_ID" ,Value=this.DS_OBJECT_ID, DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE, DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@Height" ,Value=string.IsNullOrEmpty( this.Height)?DBNull.Value:(object)double.Parse(this.Height) , DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@Width" ,Value=string.IsNullOrEmpty( this.Width)?DBNull.Value:(object)double.Parse(this.Width) ,DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@CANAL_LENGTH" ,Value=string.IsNullOrEmpty(this.Pipe_Length)?DBNull.Value:(object)double.Parse( this.Pipe_Length),DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@US_POINT_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.US_POINT_INVERT_LEVEL)?DBNull.Value:(object)double.Parse( this.US_POINT_INVERT_LEVEL),DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@US_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.US_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.US_INVERT_LEVEL) ,DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@DS_POINT_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.DS_POINT_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.DS_POINT_INVERT_LEVEL) ,DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@DS_INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.DS_INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.DS_INVERT_LEVEL) ,DbType= System.Data.DbType.Double},
                new SQLiteParameter(){ ParameterName="@SEDIMENT_DEPTH" ,Value=string.IsNullOrEmpty(this.SEDIMENT_DEPTH)?DBNull.Value:(object)double.Parse(this.SEDIMENT_DEPTH) ,DbType= System.Data.DbType.Double},

               new SQLiteParameter(){ ParameterName="@MATERIAL" ,Value=this.MATERIAL, DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE, DbType= System.Data.DbType.String},
                new SQLiteParameter(){ ParameterName="@PRESSURE" ,Value=this.PRESSURE, DbType= System.Data.DbType.String},
                 new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME, DbType= System.Data.DbType.String },
                   new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE, DbType= System.Data.DbType.String },
                     new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark, DbType= System.Data.DbType.String},
                       new SQLiteParameter(){ ParameterName="@US_SURVEY_ID" ,Value=this.US_SURVEY_ID, DbType= System.Data.DbType.String },
                            new SQLiteParameter(){ ParameterName="@DS_SURVEY_ID" ,Value=this.DS_SURVEY_ID, DbType= System.Data.DbType.String },
                            new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT, DbType= System.Data.DbType.String },
                               new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date, DbType= System.Data.DbType.String },
                               new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit, DbType= System.Data.DbType.String },
                        new SQLiteParameter(){ ParameterName="@SHAPE_Length" ,Value=string.IsNullOrEmpty(this.Lenght)?DBNull.Value:(object)double.Parse(this.Lenght) , DbType= System.Data.DbType.Double}

           };
            IDataBase pDataBase = SysDBUnitiy.OleDataBase;
            pDataBase.OpenConnection();
            try
            {
                pDataBase.ExecuteNonQuery(sql, Parameters);
                this.CurClassName = "PS_CANAL";
                //sql = "select max(objectid) from PS_CANAL";
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
