using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.BLL;
using PBL3.DTO;

namespace PBL3.BLL
{
    class BLL_PBL3
    {
        public static BLL_PBL3 _Instance;
        public static BLL_PBL3 Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_PBL3();
                }
                return _Instance;
            }
            private set { }
        }

        public Sach GetSachByMS(int x)
        {
            PBL3 db = new PBL3();
            Sach s = db.Sach.Find(x);
            return s;
        }

        public KhuyenMaiCT GetKMCTByMS(int x)
        {
            PBL3 db = new PBL3();
            KhuyenMaiCT kmct = null;
            foreach (KhuyenMaiCT i in db.KhuyenMaiCT)
            {
                if (i.MaSach == x)
                {
                    kmct = i;
                }
            }
            return kmct;
        }

        public KhuyenMai GetKMByKMCT(KhuyenMaiCT k)
        {
            PBL3 db = new PBL3();
            KhuyenMai km = db.KhuyenMai.Find(k.MaKM);
            return km;
        }

        public void AddHD(HoaDon hd)
        {
            PBL3 db = new PBL3();
            db.HoaDon.Add(hd);
            db.SaveChanges();
        }

        public void AddHDCT(HoaDonCT hdct)
        {
            PBL3 db = new PBL3();
            db.HoaDonCT.Add(hdct);
            db.SaveChanges();
        }

        public List<HoaDonCT> GetListCTHD(string MHD)
        {
            PBL3 db = new PBL3();
            List<HoaDonCT> data = new List<HoaDonCT>();
            int mhd = Convert.ToInt32(MHD);
            var l2 = db.HoaDonCT.Where(p => p.MaHD == mhd).Select(p => p);
            data = l2.ToList();
            return data;
        }

        public HoaDonCT getHDCTByMHDCT(int x)
        {
            PBL3 db = new PBL3();
            HoaDonCT s = db.HoaDonCT.Find(x);
            return s;
        }

        public void DeleteHDCT(int x)
        {
            PBL3 db = new PBL3();
            HoaDonCT s = db.HoaDonCT.Find(x);
            db.HoaDonCT.Remove(s);
            db.SaveChanges();
        }

        public void AddLogin(Login l)
        {
            PBL3 db = new PBL3();
            db.Login.Add(l);
            db.SaveChanges();
        }

        public List<Login> GetListLogin(string s)
        {
            List<Login> data = new List<Login>();
            PBL3 db = new PBL3();
            if (s == "All")
            {
                data = db.Login.Select(p => p).ToList();
                return data;
            }
            else
            {
                data = db.Login.Where(p => p.ChucVu == s).Select(p => p).ToList();
                return data;
            }
        }

        public Login GetLoginByID(int id)
        {
            PBL3 db = new PBL3();
            Login l = db.Login.Find(id);
            return l;
        }

        public void EditLogin(int id, string mk, string tendn, string cv)
        {
            PBL3 db = new PBL3();
            Login l = db.Login.Find(id);
            l.MatKhau = mk;
            l.TenDN = tendn;
            l.ChucVu = cv;
            db.SaveChanges();
        }

        public void DeleteLogin(int id)
        {
            PBL3 db = new PBL3();
            Login l = db.Login.Find(id);
            db.Login.Remove(l);
            db.SaveChanges();
        }

        public void AddMH(MatHang m)
        {
            PBL3 db = new PBL3();
            db.MatHang.Add(m);
            db.SaveChanges();
        }

        public List<KhuyenMai> GetListKM()
        {
            PBL3 db = new PBL3();
            return db.KhuyenMai.ToList();
        }

        public void AddKM(string s, DateTime t1, DateTime t2)
        {
            PBL3 db = new PBL3();
            //KhuyenMai kl = GetKMLast();
            KhuyenMai k = new KhuyenMai();
            //k.MaKM = kl.MaKM + 1;
            k.TenKM = s;
            k.TGBatDau = t1.Date;
            k.TGKetThuc = t2.Date;
            db.KhuyenMai.Add(k);
            db.SaveChanges();
        }

        public void EditKM(string makm, string s, DateTime t1, DateTime t2)
        {
            PBL3 db = new PBL3();
            int mkm = Convert.ToInt32(makm);
            KhuyenMai k = db.KhuyenMai.Find(mkm);
            k.TenKM = s;
            k.TGBatDau = t1.Date;
            k.TGKetThuc = t2.Date;
            db.SaveChanges();
        }

        public void DeleteKM(int makm)
        {
            PBL3 db = new PBL3();
            KhuyenMai k = db.KhuyenMai.Find(makm);
            db.KhuyenMai.Remove(k);
            db.SaveChanges();
        }

        public bool CheckTenDN(string s)
        {
            PBL3 db = new PBL3();
            foreach (Login i in db.Login)
            {
                if (i.TenDN == s) return false;
            }
            return true;
        }

        public void DownSLT(int x, int sl)
        {
            PBL3 db = new PBL3();
            Sach s = db.Sach.Find(x);
            s.SoLuongTon -= sl;
            db.SaveChanges();
        }

        public void UpSLT(int x, int sl)
        {
            PBL3 db = new PBL3();
            Sach s = db.Sach.Find(x);
            s.SoLuongTon += sl;
            db.SaveChanges();
        }

        public void AddNSX(NhaSX n)
        {
            PBL3 db = new PBL3();
            db.NhaSX.Add(n);
            db.SaveChanges();
        }

        public List<Sach> GetAllSach()
        {
            PBL3 db = new PBL3();
            return db.Sach.ToList();
        }

        public HoaDon GetHDByMaHD(int x)
        {
            PBL3 db = new PBL3();
            HoaDon hd = db.HoaDon.Find(x);
            return hd;
        }

        public List<SachHDCT> GetSachByMaHD(int x)
        {
            PBL3 db = new PBL3();
            var l = from p in db.HoaDonCT where p.MaHD == x select new SachHDCT { MaHDCT = p.MaHDCT, TenSach = p.Sach.TenSach, SoLuong = p.SoLuong, DonGia = p.DonGia, GiaKM = p.GiaKM };
            return l.ToList();
        }

        public List<SachPN> GetSachPNByDate(DateTime t1, DateTime t2)
        {
            PBL3 db = new PBL3();
            var l = from p in db.PhieuNhapCT where p.PhieuNhap.Ngay >= t1 where p.PhieuNhap.Ngay <= t2 select new SachPN { MaPNCT = p.MaPNCT, TenSach = p.Sach.TenSach, SoLuong = p.SoLuong, GiaNhap = p.GiaNhap };
            return l.ToList();
        }

        public List<HoaDonCT> GetHDCTByDate(DateTime t1, DateTime t2)
        {
            PBL3 db = new PBL3();
            var l = from p in db.HoaDonCT where p.HoaDon.Ngay >= t1 where p.HoaDon.Ngay <= t2 select p;
            return l.ToList();
        }

        public List<SachHD> GetSachHDByDate(DateTime t1, DateTime t2)
        {
            PBL3 db = new PBL3();
            var l = from p in db.HoaDonCT where p.HoaDon.Ngay >= t1 where p.HoaDon.Ngay <= t2 select new SachHD
            {
                MaHDCT = p.MaHDCT,
                DonGia = p.DonGia,
                GiaKM = p.GiaKM,
                SoLuong = p.SoLuong,
                TenSach = p.Sach.TenSach,
                TG = p.HoaDon.Ngay,
                Name = p.HoaDon.Login.TenDN
            };
            return l.ToList();
        }

        public List<PhieuNhapCT> GetPNCTByDate(DateTime t1, DateTime t2)
        {
            PBL3 db = new PBL3();
            var l = from p in db.PhieuNhapCT where p.PhieuNhap.Ngay >= t1 where p.PhieuNhap.Ngay <= t2 select p;
            return l.ToList();
        }

        public List<int> GetListMaSachByDate(DateTime t1, DateTime t2, int x)
        {
            PBL3 db = new PBL3();
            if (x == 1)
            {
                //var l = from p in db.HoaDonCT where p.HoaDon.Ngay >= t1 where p.HoaDon.Ngay <= t2 select new { p.MaSach };
                List<HoaDonCT> l = new List<HoaDonCT>();
                l = db.HoaDonCT.Where(p => p.HoaDon.Ngay >= t1).Where(p => p.HoaDon.Ngay <= t2).Select(p => p).ToList();
                List<int> l1 = new List<int>();
                //List<int> l2 = new List<int>();
                foreach (HoaDonCT i in l)
                {
                    l1.Add(i.MaSach);
                }
                var l2 = l1.Distinct();
                return l2.ToList();
            }
            else
            {
                List<PhieuNhapCT> l = new List<PhieuNhapCT>();
                l = db.PhieuNhapCT.Where(p => p.PhieuNhap.Ngay >= t1).Where(p => p.PhieuNhap.Ngay <= t2).Select(p => p).ToList();
                List<int> l1 = new List<int>();
                //List<int> l2 = new List<int>();
                foreach (PhieuNhapCT i in l)
                {
                    l1.Add(i.MaSach);
                }
                var l2 = l1.Distinct();
                return l2.ToList();
            }
        }
    }
}
