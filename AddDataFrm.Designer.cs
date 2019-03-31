namespace CHXQ.XMManager
{
    partial class AddDataFrm
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.TxbCoordFile = new System.Windows.Forms.TextBox();
            this.BtnCancle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbExcelPath = new System.Windows.Forms.TextBox();
            this.BtnOpenCoordExcel = new System.Windows.Forms.Button();
            this.BtnOpenExcel = new System.Windows.Forms.Button();
            this.BtnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.TxbCoordFile);
            this.panelEx1.Controls.Add(this.BtnCancle);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.txbExcelPath);
            this.panelEx1.Controls.Add(this.BtnOpenCoordExcel);
            this.panelEx1.Controls.Add(this.BtnOpenExcel);
            this.panelEx1.Controls.Add(this.BtnStart);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(812, 192);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 22;
            // 
            // TxbCoordFile
            // 
            this.TxbCoordFile.Location = new System.Drawing.Point(205, 79);
            this.TxbCoordFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxbCoordFile.Name = "TxbCoordFile";
            this.TxbCoordFile.Size = new System.Drawing.Size(385, 25);
            this.TxbCoordFile.TabIndex = 25;
            // 
            // BtnCancle
            // 
            this.BtnCancle.Location = new System.Drawing.Point(615, 130);
            this.BtnCancle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnCancle.Name = "BtnCancle";
            this.BtnCancle.Size = new System.Drawing.Size(99, 30);
            this.BtnCancle.TabIndex = 24;
            this.BtnCancle.Text = "取消";
            this.BtnCancle.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "选择坐标文件表：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "选择普查记录表：";
            // 
            // txbExcelPath
            // 
            this.txbExcelPath.Location = new System.Drawing.Point(205, 36);
            this.txbExcelPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbExcelPath.Name = "txbExcelPath";
            this.txbExcelPath.Size = new System.Drawing.Size(385, 25);
            this.txbExcelPath.TabIndex = 10;
            // 
            // BtnOpenCoordExcel
            // 
            this.BtnOpenCoordExcel.Location = new System.Drawing.Point(663, 76);
            this.BtnOpenCoordExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnOpenCoordExcel.Name = "BtnOpenCoordExcel";
            this.BtnOpenCoordExcel.Size = new System.Drawing.Size(100, 25);
            this.BtnOpenCoordExcel.TabIndex = 12;
            this.BtnOpenCoordExcel.Text = "打开";
            this.BtnOpenCoordExcel.UseVisualStyleBackColor = true;
            this.BtnOpenCoordExcel.Click += new System.EventHandler(this.BtnOpenCoordExcel_Click);
            // 
            // BtnOpenExcel
            // 
            this.BtnOpenExcel.Location = new System.Drawing.Point(663, 36);
            this.BtnOpenExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnOpenExcel.Name = "BtnOpenExcel";
            this.BtnOpenExcel.Size = new System.Drawing.Size(100, 25);
            this.BtnOpenExcel.TabIndex = 12;
            this.BtnOpenExcel.Text = "打开";
            this.BtnOpenExcel.UseVisualStyleBackColor = true;
            this.BtnOpenExcel.Click += new System.EventHandler(this.BtnOpenExcel_Click);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(495, 130);
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
            this.label2.Location = new System.Drawing.Point(598, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "(可选)";
            // 
            // AddDataFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 192);
            this.Controls.Add(this.panelEx1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AddDataFrm";
            this.Text = "选择数据";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.TextBox TxbCoordFile;
        private System.Windows.Forms.Button BtnCancle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbExcelPath;
        private System.Windows.Forms.Button BtnOpenCoordExcel;
        private System.Windows.Forms.Button BtnOpenExcel;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Label label2;
    }
}