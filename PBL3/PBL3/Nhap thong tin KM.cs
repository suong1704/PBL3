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
    public partial class Nhap_thong_tin_KM : Form
    {
        public delegate void MyDel();
        public MyDel d;
        public string makm { get; set; }
        public bool check = true;
        public Nhap_thong_tin_KM(string s)
        {
            InitializeComponent();
            makm = s;
            setGUI();
        }

        public void setGUI()
        {
            PBL3 db = new PBL3();
            int mkm = Convert.ToInt32(makm);
            KhuyenMai k = db.KhuyenMai.Find(mkm);
            if (k != null)
            {
                textBox1.Text = k.TenKM;
                dateTimePicker1.Value = k.TGBatDau;
                dateTimePicker2.Value = k.TGKetThuc;
                check = false;
            }
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (check == true)
                {
                    BLL_PBL3.Instance.AddKM(textBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
                    d();
                    //this.Dispose();
                }
                else
                {
                    BLL_PBL3.Instance.EditKM(makm, textBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
                    d();
                    //this.Dispose();
                }
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !");
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
