namespace CHXQ.XMManager
{
    partial class ErrorItemFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LstErrorItems = new System.Windows.Forms.ListBox();
            this.BtnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LstErrorItems
            // 
            this.LstErrorItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.LstErrorItems.FormattingEnabled = true;
            this.LstErrorItems.ItemHeight = 12;
            this.LstErrorItems.Location = new System.Drawing.Point(0, 0);
            this.LstErrorItems.Name = "LstErrorItems";
            this.LstErrorItems.Size = new System.Drawing.Size(417, 172);
            this.LstErrorItems.TabIndex = 0;
            // 
            // BtnOK
            // 
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(330, 178);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 1;
            this.BtnOK.Text = "确定";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // ErrorItemFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 208);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.LstErrorItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ErrorItemFrm";
            this.Text = "数据存在以下错误";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LstErrorItems;
        private System.Windows.Forms.Button BtnOK;
    }
}