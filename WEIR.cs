using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using HR.Data;
using System.Data.SQLite;

namespace CHXQ.XMManager
{
    public interface IWEIR : IPIPELine
    {
        string INVERT_LEVEL { get; set; }

        
    }

    public class WEIR : PIPELineClass, IWEIR
    {
        public WEIR()
        {
            ClassName = "PS_WEIR";
        }
        public string INVERT_LEVEL { get; set; }

       public override void FillValueByRow(System.Data.DataRow pDataRow, string Tablename)
       {
           base.FillValueByRow(pDataRow, Tablename);
           if (pDataRow.Table.Columns.Contains("CREST_LEVEL"))
               this.INVERT_LEVEL = pDataRow["CREST_LEVEL"].ToString();
       }
       public override void Update()
       {

           string sql = "update  PS_WEIR  set  US_OBJECT_ID=@US_OBJECT_ID,DS_OBJECT_ID=@DS_OBJECT_ID,SYSTEM_TYPE=@SYSTEM_TYPE,CREST_LEVEL=@CREST_LEVEL,Width=@Width,"           
             + "STATE=@STATE,ROAD_NAME=@ROAD_NAME,ACQUISITION_DATE=@ACQUISITION_DATE,Remark=@Remark,US_SURVEY_ID=@US_SURVEY_ID,DS_SURVEY_ID=@DS_SURVEY_ID,"
             + "ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_DATE=@PROCESS_DATE,PROCESS_UNIT=@PROCESS_UNIT,SHAPE_Length=@SHAPE_Length  where  ID=@ID  ";

           SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
               
               new SQLiteParameter(){ ParameterName="@US_OBJECT_ID" ,Value=this.US_OBJECT_ID },
               new SQLiteParameter(){ ParameterName="@DS_OBJECT_ID" ,Value=this.DS_OBJECT_ID },
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE },
               new SQLiteParameter(){ ParameterName="@CREST_LEVEL" ,Value=string.IsNullOrEmpty( this.INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.INVERT_LEVEL) },
               new SQLiteParameter(){ ParameterName="@Width" ,Value=string.IsNullOrEmpty( this.Width)?DBNull.Value:(object)double.Parse(this.Width) },
              
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE },           
                 new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME },
                   new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE  },
                     new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark  },
                       new SQLiteParameter(){ ParameterName="@US_SURVEY_ID" ,Value=this.US_SURVEY_ID  },
                            new SQLiteParameter(){ ParameterName="@DS_SURVEY_ID" ,Value=this.DS_SURVEY_ID },
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
           string sql = "insert into PS_WEIR(ID,US_OBJECT_ID,DS_OBJECT_ID,SYSTEM_TYPE,CREST_LEVEL,Width,"            
               + "STATE,PRESSURE,ROAD_NAME,ACQUISITION_DATE,Remark,US_SURVEY_ID,DS_SURVEY_ID,"
               + "ACQUISITION_UNIT,PROCESS_DATE,PROCESS_UNIT,SHAPE_Length)  values(@ID,@US_OBJECT_ID,@DS_OBJECT_ID,@SYSTEM_TYPE,@CREST_LEVEL,@Width,"                
                 + "@STATE,@PRESSURE,@ROAD_NAME,@ACQUISITION_DATE,@Remark,@US_SURVEY_ID,@DS_SURVEY_ID,"
                 + "@ACQUISITION_UNIT,@PROCESS_DATE,@PROCESS_UNIT,@SHAPE_Length)";
           SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
              new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID },
               new SQLiteParameter(){ ParameterName="@US_OBJECT_ID" ,Value=this.US_OBJECT_ID },
               new SQLiteParameter(){ ParameterName="@DS_OBJECT_ID" ,Value=this.DS_OBJECT_ID },
               new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE },
               new SQLiteParameter(){ ParameterName="@CREST_LEVEL" ,Value=string.IsNullOrEmpty( this.INVERT_LEVEL)?DBNull.Value:(object)double.Parse(this.INVERT_LEVEL) },
               new SQLiteParameter(){ ParameterName="@Width" ,Value=string.IsNullOrEmpty( this.Width)?DBNull.Value:(object)double.Parse(this.Width) },
              
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE },
           
                 new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME  },
                   new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE  },
                     new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark },
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
               this.CurClassName = "PS_WEIR";
               //sql = "select max(objectid) from PS_WEIR";
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
