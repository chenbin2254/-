using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevComponents.DotNetBar.Controls;
using System.Drawing;
using System.Runtime.InteropServices;

using System.Windows.Forms;
using Microsoft.Win32;
using HR.Controls;
using HR.Utility;
using System.Data;

namespace CHXQ.XMManager
{
    class FileListView : ListViewEx
    {
        private ImageList m_ImageList = null;
        public FileListView()
        {
            this.AllowDrop = true;
            this.ListViewItemSorter = new FileListViewIndexComparer();
            this.MultiSelect = false;
            this.InsertionMark.Color = Color.Red;
            //this.View = System.Windows.Forms.View.List;
            this.View = System.Windows.Forms.View.Tile;
            m_ImageList=new ImageList(){ ImageSize=new Size(32,32)};
            this.SmallImageList = m_ImageList;
            this.StateImageList = m_ImageList;
            this.LargeImageList = m_ImageList;

            //m_ImageList.Images.Add("0", Properties.Resources.Folder32);
            //m_ImageList.Images.Add("1", Properties.Resources.DraftsFolder32);
            //m_ImageList.Images.Add("2", Properties.Resources.FolderWhite32);

            this.ItemDrag += FileListView_ItemDrag;
            this.DragEnter += FileListView_DragEnter;
            this.DragOver += FileListView_DragOver;
            this.DragLeave += FileListView_DragLeave;
            this.DragDrop += FileListView_DragDrop;

            
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            ListViewItem ClickItem = this.GetItemAt(e.X, e.Y);
            if (ClickItem != null)
            {
                IFile YWFile = ClickItem.Tag as IFile;
                if (YWFile != null)
                {
                    string TempPath = System.IO.Path.GetTempPath() +  System.IO.Path.GetFileName(YWFile.Path);
                    YWFile.DownloadFile(TempPath);
                    System.Diagnostics.Process pExecuteEXE = new System.Diagnostics.Process();
                    pExecuteEXE.StartInfo.FileName = TempPath;
                    pExecuteEXE.Start();
                }
 
            }
        }
        public void LoadFileNamesByDir(int DirID)
        {

            string sql = string.Format("select ID from up_files where  Dir={0} order by sortid", DirID);
            DataSet ds = SysDBConfig.GetInstance().GetOleDataBase("OrclConn"). ExecuteQuery(sql);
            DataTable dt = ds.Tables[0];
            IFile[] pFiles = new IFile[dt.Rows.Count];
            for (int i = 0; i < pFiles.Length; i++)
            {
                pFiles[i] = new FtpFile(int.Parse(dt.Rows[i]["ID"].ToString()));
            }
            LoadFileNamesList(pFiles);
        }
        public void LoadFileNamesList(IFile[] FileNames)
        {
            this.Items.Clear();
            for (int i = 0; i < FileNames.Length; i++)
            {
                string FileExt = System.IO.Path.GetExtension(FileNames[i].Path);
                if (!m_ImageList.Images.ContainsKey(FileExt))
                {
                    Icon largeIcon, smallIcon;
                    string description = null;
                    GetExtsIconAndDescription(FileExt, out largeIcon, out smallIcon, out description);
                    //Icon pIcon = GetExtsIconAndDescription(YWFile.Name);
                    m_ImageList.Images.Add(FileExt, largeIcon);
                }
                ListViewItem pFileItem = new ListViewItem();
                pFileItem.Name = FileNames[i].ID.ToString();
                if (FileNames[i].Name.Length > 8)
                    pFileItem.Text = FileNames[i].Name.Substring(0, 8) +"\n"+FileNames[i].Name.Substring(8);
                else
                {
                    pFileItem.Text = FileNames[i].Name;
                }
                pFileItem.Tag = FileNames[i].Path;
                pFileItem.ImageKey = FileExt;
                this.Items.Add(pFileItem);

            }
          
        }
       
        #region Win32 API
        [System.Runtime.InteropServices.DllImportAttribute("shell32.dll", EntryPoint = "ExtractIconExW", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint ExtractIconExW
            ([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpszFile, int nIconIndex, ref System.IntPtr phiconLarge, ref System.IntPtr phiconSmall, uint nIcons);

        public static void GetExtsIconAndDescription
            (string ext, out Icon largeIcon, out Icon smallIcon, out string description)
        {
            GetDefaultIcon(out largeIcon, out smallIcon);   //得到缺省图标
            description = "未知类型";                               //缺省类型描述
            RegistryKey extsubkey = Registry.ClassesRoot.OpenSubKey(ext);   //从注册表中读取扩展名相应的子键
            if (extsubkey == null) return;

            string extdefaultvalue = extsubkey.GetValue(null) as string;     //取出扩展名对应的文件类型名称
            if (extdefaultvalue == null) return;

            if (extdefaultvalue.Equals("exefile", StringComparison.OrdinalIgnoreCase))  //扩展名类型是可执行文件
            {
                RegistryKey exefilesubkey = Registry.ClassesRoot.OpenSubKey(extdefaultvalue);  //从注册表中读取文件类型名称的相应子键
                if (exefilesubkey != null)
                {
                    string exefiledescription = exefilesubkey.GetValue(null) as string;   //得到exefile描述字符串
                    if (exefiledescription != null) description = exefiledescription;
                }
                System.IntPtr exefilePhiconLarge = new IntPtr();
                System.IntPtr exefilePhiconSmall = new IntPtr();
                ExtractIconExW(System.IO.Path.Combine(Environment.SystemDirectory, "shell32.dll"), 2, ref exefilePhiconLarge, ref exefilePhiconSmall, 1);
                if (exefilePhiconLarge.ToInt32() > 0) largeIcon = Icon.FromHandle(exefilePhiconLarge);
                if (exefilePhiconSmall.ToInt32() > 0) smallIcon = Icon.FromHandle(exefilePhiconSmall);
                return;
            }

            RegistryKey typesubkey = Registry.ClassesRoot.OpenSubKey(extdefaultvalue);  //从注册表中读取文件类型名称的相应子键
            if (typesubkey == null) return;

            string typedescription = typesubkey.GetValue(null) as string;   //得到类型描述字符串
            if (typedescription != null) description = typedescription;

            RegistryKey defaulticonsubkey = typesubkey.OpenSubKey("DefaultIcon");  //取默认图标子键
            if (defaulticonsubkey == null) return;

            //得到图标来源字符串
            string defaulticon = defaulticonsubkey.GetValue(null) as string; //取出默认图标来源字符串
            if (defaulticon == null) return;
            string[] iconstringArray = defaulticon.Split(',');
            int nIconIndex = 0; //声明并初始化图标索引
            if (iconstringArray.Length > 1)
                if (!int.TryParse(iconstringArray[1], out nIconIndex))
                    nIconIndex = 0;     //int.TryParse转换失败，返回0

            //得到图标
            System.IntPtr phiconLarge = new IntPtr();
            System.IntPtr phiconSmall = new IntPtr();
            ExtractIconExW(iconstringArray[0].Trim('"'), nIconIndex, ref phiconLarge, ref phiconSmall, 1);
            if (phiconLarge.ToInt32() > 0) largeIcon = Icon.FromHandle(phiconLarge);
            if (phiconSmall.ToInt32() > 0) smallIcon = Icon.FromHandle(phiconSmall);
        }

        public static void GetDefaultIcon(out Icon largeIcon, out Icon smallIcon)
        {
            largeIcon = smallIcon = null;
            System.IntPtr phiconLarge = new IntPtr();
            System.IntPtr phiconSmall = new IntPtr();
            ExtractIconExW(System.IO.Path.Combine(Environment.SystemDirectory, "shell32.dll"), 0, ref phiconLarge, ref phiconSmall, 1);
            if (phiconLarge.ToInt32() > 0) largeIcon = Icon.FromHandle(phiconLarge);
            if (phiconSmall.ToInt32() > 0) smallIcon = Icon.FromHandle(phiconSmall);
        }

        #endregion

        private void FileListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Dictionary<ListViewItem, int> itemsCopy = new Dictionary<ListViewItem, int>();
            foreach (ListViewItem item in this.SelectedItems)
                itemsCopy.Add(item, item.Index);
            this.DoDragDrop(itemsCopy, DragDropEffects.Move);
            //this.DoDragDrop(e.Item,DragDropEffects.Move);
        }
        private void FileListView_DragEnter(object sender, DragEventArgs e)  
       {  
            e.Effect = e.AllowedEffect;  
       }

        private void FileListView_DragOver(object sender, DragEventArgs e)  
        {   
            Point point = this.PointToClient(new Point(e.X, e.Y));  
            int index = this.InsertionMark.NearestIndex(point);  
            if (index > -1)  
            {
                Rectangle itemBounds = this.GetItemRect(index);  
                if (point.X > itemBounds.Left + (itemBounds.Width / 2))  
                {
                    this.InsertionMark.AppearsAfterItem = true;  
                }  
                else  
                {
                    this.InsertionMark.AppearsAfterItem = false;  
                }  
            }
            this.InsertionMark.Index = index;  
        }

        private void FileListView_DragLeave(object sender, EventArgs e)
        {
            this.InsertionMark.Index = -1;
        }

        private void FileListView_DragDrop(object sender, DragEventArgs e)
        {
            int Index = this.InsertionMark.Index;
            if (Index == -1)
            {
                return;
            }

            if (this.InsertionMark.AppearsAfterItem)
            {
                Index++;
            }
            Dictionary<ListViewItem, int> items = (Dictionary<ListViewItem, int>)e.Data.GetData(typeof(Dictionary<ListViewItem, int>));
            foreach (var item in items)
            {
                this.Items.Insert(Index, (ListViewItem)item.Key.Clone()); 
                this.Items.Remove(item.Key);
                if (item.Value >= Index) Index++;
            }
            string sql=string.Empty;
            foreach (ListViewItem item in this.Items)
            {

                sql = string.Format("update up_files set SORTID='{0}' where ID={1}", item.Index, item.Name);
                SysDBConfig.GetInstance().GetOleDataBase("OrclConn").ExecuteQuery(sql);
            }
            //sql=sql.TrimEnd('\n');
            //sql = sql.TrimEnd(';');
            //SysDBConfig.GetInstance().OleDataBase.ExecuteQuery(sql);

  

        }
        class FileListViewIndexComparer:System.Collections.IComparer
        {
            public int Compare(object x, object y)  
            {  
               return ((ListViewItem)x).Index - ((ListViewItem)y).Index;  
            } 
        }



    }
    
}
