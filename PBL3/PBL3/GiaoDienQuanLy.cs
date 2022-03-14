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
    public partial class GiaoDienQuanLy : Form
    {
        public delegate void Send(int ID, string CV);
        public Send Sender { get; set; }
        public GiaoDienQuanLy()
        {
            InitializeComponent();
            Sender = new Send(SetLabel);
            setCBB();
            setCBBMaSachKM();
        }

        public void setdtUser()
        {
            //dtgrid_User.Columns["ID"].Visible = false;
            dtgrid_User.Columns["HoaDon"].Visible = false;
            dtgrid_User.Columns["PhieuNhap"].Visible = false;
            dtgrid_User.Columns["TenDN"].HeaderText = "Tên đăng nhập";
            dtgrid_User.Columns["TenDN"].Width = 130;
            dtgrid_User.Columns["MatKhau"].HeaderText = "Mật khẩu";
            dtgrid_User.Columns["ChucVu"].HeaderText = "Chức vụ";
        }

        public void setdtSachKM()
        {
            dtgrid_SachKM.Columns["MaKMCT"].HeaderText = "Mã KMCT";
            dtgrid_SachKM.Columns["TenSach"].HeaderText = "Tên sách";
            dtgrid_SachKM.Columns["DonGiaKM"].HeaderText = "Đơn giá KM";
            dtgrid_SachKM.Columns["TenKM"].HeaderText = "Tên KM";
        }

        public void setdtKM()
        {
            dtgrid_KM.Columns["KhuyenMaiCT"].Visible = false;
            dtgrid_KM.Columns["MaKM"].HeaderText = "Mã KM";
            dtgrid_KM.Columns["TenKM"].HeaderText = "Tên KM";
            dtgrid_KM.Columns["TGBatDau"].HeaderText = "Thời gian bắt đầu";
            dtgrid_KM.Columns["TGBatDau"].Width = 160;
            dtgrid_KM.Columns["TGKetThuc"].HeaderText = "Thời gian kết thúc";
            dtgrid_KM.Columns["TGKetThuc"].Width = 160;
        }

        public void setCBBMaSachKM()
        {
            foreach(Sach i in BLL_KM.Instance.GetAllSach())
            {
                cbb_MaSachKM.Items.Add(new CBBItem
                {
                    Value = i.MaSach,
                    Text = i.TenSach
                });
            }
        }

        private void bt_AddU_Click(object sender, EventArgs e)
        {
            if (txb_MK.Text != "" && txb_TenDN.Text != "" && cbb_CV.SelectedItem.ToString() != "" )
            {
                if (BLL_PBL3.Instance.CheckTenDN(txb_TenDN.Text) == true)
                {
                    //Login l = BLL_PBL3.Instance.GetLoginLast();
                    Login ll = new Login();
                    //ll.ID = l.ID + 1;
                    ll.MatKhau = txb_MK.Text;
                    ll.TenDN = txb_TenDN.Text;
                    ll.ChucVu = cbb_CV.SelectedItem.ToString();
                    BLL_PBL3.Instance.AddLogin(ll);
                    dtgrid_User.DataSource = BLL_PBL3.Instance.GetListLogin("All");
                    setdtUser();
                    txb_MK.Text = "";
                    cbb_CV.Text = "";
                    txb_TenDN.Text = "";
                }
                else MessageBox.Show("Tên đăng nhập đã tồn tại !");
            }
            else
            {
                MessageBox.Show("Nhập đầy đủ thông tin !");
            }
        }

        private void bt_ShowU_Click(object sender, EventArgs e)
        {
            if (cbb_ChucVu.Text != "")
            {
                dtgrid_User.DataSource = BLL_PBL3.Instance.GetListLogin(cbb_ChucVu.SelectedItem.ToString());
                setdtUser();
            }
        }

        public void ShowKM()
        {
            dtgrid_KM.DataSource = BLL_PBL3.Instance.GetListKM();
            setdtKM();
        }

        private void bt_AddKM_Click(object sender, EventArgs e)
        {
            cbb_KM.Items.Clear();
            Nhap_thong_tin_KM f = new Nhap_thong_tin_KM(null);
            f.d += new Nhap_thong_tin_KM.MyDel(ShowKM);
            f.d += new Nhap_thong_tin_KM.MyDel(setCBB);
            f.Show();
        }

        public void SetLabel(int ID, string CV)
        {
            lb_ID.Text = ID.ToString();
            lb_CV.Text = CV;
        }

        private void bt_DangXuat_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frm_DangNhap f = new frm_DangNhap();
                f.ShowDialog();
            }
        }

        void setCBB()
        {
            
            cbb_KM.Items.Add(new CBBItem { Value = 0, Text = "All" });
            foreach (KhuyenMai i in BLL_KM.Instance.GetAllKM())
            {
                cbb_KM.Items.Add(new CBBItem { Text = i.TenKM, Value = i.MaKM });
            }
        }

        private void bt_Show_Click(object sender, EventArgs e)
        {
            if (cbb_KM.Text != "")
            {
                int Makm = ((CBBItem)cbb_KM.SelectedItem).Value;
                int MaSach = 0;
                if (cbb_MaSachKM.Text != "")
                {
                    int a = Convert.ToInt32(((CBBItem)cbb_MaSachKM.SelectedItem).Value);
                    MaSach = a;
                }
                dtgrid_SachKM.DataSource = BLL_KM.Instance.GetKhuyenMaiCTByMaKM(Makm, MaSach);
                setdtSachKM();
            }
        }

        void ShowAll()
        {
            dtgrid_SachKM.DataSource = BLL_KM.Instance.GetKhuyenMaiCT();
            setdtSachKM();
        }

        private void bt_Add_Click(object sender, EventArgs e)
        {
            Nhap_sach_KM f = new Nhap_sach_KM(0);
            f.d += new Nhap_sach_KM.MyDel(ShowAll);
            f.Show();
        }

        private void bt_Del_Click(object sender, EventArgs e)
        {
            if (dtgrid_SachKM.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dtgrid_SachKM.SelectedRows.Count; i++)
                {
                    int MaKMCT = Convert.ToInt32(dtgrid_SachKM.SelectedRows[i].Cells["MaKMCT"].Value.ToString());
                    BLL_KM.Instance.DeleteKMCT(MaKMCT);
                }


                ShowAll();
            }
        }

        private void bt_Edit_Click(object sender, EventArgs e)
        {
            if (dtgrid_SachKM.SelectedRows.Count == 1)
            {
                int MaKMCT = Convert.ToInt32(dtgrid_SachKM.SelectedRows[0].Cells["MaKMCT"].Value.ToString());
                Nhap_sach_KM f = new Nhap_sach_KM(MaKMCT);
                f.d += new Nhap_sach_KM.MyDel(ShowAll);
                f.Show();
            }
            else MessageBox.Show("Vui lòng chọn một đối tượng !");
        }

        private void bt_XemKM_Click(object sender, EventArgs e)
        {
            ShowKM();
        }

        private void bt_EditKM_Click(object sender, EventArgs e)
        {
            if (dtgrid_KM.SelectedRows.Count == 1)
            {
                cbb_KM.Items.Clear();
                Nhap_thong_tin_KM f = new Nhap_thong_tin_KM(dtgrid_KM.SelectedRows[0].Cells[0].Value.ToString());
                f.d += new Nhap_thong_tin_KM.MyDel(ShowKM);
                f.d += new Nhap_thong_tin_KM.MyDel(setCBB);
                f.Show();
                dtgrid_KM.DataSource = BLL_PBL3.Instance.GetListKM();
                setdtKM();
            }
            else MessageBox.Show("Vui lòng chọn một đối tượng !");
        }

        private void bt_DelKM_Click(object sender, EventArgs e)
        {
            if (dtgrid_KM.SelectedRows.Count > 0)
            {
                cbb_KM.Items.Clear();
                foreach (DataGridViewRow i in dtgrid_KM.SelectedRows)
                {
                    int makm = Convert.ToInt32(i.Cells[0].Value.ToString());
                    BLL_PBL3.Instance.DeleteKM(makm);
                }
                setCBB();
                ShowKM();
            }
        }

        private void bt_BCXuat_Click(object sender, EventArgs e)
        {
            if (dtpicker_TGD.Value.Date <= dtpicker_TGC.Value.Date)
            {
                Bao_cao_xuat fbcx = new Bao_cao_xuat(dtpicker_TGD.Value, dtpicker_TGC.Value);
                fbcx.Show();
            }
            else MessageBox.Show("Thời gian không phù hợp !");
        }
        private void bt_EditU_Click(object sender, EventArgs e)
        {
            if (dtgrid_User.SelectedRows.Count == 1)
            {
                txb_MK.Text = dtgrid_User.SelectedRows[0].Cells[1].Value.ToString();
                txb_TenDN.Text = dtgrid_User.SelectedRows[0].Cells[2].Value.ToString();
                cbb_CV.Text = dtgrid_User.SelectedRows[0].Cells[3].Value.ToString();
                txb_TenDN.Enabled = false;
                cbb_CV.Enabled = false;
            }
            //else MessageBox.Show("Vui lòng chọn một đối tượng!");
        }

        private void bt_DelU_Click(object sender, EventArgs e)
        {
            if (dtgrid_User.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow i in dtgrid_User.SelectedRows)
                {
                    int id = Convert.ToInt32(i.Cells[0].Value.ToString());
                    BLL_PBL3.Instance.DeleteLogin(id);
                }
                dtgrid_User.DataSource = BLL_PBL3.Instance.GetListLogin("All");
                setdtUser();
            }
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            //PBL3_DB db = new PBL3_DB();
            if (dtgrid_User.SelectedRows.Count == 1 && txb_MK.Text != "")
            {
                int id = Convert.ToInt32(dtgrid_User.SelectedRows[0].Cells[0].Value.ToString());
                BLL_PBL3.Instance.EditLogin(id, txb_MK.Text, txb_TenDN.Text, cbb_CV.Text);
                txb_MK.Text = "";
                cbb_CV.Text = "";
                txb_TenDN.Text = "";
                txb_TenDN.Enabled = true;
                cbb_CV.Enabled = true;
                dtgrid_User.DataSource = BLL_PBL3.Instance.GetListLogin("All");
                setdtUser();
            }
            else MessageBox.Show("Thông tin chưa chính xác !");
        }

        private void bt_Nhap_Click(object sender, EventArgs e)
        {
            if (dtpicker_TGD.Value.Date <= dtpicker_TGC.Value.Date)
            {
                Bao_cao_nhap fbcx = new Bao_cao_nhap(dtpicker_TGD.Value.Date, dtpicker_TGC.Value.Date);
                fbcx.Show();
            }
            else MessageBox.Show("Thời gian không phù hợp !");
        }
    }
}
