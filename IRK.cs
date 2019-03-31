using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HR.Geometry;
using System.Data;
using SRDMS.FTPClient.Bussiness;
using HR.Data;
using System.Net;
using System.IO;
using System.Web;
using HR.Utility;

namespace CHXQ.XMManager
{
    public interface IRK
    {
        int ID { get;  }
        string CADPath { get; }
        /// <summary>
        /// 数据更新或入库
        /// </summary>
        void Save();
        /// <summary>
        /// 数据删除
        /// </summary>
        void Delete();

        /// <summary>
        /// 入库图形
        /// </summary>
        ISTGeometry STGeometry { get; set; }
        void UploadCAD(string CadPath);
        /// <summary>
        /// 下载CAD图纸到本地
        /// </summary>
        /// <param name="SavePath"></param>
        void DownloadAD(string SavePath);
        void UpdateMapServerCache(double XMin, double YMin, double XMax, double YMax);
      //  string UploadCADPath { get; set; }
        bool GetIsGeoCange();

        /// <summary>
        /// CAD入库图形原始点集合
        /// </summary>
        ISTPointCollection SoucePoints { get; set; }
    }
    public abstract class RKClass : IRK
    {
        protected int m_ID=-1;
       public int ID { get { return m_ID; } }
       protected bool GeometryChange = false;
       public bool GetIsGeoCange()
       {
           return GeometryChange;
       }
       protected ISTGeometry m_STGeometry = null;
       public ISTGeometry STGeometry { get { return m_STGeometry; } set { m_STGeometry = value; GeometryChange = true; } }
       protected string m_CADPath;
       public string CADPath { get { return m_CADPath; } }
        public abstract void Save();
        public abstract void  Delete();
      //  public string UploadCADPath { get; set; }
        protected ISTPointCollection pSoucePoints = null;
        public ISTPointCollection SoucePoints
        {
            get
            {
                if (pSoucePoints == null && this.m_ID != -1)
                {
                    pSoucePoints = new STPointCollectionClass();
                    string sql = string.Format("select x,y from zd_points where RKID={0}  order by IndexID", this.m_ID);
                    DataTable CTable = SysDBConfig.GetInstance().GetOleDataBase("SdeOrclconn").ExecuteQuery(sql).Tables[0];
                    for (int j = 0; j < CTable.Rows.Count; j++)
                    {
                        ISTPoint pSTPoint = new STPointClass()
                        {
                            X = double.Parse(CTable.Rows[j]["x"].ToString()),
                            Y = double.Parse(CTable.Rows[j]["y"].ToString())
                        };
                        pSoucePoints.AddPoint(pSTPoint);

                    }
                }
                return pSoucePoints;
            }
            set { pSoucePoints = value; }
        }
        protected virtual void FillByRow(DataRow pRow)
        {
            m_ID = Int32.Parse(pRow["ObjectID"].ToString());
            m_CADPath = pRow["CADPath"].ToString();
        }
        private void UploadFile(string FilePath, string UploadPath)
        {
            IFtpClient pFtpClient = HR.Utility.SysDBConfig.GetInstance().GetFtpClient("CHXQ");
            pFtpClient.Connect();
            string Dir = System.IO.Path.GetDirectoryName(UploadPath);
            if (!pFtpClient.DirectoryExists(Dir))
            {
                pFtpClient.CreateDirectory(Dir);
            }
            if (pFtpClient.Exists(UploadPath))
                pFtpClient.DeleteFile(UploadPath);
            pFtpClient.UploadFile(FilePath, UploadPath);
            pFtpClient.Close();

        }
        public void UploadCAD(string CadPath)
        {
          
                UploadFile(CadPath, m_CADPath);
          
           
        }
        public void DownloadAD(string SavePath)
        {
            if (string.IsNullOrEmpty(m_CADPath))
                throw new Exception("图纸未上传");
            IFtpClient pFtpClient = HR.Utility.SysDBConfig.GetInstance().GetFtpClient("CHXQ");
            pFtpClient.Connect();
            try
            {
                pFtpClient.DownloadFile(SavePath, this.m_CADPath);
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
        protected string service_url = string.Empty;
        public void UpdateMapServerCache(double XMin, double YMin, double XMax, double YMax)
        {
            UpdateCacheConfig pUpdateCacheConfig = UpdateCacheConfig.GetUpdateCacheConfig();
            string UpdateCacheUrl = pUpdateCacheConfig.Url;
            string UpdateExtent = string.Format("{0},{1},{2},{3}", XMin, YMin, XMax, YMax);
            UpdateExtent = "{" + UpdateExtent + "}";
            Encoding myEncoding = Encoding.GetEncoding("gb2312");
            string data = "service_url=" + HttpUtility.UrlEncode(service_url, myEncoding);
            data += string.Format("&levels={0}&thread_count={1}&update_mode=RECREATE_ALL_TILES&constraining_extent=", pUpdateCacheConfig.Levels, pUpdateCacheConfig.Thread);
            data += UpdateExtent;
            PostData(UpdateCacheUrl, data);         
        }
        private string PostData(string url, string data)
        {

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=gb2312";
            byte[] bs = Encoding.ASCII.GetBytes(data);
            request.ContentLength = bs.Length;
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(bs, 0, bs.Length);
            reqStream.Close();

            string responseString = null;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                responseString = reader.ReadToEnd();
                reader.Close();
            }
            return responseString;
        }
    }
}
