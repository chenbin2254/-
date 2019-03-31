using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using HR.Data;
using System.Data.SQLite;

namespace CHXQ.XMManager
{
    interface ICOMB : IPipePoint
    {
        /// <summary>
        /// 淤积深度
        /// </summary>
        string SEDIMENT_DEPTH
        {
            get;

            set;
        }
        /// <summary>
        /// 雨水口形式
        /// </summary>
        string OBJECT_TYPE
        {
            get;

            set;
        }
        /// <summary>
        /// 篦子个数
        /// </summary>
        string GULLY_NUMBER
        {
            get;
             
            set;
        }


    }
    class COMB : PipePoint, ICOMB
    {
        public COMB()
        {
            ClassName = "PS_COMB";
            this.SYSTEM_TYPE = "雨水";
 
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
        /// 雨水口形式
        /// </summary>
        public string OBJECT_TYPE
        {
            get;
            set;
        }
        /// <summary>
        /// 篦子个数
        /// </summary>
        public string GULLY_NUMBER
        {
            get;
            set;
        }

       public override void FillValueByRow(System.Data.DataRow pDataRow )
        {
            base.FillValueByRow(pDataRow );

            if (pDataRow.Table.Columns.Contains("SEDIMENT_DEPTH"))
                this.SEDIMENT_DEPTH = pDataRow["SEDIMENT_DEPTH"].ToString();

            if (pDataRow.Table.Columns.Contains("OBJECT_TYPE"))
                this.OBJECT_TYPE = pDataRow["OBJECT_TYPE"].ToString();

            if (pDataRow.Table.Columns.Contains("GULLY_NUMBER"))
                this.GULLY_NUMBER = pDataRow["GULLY_NUMBER"].ToString();
        }
         public override void AddNew()
      {
          SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
               new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID },
              // new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE},
               new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME },
               new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE },
               new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT },
               new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit },
               new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date },
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE },
               new SQLiteParameter(){ ParameterName="@Remark" ,Value=this.Remark },

               new SQLiteParameter(){ ParameterName="@GROUND_LEVEL" ,Value=string.IsNullOrEmpty( this.GROUND_LEVEL)?DBNull.Value:(object) this.GROUND_LEVEL },
               new SQLiteParameter(){ ParameterName="@CO_X" ,Value=this.X },
               new SQLiteParameter(){ ParameterName="@CO_Y" ,Value=this.Y },
               new SQLiteParameter(){ ParameterName="@SURVEY_ID" ,Value=this.SURVEY_ID  },
               new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.INVERT_LEVEL)?DBNull.Value: (object) this.INVERT_LEVEL  },               
               new SQLiteParameter(){ ParameterName="@SEDIMENT_DEPTH" ,Value=string.IsNullOrEmpty( this.SEDIMENT_DEPTH)?DBNull.Value:(object) this.SEDIMENT_DEPTH  },
               new SQLiteParameter(){ ParameterName="@OBJECT_TYPE" ,Value=this.OBJECT_TYPE  },
              new SQLiteParameter(){ ParameterName="@GULLY_NUMBER" ,Value=string.IsNullOrEmpty( this.GULLY_NUMBER)?DBNull.Value:(object) this.GULLY_NUMBER.Substring(0,1) },
               // new SQLiteParameter(){ ParameterName="@ObjectID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer}

           };
          string sql = "insert into PS_COMB(ID,ROAD_NAME,ACQUISITION_DATE,ACQUISITION_UNIT"
               + ",PROCESS_UNIT,PROCESS_DATE,STATE,Remark,GROUND_LEVEL,CO_X,CO_Y,SURVEY_ID,INVERT_LEVEL,"
               + " SEDIMENT_DEPTH,OBJECT_TYPE,GULLY_NUMBER"               
               + ") values( "
               + " @ID,@ROAD_NAME,@ACQUISITION_DATE,@ACQUISITION_UNIT,@PROCESS_UNIT,@PROCESS_DATE,@STATE,"
               + " @Remark,@GROUND_LEVEL,@CO_X,@CO_Y,@SURVEY_ID,@INVERT_LEVEL,@SEDIMENT_DEPTH,@OBJECT_TYPE,@GULLY_NUMBER"
               + ")";
          IDataBase pDataBase = SysDBUnitiy.OleDataBase;
          pDataBase.OpenConnection();
          try
          {
              pDataBase.ExecuteNonQuery(sql, Parameters);
              sql = string.Format("insert into pointstable(ID,CO_X,CO_Y,ClassName,SURVEY_ID) values('{0}',{1},{2},'{3}','{4}')",
            ID, this.X, this.Y, this.ClassName, this.SURVEY_ID);
              pDataBase.ExecuteNonQuery(sql);
              this.CurClassName = "PS_COMB";
              //sql = "select max(objectid) from PS_COMB";
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
           
               //new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE },
               new SQLiteParameter(){ ParameterName="@ROAD_NAME" ,Value=this.ROAD_NAME },
               new SQLiteParameter(){ ParameterName="@ACQUISITION_DATE" ,Value=this.ACQUISITION_DATE },
               new SQLiteParameter(){ ParameterName="@ACQUISITION_UNIT" ,Value=this.ACQUISITION_UNIT },
               new SQLiteParameter(){ ParameterName="@PROCESS_UNIT" ,Value=this.PROCESS_Unit },
               new SQLiteParameter(){ ParameterName="@PROCESS_DATE" ,Value=this.PROCESS_Date },
               new SQLiteParameter(){ ParameterName="@STATE" ,Value=this.STATE },
               new SQLiteParameter(){ ParameterName="@REMARK" ,Value=this.Remark },

               new SQLiteParameter(){ ParameterName="@GROUND_LEVEL" ,Value=string.IsNullOrEmpty( this.GROUND_LEVEL)?DBNull.Value:(object) this.GROUND_LEVEL },
               new SQLiteParameter(){ ParameterName="@CO_X" ,Value=this.X },
               new SQLiteParameter(){ ParameterName="@CO_Y" ,Value=this.Y },
               new SQLiteParameter(){ ParameterName="@SURVEY_ID" ,Value=this.SURVEY_ID  },
               new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.INVERT_LEVEL)?DBNull.Value: (object) this.INVERT_LEVEL  },               
               new SQLiteParameter(){ ParameterName="@SEDIMENT_DEPTH" ,Value=string.IsNullOrEmpty( this.SEDIMENT_DEPTH)?DBNull.Value:(object) this.SEDIMENT_DEPTH  },
               new SQLiteParameter(){ ParameterName="@OBJECT_TYPE" ,Value=this.OBJECT_TYPE },
              new SQLiteParameter(){ ParameterName="@GULLY_NUMBER" ,Value=string.IsNullOrEmpty( this.GULLY_NUMBER)?DBNull.Value:(object) this.GULLY_NUMBER  },
                 new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID }
             //   new SQLiteParameter(){ ParameterName="@ObjectID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer}

           };
             string sql = string.Format("update PS_COMB set ROAD_NAME=@ROAD_NAME,"
                 + "ACQUISITION_DATE=@ACQUISITION_DATE,ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_UNIT=@PROCESS_UNIT,"
                  + "PROCESS_DATE=@PROCESS_DATE,STATE=@STATE,REMARK=@REMARK,"
                  + "GROUND_LEVEL=@GROUND_LEVEL,CO_X=@CO_X,CO_Y=@CO_Y,SURVEY_ID=@SURVEY_ID,INVERT_LEVEL=@INVERT_LEVEL,"
                  + " SEDIMENT_DEPTH=@SEDIMENT_DEPTH,OBJECT_TYPE=@OBJECT_TYPE,GULLY_NUMBER=@GULLY_NUMBER"
                  + "  where ID=@ID ");

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
