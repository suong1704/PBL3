using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3.BLL;
using PBL3.DTO;

namespace PBL3
{
    public partial class Nhap_thong_tin_MH : Form
    {
        public delegate void MyDelMH();
        public MyDelMH d;
        public Nhap_thong_tin_MH()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                //MatHang mh = BLL_PBL3.Instance.GetMHLast();
                MatHang m = new MatHang();
                //m.MaMH = mh.MaMH + 1;
                m.TenMH = textBox2.Text;
                BLL_PBL3.Instance.AddMH(m);
                d();
                //this.Dispose();
            }
            else MessageBox.Show("Vui lòng nhập đầy đủ thông tin !");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
