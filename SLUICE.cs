using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using HR.Data;
using System.Data.SQLite;

namespace CHXQ.XMManager
{
    public interface ISLUICE : IPIPELine
    {
        string INVERT_LEVEL { get; set; }

        string OPEN_LIMIT { get; set; }

        string SLUICE_TYPE { get; set; }
 
    }

    public class SLUICE : PIPELineClass, ISLUICE
    {
        public SLUICE()
        {
            ClassName = "PS_SLUICE";
        }

      public  string INVERT_LEVEL { get; set; }

      public string OPEN_LIMIT { get; set; }

      public string SLUICE_TYPE { get; set; }

      public override void FillValueByRow(System.Data.DataRow pDataRow, string Tablename)
      {
          base.FillValueByRow(pDataRow, Tablename);
          //if (pDataRow.Table.Columns.Contains("INVERT_LEVEL"))
          //    this.INVERT_LEVEL = pDataRow["INVERT_LEVEL"].ToString();

          if (pDataRow.Table.Columns.Contains("OPEN_LIMIT"))
              this.OPEN_LIMIT = pDataRow["OPEN_LIMIT"].ToString();

          if (pDataRow.Table.Columns.Contains("SLUICE_TYPE"))
              this.SLUICE_TYPE = pDataRow["SLUICE_TYPE"].ToString();
      }
      public override void Update()
      {

          string sql = "update  PS_SLUICE  set  US_OBJECT_ID=@US_OBJECT_ID,DS_OBJECT_ID=@DS_OBJECT_ID,SYSTEM_TYPE=@SYSTEM_TYPE,INVERT_LEVEL=@INVERT_LEVEL,Width=@Width,"
            + "OPEN_LIMIT=@OPEN_LIMIT,SLUICE_TYPE=@SLUICE_TYPE,STATE=@STATE,ROAD_NAME=@ROAD_NAME,ACQUISITION_DATE=@ACQUISITION_DATE,Remark=@Remark,US_SURVEY_ID=@US_SURVEY_ID,DS_SURVEY_ID=@DS_SURVEY_ID,"
            + "ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_DATE=@PROCESS_DATE,PROCESS_UNIT=@PROCESS_UNIT,SHAPE_Length=@SHAPE_Length  where  ID=@ID  ";

          SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
               
               new SQLiteParameter(){ ParameterName="@US_OBJECT_ID" ,Value=this.US_OBJECT_ID },
               new SQLiteParameter(){ ParameterName="@DS_OBJECT_ID" ,Value=this.DS_OBJECT_ID },
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE },
               new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty( this.INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.INVERT_LEVEL) },
               new SQLiteParameter(){ ParameterName="@Width" ,Value=string.IsNullOrEmpty( this.Width)?DBNull.Value:(object)double.Parse(this.Width) },
              new SQLiteParameter(){ ParameterName="@OPEN_LIMIT" ,Value=string.IsNullOrEmpty( this.OPEN_LIMIT)?DBNull.Value:(object)double.Parse(this.OPEN_LIMIT)  },
              new SQLiteParameter(){ ParameterName="@SLUICE_TYPE" ,Value=this.SLUICE_TYPE },           
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE },           
                 new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME  },
                   new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE  },
                     new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark  },
                       new SQLiteParameter(){ ParameterName="@US_SURVEY_ID" ,Value=this.US_SURVEY_ID  },
                            new SQLiteParameter(){ ParameterName="@DS_SURVEY_ID" ,Value=this.DS_SURVEY_ID  },
                            new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT  },
                               new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date },
                               new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit },
                               new SQLiteParameter(){ ParameterName="@SHAPE_Length" ,Value=string.IsNullOrEmpty(this.Lenght)?DBNull.Value:(object)double.Parse(this.Lenght) },
                                         new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID }

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
          string sql = "insert into PS_SLUICE(ID,US_OBJECT_ID,DS_OBJECT_ID,SYSTEM_TYPE,INVERT_LEVEL,Width,OPEN_LIMIT,SLUICE_TYPE,"
              + "STATE,PRESSURE,ROAD_NAME,ACQUISITION_DATE,Remark,US_SURVEY_ID,DS_SURVEY_ID,"
              + "ACQUISITION_UNIT,PROCESS_DATE,PROCESS_UNIT,SHAPE_Length)"
              +"  values(@ID,@US_OBJECT_ID,@DS_OBJECT_ID,@SYSTEM_TYPE,@INVERT_LEVEL,@Width,@OPEN_LIMIT,@SLUICE_TYPE,"
                + "@STATE,@PRESSURE,@ROAD_NAME,@ACQUISITION_DATE,@Remark,@US_SURVEY_ID,@DS_SURVEY_ID,"
                + "@ACQUISITION_UNIT,@PROCESS_DATE,@PROCESS_UNIT,@SHAPE_Length)";
          SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
              new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID },
               new SQLiteParameter(){ ParameterName="@US_OBJECT_ID" ,Value=this.US_OBJECT_ID },
               new SQLiteParameter(){ ParameterName="@DS_OBJECT_ID" ,Value=this.DS_OBJECT_ID },
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE },
               new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty( this.INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.INVERT_LEVEL) },
               new SQLiteParameter(){ ParameterName="@Width" ,Value=string.IsNullOrEmpty( this.Width)?DBNull.Value:(object)double.Parse(this.Width) },
              new SQLiteParameter(){ ParameterName="@OPEN_LIMIT" ,Value=string.IsNullOrEmpty( this.OPEN_LIMIT)?DBNull.Value:(object)double.Parse(this.OPEN_LIMIT) },
              new SQLiteParameter(){ ParameterName="@SLUICE_TYPE" ,Value=this.SLUICE_TYPE },           
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE },           
              new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME  },
              new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE  },
              new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark  },
              new SQLiteParameter(){ ParameterName="@US_SURVEY_ID" ,Value=this.US_SURVEY_ID  },
              new SQLiteParameter(){ ParameterName="@DS_SURVEY_ID" ,Value=this.DS_SURVEY_ID  },
              new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT  },
              new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date  },
              new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit  },
              new SQLiteParameter(){ ParameterName="@SHAPE_Length" ,Value=string.IsNullOrEmpty(this.Lenght)?DBNull.Value:(object)double.Parse(this.Lenght) }

           };
          IDataBase pDataBase = SysDBUnitiy.OleDataBase;
          pDataBase.OpenConnection();
          try
          {
              pDataBase.ExecuteNonQuery(sql, Parameters);
              this.CurClassName = "PS_SLUICE";
              //sql = "select max(objectid) from PS_SLUICE";
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
