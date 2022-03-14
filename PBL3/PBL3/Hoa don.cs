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
    public partial class Hoa_don : Form
    {
        public string mhd;
        public string Ttien;
        public string Tnhan;
        public string Tthoi;
        public Hoa_don(string s, string ttien, string tnhan, string tthoi)
        {
            InitializeComponent();
            mhd = s;
            Ttien = ttien;
            Tnhan = tnhan;
            Tthoi = tthoi;
            setGUI();
        }

        public void setdtHD()
        {
            dataGridView1.Columns["MaHDCT"].HeaderText = "Mã HDCT";
            dataGridView1.Columns["TenSach"].HeaderText = "Tên sách";
            dataGridView1.Columns["SoLuong"].HeaderText = "Số lượng";
            dataGridView1.Columns["DonGia"].HeaderText = "Đơn giá";
            dataGridView1.Columns["GiaKM"].HeaderText = "Giá KM";
        }

        public void setGUI()
        {
            int MHD = Convert.ToInt32(mhd);
            HoaDon hd = BLL_PBL3.Instance.GetHDByMaHD(MHD);
            if (hd != null)
            {
                textBox1.Text = hd.Ngay.Date.ToString();
                textBox2.Text = Ttien;
                textBox3.Text = Tnhan;
                textBox4.Text = Tthoi;
            }
            dataGridView1.DataSource = BLL_PBL3.Instance.GetSachByMaHD(MHD);
            setdtHD();
        }

        private void bt_Thoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
            
        }
    }
}
