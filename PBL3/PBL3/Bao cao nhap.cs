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
    public partial class Bao_cao_nhap : Form
    {
        public float tt = 0;
        public int tsp = 0;
        public DateTime TGD { get; set; }
        public DateTime TGC { get; set; }
        public Bao_cao_nhap(DateTime d1, DateTime d2)
        {
            InitializeComponent();
            TGD = d1;
            TGC = d2;
            setGUI();
        }

        public void setdtPN()
        {
            dataGridView1.Columns["MaPNCT"].HeaderText = "Mã PNCT";
            dataGridView1.Columns["TenSach"].HeaderText = "Tên sách";
            dataGridView1.Columns["SoLuong"].HeaderText = "Số lượng";
            dataGridView1.Columns["GiaNhap"].HeaderText = "Giá Nhập";
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
            dataGridView1.DataSource = BLL_PBL3.Instance.GetSachPNByDate(TGD.Date, TGC.Date);
            setdtPN();
            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                tt += Convert.ToInt32(i.Cells[3].Value.ToString());
                tsp += Convert.ToInt32(i.Cells[2].Value.ToString());
            }
            textBox3.Text = tt.ToString();
            textBox4.Text = tsp.ToString();
            List<SachTK> l = new List<SachTK>();
            foreach (int ms in BLL_PBL3.Instance.GetListMaSachByDate(TGD.Date, TGC.Date, 0))
            {
                int sl = 0;
                string name = "";
                double gia = 0;
                foreach (PhieuNhapCT i in BLL_PBL3.Instance.GetPNCTByDate(TGD.Date, TGC.Date))
                {
                    if (ms == i.MaSach)
                    {
                        sl += i.SoLuong;
                        name = i.Sach.TenSach;
                        gia += i.SoLuong * i.GiaNhap;
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
