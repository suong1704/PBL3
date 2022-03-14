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
    public partial class GiaoDienNhanVien : Form
    {
        public bool check = true;
        public double tt = 0;
        public delegate void Send(int ID, string CV);
        public Send Sender { get; set; }
        public GiaoDienNhanVien()
        {
            InitializeComponent();
            setCBBHĐ();
            SetCBB_MH();
            Sender = new Send(SetLabel);
            bt_Show.Click += bt_Show_Click;
            setComboBox();
            LoadTbSearchSach();
        }

        public void LoadTbSearchSach()
        {
            txb_Search.AutoCompleteCustomSource.AddRange(BLL_LGSach.Instance.GetListTenSach().ToArray());
        }

        public void setdtHD()
        {
            
            dtgridview_Sach.Columns["MaHDCT"].HeaderText = "Mã HDCT";
            dtgridview_Sach.Columns["TenSach"].HeaderText = "Tên sách";
            dtgridview_Sach.Columns["SoLuong"].HeaderText = "Số lượng";
            dtgridview_Sach.Columns["DonGia"].HeaderText = "Đơn giá";
            dtgridview_Sach.Columns["GiaKM"].HeaderText = "Giá KM";

        }

        public void setdtPN()
        {
            dtgrid_PN.Columns["MaPNCT"].HeaderText = "Mã PNCT";
            dtgrid_PN.Columns["TenSach"].HeaderText = "Tên sách";
            dtgrid_PN.Columns["SoLuong"].HeaderText = "Số lượng";
            dtgrid_PN.Columns["GiaNhap"].HeaderText = "Giá nhập";
        }

        public void setdtSach()
        {
            dtgrid_SachQL.Columns["MaSach"].HeaderText = "Mã sách";
            dtgrid_SachQL.Columns["TenSach"].HeaderText = "Tên sách";
            dtgrid_SachQL.Columns["DVT"].HeaderText = "Đơn vị tính";
            dtgrid_SachQL.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";
            dtgrid_SachQL.Columns["DonGia"].HeaderText = "Đơn giá";
            dtgrid_SachQL.Columns["GiaNhap"].HeaderText = "Giá nhập";
            dtgrid_SachQL.Columns["TenMH"].HeaderText = "Tên mặt hàng";
            dtgrid_SachQL.Columns["TenNSX"].HeaderText = "Tên nhà sản xuất";
            dtgrid_SachQL.Columns["MoTa"].HeaderText = "Mô tả";

        }

        public void SetCBB_MH()
        {
            cbb_MatHang.Items.Add(new CBBItem { Value = 0, Text = "All" });
            foreach (MatHang i in BLL_LGSach.Instance.GetAll_MH())
            {
                cbb_MatHang.Items.Add(new CBBItem
                {
                    Value = i.MaMH,
                    Text = i.TenMH
                });
            }
        }

        public void setCBBHĐ()
        {
            foreach (Sach i in BLL_PBL3.Instance.GetAllSach())
            {
                cbb_MaSachHD.Items.Add(new CBBItem
                {
                    Value = i.MaSach,
                    Text = i.MaSach.ToString()
                });
            }
        }

        private void bt_Them_Click(object sender, EventArgs e)
        {
            if (cbb_MaSachHD.Text != "" && num_SLSachHD.Value > 0)
            {
                if (check == true)
                {
                    HoaDon hd = new HoaDon();
                    hd.ID = Convert.ToInt32(lb_ID.Text);
                    //hd.MaHD = h.MaHD + 1;

                    hd.Ngay = dtpicker_HD.Value.Date;
                    check = false;
                    BLL_PBL3.Instance.AddHD(hd);
                    txb_MHD.Text = hd.MaHD.ToString();
                }
                Sach s = BLL_PBL3.Instance.GetSachByMS(Convert.ToInt32(cbb_MaSachHD.SelectedItem.ToString()));
                if (num_SLSachHD.Value <= s.SoLuongTon)
                {
                    HoaDonCT hdct = new HoaDonCT();
                    hdct.MaSach = Convert.ToInt32(cbb_MaSachHD.SelectedItem.ToString());
                    hdct.SoLuong = Convert.ToInt32(num_SLSachHD.Value);
                    BLL_PBL3.Instance.DownSLT(Convert.ToInt32(cbb_MaSachHD.SelectedItem.ToString()), Convert.ToInt32(num_SLSachHD.Value));
                    hdct.DonGia = s.DonGia * hdct.SoLuong;
                    KhuyenMaiCT kmct = BLL_PBL3.Instance.GetKMCTByMS(Convert.ToInt32(cbb_MaSachHD.SelectedItem.ToString()));
                    if (kmct != null)
                    {
                        KhuyenMai km = BLL_PBL3.Instance.GetKMByKMCT(kmct);
                        if (km.TGBatDau < DateTime.Now && DateTime.Now < km.TGKetThuc)
                        {
                            hdct.GiaKM = hdct.DonGia * (1 - kmct.DonGiaKM);
                        }
                        else hdct.GiaKM = hdct.DonGia;
                    }
                    else hdct.GiaKM = hdct.DonGia;
                    hdct.MaHD = Convert.ToInt32(txb_MHD.Text);
                    tt += hdct.GiaKM;
                    txb_TCong.Text = tt.ToString();
                    BLL_PBL3.Instance.AddHDCT(hdct);
                    dtgridview_Sach.DataSource = BLL_PBL3.Instance.GetSachByMaHD(Convert.ToInt32(txb_MHD.Text));
                    setdtHD();
                }
                else MessageBox.Show("Vượt quá số lượng !");
            }
            else MessageBox.Show("Thông tin chưa chính xác !");
        }

        private void bt_Xoa_Click(object sender, EventArgs e)
        {
            if (dtgridview_Sach.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow i in dtgridview_Sach.SelectedRows)
                {
                    int mhdct = Convert.ToInt32(i.Cells[0].Value);
                    HoaDonCT s = BLL_PBL3.Instance.getHDCTByMHDCT(mhdct);
                    tt -= s.GiaKM;
                    txb_TCong.Text = tt.ToString();
                    BLL_PBL3.Instance.UpSLT(Convert.ToInt32(cbb_MaSachHD.Text), Convert.ToInt32(i.Cells[2].Value));
                    BLL_PBL3.Instance.DeleteHDCT(mhdct);
                }
                dtgridview_Sach.DataSource = BLL_PBL3.Instance.GetSachByMaHD(Convert.ToInt32(txb_MHD.Text));
                setdtHD();
            }
            
        }

        private void bt_TinhTien_Click(object sender, EventArgs e)
        {
            if (txb_TCong.Text != "" && txb_TNhan.Text != "")
            {
                if (Convert.ToInt32(txb_TNhan.Text) < Convert.ToInt32(txb_TCong.Text) || txb_TNhan.Text == "")
                {
                    MessageBox.Show("Tiền nhận không đủ !");
                }
                txb_TThoi.Text = (Convert.ToDouble(txb_TNhan.Text) - tt).ToString();
            }
            else MessageBox.Show("Chưa nhập đủ thông tin !");
        }

        private void bt_Xuat_Click(object sender, EventArgs e)
        {
            if (dtgridview_Sach.Rows.Count > 0 && txb_TThoi.Text != "" && txb_TNhan.Text != "")
            {
                Hoa_don fhd = new Hoa_don(txb_MHD.Text, txb_TCong.Text, txb_TNhan.Text, txb_TThoi.Text);
                fhd.Show();
                ResetForm();
            }
        }

        public void ResetForm()
        {
            check = true;
            dtgridview_Sach.DataSource = null;
            cbb_MaSachHD.Text = "";
            txb_MHD.Text = "";
            txb_TCong.Text = "";
            txb_TNhan.Text = "";
            txb_TThoi.Text = "";
            tt = 0;
            dtpicker_HD.Value = DateTime.Now;
            num_SLSachHD.Value = 1;
        }

        public void SetLabel(int ID, string CV)
        {
            lb_ID.Text = ID.ToString();
            lb_CV.Text = CV;

        }

        public void ShowSach()
        {
            //if(cbb_MatHang.Text == "") 
            dtgrid_SachQL.DataSource = BLL_LGSach.Instance.GetAllSach();
            setdtSach();
        }

        private void bt_Show_Click(object sender, EventArgs e)
        {
            if (cbb_MatHang.Text != "")
            {
                dtgrid_SachQL.DataSource = BLL_LGSach.Instance.GetSachByMaMH(((CBBItem)cbb_MatHang.SelectedItem).Value);
                setdtSach();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frm_DangNhap f = new frm_DangNhap();
                f.ShowDialog();
            }
        }

        private void bt_Search_Click(object sender, EventArgs e)
        {
            if (cbb_MatHang.Text != "" && txb_Search.Text != "")
            {
                dtgrid_SachQL.DataSource = BLL_LGSach.Instance.GetTenSach(((CBBItem)cbb_MatHang.SelectedItem).Value, txb_Search.Text);
                setdtSach();
            }
        }

        private void bt_Add_Click(object sender, EventArgs e)
        {
            cbb_MaSachHD.Items.Clear();
            cbb_Sach.Items.Clear();
            Nhap_thong_tin_sach f = new Nhap_thong_tin_sach(0);
            f.d += ShowSach;
            f.ShowDialog();
            setCBBHĐ();
            setComboBox();
        }

        private void bt_Edit_Click(object sender, EventArgs e)
        {
            if (dtgrid_SachQL.SelectedRows.Count == 1)
            {
                Nhap_thong_tin_sach f = new Nhap_thong_tin_sach(Convert.ToInt32(dtgrid_SachQL.SelectedRows[0].Cells["MaSach"].Value.ToString()));
                f.d += ShowSach;
                f.ShowDialog();
            }
            else MessageBox.Show("Vui lòng chọn một đối tượng!");
        }

        private void bt_Del_Click(object sender, EventArgs e)
        {
            if (dtgrid_SachQL.SelectedRows.Count > 0)
            {
                cbb_MaSachHD.Items.Clear();
                cbb_Sach.Items.Clear();
                foreach (DataGridViewRow i in dtgrid_SachQL.SelectedRows)
                {
                    BLL_LGSach.Instance.DeleteSach(Convert.ToInt32(i.Cells[0].Value.ToString()));
                }
                ShowSach();
                setCBBHĐ();
                setComboBox();
            }
        }

        private void bt_Sort_Click(object sender, EventArgs e)
        {
            if (cbb_Sort.Text != "")
            {
                switch (cbb_Sort.Text)
                {
                    case "Mã Sách A-Z":
                        List<SachQL> list1 = BLL_LGSach.Instance.GetSachByMaMH(((CBBItem)cbb_MatHang.SelectedItem).Value);
                        list1.Sort(new Comparison<SachQL>(BLL_LGSach.Instance.CompareUp_MaSach));
                        dtgrid_SachQL.DataSource = list1;
                        setdtSach();
                        break;
                    case "Mã Sách Z-A":
                        List<SachQL> list2 = BLL_LGSach.Instance.GetSachByMaMH(((CBBItem)cbb_MatHang.SelectedItem).Value);
                        list2.Sort(new Comparison<SachQL>(BLL_LGSach.Instance.CompareDown_MaSach));
                        dtgrid_SachQL.DataSource = list2;
                        setdtSach();
                        break;
                    case "Tên Sách A-Z":
                        List<SachQL> list3 = BLL_LGSach.Instance.GetSachByMaMH(((CBBItem)cbb_MatHang.SelectedItem).Value);
                        list3.Sort(new Comparison<SachQL>(BLL_LGSach.Instance.CompareUp_TenSach));
                        dtgrid_SachQL.DataSource = list3;
                        setdtSach();
                        break;
                    case "Tên Sách Z-A":
                        List<SachQL> list4 = BLL_LGSach.Instance.GetSachByMaMH(((CBBItem)cbb_MatHang.SelectedItem).Value);
                        list4.Sort(new Comparison<SachQL>(BLL_LGSach.Instance.CompareDown_TenSach));
                        dtgrid_SachQL.DataSource = list4;
                        setdtSach();
                        break;
                    /*default:
                        MessageBox.Show("Vui lòng chọn đối tượng sắp xếp.");
                        break;*/
                }
            }
        }

        private void bt_ThemMH_Click(object sender, EventArgs e)
        {
            cbb_MatHang.Items.Clear();
            Nhap_thong_tin_MH f = new Nhap_thong_tin_MH();
            f.d += new Nhap_thong_tin_MH.MyDelMH(SetCBB_MH);
            f.Show();
        }

        private void bt_ThemNSX_Click(object sender, EventArgs e)
        {
            Nhap_thong_tin_NSX f = new Nhap_thong_tin_NSX();
            f.Show();
        }

        void setComboBox()
        {
            foreach (Sach i in BLL_PhieuNhap.instance.GetAllSach())
            {

                cbb_Sach.Items.Add(new CBBItem
                {
                    Value = i.MaSach,
                    Text = i.MaSach.ToString()
                });
            }
        }

        private void bt_ThemPN_Click(object sender, EventArgs e)
        {
            if (cbb_Sach.Text != "" && num_SLSachPN.Value > 0)
            {
                int MaPN = 0;
                int MaPNCT = 0;
                if (txb_MPN.Text == "")
                {
                    PhieuNhap pn = new PhieuNhap
                    {
                        ID = Convert.ToInt32(lb_ID.Text),
                        //ID = 1001,
                        MaPN = MaPN,
                        Ngay = dtpicker_PN.Value.Date
                    };
                    BLL_PhieuNhap.instance.addPhieuNhap(pn);
                    txb_MPN.Text = pn.MaPN.ToString();

                    PhieuNhapCT pnct = new PhieuNhapCT
                    {
                        MaPNCT = MaPNCT,
                        MaSach = ((CBBItem)cbb_Sach.SelectedItem).Value,
                        GiaNhap = BLL_PhieuNhap.instance.GetGiaNhapByMaSach(((CBBItem)cbb_Sach.SelectedItem).Value),
                        SoLuong = (int)num_SLSachPN.Value,
                        MaPN = Convert.ToInt32(txb_MPN.Text)
                    };
                    BLL_PhieuNhap.instance.addPhieuNhapCT(pnct);
                    BLL_PhieuNhap.instance.UpdateSoLuongSachTang((int)num_SLSachPN.Value, ((CBBItem)cbb_Sach.SelectedItem).Value);
                }
                else
                {
                    PhieuNhapCT pnct = new PhieuNhapCT
                    {
                        MaPNCT = MaPNCT,
                        MaSach = ((CBBItem)cbb_Sach.SelectedItem).Value,
                        GiaNhap = BLL_PhieuNhap.instance.GetGiaNhapByMaSach(((CBBItem)cbb_Sach.SelectedItem).Value),
                        SoLuong = (int)num_SLSachPN.Value,
                        MaPN = Convert.ToInt32(txb_MPN.Text)
                    };

                    BLL_PhieuNhap.instance.addPhieuNhapCT(pnct);
                    BLL_PhieuNhap.instance.UpdateSoLuongSachTang((int)num_SLSachPN.Value, ((CBBItem)cbb_Sach.SelectedItem).Value);
                }
                dtgrid_PN.DataSource = BLL_PhieuNhap.instance.GetSachPNByMaPN(Convert.ToInt32(txb_MPN.Text));
                setdtPN();
            }
            else MessageBox.Show("Thông tin chưa chính xác !");
        }

        private void bt_XoaPN_Click(object sender, EventArgs e)
        {
            if (dtgrid_PN.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dtgrid_PN.SelectedRows.Count; i++)
                {
                    int MaPNCT = Convert.ToInt32(dtgrid_PN.SelectedRows[i].Cells["MaPNCT"].Value.ToString());
                    BLL_PhieuNhap.instance.DeletePNCT(MaPNCT);
                    BLL_PhieuNhap.instance.UpdateSoLuongSachGiam((int)num_SLSachPN.Value, ((CBBItem)cbb_Sach.SelectedItem).Value);
                }
            }
            dtgrid_PN.DataSource = BLL_PhieuNhap.instance.GetSachPNByMaPN(Convert.ToInt32(txb_MPN.Text));
            setdtPN();

        }

        private void bt_OKPN_Click(object sender, EventArgs e)
        {
            txb_MPN.Text = "";
            cbb_Sach.Text = "";
            num_SLSachPN.Value = 1;
            dtgrid_PN.DataSource = null;
        }
    }
}
