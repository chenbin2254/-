using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HR.Utility;
using SRDMS.FTPClient.Bussiness;
using HR.Data;

namespace CHXQ.XMManager
{
    public interface IFile
    {
        int ID { get; }
        string Name { get; }
        string Path { get; }
        void Delete();
        void DownloadFile(string SavePath);
    }
    public abstract class FileClass : IFile
    {
        protected int m_ID;
        protected string m_Name;
        protected string m_Path;
        public int ID { get { return m_ID; } }
        public string Path { get { return m_Path; } }
        public string Name { get { return m_Name; } }
    
        public abstract void Delete();
        public FileClass(int ID) {
            m_ID = ID;
        }
        public override string ToString()
        {
            return m_Name;
        }
        public virtual void DownloadFile(string SavePath)
        {
            IFtpClient pFtpClient = SysDBConfig.GetInstance().GetFtpClient("CHXQ");
            pFtpClient.Connect();
            try
            {
                pFtpClient.DownloadFile(SavePath, this.Path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pFtpClient.Close();
            }

        }
    }
   
    public class FtpFile : FileClass
    {
    
        public FtpFile(int ID):base(ID)
        {
            string sql = string.Format("select Name,Path from up_files where ID={0}", ID);
            DataTable pTable = SysDBConfig.GetInstance().GetOleDataBase("OrclConn").ExecuteQuery(sql).Tables[0];
            if (pTable.Rows.Count > 0)
            {
                m_Name = pTable.Rows[0]["Name"].ToString();
                m_Path = pTable.Rows[0]["Path"].ToString();
                m_ID = ID;
            }
        }
        public override void Delete()
        {
            IFtpClient pFtpClient = SysDBConfig.GetInstance().GetFtpClient("CHXQ");
            pFtpClient.Connect();
            try
            {
                pFtpClient.DeleteFile(this.Path);
                string sql = string.Format("delete from up_files where ID={0}", this.m_ID);
                IDataBase OrclDataBase = HR.Utility.SysDBConfig.GetInstance().GetOleDataBase("OrclConn");
                OrclDataBase.OpenConnection();
                OrclDataBase.ExecuteNonQuery(sql);
                OrclDataBase.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pFtpClient.Close();
            }

        }

    }
}
