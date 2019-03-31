using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
 

namespace CHXQ.XMManager
{
    public partial class QueryFrm : Form
    {
        public QueryFrm()
        {
            InitializeComponent();
            QueryItem ManholeItem = new QueryItem() { QueryItemName="检查井", QueryClassName="PS_Manhole" };
            ManholeItem.QueryFields = new QueryField[] {                   
                new QueryField() { FieldName = "ID", ItemName = "检查井编码" , pDataType= DataType.text},
                new QueryField() { FieldName = "SURVEY_ID", ItemName = "摸查点号" , pDataType= DataType.text},
               new QueryField() { FieldName = "SYSTEM_TYPE", ItemName = "属性" , pDataType= DataType.text},
                new QueryField() { FieldName = "GROUND_LEVEL", ItemName = "井盖高程" , pDataType= DataType.number},
                new QueryField() { FieldName = "INVERT_LEVEL", ItemName = "井底高程" , pDataType= DataType.number},
                new QueryField() { FieldName = "WATER_LEVEL", ItemName = "水位高程" , pDataType= DataType.number},
                new QueryField() { FieldName = "SEDIMENT_DEPTH", ItemName = "淤积深度" , pDataType= DataType.number},
                 new QueryField() { FieldName = "COVER_MATERIAL", ItemName = "井盖材质" , pDataType= DataType.text},
                 new QueryField() { FieldName = "MANHOLE_MATERIAL", ItemName = "井室材质" , pDataType= DataType.text},
                 new QueryField() { FieldName = "BOTTOM_TYPE", ItemName = "井底形式" , pDataType= DataType.text},
                 new QueryField() { FieldName = "MANHOLE_SHAPE", ItemName = "井室类型" , pDataType= DataType.text},
                 new QueryField() { FieldName = "MANHOLE_SIZE", ItemName = "井室尺寸" , pDataType= DataType.text},
                 new QueryField() { FieldName = "ROAD_NAME", ItemName = "所在道路" , pDataType= DataType.text}
                 
            };

            QueryItem PipePointItem = new QueryItem() { QueryItemName = "管线点", QueryClassName = "PS_VIRTUAL_POINT" };
            PipePointItem.QueryFields = new QueryField[] {
                new QueryField() { FieldName = "ID", ItemName = "管线点编码" , pDataType= DataType.text},
                new QueryField() { FieldName = "SURVEY_ID", ItemName = "摸查点号" , pDataType= DataType.text},
                 new QueryField() { FieldName = "GROUND_LEVEL", ItemName = "地面高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "INVERT_LEVEL", ItemName = "底部高程" , pDataType= DataType.number},
                 new QueryField() { FieldName = "ROAD_NAME", ItemName = "所在道路" , pDataType= DataType.text}
            };

            CmbDataType.Items.AddRange(new QueryItem[] { ManholeItem, PipePointItem });

            CmbDataType.SelectedIndexChanged += new EventHandler(CmbDataType_SelectedIndexChanged);
            CmbFields.SelectedIndexChanged += new EventHandler(CmbFields_SelectedIndexChanged);
        }

        private void CmbFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbFields.SelectedIndex == -1) return;
            QueryField pQueryField = CmbFields.SelectedItem as QueryField;
            CmbCalc.Items.Clear();

            if (pQueryField.pDataType == DataType.text)
            {
                CmbCalc.Items.AddRange(new string[]{"=","like"} );
 
            }
            else if (pQueryField.pDataType == DataType.number)
            {
                CmbCalc.Items.AddRange(new string[] { ">=","<=", "=" }); 
            }
        }

       private void CmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbDataType.SelectedIndex == -1) return;
            QueryItem pQueryItem = CmbDataType.SelectedItem as QueryItem;
            CmbFields.Items.Clear();
            CmbFields.Items.AddRange(pQueryItem.QueryFields);
            TxbQueryValue.Clear();
           
        }

       private void BtnQuery_Click(object sender, EventArgs e)
       {
           if (CmbDataType.SelectedIndex == -1) return;
           if (CmbFields.SelectedIndex == -1) return;
           if(CmbCalc.SelectedIndex==-1 ) return;
           if (TxbQueryValue.Text == string.Empty) return;

           ClassName = (CmbDataType.SelectedItem as QueryItem).QueryClassName;
           string FieldName = (CmbFields.SelectedItem as QueryField).FieldName;
           string Calc = CmbCalc.Text;
           string KeyValue = TxbQueryValue.Text;

           string QueryField="ObjectID as 序号";
           QueryField += string.Format(",{0} as {1}", FieldName, (CmbFields.SelectedItem as QueryField).ItemName);
                
           string where =" where ";
           if((CmbFields.SelectedItem as QueryField).pDataType== DataType.text )
           {
               if(Calc.Equals("like", StringComparison.CurrentCultureIgnoreCase) )
               {
                   where+=string.Format(" {0} {1}  '?{2}?'",FieldName,Calc,KeyValue);
               }
               else
               {
                   where+=string.Format(" {0} {1}  '{2}'",FieldName,Calc,KeyValue);
               }
           }
           else
           {
               where+=string.Format(" {0} {1}  {2} ",FieldName,Calc,KeyValue);
           }
           string sql = string.Format("select {0} from {1} {2}",
               QueryField, ClassName, where);
           QueryTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
           this.Close();

       }

       public DataTable QueryTable = null;
       public string ClassName;
       private void BtnQueryAll_Click(object sender, EventArgs e)
       {
           if (CmbDataType.SelectedIndex == -1) return;
           ClassName = (CmbDataType.SelectedItem as QueryItem).QueryClassName;
           string sql = string.Format("select ObjectID as 序号,ID as 设施编号 from {0}", ClassName);
           QueryTable = SysDBUnitiy.OleDataBase.ExecuteQuery(sql).Tables[0];
           this.Close();
       }

      
    }
    public class QueryItem
    {
        public string QueryItemName;
        public string QueryClassName;
        public QueryField[] QueryFields=null;
        public override string ToString()
        {
            return QueryItemName;
        }
    }
    public class QueryField
    {
        public string ItemName;
        public string FieldName;
        public DataType pDataType = DataType.text;
        public override string ToString()
        {
            return ItemName;
        }
    }
    public enum DataType
    {
        text,
        number
    }
}
