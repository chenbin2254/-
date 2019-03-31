using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HR.Utility;
using System.Xml;

namespace CHXQ.XMManager
{
    class UpdateCacheConfig
    {
        private string pUrl;
        public string Url { get { return pUrl; } }
        private string pLevels;
        public string Levels
        { get { return pLevels; } }
        private string pThread;
        public string Thread { get { return pThread; } }
        private static UpdateCacheConfig pUpdateCacheConfig = null;
        public static UpdateCacheConfig GetUpdateCacheConfig()
        {
            if (pUpdateCacheConfig == null)
            {
                string ConfigPath = CommonConstString.STR_DataPath + "\\SysConfig.xml";
                XmlDocument pConfigDoc = XmlUtil.LoadXmlDocument(ConfigPath);
                pUpdateCacheConfig = UpdateCacheConfig.Create(pConfigDoc["SysComfig"]["CacheConfig"]);
            }
            return pUpdateCacheConfig;
        }

        private static UpdateCacheConfig Create(System.Xml.XmlNode section)
        {
            UpdateCacheConfig pConfig = new UpdateCacheConfig();
            if (section["Url"] != null)
                pConfig.pUrl = section["Url"].InnerText;
            if (section["levels"] != null)
                pConfig.pLevels = section["levels"].InnerText;
            if (section["thread"] != null)
                pConfig.pThread = section["thread"].InnerText;

            return pConfig;
        }
    }
}
