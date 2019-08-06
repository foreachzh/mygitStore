using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandExtension2
{
    public partial class frmInput : Form
    {
        public string initSqlpath { get; set; }
        public string initHtmlpath { get; set; }
        public frmInput()
        {
            InitializeComponent();
            this.AcceptButton = btnOK;
            txtDllpath.Text = GetAssemblyPath();
        }

        private void txtSqlPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            // 关键字文本框改变，另外两个跟着变
            txtSqlPath.Text = initSqlpath + "V_" + txtKeyword.Text+".sql";
            txtHtmlPath.Text = initHtmlpath + txtKeyword.Text+".html";
        }

        private void frmInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTableName.Text))
            {
                MessageBox.Show("表名是必填项！");
                txtTableName.Focus();
                e.Cancel = true; //取消关闭操作
            }

        }


        public static String GetAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
