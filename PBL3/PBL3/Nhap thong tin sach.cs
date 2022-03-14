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
    public partial class Nhap_thong_tin_sach : Form
    {
        public delegate void MyDel();
        public MyDel d;
        public int masach;
        public Nhap_thong_tin_sach(int m)
        {
            InitializeComponent();
            masach = m;
            SetCBB_MH();
            SetCBB_NSX();
            SetGUI();
        }

        public void SetCBB_MH()
        {
            foreach (MatHang i in BLL_LGSach.Instance.GetAll_MH())
            {
                comboBox1.Items.Add(new CBBItem
                {
                    Value = i.MaMH,
                    Text = i.TenMH
                });
            }
        }

        public void SetCBB_NSX()
        {
            foreach (NhaSX i in BLL_LGSach.Instance.GetAll_NSX())
            {
                comboBox2.Items.Add(new CBBItem
                {
                    Value = i.MaNSX,
                    Text = i.TenNSX
                });
            }
        }

        public void SetGUI()
        {
            Sach s = new Sach();
            s = BLL_LGSach.Instance.GetSachByMaSach(masach);
            textBox1.Text = s.MaSach.ToString();
            textBox2.Text = s.TenSach;
            comboBox3.Text = s.DVT;
            textBox5.Text = s.SoLuongTon.ToString();
            textBox3.Text = s.DonGia.ToString();
            textBox4.Text = s.GiaNhap.ToString();
            foreach (MatHang i in BLL_LGSach.Instance.GetAll_MH())
            {
                if (i.MaMH == s.MaMH) comboBox1.Text = i.TenMH.ToString();
            }
            foreach (NhaSX i in BLL_LGSach.Instance.GetAll_NSX())
            {
                if (i.MaNSX == s.MaNSX) comboBox2.Text = i.TenNSX.ToString();
            }
            textBox5.Text = s.SoLuongTon.ToString();
            textBox7.Text = s.MoTa;
        }

        public bool CheckCBB()
        {
            bool check = false;
            if (comboBox1.Text != "" || comboBox2.Text != "") check = true;
            return check;
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "" && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" &&
                textBox5.Text != "" && comboBox3.Text != "")
            {
                Sach sach = new Sach();
                sach.MaSach = Convert.ToInt32(textBox1.Text);
                sach.TenSach = textBox2.Text;
                sach.DVT = comboBox3.Text;
                sach.SoLuongTon = Convert.ToInt32(textBox5.Text);
                sach.DonGia = Convert.ToDouble(textBox3.Text);
                sach.GiaNhap = Convert.ToDouble(textBox4.Text);
                sach.MaMH = Convert.ToInt32(((CBBItem)comboBox1.SelectedItem).Value);
                sach.MaNSX = Convert.ToInt32(((CBBItem)comboBox2.SelectedItem).Value);
                sach.MoTa = textBox7.Text;
                BLL_LGSach.Instance.ExecuteSach(sach);
                d();
                //this.Close();
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin !");
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            /*textBox2.Text = "";
            textBox6.Text = "";
            textBox5.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";*/
            this.Dispose();
        }
    }
}
