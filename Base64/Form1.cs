using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base64
{
    public partial class Form1 : Form
    {
        private string base64Str;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = this.textBox1.Text;
            var ebytes = System.Text.Encoding.Default.GetBytes(str);
            //bytes进行base64加密
            var strBase64 = Convert.ToBase64String(ebytes);
            this.textBox2.Text = strBase64;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] bytes = Convert.FromBase64String(this.textBox3.Text);
            string str = Encoding.GetEncoding("UTF-8").GetString(bytes);
            this.textBox4.Text = str;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "所有文件|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName != "")
                {
                    this.textBox5.Text = System.IO.Path.GetFullPath(ofd.FileName);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path = textBox5.Text;  //界面上第一个文件路径
            string tempPath = textBox6.Text; //界面上第二个文件路径
            FileStream filestream = new FileStream(path, FileMode.Open);

            byte[] bt = new byte[filestream.Length];

            //调用read读取方法
            filestream.Read(bt, 0, bt.Length);
            this.base64Str = Convert.ToBase64String(bt);
            filestream.Close();

            //将Base64串写入临时文本文件
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath, FileMode.Create);
            byte[] data = System.Text.Encoding.Default.GetBytes(this.base64Str);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string outPath = textBox7.Text;  //界面上第三个文件路径
            var contents = Convert.FromBase64String(this.base64Str);
            using (var fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(contents, 0, contents.Length);
                fs.Flush();
            }
        }
    }
}
