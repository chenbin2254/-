using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HR.Controls;
using HR.Utility;
using HR.UserManager;
using DevComponents.DotNetBar;

namespace CHXQ.XMManager
{
    public partial class LoginFrm : Form
    {
        public LoginFrm()
        {
            InitializeComponent();
            
            ImgCmbUserName.TextChanged += new EventHandler(ImgCmbUserName_TextChanged);
            InitLoginCmb();
            ImgCmbUserName.SelectedIndexChanged += new EventHandler(ImgCmbUserName_SelectedIndexChanged);
            //ImgCmbUserName.Text = CHXQ.XMManager.Properties.Settings.Default.LastLoginUser;
            this.Text = SystemInfo.SystemName;         
        }
        private List<LocalLoginUser> LocalLoginUserList = null;
        private void ImgCmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocalLoginUser pUser = LocalLoginUserList[ImgCmbUserName.SelectedIndex];
            txtPassword.Text = pUser.PassWord;
            RememberPwd.Checked = !string.IsNullOrEmpty(pUser.PassWord);
        }
        private void InitLoginCmb()
        {
            LocalLoginUserList = LocalLoginManager.GetLocalUsers();
            ImgCmbUserName.Items.Clear();
            foreach (LocalLoginUser pUser in LocalLoginUserList)
            {
                ImageComboItem pItem = new ImageComboItem(pUser.UserName, 0);
                ImgCmbUserName.Items.Add(pItem);
            }
        }
        private void ImgCmbUserName_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ImgCmbUserName.Items.Count; i++)
            {
                if (ImgCmbUserName.Items[i].ToString().Equals(ImgCmbUserName.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    ImgCmbUserName.SelectedItem = ImgCmbUserName.Items[i];
                    return;
                }
            }
        }
        /*
        public string UserName
        {
            get
            {
                return this.ImgCmbUserName.Text;
            }
        }

        /// <summary>
        /// 获取密码
        /// </summary>
        public string Password
        {
            get
            {
                return this.txtPassword.Text;
            }
        }*/

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (ImgCmbUserName.Text.Trim() == string.Empty)
            {
                this.lblMessage.Text="请输入用户名！";
                ImgCmbUserName.Select();
                return;
            }
            else
            {
                //if (ImgCmbUserName.Text.Equals("chenbin") && txtPassword.Text.Equals("1987"))
                //    this.DialogResult = DialogResult.OK;
                panel1.Enabled = false;
                try
                {
                    this.lblMessage.Text = "正在验证用户名密码";
                    if (!ValidateLogin(ImgCmbUserName.Text, txtPassword.Text))
                    {
                        this.lblMessage.Text = "用户名和密码出错！请重新输入";
                        this.txtPassword.ResetText();                                         
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }
                finally
                {
                    panel1.Enabled = true;
                }
            }
        }
        private bool ValidateLogin(string UserName, string Password)
        {
            string UserID = string.Empty;
            if (LocalLoginManager.ValidateLogin(UserName, Password, ref UserID))
            {
               // SystemInfo.UserID = UserID;               
                User pUser = User.GetUserByID(UserID);
                SystemInfo.UserName = pUser.UserName;
                SystemInfo.UserID = UserID;
                //pUser.GetRoleName();   
                //if (!pUser.GetRoleName().Equals("管理员用户"))
                //{
                //    return false;
                //}
                this.LoginRight();
                return true;
            }
            else
            {

                this.LoginError();
                return false;
            }
        }
        private void LoginRight()
        {
            //Properties.Settings.Default.LastLoginUser = ImgCmbUserName.Text;
            //Properties.Settings.Default.Save();
            LocalLoginUser pUser = new LocalLoginUser();
            pUser.UserName = this.ImgCmbUserName.Text;
            pUser.PassWord = this.txtPassword.Text; 
            if (!RememberPwd.Checked)
            {
                pUser.PassWord = string.Empty;
            }
            if (ImgCmbUserName.SelectedIndex == -1)
            {
                LocalLoginManager.AddLocalLoginInfo(pUser);
            }
            else
            {
                LocalLoginManager.SaveLoginInfo(pUser);
            }              
            this.DialogResult = DialogResult.OK;
        }
        public void LoginError()
        {
            txtPassword.Clear();
        }
    }
}
