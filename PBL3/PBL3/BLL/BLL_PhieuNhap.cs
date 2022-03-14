using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.BLL;
using PBL3.DTO;

namespace PBL3.BLL
{
    class BLL_PhieuNhap
    {
        private static BLL_PhieuNhap _instance;
        public static BLL_PhieuNhap instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BLL_PhieuNhap();
                }
                return _instance;
            }
            set
            {

            }
        }

        public List<int> GetAllMaSach()
        {
            List<int> data = new List<int>();
            PBL3 db = new PBL3();
            foreach (Sach i in db.Sach)
            {
                data.Add(i.MaSach);
            }

            return data;
        }

        public List<Sach> GetAllSach()
        {
            List<Sach> data = new List<Sach>();
            PBL3 db = new PBL3();
            foreach (Sach i in db.Sach)
            {
                data.Add(i);
            }

            return data;
        }

        public List<PhieuNhap> GetAllPhieuNhap()
        {
            List<PhieuNhap> data = new List<PhieuNhap>();
            PBL3 db = new PBL3();
            foreach (PhieuNhap i in db.PhieuNhap)
            {
                data.Add(i);
            }

            return data;
        }

        public void addPhieuNhap(PhieuNhap s)
        {
            PBL3 db = new PBL3();

            db.PhieuNhap.Add(s); // tu tu hinh nhu sai roi
            // a khong chay la van ra dung ma

            db.SaveChanges();
            //int n = GetAllPhieuNhap()[GetAllPhieuNhap().Count - 1].MaPN;//ni là chi la tra ve MaPN cuoi cung vi khi them vo thi doi tuong moi,  ủa m chưa chỉnh tự rand mã à thi sau khi rand t lay ma no trong database ra do
            //return n;
        }

        public void addPhieuNhapCT(PhieuNhapCT s)
        {
            PBL3 db = new PBL3();

            db.PhieuNhapCT.Add(s);
            db.SaveChanges();

        }

        public double GetGiaNhapByMaSach(int MaSach)
        {
            double s = 0;
            foreach (Sach i in GetAllSach())
            {
                if (i.MaSach == MaSach)
                {
                    s = i.DonGia;
                }
            }
            return s;
        }

        public void UpdateSoLuongSachTang(int soluong, int MaSach)
        {
            PBL3 db = new PBL3();
            Sach edit = db.Sach.Find(MaSach);
            edit.SoLuongTon += soluong;

            db.SaveChanges();

        }

        public void UpdateSoLuongSachGiam(int soluong, int MaSach)
        {
            PBL3 db = new PBL3();
            Sach edit = db.Sach.Find(MaSach);
            edit.SoLuongTon -= soluong;

            db.SaveChanges();

        }

        public void DeletePNCT(int MaPNCT)
        {
            PBL3 db = new PBL3();
            PhieuNhapCT delS = db.PhieuNhapCT.Find(MaPNCT); // tim theo thuoc tinh khoa chinh
            db.PhieuNhapCT.Remove(delS);
            db.SaveChanges();
        }

        public List<SachPN> GetSachPNByMaPN(int x)
        {
            PBL3 db = new PBL3();
            var l = from p in db.PhieuNhapCT where p.MaPN == x select new SachPN { MaPNCT = p.MaPNCT, TenSach = p.Sach.TenSach, SoLuong = p.SoLuong, GiaNhap = p.GiaNhap};
            return l.ToList();
        }
    }
}
