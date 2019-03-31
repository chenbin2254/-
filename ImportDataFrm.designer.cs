namespace CHXQ.XMManager
{
    partial class ImportDataFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            //AcadDoc = AcadApp.ActiveDocument;
            //AcadDoc.Close(false);
            //AcadApp.Quit();
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnMDBSave = new System.Windows.Forms.Button();
            this.BtnOpenExcel = new System.Windows.Forms.Button();
            this.TxbSaveDir = new System.Windows.Forms.TextBox();
            this.txbExcelPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.BtnOnline = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.TxbLableMin = new HR.Controls.AugurTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxbCoordFile = new System.Windows.Forms.TextBox();
            this.BtnCancle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnOpenCoordExcel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TxbArrowMin = new HR.Controls.AugurTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(510, 266);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(99, 30);
            this.BtnStart.TabIndex = 14;
            this.BtnStart.Text = "导入";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "选择DWG保存目录：";
            // 
            // BtnMDBSave
            // 
            this.BtnMDBSave.Location = new System.Drawing.Point(632, 125);
            this.BtnMDBSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnMDBSave.Name = "BtnMDBSave";
            this.BtnMDBSave.Size = new System.Drawing.Size(100, 29);
            this.BtnMDBSave.TabIndex = 11;
            this.BtnMDBSave.Text = "选择";
            this.BtnMDBSave.UseVisualStyleBackColor = true;
            this.BtnMDBSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnOpenExcel
            // 
            this.BtnOpenExcel.Location = new System.Drawing.Point(630, 38);
            this.BtnOpenExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnOpenExcel.Name = "BtnOpenExcel";
            this.BtnOpenExcel.Size = new System.Drawing.Size(100, 25);
            this.BtnOpenExcel.TabIndex = 12;
            this.BtnOpenExcel.Text = "打开";
            this.BtnOpenExcel.UseVisualStyleBackColor = true;
            this.BtnOpenExcel.Click += new System.EventHandler(this.BtnOpenExcel_Click);
            // 
            // TxbSaveDir
            // 
            this.TxbSaveDir.Location = new System.Drawing.Point(222, 128);
            this.TxbSaveDir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxbSaveDir.Name = "TxbSaveDir";
            this.TxbSaveDir.Size = new System.Drawing.Size(385, 25);
            this.TxbSaveDir.TabIndex = 9;
            // 
            // txbExcelPath
            // 
            this.txbExcelPath.Location = new System.Drawing.Point(222, 36);
            this.txbExcelPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbExcelPath.Name = "txbExcelPath";
            this.txbExcelPath.Size = new System.Drawing.Size(385, 25);
            this.txbExcelPath.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "选择普查记录表：";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.BtnOnline);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.TxbArrowMin);
            this.panelEx1.Controls.Add(this.TxbLableMin);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.TxbCoordFile);
            this.panelEx1.Controls.Add(this.BtnCancle);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.txbExcelPath);
            this.panelEx1.Controls.Add(this.TxbSaveDir);
            this.panelEx1.Controls.Add(this.BtnOpenCoordExcel);
            this.panelEx1.Controls.Add(this.BtnOpenExcel);
            this.panelEx1.Controls.Add(this.BtnMDBSave);
            this.panelEx1.Controls.Add(this.BtnStart);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(815, 327);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 21;
            // 
            // BtnOnline
            // 
            this.BtnOnline.Location = new System.Drawing.Point(85, 266);
            this.BtnOnline.Name = "BtnOnline";
            this.BtnOnline.Size = new System.Drawing.Size(192, 30);
            this.BtnOnline.TabIndex = 32;
            this.BtnOnline.Text = "查看手机在线普查记录";
            this.BtnOnline.UseVisualStyleBackColor = true;
            this.BtnOnline.Click += new System.EventHandler(this.BtnOnline_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(371, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(236, 15);
            this.label6.TabIndex = 31;
            this.label6.Text = "米     (不填或者0则代表不控制)";
            // 
            // TxbLableMin
            // 
            this.TxbLableMin.Location = new System.Drawing.Point(220, 177);
            this.TxbLableMin.MaxValue = 0D;
            this.TxbLableMin.MinValue = 0D;
            this.TxbLableMin.Name = "TxbLableMin";
            this.TxbLableMin.Size = new System.Drawing.Size(131, 25);
            this.TxbLableMin.TabIndex = 30;
            this.TxbLableMin.Text = "10";
            this.TxbLableMin.TextBoxType = HR.Controls.AugurTextBox.DciTextBoxTypes.DCI_TEXTBOX_INT;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "标注管线最短长度：";
            // 
            // TxbCoordFile
            // 
            this.TxbCoordFile.Location = new System.Drawing.Point(222, 79);
            this.TxbCoordFile.Margin = new System.Windows.Forms.Padding(4);
            this.TxbCoordFile.Name = "TxbCoordFile";
            this.TxbCoordFile.Size = new System.Drawing.Size(385, 25);
            this.TxbCoordFile.TabIndex = 25;
            // 
            // BtnCancle
            // 
            this.BtnCancle.Location = new System.Drawing.Point(630, 266);
            this.BtnCancle.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancle.Name = "BtnCancle";
            this.BtnCancle.Size = new System.Drawing.Size(99, 30);
            this.BtnCancle.TabIndex = 24;
            this.BtnCancle.Text = "取消";
            this.BtnCancle.UseVisualStyleBackColor = true;
            this.BtnCancle.Click += new System.EventHandler(this.BtnCancle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "选择坐标文件表：";
            // 
            // BtnOpenCoordExcel
            // 
            this.BtnOpenCoordExcel.Location = new System.Drawing.Point(630, 78);
            this.BtnOpenCoordExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnOpenCoordExcel.Name = "BtnOpenCoordExcel";
            this.BtnOpenCoordExcel.Size = new System.Drawing.Size(100, 25);
            this.BtnOpenCoordExcel.TabIndex = 12;
            this.BtnOpenCoordExcel.Text = "打开";
            this.BtnOpenCoordExcel.UseVisualStyleBackColor = true;
            this.BtnOpenCoordExcel.Click += new System.EventHandler(this.BtnOpenCoordExcel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 15);
            this.label4.TabIndex = 29;
            this.label4.Text = "绘制管线箭头最短长度：";
            // 
            // TxbArrowMin
            // 
            this.TxbArrowMin.Location = new System.Drawing.Point(220, 218);
            this.TxbArrowMin.MaxValue = 0D;
            this.TxbArrowMin.MinValue = 0D;
            this.TxbArrowMin.Name = "TxbArrowMin";
            this.TxbArrowMin.Size = new System.Drawing.Size(131, 25);
            this.TxbArrowMin.TabIndex = 30;
            this.TxbArrowMin.Text = "10";
            this.TxbArrowMin.TextBoxType = HR.Controls.AugurTextBox.DciTextBoxTypes.DCI_TEXTBOX_INT;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(371, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(236, 15);
            this.label7.TabIndex = 31;
            this.label7.Text = "米     (不填或者0则代表不控制)";
            // 
            // ImportDataFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 327);
            this.Controls.Add(this.panelEx1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "ImportDataFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "成图入库软件";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnMDBSave;
        private System.Windows.Forms.Button BtnOpenExcel;
        private System.Windows.Forms.TextBox TxbSaveDir;
        private System.Windows.Forms.TextBox txbExcelPath;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Button BtnCancle;
        private System.Windows.Forms.TextBox TxbCoordFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnOpenCoordExcel;
        private System.Windows.Forms.Label label6;
        private HR.Controls.AugurTextBox TxbLableMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnOnline;
        private System.Windows.Forms.Label label7;
        private HR.Controls.AugurTextBox TxbArrowMin;
        private System.Windows.Forms.Label label4;
    }
}

