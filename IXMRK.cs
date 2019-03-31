using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HR.Geometry;
using HR.Data;
using System.Data;
using System.Data.OracleClient;
using SRDMS.FTPClient.Bussiness;

namespace CHXQ.XMManager
{
   public interface IXMRK:IRK
    {
        /// <summary>
        /// 立项编号
        /// </summary>
       string LXBH { get; set; }
       /// <summary>
       /// 立项时间
       /// </summary>
       DateTime LASJ { get; set; }
       /// <summary>
       /// 项目名称
       /// </summary>
        string XMMC { get; set; }
       
        
       
    }
   public class XMRKClass : RKClass, IXMRK
   {
       public XMRKClass()
       {
         //  service_url = @"GTZT/XMYD:Mapserver";
           service_url = HR.Utility.CommonVariables.GetAppSetString("XMYD");
       }

       private string m_LXBH;
       public string LXBH { get { return m_LXBH; } set { m_LXBH = value; } }

       private DateTime m_LASJ;
       public DateTime LASJ { get { return m_LASJ; } set { m_LASJ = value; } }
       
       /// <summary>
       /// 项目名称
       /// </summary>
       public string XMMC { get; set; }
       
       protected override void FillByRow(DataRow pRow)
       {
           base.FillByRow(pRow);
           //添加本接口属性代码       
           
           this.XMMC = pRow["XMMC"].ToString();           
           this.m_LXBH = pRow["LABH"].ToString();
           this.m_LASJ = !string.IsNullOrEmpty(pRow["LASJ"].ToString()) ?
               DateTime.ParseExact(pRow["LASJ"].ToString(), "yyyy-MM-dd",
               System.Globalization.CultureInfo.CurrentCulture) : DateTime.MinValue;
       }

       public override void Save()
       {
          
           IDataBase pDataBase = HR.Utility.SysDBConfig.GetInstance().GetOleDataBase("OrclConn");
           pDataBase.OpenConnection();
           pDataBase.BeginTransaction();
          // string ProcedureName;
           //IDataParameter[] DataParameters;
           try
           {
               string sql = string.Format("update CHXQ_XMTABLE t set t.XMNAME='{0}',t.LABH='{1}',t.LASJ=to_date('{2}','YYYY-MM-DD HH24:MI:SS'),t.ISACTIVE=1 where t.ID={3}"
                   , this.XMMC, this.LXBH, this.LASJ, this.ID);
               pDataBase.ExecuteNonQuery(sql);
               base.Save();
               pDataBase.Commit();
           }
           catch (Exception ex)
           {
               pDataBase.Rollback();
               throw ex;
           }
           finally
           {
               pDataBase.CloseConnection();
           }
       }
       public static IXMRK GetXMRKByID(int XMID)
       {
           string sql = string.Format("select XMName,LABH,to_char(LASJ,'yyyy-mm-dd') LASJ from CHXQ_XMTable where ID={0}",XMID);

           IDataBase pDataBase = HR.Utility.SysDBConfig.GetInstance().GetOleDataBase("OrclConn");
           DataTable ptable = pDataBase.ExecuteQuery(sql).Tables[0];
           if (ptable.Rows.Count == 0) return null;
           IXMRK pXMRK = new XMRKClass();
         
           (pXMRK as XMRKClass).FillByRow(ptable.Rows[0]);
           return pXMRK;
 
       }
       /*
       public static IXMRK GetXMRKByTZBH(string TZBH)       
       {
           string sql = string.Format("select ObjectID,XMMC,XMBH,DM,BLC,to_char(CLSJ,'yyyy-mm-dd') CLSJ,"
            + "to_char(CTSJ,'yyyy-mm-dd') CTSJ,CLGS,CadPath,XMMJ,ZDMJ,TZBH,round(t.Shape.area,2) as Area  from xmyd t where TZBH='{0}'", TZBH);
           IDataBase pDataBase = HR.Utility.SysDBConfig.GetInstance().GetOleDataBase("SdeOrclConn");
           DataTable ptable = pDataBase.ExecuteQuery(sql).Tables[0];
           if (ptable.Rows.Count == 0) return null;

           IXMRK pXMRK = new XMRKClass();
           (pXMRK as XMRKClass).FillByRow(ptable.Rows[0]);
           return pXMRK;
       }*/
       public static IXMRK[] GetXMRKByXMBH(string XMBH)
       {
           string sql = string.Format("select ObjectID,XMMC,XMBH,DM,BLC,to_char(CLSJ,'yyyy-mm-dd') CLSJ,"
            + "to_char(CTSJ,'yyyy-mm-dd') CTSJ,CLGS,CadPath,XMMJ,ZDMJ,TZBH,round(t.Shape.area,2) as Area  from xmyd t where XMBH  like '{0}%' or XMBH='{0}'", XMBH);
           IDataBase pDataBase = HR.Utility.SysDBConfig.GetInstance().GetOleDataBase("SdeOrclConn");
           DataTable ptable = pDataBase.ExecuteQuery(sql).Tables[0];
           if (ptable.Rows.Count == 0) return null;

           IXMRK[] XMRKs = new IXMRK[ptable.Rows.Count];
           int i = 0;
           foreach (DataRow pRow in ptable.Rows)
           {
               IXMRK pXMRK = new XMRKClass();
               (pXMRK as XMRKClass).FillByRow(ptable.Rows[0]);
               XMRKs[i] = pXMRK;
               i++;
           }
           return XMRKs;
       }
       public override string ToString()
       {
           return this.XMMC;
       }
       public override void Delete()
       {
           
           IDataBase pDataBase = HR.Utility.SysDBConfig.GetInstance().GetOleDataBase("SdeOrclConn");
           pDataBase.OpenConnection();
           pDataBase.BeginTransaction();
           string ProcedureName;
           IDataParameter[] DataParameters;
           try
           {
               DataParameters = new IDataParameter[] { new OracleParameter() { ParameterName = "V_XMID", OracleType= OracleType.Number,Value=this.m_ID } };
               ProcedureName = "sde.DeleteXMYD";
               pDataBase.ExecuteProcedure(ProcedureName, ref DataParameters);
               ProcedureName = "chxq.DeleteXMYD";
               pDataBase.ExecuteProcedure(ProcedureName, ref DataParameters);
               pDataBase.Commit();
           }
           catch (Exception ex)
           {
               pDataBase.Rollback();
               throw ex; 
           }
           finally
           {
               pDataBase.CloseConnection(); 
           }
       }
   }
}
