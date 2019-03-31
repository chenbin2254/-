using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HR.Data;
using System.IO;
using System.Reflection;


namespace CHXQ.XMManager
{
    static class SysDBUnitiy
    {
        public static string MDBPath = string.Empty;
        public static IDataBase OleDataBase
        {
            get
            {
                string Conn = string.Format("Pooling=true;FailIfMissing=false;Data Source={0}", MDBPath);
                //string Conn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", MDBPath);

                IDataBaseFactory pDataBaseFactory = new SQLiteDatabaseFactoryClass(Conn);
                IDataBase pDataBase = pDataBaseFactory.Open();
                return pDataBase;
            }
        }
        public static IDataBase SysDataBase
        {
            get
            {
                string Conn = string.Format("Data Source={0};Pooling=true;FailIfMissing=false", RootDir + "\\sys.db");
                IDataBaseFactory pDataBaseFactory = new SQLiteDatabaseFactoryClass(Conn);
                IDataBase pDataBase = pDataBaseFactory.Open();
                return pDataBase;
            }
        }
        public static string RootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //public static string YingshesPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\字段映射配置";
    }
}
