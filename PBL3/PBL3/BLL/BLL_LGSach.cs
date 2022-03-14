using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DTO;
using PBL3.BLL;

namespace PBL3.BLL
{
    class BLL_LGSach
    {
        public static BLL_LGSach _Instance;
        public static BLL_LGSach Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_LGSach();
                }
                return _Instance;
            }
            private set { }
        }
        public bool Check(string ChucVu, string User, string MatKhau)
        {
            PBL3 ql = new PBL3();
            foreach (Login i in ql.Login)
            {
                if (i.ChucVu == ChucVu && i.TenDN == User && i.MatKhau == MatKhau) return true;
            }
            return false;

        }
        public int GetID(string User, string MatKhau)
        {
            // TenDN khong duoc trung nhau
            PBL3 ql = new PBL3();
            Login lg = new Login();
            foreach (Login i in ql.Login.Where(p => p.TenDN.Contains(User) && p.MatKhau.Contains(MatKhau)).ToList())
            {
                lg = i;
            }
            return lg.ID;
        }
        public string GetCV(string User, string MatKhau)
        {
            // TenDN khong duoc trung nhau
            PBL3 ql = new PBL3();
            Login lg = new Login();
            foreach (Login i in ql.Login.Where(p => p.TenDN.Contains(User) && p.MatKhau.Contains(MatKhau)).ToList())
            {
                lg = i;
            }
            return lg.ChucVu;
        }
        public List<MatHang> GetAll_MH()
        {
            PBL3 ql = new PBL3();
            return ql.MatHang.Select(p => p).ToList();
        }
        public List<NhaSX> GetAll_NSX()
        {
            PBL3 ql = new PBL3();
            return ql.NhaSX.Select(p => p).ToList();
        }
        public List<SachQL> GetSachByMaMH(int MaMH)
        {
            PBL3 ql = new PBL3();
            if (MaMH == 0)
            {
                var l = from p in ql.Sach
                        select new SachQL
                        {
                            MaSach = p.MaSach,
                            TenSach = p.TenSach,
                            DVT = p.DVT,
                            SoLuongTon = p.SoLuongTon,
                            DonGia = p.DonGia,
                            GiaNhap = p.GiaNhap,
                            TenMH = p.MatHang.TenMH,
                            TenNSX = p.NhaSX.TenNSX,
                            MoTa = p.MoTa
                        };
                return l.ToList();
            }
            else
            {
                var l = from p in ql.Sach where p.MaMH == MaMH
                        select new SachQL
                        {
                            MaSach = p.MaSach,
                            TenSach = p.TenSach,
                            DVT = p.DVT,
                            SoLuongTon = p.SoLuongTon,
                            DonGia = p.DonGia,
                            GiaNhap = p.GiaNhap,
                            TenMH = p.MatHang.TenMH,
                            TenNSX = p.NhaSX.TenNSX,
                            MoTa = p.MoTa
                        };
                return l.ToList();
            }    
        }
        public List<SachQL> GetTenSach(int MaMH, string Ten)
        {
            PBL3 ql = new PBL3();
            if (MaMH == 0)
            {
                var l = from p in ql.Sach
                        where p.TenSach == Ten
                        select new SachQL
                        {
                            MaSach = p.MaSach,
                            TenSach = p.TenSach,
                            DVT = p.DVT,
                            SoLuongTon = p.SoLuongTon,
                            DonGia = p.DonGia,
                            GiaNhap = p.GiaNhap,
                            TenMH = p.MatHang.TenMH,
                            TenNSX = p.NhaSX.TenNSX,
                            MoTa = p.MoTa
                        };
                return l.ToList();
            }
            else
            {
                var l = from p in ql.Sach
                        where p.TenSach == Ten where p.MaMH == MaMH
                        select new SachQL
                        {
                            MaSach = p.MaSach,
                            TenSach = p.TenSach,
                            DVT = p.DVT,
                            SoLuongTon = p.SoLuongTon,
                            DonGia = p.DonGia,
                            GiaNhap = p.GiaNhap,
                            TenMH = p.MatHang.TenMH,
                            TenNSX = p.NhaSX.TenNSX,
                            MoTa = p.MoTa
                        };
                return l.ToList();
            }
        }
        public Sach GetSachByMaSach(int MaSach)
        {
            PBL3 db = new PBL3();
            Sach s = db.Sach.Find(MaSach);
            if (s == null) s = new Sach();
            return s;
        }
        public void AddSach(Sach s)
        {
            PBL3 db = new PBL3();
            db.Sach.Add(s);
            db.SaveChanges();
        }
        public void EditSach(Sach s)
        {
            PBL3 db = new PBL3();
            Sach sedit = db.Sach.Find(s.MaSach);
            sedit.TenSach = s.TenSach;
            sedit.MaMH = s.MaMH;
            sedit.MaNSX = s.MaNSX;
            sedit.DonGia = s.DonGia;
            sedit.GiaNhap = s.GiaNhap;
            sedit.DVT = s.DVT;
            sedit.SoLuongTon = s.SoLuongTon;
            sedit.MoTa = s.MoTa;
            db.SaveChanges();
        }
        public void ExecuteSach(Sach s)
        {
            bool check = false;
            PBL3 db = new PBL3();
            if (db.Sach.Find(s.MaSach) != null) check = true;
            if (check == true) EditSach(s);
            else AddSach(s);
        }
        public void DeleteSach(int MaSach)
        {
            PBL3 db = new PBL3();
            Sach s = db.Sach.Find(MaSach);
            db.Sach.Remove(s);
            db.SaveChanges();
        }
        public int CompareUp_MaSach(SachQL s1, SachQL s2)
        {
            return s1.MaSach.CompareTo(s2.MaSach);
        }
        public int CompareUp_TenSach(SachQL s1, SachQL s2)
        {
            return s1.TenSach.CompareTo(s2.TenSach);
        }
        public int CompareDown_MaSach(SachQL s1, SachQL s2)
        {
            return s2.MaSach.CompareTo(s1.MaSach);
        }
        public int CompareDown_TenSach(SachQL s1, SachQL s2)
        {
            return s2.TenSach.CompareTo(s1.TenSach);
        }
        public List<SachQL> GetAllSach()
        {
            PBL3 db = new PBL3();
            var l = from p in db.Sach
                    select new SachQL
                    {
                        MaSach = p.MaSach,
                        TenSach = p.TenSach,
                        DVT = p.DVT,
                        SoLuongTon = p.SoLuongTon,
                        DonGia = p.DonGia,
                        GiaNhap = p.GiaNhap,
                        TenMH = p.MatHang.TenMH,
                        TenNSX = p.NhaSX.TenNSX,
                        MoTa = p.MoTa
                    };
            return l.ToList();
        }
        public List<string> GetListTenSach()
        {
            List<string> ltensach = new List<string>();
            PBL3 db = new PBL3();
            var l = db.Sach.Select(p => p).ToList();
            foreach(Sach i in l)
            {
                ltensach.Add(i.TenSach);
            }
            return ltensach;
        }
    }
}
