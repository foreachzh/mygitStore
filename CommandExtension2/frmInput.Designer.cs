namespace CommandExtension2
{
    partial class frmInput
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
            this.btn_SetSqlPath = new System.Windows.Forms.Button();
            this.txtSqlPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHtmlPath = new System.Windows.Forms.TextBox();
            this.btn_SetHtmlPath = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDllpath = new System.Windows.Forms.TextBox();
            this.cbJson = new System.Windows.Forms.CheckBox();
            this.cbClose = new System.Windows.Forms.CheckBox();
            this.cbDetail = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_SetSqlPath
            // 
            this.btn_SetSqlPath.Location = new System.Drawing.Point(737, 38);
            this.btn_SetSqlPath.Name = "btn_SetSqlPath";
            this.btn_SetSqlPath.Size = new System.Drawing.Size(102, 32);
            this.btn_SetSqlPath.TabIndex = 0;
            this.btn_SetSqlPath.Text = "设置Sql路径";
            this.btn_SetSqlPath.UseVisualStyleBackColor = true;
            // 
            // txtSqlPath
            // 
            this.txtSqlPath.Location = new System.Drawing.Point(78, 44);
            this.txtSqlPath.Name = "txtSqlPath";
            this.txtSqlPath.Size = new System.Drawing.Size(653, 21);
            this.txtSqlPath.TabIndex = 1;
            this.txtSqlPath.TextChanged += new System.EventHandler(this.txtSqlPath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sql路径：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Html路径：";
            // 
            // txtHtmlPath
            // 
            this.txtHtmlPath.Location = new System.Drawing.Point(78, 78);
            this.txtHtmlPath.Name = "txtHtmlPath";
            this.txtHtmlPath.Size = new System.Drawing.Size(653, 21);
            this.txtHtmlPath.TabIndex = 4;
            // 
            // btn_SetHtmlPath
            // 
            this.btn_SetHtmlPath.Location = new System.Drawing.Point(737, 76);
            this.btn_SetHtmlPath.Name = "btn_SetHtmlPath";
            this.btn_SetHtmlPath.Size = new System.Drawing.Size(102, 28);
            this.btn_SetHtmlPath.TabIndex = 3;
            this.btn_SetHtmlPath.Text = "设置Html路径";
            this.btn_SetHtmlPath.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(368, 150);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(102, 33);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "关键字：";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(75, 13);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(177, 21);
            this.txtKeyword.TabIndex = 8;
            this.txtKeyword.TextChanged += new System.EventHandler(this.txtKeyword_TextChanged);
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(338, 12);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(177, 21);
            this.txtTableName.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "表名：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "DLL路径：";
            // 
            // txtDllpath
            // 
            this.txtDllpath.Location = new System.Drawing.Point(78, 113);
            this.txtDllpath.Name = "txtDllpath";
            this.txtDllpath.Size = new System.Drawing.Size(653, 21);
            this.txtDllpath.TabIndex = 11;
            // 
            // cbJson
            // 
            this.cbJson.AutoSize = true;
            this.cbJson.Location = new System.Drawing.Point(534, 16);
            this.cbJson.Name = "cbJson";
            this.cbJson.Size = new System.Drawing.Size(84, 16);
            this.cbJson.TabIndex = 14;
            this.cbJson.Text = "json格式化";
            this.cbJson.UseVisualStyleBackColor = true;
            // 
            // cbClose
            // 
            this.cbClose.AutoSize = true;
            this.cbClose.Location = new System.Drawing.Point(624, 16);
            this.cbClose.Name = "cbClose";
            this.cbClose.Size = new System.Drawing.Size(72, 16);
            this.cbClose.TabIndex = 15;
            this.cbClose.Text = "关闭按钮";
            this.cbClose.UseVisualStyleBackColor = true;
            // 
            // cbDetail
            // 
            this.cbDetail.AutoSize = true;
            this.cbDetail.Location = new System.Drawing.Point(702, 16);
            this.cbDetail.Name = "cbDetail";
            this.cbDetail.Size = new System.Drawing.Size(72, 16);
            this.cbDetail.TabIndex = 16;
            this.cbDetail.Text = "详情列表";
            this.cbDetail.UseVisualStyleBackColor = true;
            // 
            // frmInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 184);
            this.Controls.Add(this.cbDetail);
            this.Controls.Add(this.cbClose);
            this.Controls.Add(this.cbJson);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDllpath);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHtmlPath);
            this.Controls.Add(this.btn_SetHtmlPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSqlPath);
            this.Controls.Add(this.btn_SetSqlPath);
            this.Name = "frmInput";
            this.Text = "frmInput";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInput_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_SetSqlPath;
        public System.Windows.Forms.TextBox txtSqlPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtHtmlPath;
        private System.Windows.Forms.Button btn_SetHtmlPath;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtKeyword;
        public System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtDllpath;
        public System.Windows.Forms.CheckBox cbJson;
        public System.Windows.Forms.CheckBox cbClose;
        public System.Windows.Forms.CheckBox cbDetail;
    }
}