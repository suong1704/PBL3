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
    public partial class Nhap_sach_KM : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        int MaKMCT;
        public Nhap_sach_KM(int m)
        {
            InitializeComponent();
            MaKMCT = m;
            SetCBB();
            SetGUI();
        }

        public void SetCBB()
        {
            //comboBox1.Items.Add(new CBBItem {  });
            PBL3 db = new PBL3();
            foreach (Sach i in db.Sach)
            {
                comboBox1.Items.Add(new CBBItem
                {
                    Value = i.MaSach,
                    Text = i.TenSach
                });
            }
            foreach (KhuyenMai i in db.KhuyenMai)
            {
                comboBox2.Items.Add(new CBBItem
                {
                    Value = i.MaKM,
                    Text = i.TenKM
                });
            }
        }

        public void SetGUI()
        {
            if (MaKMCT != 0)
            {
                //ten sach, don gia KM
                PBL3 db = new PBL3();
                comboBox1.Text = BLL_KM.Instance.GetTenSachByMaKMCT(MaKMCT);
                txb_Dongia.Text = BLL_KM.Instance.GetDonGiaKMByMaKMCT(MaKMCT).ToString();
                comboBox2.Text = BLL_KM.Instance.GetTenKMByMaKM((BLL_KM.Instance.GetMaKMByMaKMCT(MaKMCT)));
            }
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || txb_Dongia.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !");
            }
            else
            {
                KhuyenMaiCT s = new KhuyenMaiCT
                {
                    MaKMCT = MaKMCT,
                    MaSach = ((CBBItem)comboBox1.SelectedItem).Value,
                    DonGiaKM = Convert.ToDouble(txb_Dongia.Text),
                    MaKM = ((CBBItem)comboBox2.SelectedItem).Value
                };
                BLL_KM.Instance.ExecuteDB(s);
            }
            //Dispose();
            d();
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
