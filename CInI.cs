using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
namespace CHXQ.XMManager
{
    class CIni
    {
        //声明读写INI文件的API函数    
        public static string path = SysDBUnitiy.RootDir + "\\Config.ini";
        public CIni()
        {
        }
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// ini文件编写器
        /// </summary>
        /// <param name="section">查找ini文件的节点[]名</param>
        /// <param name="key">节点下边的键</param>
        /// <param name="val">节点的值</param>
        /// <param name="filePath">来自的文件</param>
        public static void WriterINI(string section, string key, string val)
        {
            //path = System.Environment.CurrentDirectory + "\\" + filePath;
            WritePrivateProfileString(section, key, val, path);
        }
        /// <summary>
        /// 读取Ini文件
        /// </summary>
        /// <param name="section">获得节点</param>
        /// <param name="key">节点下边的键</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回的值</returns>
        public static string ReadINI(string section, string key)
        {
            //path = System.Environment.CurrentDirectory + "\\" + filePath;
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString().Trim();
        }
    }
}
 
   