using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;

namespace CHXQ.XMManager
{
    public partial class OnlineTable : Form
    {
        public OnlineTable()
        {
            InitializeComponent();
            string PCRecords = GetPCRecordTable();
            XmlDocument ReturnXml = XmlUtil.StringToDocument(PCRecords);
            if (ReturnXml["QueryRtObject"]["ReturnCode"].InnerText != "1") return;
            string RetureJoson = ReturnXml["QueryRtObject"]["JsonData"].InnerText;
            DataTable pDataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(RetureJoson);
            superGridControl1.PrimaryGrid.DataSource = pDataTable;
            //Newtonsoft.Json.Linq.JArray pJArray = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(RetureJoson);
        }

        private string GetPCRecordTable()
        {
            string json = "";
            try
            {
                string PostParam ="querySql=select * from view_ps_gxpc_gx  ";
                //string id = "id=" + ID;
                string url = CIni.ReadINI("updateconfig", "DataUrl");
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //request.Accept = "text/html, application/xhtml+xml, */*";
                request.Accept = "application/json";
                request.ContentType = "application/x-www-form-urlencoded";
                //request.Connection = "Keep-Alive";
                byte[] buffer = encoding.GetBytes(PostParam);
                request.ContentLength = buffer.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
                requestStream.Close();
                responseStream.Close();
            }
            catch (Exception e)
            {
                throw e;
                //ExceptionLog.LogError(ID, e);
                //return "";
                //json = e.Message;
            }
            //ExceptionLog.LogError(ID + ":" + json, null);
            return json;
        }
    }
}
