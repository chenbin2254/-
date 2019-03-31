using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CHXQ.XMManager
{
  public  class Pipeobject
    {
      public static IPipeData GetDataByID(string ID, string TableName)
      {
          IPipeData pPipeData = null;
          if (TableName.Equals("PS_MANHOLE"))
          {
              
              pPipeData = new MANHOLE();
          }
          else if (TableName.Equals("PS_VIRTUAL_POINT"))
          {
            
              pPipeData = new PipePoint();
          }
          else if (TableName.Equals("PS_COMB"))
          {
              
              pPipeData = new COMB();

          }
          else if (TableName.Equals("PS_OUTFALL"))
          {
             
              pPipeData = new OUTFALL();

          }

          else if (TableName.Equals("PS_PUMP_STORAGE"))
          {
              
              pPipeData = new PUMP();
          }
          else if (TableName.Equals("PS_PIPE"))
          {
              
              pPipeData = new PIPELineClass();

          }
          else if (TableName.Equals("PS_CANAL"))
          {
             
              pPipeData = new CANAL();

          }
          else if (TableName.Equals("PS_FLAP"))
          {
            
              pPipeData = new FLAP();

          }
          else if (TableName.Equals("PS_WEIR"))
          {
            
              pPipeData = new WEIR();

          }
          else if (TableName.Equals("PS_SLUICE"))
          {
              
              pPipeData = new SLUICE();
          }
          else
          {
         
              pPipeData = null;

          }
          if (pPipeData == null) return null;
          string sql = string.Format("select * from {0} where ID='{1}'", TableName, ID);
          DataTable pTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
          pPipeData.FillValueByRow(pTable.Rows[0], TableName);
          return pPipeData;
      }
    }
}
