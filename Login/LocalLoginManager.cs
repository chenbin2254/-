using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Data;
using System.IO;
using System.Xml;
using HR.Utility;
using HR.Data;


namespace CHXQ.XMManager
{
    public static class LocalLoginManager
    {
       
        /// <summary>
        /// 验证登录信息
        /// </summary>
        /// <param name="UserName">登录用户名</param>
        /// <param name="Password">登录密码</param>       
        /// <returns></returns>
        public static bool ValidateLogin(string UserName, string Password,ref string UserID)
        {
            //string EncryptPassword = AG.COM.SDM.Utility.Security.EncryptDES(Password.Trim());
            string pUser = UserName.ToLower();
            //string EncryptPassword = Security.EncryptDES(Password).ToLower();
            string strHQL;
            if (!string.IsNullOrEmpty(Password))
                strHQL = string.Format("select t.ID from hr_staff  t where lower(t.Name)='{0}' and lower(t.PASSWORD)='{1}'", pUser, Password);
            else
                strHQL = string.Format("select t.ID from hr_staff  t where lower(t.Name)='{0}' and lower(t.PASSWORD) is null", pUser);
            IDataBase pDataBase = SysDBConfig.GetInstance().GetOleDataBase();
            pDataBase.OpenConnection();
            object value = pDataBase.ExecuteScalar(strHQL);
            pDataBase.CloseConnection();
            if (value != null)
            {
                UserID = value.ToString();                
                return true;
            }
            else
            {
                UserID =string.Empty;
                return false;
            }
        }

        /// <summary>
        /// 通过用户名获取本地存储的用户信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static List<LocalLoginUser> GetLocalUsers()
        {            
            string LocalLoginUserFile = CommonConstString.STR_DataPath + "\\LocalLoginUsers.xml";
            if (!System.IO.File.Exists(LocalLoginUserFile))
            {
               XmlDocument pXmlDocument= XmlUtil.CreateXmlDocument("LocalLoginUsers");
               pXmlDocument.Save(LocalLoginUserFile);
                return new List<LocalLoginUser>();
            }
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(LocalLoginUserFile);
                List<LocalLoginUser> LocalLoginUserList = new List<LocalLoginUser>();
                if (ds.Tables.Count == 0)
                    return LocalLoginUserList;

                foreach (DataRow pRow in ds.Tables[0].Rows)
                {
                    LocalLoginUser pLocalLoginUser = new LocalLoginUser();
                    pLocalLoginUser.FillData(pRow);
                    pLocalLoginUser.DecryptPassword();
                    LocalLoginUserList.Add(pLocalLoginUser);
                }
                return LocalLoginUserList;
            }
            catch { return new List<LocalLoginUser>(); }
        }
        /// <summary>
        /// 将本地登录的信息存储起来
        /// </summary>
        /// <param name="pLocalLoginUser"></param>
        public static void AddLocalLoginInfo(LocalLoginUser pLocalLoginUser)
        {
            string LocalLoginUserFile = CommonConstString.STR_DataPath + "\\LocalLoginUsers.xml";
            pLocalLoginUser.EncryptpassWord();
            XmlDocument pLocalLoginUserDoc= XmlUtil.LoadXmlDocument(LocalLoginUserFile);           
            XmlUtil.AddXmlElementByDataTable(pLocalLoginUserDoc, pLocalLoginUserDoc["LocalLoginUsers"], pLocalLoginUser.ConvertToDataTable().Table);
            pLocalLoginUserDoc.Save(LocalLoginUserFile);          
        }
        /// <summary>
        /// 保存登录信息
        /// </summary>
        /// <param name="pLocalLoginUser"></param>
        public static void SaveLoginInfo(LocalLoginUser pLocalLoginUser)
        {
            string LocalLoginUserFile = CommonConstString.STR_DataPath + "\\LocalLoginUsers.xml";
            XmlDocument pLocalLoginUserDoc = XmlUtil.LoadXmlDocument(LocalLoginUserFile);
            XmlElement RootElement = pLocalLoginUserDoc["LocalLoginUsers"];
            foreach (XmlNode pUserElement in RootElement.ChildNodes)
            {
                if (string.Equals(pUserElement["UserName"].InnerText, pLocalLoginUser.UserName, StringComparison.OrdinalIgnoreCase))
                {
                    pLocalLoginUser.EncryptpassWord();
                    pUserElement["Password"].InnerText = pLocalLoginUser.PassWord;
                }
            }
            pLocalLoginUserDoc.Save(LocalLoginUserFile);   
 
        }
        /// <summary>
        /// 移除指定用户的本地登录信息
        /// </summary>
        /// <param name="UserName"></param>
        public static void RemoveLocalLoginInfo(string UserName)
        {
            string LocalLoginUserFile = CommonConstString.STR_DataPath + "\\LocalLoginUsers.xml";
            XmlDocument pLocalLoginUserDoc = XmlUtil.LoadXmlDocument(LocalLoginUserFile);
            XmlElement RootElement = pLocalLoginUserDoc["LocalLoginUser"];
            foreach (XmlNode pUserElement in RootElement.ChildNodes)
            {
                if (string.Equals(pUserElement["UserName"].InnerText, UserName, StringComparison.OrdinalIgnoreCase))
                {
                    RootElement.RemoveChild(pUserElement);
                   
                }
            }
            pLocalLoginUserDoc.Save(LocalLoginUserFile);   
        }
    }
}
