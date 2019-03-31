using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HR.Utility;


//using AG.COM.UIDesign.Utility.Common;
namespace CHXQ.XMManager
{
    public class LocalLoginUser
    {
        //Security pSecurity = new Security();
        private string _UserName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        private string _PassWord;
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get
            {
                return _PassWord;
            }
            set
            {
                _PassWord=value;
            }
        }

        public void FillData(DataRow pRow)
        {
            if (pRow.Table.Columns.Contains("UserName"))
                _UserName = pRow["UserName"].ToString();
            if (pRow.Table.Columns.Contains("Password"))
                _PassWord = pRow["Password"].ToString();
        }
        public DataRow ConvertToDataTable()
        {
            DataTable pTable = new DataTable("LocalLoginUsers");
            pTable.Columns.Add("UserName");
            pTable.Columns.Add("Password");

            DataRow pRow = pTable.NewRow();
            pRow[0] = _UserName;
            pRow[1] = _PassWord;
            pTable.Rows.Add(pRow);
            return pRow;            
        }

        public void EncryptpassWord()
        {
            _PassWord = Security.EncryptDES(_PassWord);
            
            //_PassWord
        }
        public void DecryptPassword()
        {
            _PassWord = Security.DecryptDES(_PassWord);
        }

    }
}
