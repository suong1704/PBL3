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
    public partial class frm_DangNhap : Form
    {
        public frm_DangNhap()
        {
            InitializeComponent();
        }

        private void bt_Thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void bt_DangNhap_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                 if (BLL_LGSach.Instance.Check("NhanVien", textBox1.Text, textBox2.Text))
                 {
                        GiaoDienNhanVien f = new GiaoDienNhanVien();
                        f.Sender(BLL_LGSach.Instance.GetID(textBox1.Text, textBox2.Text),"NhanVien");
                        this.Hide();
                        f.ShowDialog();
                   
                 }
                else label5.Text = "*Tên tài khoản hoặc mật khẩu không đúng";
                if (BLL_LGSach.Instance.Check("QuanLy", textBox1.Text, textBox2.Text))
                 {
                        GiaoDienQuanLy f = new GiaoDienQuanLy();
                        f.Sender(BLL_LGSach.Instance.GetID(textBox1.Text, textBox2.Text),"QuanLy");
                        this.Hide();
                        f.ShowDialog();
                 }
                 else label5.Text = "*Tên tài khoản hoặc mật khẩu không đúng";
            }
            else MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = (char)0;
            }
            else textBox2.PasswordChar = '*';
        }

        private void frm_DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void frm_DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
           /* if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;*/
        }
    }
}
