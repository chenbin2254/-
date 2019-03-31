using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using HR.Data;
using Autodesk.AutoCAD.Interop.Common;
using System.Data;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

namespace CHXQ.XMManager
{
  public  interface IPCPoint : IMANHOLE
    {
        string Type { get; set; }
      
      /// <summary>
      /// 井深
      /// </summary>
      string  Depth { get; set; }
      /// <summary>
      /// 篦子个数
      /// </summary>
      string GULLY_NUMBER
      {
          get;

          set;
      }

    }
  public class PCPoint : MANHOLE, IPCPoint
  {
      public PCPoint()
      {
          ClassName = "Points";
 
      }
      public string Type { get; set; }

      /// <summary>
      /// 篦子个数
      /// </summary>
      public string GULLY_NUMBER
      {
          get;

          set;
      }
      public string Depth { get; set; }

      private string GetTrueType(string pType)
      {
          string sql = string.Format("select TableName from tabledict where ClassValue='{0}'", pType);
          System.Data.DataTable pTable = SysDBUnitiy.SysDataBase.ExecuteQuery(sql).Tables[0];
          if (pTable.Rows.Count == 0)
              return string.Empty;
          else
              return pTable.Rows[0][0].ToString();
      }
      public override void DeleteCADObject(Autodesk.AutoCAD.ApplicationServices.Document doc)
      {

          string[] Layers = null;

          if (this.SURVEY_ID.StartsWith("WS"))
              Layers = new string[] { "0", "WSPoint", "WSText" };
          else
              Layers = new string[] { "0", "YSPoint", "YSText" };
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
      public override void DrawCADObject(Autodesk.AutoCAD.Interop.AcadDocument AcadDoc)
      {

          double pX = double.Parse(this.X);
          double pY = double.Parse(this.Y);
          double[] InsertPoint = new double[] { pX, pY, 0 };

          string BlockName = "一般管线点";
          int LayerID = GetLayerIndex("0", AcadDoc);

          
              //string TrueType = GetTrueType(this.Type);
          if (this.Type == "雨水篦")
                  BlockName = "雨篦";
          else if (this.Type == "检修井")
                  BlockName = "排水检修井";
          else if (this.Type == "出水口")
                  BlockName = "出水口";
              if (!string.IsNullOrEmpty(this.Type))
              {
                  if (this.SURVEY_ID.StartsWith("WS"))
                      LayerID = GetLayerIndex("WSPoint", AcadDoc);
                  else if (this.SURVEY_ID.StartsWith("YS"))
                      LayerID = GetLayerIndex("YSPoint", AcadDoc);
              }
         

          AcadDoc.ActiveLayer = AcadDoc.Layers.Item(LayerID);

          AcadMInsertBlock pAcadMInsertBlock = AcadDoc.ModelSpace.AddMInsertBlock(InsertPoint, BlockName, 1, 1, 1, 0, 1, 1, 1, 1);
       
          //pAcadMInsertBlock.TrueColor.SetRGB(255, 255, 255);
          AcadDictionary pAcadDictionary = pAcadMInsertBlock.GetExtensionDictionary();
          //pAcadDictionary.AddXRecord(ClassName);
          pAcadDictionary.AddXRecord(this.ID);

          LayerID = GetLayerIndex("0", AcadDoc);
          if (!string.IsNullOrEmpty(this.Type))
          {
              if (this.SURVEY_ID.StartsWith("WS"))
                  LayerID = GetLayerIndex("WSText", AcadDoc);
              else if (this.SURVEY_ID.StartsWith("YS"))
                  LayerID = GetLayerIndex("YSText", AcadDoc);
          }
          AcadDoc.ActiveLayer = AcadDoc.Layers.Item(LayerID);

          AcadText pAcadText = AcadDoc.ModelSpace.AddText(this.SURVEY_ID, InsertPoint, 2.0);
           
          pAcadDictionary = pAcadText.GetExtensionDictionary();
          //pAcadDictionary.AddXRecord(ClassName);
          pAcadDictionary.AddXRecord(this.ID);
          AcadDoc.Save();
      }

      public override void AddNew()
      {
          if (!string.IsNullOrEmpty(GROUND_LEVEL) && !string.IsNullOrEmpty(Depth))
              this.INVERT_LEVEL = (double.Parse(this.GROUND_LEVEL) - double.Parse(this.Depth)).ToString();
          SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
               new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID},
           
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
              
               new SQLiteParameter(){ ParameterName="@SEDIMENT_DEPTH" ,Value=string.IsNullOrEmpty( this.SEDIMENT_DEPTH)?DBNull.Value:(object) this.SEDIMENT_DEPTH },
               new SQLiteParameter(){ ParameterName="@FLOW_STATE" ,Value=this.FLOW_STATE },
               new SQLiteParameter(){ ParameterName="@BOTTOM_TYPE" ,Value=this.BOTTOM_TYPE },
               new SQLiteParameter(){ ParameterName="@MANHOLE_MATERIAL" ,Value=this.MANHOLE_MATERIAL },
               new SQLiteParameter(){ ParameterName="@MANHOLE_SHAPE" ,Value=this.MANHOLE_SHAPE },
                new SQLiteParameter(){ ParameterName="@MANHOLE_SIZE" ,Value=this.MANHOLE_SIZE },
               new SQLiteParameter(){ ParameterName="@COVER_MATERIAL" ,Value=this.COVER_MATERIAL },
               new SQLiteParameter(){ ParameterName="@COVER_STATE" ,Value=this.COVER_STATE },
             
               new SQLiteParameter(){ ParameterName="@Depth" ,Value=this.Depth },
                new SQLiteParameter(){ ParameterName="@PointType" ,Value=this.Type },
                new SQLiteParameter(){ ParameterName="@GULLY_NUMBER" ,Value=string.IsNullOrEmpty( this.GULLY_NUMBER)?DBNull.Value:(object)int.Parse(this.GULLY_NUMBER) },
                new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=this.INVERT_LEVEL }
              

           };
          string sql = "insert into Points(ID,ROAD_NAME,ACQUISITION_DATE,ACQUISITION_UNIT"
               + ",PROCESS_UNIT,PROCESS_DATE,STATE,Remark,GROUND_LEVEL,CO_X,CO_Y,SURVEY_ID,"
               + " SEDIMENT_DEPTH,FLOW_STATE,BOTTOM_TYPE,MANHOLE_MATERIAL,MANHOLE_SHAPE,MANHOLE_SIZE,COVER_MATERIAL,"
               + "COVER_STATE,Depth,PointType,GULLY_NUMBER,INVERT_LEVEL"
               + ") values( "
               + " @ID,@ROAD_NAME,@ACQUISITION_DATE,@ACQUISITION_UNIT,@PROCESS_UNIT,@PROCESS_DATE,@STATE,"
               + " @Remark,@GROUND_LEVEL,@CO_X,@CO_Y,@SURVEY_ID,@SEDIMENT_DEPTH,@FLOW_STATE,"
               + "@BOTTOM_TYPE,@MANHOLE_MATERIAL,@MANHOLE_SHAPE,@MANHOLE_SIZE,@COVER_MATERIAL,@COVER_STATE,@Depth,@PointType,@GULLY_NUMBER,@INVERT_LEVEL)";
          IDataBase pDataBase = SysDBUnitiy.OleDataBase;
          pDataBase.OpenConnection();
          try
          {
              pDataBase.ExecuteNonQuery(sql, Parameters);
             
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

      public override void Update()
      {
          if (!string.IsNullOrEmpty(GROUND_LEVEL) && !string.IsNullOrEmpty(Depth))
              this.INVERT_LEVEL = (double.Parse(this.GROUND_LEVEL) - double.Parse(this.Depth)).ToString();
          SQLiteParameter[] Parameters = new SQLiteParameter[] 
           {
              // new SQLiteParameter(){ ParameterName="@SYSTEM_TYPE" ,Value=this.SYSTEM_TYPE},
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
               //new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=string.IsNullOrEmpty(this.INVERT_LEVEL)?DBNull.Value: (object) this.INVERT_LEVEL },
               //new SQLiteParameter(){ ParameterName="@MANHOLE_TYPE" ,Value=this.MANHOLE_TYPE },
               new SQLiteParameter(){ ParameterName="@SEDIMENT_DEPTH" ,Value=string.IsNullOrEmpty( this.SEDIMENT_DEPTH)?DBNull.Value:(object) this.SEDIMENT_DEPTH },
               new SQLiteParameter(){ ParameterName="@FLOW_STATE" ,Value=this.FLOW_STATE },
               new SQLiteParameter(){ ParameterName="@BOTTOM_TYPE" ,Value=this.BOTTOM_TYPE },
               new SQLiteParameter(){ ParameterName="@MANHOLE_MATERIAL" ,Value=this.MANHOLE_MATERIAL },
               new SQLiteParameter(){ ParameterName="@MANHOLE_SHAPE" ,Value=this.MANHOLE_SHAPE },
                 new SQLiteParameter(){ ParameterName="@MANHOLE_SIZE" ,Value=this.MANHOLE_SIZE },
               new SQLiteParameter(){ ParameterName="@COVER_MATERIAL" ,Value=this.COVER_MATERIAL },
               new SQLiteParameter(){ ParameterName="@COVER_STATE" ,Value=this.COVER_STATE },
               //new SQLiteParameter(){ ParameterName="@Dirtcion" ,Value=this.Dirction },
               new SQLiteParameter(){ ParameterName="@Depth" ,Value=this.Depth },
                new SQLiteParameter(){ ParameterName="@PointType" ,Value=this.Type },
              new SQLiteParameter(){ ParameterName="@GULLY_NUMBER" ,Value=string.IsNullOrEmpty( this.GULLY_NUMBER)?DBNull.Value:(object)int.Parse(this.GULLY_NUMBER) },
                new SQLiteParameter(){ ParameterName="@INVERT_LEVEL" ,Value=this.INVERT_LEVEL },
               new SQLiteParameter(){ ParameterName="@ID" ,Value=this.ID}
                //new SQLiteParameter(){ ParameterName="@ObjectID" ,Value=this.ObjectID,OleDbType= OleDbType.Integer}

           };
          string sql = string.Format("update Points set  ROAD_NAME=@ROAD_NAME,"
              + "ACQUISITION_DATE=@ACQUISITION_DATE,ACQUISITION_UNIT=@ACQUISITION_UNIT,PROCESS_UNIT=@PROCESS_UNIT,"
               + "PROCESS_DATE=@PROCESS_DATE,STATE=@STATE,REMARK=@REMARK,"
               + "GROUND_LEVEL=@GROUND_LEVEL,CO_X=@CO_X,CO_Y=@CO_Y,SURVEY_ID=@SURVEY_ID,"
               + "SEDIMENT_DEPTH=@SEDIMENT_DEPTH,FLOW_STATE=@FLOW_STATE,BOTTOM_TYPE=@BOTTOM_TYPE,"
               + " MANHOLE_MATERIAL=@MANHOLE_MATERIAL,MANHOLE_SHAPE=@MANHOLE_SHAPE,MANHOLE_SIZE=@MANHOLE_SIZE,COVER_MATERIAL=@COVER_MATERIAL,COVER_STATE=@COVER_STATE,"
               + " Depth=@Depth,PointType=@PointType,GULLY_NUMBER=@GULLY_NUMBER,INVERT_LEVEL=@INVERT_LEVEL "
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
      public override void FillValueByRow(System.Data.DataRow pDataRow )
      {
          base.FillValueByRow(pDataRow );
       

          if (pDataRow.Table.Columns.Contains("Depth"))
              this.Depth = pDataRow["Depth"].ToString();

          if (pDataRow.Table.Columns.Contains("PointType"))
              this.Type = pDataRow["PointType"].ToString();

          if (pDataRow.Table.Columns.Contains("GULLY_NUMBER"))
              this.GULLY_NUMBER = pDataRow["GULLY_NUMBER"].ToString();
      }
  }
}
