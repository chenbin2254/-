using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HR.Controls;
using System.Windows.Forms;
using System.Drawing;
using HR.Utility;
using System.Data;
using HR.Data;
using SRDMS.FTPClient.Bussiness;
using System.Data.OracleClient;

namespace CHXQ.XMManager
{
    class XMDirTreeView: ThreeStateTreeView
    {
        private ImageList pImageList = null;
        //private string DirTableName;
        private int pDirType = 0;
        public XMDirTreeView(int DirType=0)
        {
            this.CheckBoxes = false;
            pImageList = new ImageList();

            pImageList.Images.Add("0", Properties.Resources.Folder16);
            pImageList.Images.Add("1", Properties.Resources.FolderOpenState16);

            this.ImageList = pImageList;
            pImageList.ImageSize = new Size(16, 16);

            
            pDirType = DirType;
        }
        private int m_XMID=-1;
        public int XMID
        {
            get { return m_XMID; }
            set
            {
                m_XMID = value;
                if (m_XMID == -1) this.Nodes.Clear();
                else
                {
                    this.LoadData();
                    this.ExpandAll();
                }
            }
        }
        public void UploadFiles(string[] FilePaths, TreeNode pNode)
        {            
            IFtpClient pFtpClient = SysDBConfig.GetInstance().GetFtpClient("CHXQ");
            pFtpClient.Connect();
            IDataBase pDataBase = SysDBConfig.GetInstance().GetOleDataBase("OrclConn");
            pDataBase.OpenConnection();
            string DirPath = GetFtpPathByNode(pNode);
            try
            {
                if (!pFtpClient.DirectoryExists(DirPath))
                    pFtpClient.CreateDirectory(DirPath);
                foreach (string FilePath in FilePaths)
                {
                    string RemotePath = string.Format("{0}\\{1}", DirPath, System.IO.Path.GetFileName(FilePath));
                    pFtpClient.UploadFile(FilePath, RemotePath);
                    string ProcedureName = "AddFile";
                    IDataParameter[] DataParameters = new IDataParameter[] {
                        new OracleParameter(){ ParameterName="V_FileName", OracleType= OracleType.NVarChar, Value=System.IO.Path.GetFileNameWithoutExtension(FilePath)},
                        new OracleParameter(){ ParameterName="V_Path", OracleType= OracleType.NVarChar, Value=RemotePath},
                        new OracleParameter(){ ParameterName="V_DirID", OracleType= OracleType.Number, Value=this.GetNodeID(pNode)}
                    };
                    pDataBase.ExecuteProcedure(ProcedureName, ref DataParameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pDataBase.CloseConnection();
                pFtpClient.Close();
            }
        }
        public override DataTable GetDataSourceTable()
        {
            string sql = string.Format("select ID,Name,parent as parentid,0 as imagetype,path"
                + " from up_dirs where xmid={0} and Type={1}", m_XMID, pDirType);
            DataSet ds = SysDBConfig.GetInstance().GetOleDataBase("OrclConn").ExecuteQuery(sql);
            DataTable dt = ds.Tables[0];
            
            DataRow[] pRows = dt.Select(string.Format( "parentid=-1"));
            this.m_RootID = pRows[0]["ID"];
            return dt;
        }
        protected override TreeNode GetNodeByDataRow(DataRow pRow)
        {
            TreeNode pNode = base.GetNodeByDataRow(pRow);
            pNode.SelectedImageKey = "1";
            return pNode;
        }
        protected override System.Windows.Forms.ContextMenuStrip GetNodeContextMenu(System.Windows.Forms.TreeNode node)
        {
            ContextMenuStrip pContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();

            ToolStripMenuItem CreateSubDir = new ToolStripMenuItem();
            CreateSubDir.Text = "创建子目录";
            CreateSubDir.Click += new EventHandler(CreateSubDir_Click);

            ToolStripMenuItem ReName = new ToolStripMenuItem();
            ReName.Text = "重命名";
            ReName.Click += new EventHandler(ReName_Click);

            ToolStripMenuItem Delete = new ToolStripMenuItem();
            Delete.Text = "删除";
            Delete.Click += new EventHandler(Delete_Click);
            pContextMenuStrip.Items.AddRange(new ToolStripMenuItem[] { CreateSubDir, ReName, Delete });
            return pContextMenuStrip;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除该文件夹", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.Cancel)
                return;
            TreeNode pNode = this.SelectedNode;
            int ID = this.GetNodeID(pNode);

            IFtpClient pFtpClient = SysDBConfig.GetInstance().GetFtpClient("CHXQ");
            pFtpClient.Connect();
            pFtpClient.DeleteDirectory(GetFtpPathByNode(pNode));
            pFtpClient.Close();

            string sql = string.Format("delete from up_dirs where id={0}", ID);

            IDataBase pDataBase = SysDBConfig.GetInstance().GetOleDataBase("OrclConn");
            pDataBase.OpenConnection();
            pDataBase.ExecuteNonQuery(sql);
            pDataBase.CloseConnection();

            this.RefreshSubTreeView(pNode.Parent);
        }

        private void ReName_Click(object sender, EventArgs e)
        {
            this.LabelEdit = true;
            this.SelectedNode.BeginEdit();
        }
        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            string NewName = e.Label;
            TreeNode pNode = this.SelectedNode;
            string sql = string.Format("update up_dirs set name='{0}' where id={1}",
                NewName, this.GetNodeID(pNode));
            IDataBase pDataBase = SysDBConfig.GetInstance().GetOleDataBase("OrclConn");
            pDataBase.OpenConnection();
            pDataBase.ExecuteNonQuery(sql);
            pDataBase.CloseConnection();
            this.LabelEdit = false;
        }
        private void CreateSubDir_Click(object sender, EventArgs e)
        {
            TreeNode pNode = this.SelectedNode;
            int ID = this.GetNodeID(pNode);
            string ParentPath=this.GetFtpPathByNode(pNode);
            string sql = "select SEQ_UP_DIRS.nextval from dual";            
            IDataBase pDataBase = SysDBConfig.GetInstance().GetOleDataBase("OrclConn");
            pDataBase.OpenConnection();
            string DirID = pDataBase.ExecuteScalar(sql).ToString();
            string Path = string.Format("{0}/{1}", this.GetFtpPathByNode(pNode), DirID);
            sql = string.Format("insert into up_dirs values({0},'新建目录',{1},'{2}\\{0}',1)", DirID, ID, ParentPath);
            pDataBase.ExecuteNonQuery(sql);            
            pDataBase.CloseConnection();

            this.RefreshSubTreeView(pNode);
            pNode.ExpandAll();
            TreeNode NewNode = this.FindNodeByID(int.Parse(DirID));

            IFtpClient pFtpClient = SysDBConfig.GetInstance().GetFtpClient("CHXQ");
            pFtpClient.Connect();
            string FtpPath = GetFtpPathByNode(NewNode);
            pFtpClient.CreateDirectory(FtpPath);
            pFtpClient.Close();
            this.SelectedNode = NewNode;
            this.LabelEdit = true;
            NewNode.BeginEdit();
        }
        private string GetFtpPathByNode(TreeNode pNode)
        {
            
            DataRow pDataRow = pNode.Tag as DataRow;
            string Path = pDataRow["Path"].ToString();
            return Path;
        }
    }
}
