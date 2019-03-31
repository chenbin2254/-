namespace CHXQ.XMManager
{
    partial class SURVEYIDQueryFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxbSURVEYID = new System.Windows.Forms.TextBox();
            this.BtnQuery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "物探点号：";
            // 
            // TxbSURVEYID
            // 
            this.TxbSURVEYID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxbSURVEYID.Location = new System.Drawing.Point(114, 32);
            this.TxbSURVEYID.Name = "TxbSURVEYID";
            this.TxbSURVEYID.Size = new System.Drawing.Size(181, 25);
            this.TxbSURVEYID.TabIndex = 1;
            // 
            // BtnQuery
            // 
            this.BtnQuery.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnQuery.Location = new System.Drawing.Point(313, 35);
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.Size = new System.Drawing.Size(75, 23);
            this.BtnQuery.TabIndex = 2;
            this.BtnQuery.Text = "查询";
            this.BtnQuery.UseVisualStyleBackColor = true;
            // 
            // SURVEYIDQueryFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 106);
            this.Controls.Add(this.BtnQuery);
            this.Controls.Add(this.TxbSURVEYID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SURVEYIDQueryFrm";
            this.Text = "物探点号查询";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxbSURVEYID;
        private System.Windows.Forms.Button BtnQuery;
    }
}