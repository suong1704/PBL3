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
    public partial class Bao_cao_xuat : Form
    {
        public float tt = 0;
        public int tsp = 0;
        public DateTime TGD { get; set; }
        public DateTime TGC { get; set; }
        public Bao_cao_xuat(DateTime d1, DateTime d2)
        {
            InitializeComponent();
            TGD = d1;
            TGC = d2;
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

        public void setdtTK()
        {
            dataGridView2.Columns["TenSach"].HeaderText = "Tên sách";
            dataGridView2.Columns["SoLuong"].HeaderText = "Số lượng";
            dataGridView2.Columns["TongTien"].HeaderText = "Tổng tiền";
        }

        public void setGUI()
        {
            textBox1.Text = TGD.ToString();
            textBox2.Text = TGC.ToString();
            dataGridView1.DataSource = BLL_PBL3.Instance.GetSachHDByDate(TGD.Date, TGC.Date);
            setdtHD();
            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                tt += Convert.ToInt32(i.Cells[4].Value.ToString());
                tsp += Convert.ToInt32(i.Cells[2].Value.ToString());
            }
            textBox4.Text = tt.ToString();
            textBox3.Text = tsp.ToString();
            //dt2
            List<SachTK> l = new List<SachTK>();
            foreach (int ms in BLL_PBL3.Instance.GetListMaSachByDate(TGD.Date,TGC.Date, 1))
            {
                int sl = 0;
                string name = "";
                double gia = 0;
                foreach (HoaDonCT i in BLL_PBL3.Instance.GetHDCTByDate(TGD.Date, TGC.Date))
                {
                    if(ms == i.MaSach)
                    {
                        sl += i.SoLuong;
                        name = i.Sach.TenSach;
                        gia += i.GiaKM;
                    }
                }
                l.Add(new SachTK
                {
                    TenSach = name,
                    SoLuong = sl,
                    TongTien = gia
                });
            }
            dataGridView2.DataSource = l;
            setdtTK();
        }

        private void bt_Thoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
