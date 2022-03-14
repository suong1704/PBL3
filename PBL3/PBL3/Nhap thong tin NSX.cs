using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3.DTO;
using PBL3.BLL;

namespace PBL3
{
    public partial class Nhap_thong_tin_NSX : Form
    {
        public Nhap_thong_tin_NSX()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                NhaSX n = new NhaSX();
                n.TenNSX = textBox1.Text;
                n.SDT = textBox2.Text;
                n.DiaChi = textBox3.Text;
                BLL_PBL3.Instance.AddNSX(n);
            }
            else MessageBox.Show("Vui lòng nhập đầy đủ thông tin !");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
