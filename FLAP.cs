using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using HR.Data;
using System.Data.SQLite;

namespace CHXQ.XMManager
{
    public interface IFLAP : IPIPELine
    {
        string Flap_Type { get; set; }
        string Flap_number { get; set; }
        string INVERT_LEVEL { get; set; }
 
    }
    class FLAP : PIPELineClass, IFLAP
    {
        public FLAP()
        {
            ClassName = "PS_FLAP";
        }
      public  string Flap_Type { get; set; }
      public string Flap_number { get; set; }
      public string INVERT_LEVEL { get; set; }

      public override void FillValueByRow(System.Data.DataRow pDataRow, string Tablename)
      {
          base.FillValueByRow(pDataRow, Tablename);
          if (pDataRow.Table.Columns.Contains("FLAP_TYPE"))
              this.Flap_Type = pDataRow["FLAP_TYPE"].ToString();
          if (pDataRow.Table.Columns.Contains("Flap_number"))
              this.Flap_number = pDataRow["FLAP_NUMBER"].ToString();
          if (pDataRow.Table.Columns.Contains("INVERT_LEVEL"))
              this.INVERT_LEVEL = pDataRow["INVERT_LEVEL"].ToString();
      }
      public override void Update()
      {

          string sql = "update  PS_FLAP  set  US_OBJECT_ID=@US_OBJECT_ID,DS_OBJECT_ID=@DS_OBJECT_ID,SYSTEM_TYPE=@SYSTEM_TYPE,FLAP_TYPE=@FLAP_TYPE,"
            + "INVERT_LEVEL=@INVERT_LEVEL,FLAP_NUMBER=@FLAP_NUMBER,"
            + "STATE=@STATE,ROAD_NAME=@ROAD_NAME,ACQUISITION_DATE=@ACQUISITION_DATE,Remark=@Remark,US_SURVEY_ID=@US_SURVEY_ID,DS_SURVEY_ID=@DS_SURVEY_ID,"
            + "ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_DATE=@PROCESS_DATE,PROCESS_UNIT=@PROCESS_UNIT,SHAPE_Length=@SHAPE_Length  where  ID=@ID  ";

          SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
                
               new SQLiteParameter(){ ParameterName="@US_OBJECT_ID" ,Value=this.US_OBJECT_ID,DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@DS_OBJECT_ID" ,Value=this.DS_OBJECT_ID,DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE,DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@FLAP_TYPE" ,Value=this.Flap_Type,DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.INVERT_LEVEL)?DBNull.Value:(object)double.Parse( this.Pipe_Length),DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@FLAP_NUMBER" ,Value=string.IsNullOrEmpty(this.Flap_number)?DBNull.Value:(object)double.Parse( this.Flap_number),DbType= System.Data.DbType.Int32},

            //   new SQLiteParameter(){ ParameterName="@MATERIAL" ,Value=this.MATERIAL},
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE,DbType= System.Data.DbType.String},
                
                 new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME,DbType= System.Data.DbType.String },
                   new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE,DbType= System.Data.DbType.String},
                     new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark,DbType= System.Data.DbType.String},
                       new SQLiteParameter(){ ParameterName="@US_SURVEY_ID" ,Value=this.US_SURVEY_ID,DbType= System.Data.DbType.String },
                            new SQLiteParameter(){ ParameterName="@DS_SURVEY_ID" ,Value=this.DS_SURVEY_ID,DbType= System.Data.DbType.String },
                            new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT,DbType= System.Data.DbType.String},
                               new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date,DbType= System.Data.DbType.String },
                               new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit,DbType= System.Data.DbType.String },
                               new SQLiteParameter(){ ParameterName="@SHAPE_Length" ,Value=string.IsNullOrEmpty(this.Lenght)?DBNull.Value:(object)double.Parse(this.Lenght) ,DbType= System.Data.DbType.String},
                                      new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID,DbType= System.Data.DbType.String}

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
          string sql = "insert into PS_FLAP(ID,US_OBJECT_ID,DS_OBJECT_ID,SYSTEM_TYPE,FLAP_TYPE,"
              + "INVERT_LEVEL,FLAP_NUMBER,"
              + "STATE,ROAD_NAME,ACQUISITION_DATE,Remark,US_SURVEY_ID,DS_SURVEY_ID,"
              + "ACQUISITION_UNIT,PROCESS_DATE,PROCESS_UNIT,SHAPE_Length)  values(@ID,@US_OBJECT_ID,@DS_OBJECT_ID,@SYSTEM_TYPE,"
                + "@FLAP_TYPE,@INVERT_LEVEL,@FLAP_NUMBER,"
                + "@MATERIAL,@STATE,@ROAD_NAME,@ACQUISITION_DATE,@Remark,@US_SURVEY_ID,@DS_SURVEY_ID,"
                + "@ACQUISITION_UNIT,@PROCESS_DATE,@PROCESS_UNIT,@SHAPE_Length)";
          SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
                new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID,DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@US_OBJECT_ID" ,Value=this.US_OBJECT_ID,DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@DS_OBJECT_ID" ,Value=this.DS_OBJECT_ID,DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE,DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@FLAP_TYPE" ,Value=this.Flap_Type,DbType= System.Data.DbType.String},
               new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.INVERT_LEVEL)?DBNull.Value:(object)double.Parse( this.Pipe_Length),DbType= System.Data.DbType.Double},
               new SQLiteParameter(){ ParameterName="@FLAP_NUMBER" ,Value=string.IsNullOrEmpty(this.Flap_number)?DBNull.Value:(object)double.Parse( this.Flap_number),DbType= System.Data.DbType.Int32},

              // new SQLiteParameter(){ ParameterName="@MATERIAL" ,Value=this.MATERIAL},
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE },
                
                 new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME },
                   new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE  },
                     new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark },
                       new SQLiteParameter(){ ParameterName="@US_SURVEY_ID" ,Value=this.US_SURVEY_ID  },
                            new SQLiteParameter(){ ParameterName="@DS_SURVEY_ID" ,Value=this.DS_SURVEY_ID  },
                            new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT },
                               new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date  },
                               new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit  },
                                 new SQLiteParameter(){ ParameterName="@SHAPE_Length" ,Value=string.IsNullOrEmpty(this.Lenght)?DBNull.Value:(object)double.Parse(this.Lenght) }
           };
          IDataBase pDataBase = SysDBUnitiy.OleDataBase;
          pDataBase.OpenConnection();
          try
          {
              pDataBase.ExecuteNonQuery(sql, Parameters);
              this.CurClassName = "PS_FLAP";
              //sql = "select max(objectid) from PS_FLAP";
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
