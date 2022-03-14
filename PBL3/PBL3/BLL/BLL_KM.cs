using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.BLL;
using PBL3.DTO;

namespace PBL3.BLL
{
    class BLL_KM
    {
        public static BLL_KM _Instance;
        public static BLL_KM Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_KM();
                }
                return _Instance;
            }
            private set { }
        }
        public List<KhuyenMaiCT> GetListKhuyenMaiCTVyByMaKM(int MaKM)
        {
            PBL3 db = new PBL3();
            // List<KhuyenMaiCT> data = new List<KhuyenMaiCT>();
            return db.KhuyenMaiCT.Where(p => p.MaKMCT == MaKM).ToList();
        }
        public List<int> GetMaSach(int MaKM)
        {
            List<int> data = new List<int>();
            foreach (KhuyenMaiCT i in GetListKhuyenMaiCTVyByMaKM(MaKM))
            {
                data.Add(i.MaSach);
            }
            return data;
        }
        public Sach GetSachByMaSach(int MaSach)
        {
            Sach s = null;
            PBL3 db = new PBL3();
            foreach (Sach i in db.Sach)
            {
                if (i.MaSach == MaSach)
                {
                    s = i;
                }
            }
            return s;
        }
        public List<Sach> GetSachKM(int MaKM, string tenKM)
        {
            PBL3 db = new PBL3();
            List<Sach> data = new List<Sach>();
            foreach (int i in GetMaSach(MaKM))
            {
                data.Add(GetSachByMaSach(i));
            }
            return data;

        }
        public List<SachKM> GetKhuyenMaiCT()
        {
            PBL3 db = new PBL3();
            var l = from p in db.KhuyenMaiCT select new SachKM { MaKMCT = p.MaKMCT, TenSach = p.Sach.TenSach, DonGiaKM = p.DonGiaKM, TenKM = p.KhuyenMai.TenKM };
            return l.ToList();
        }
        public List<SachKM> GetKhuyenMaiCTByMaKM(int MaKM, int MaSach)
        {
            PBL3 db = new PBL3();
            if (MaKM == 0 && MaSach == 0)
            {
                var l = from p in db.KhuyenMaiCT select new SachKM { MaKMCT = p.MaKMCT, TenSach = p.Sach.TenSach, DonGiaKM = p.DonGiaKM, TenKM = p.KhuyenMai.TenKM};
                return l.ToList();

            }
            else if (MaKM == 0 && MaSach != 0)
            {
                var l = from p in db.KhuyenMaiCT where p.MaSach == MaSach select new SachKM { MaKMCT = p.MaKMCT, TenSach = p.Sach.TenSach, DonGiaKM = p.DonGiaKM, TenKM = p.KhuyenMai.TenKM };
                return l.ToList();
            }
            else if (MaKM != 0 && MaSach == 0)
            {
                var l = from p in db.KhuyenMaiCT where p.MaKM == MaKM select new SachKM { MaKMCT = p.MaKMCT, TenSach = p.Sach.TenSach, DonGiaKM = p.DonGiaKM, TenKM = p.KhuyenMai.TenKM };
                return l.ToList();
            }
            else
            {
                var l = from p in db.KhuyenMaiCT where p.MaSach == MaSach  where p.MaKM == MaKM select new SachKM { MaKMCT = p.MaKMCT, TenSach = p.Sach.TenSach, DonGiaKM = p.DonGiaKM, TenKM = p.KhuyenMai.TenKM };
                return l.ToList();
            }

        }
        public string GetTenSachByMaKMCT(int MaKMCT)
        {
            string s = "";
            PBL3 db = new PBL3();
            foreach (KhuyenMaiCT i in db.KhuyenMaiCT)
            {
                if (i.MaKMCT == MaKMCT)
                {
                    s = GetSachByMaSach(i.MaSach).TenSach;
                }
            }
            return s;
        }
        public double GetDonGiaKMByMaKMCT(int MaKMCT)
        {
            double s = 0;
            PBL3 db = new PBL3();
            foreach (KhuyenMaiCT i in db.KhuyenMaiCT)
            {
                if (i.MaKMCT == MaKMCT)
                {
                    s = i.DonGiaKM;
                }
            }
            return s;
        }
        public int GetMaKMByMaKMCT(int MaKMCT)
        {
            int s = 0;
            PBL3 db = new PBL3();
            foreach (KhuyenMaiCT i in db.KhuyenMaiCT)
            {
                if (i.MaKMCT == MaKMCT)
                {
                    s = i.MaKM;
                }
            }
            return s;
        }
        public string GetTenKMByMaKM(int MaKM)
        {
            string s = "";
            PBL3 db = new PBL3();
            foreach (KhuyenMai i in db.KhuyenMai)
            {
                if (i.MaKM == MaKM)
                {
                    s = i.TenKM;
                }
            }
            return s;
        }
        public void ExecuteDB(KhuyenMaiCT s)
        {
            PBL3 db = new PBL3();
            bool check = false;
            foreach (KhuyenMaiCT i in db.KhuyenMaiCT)
            {
                if (i.MaKMCT == s.MaKMCT)
                {
                    check = true;
                }
            }
            if (check)
            {
                // EditDB(s);
                KhuyenMaiCT edit = db.KhuyenMaiCT.Find(s.MaKMCT);
                edit.MaSach = s.MaSach;
                edit.DonGiaKM = s.DonGiaKM;
                edit.MaKM = s.MaKM;
                db.SaveChanges();

            }
            else
            {
                //AddDB(s);
                {
                    db.KhuyenMaiCT.Add(s);
                    db.SaveChanges();
                }
            }
        }
        public void DeleteKMCT(int MaKMCT)
        {
            PBL3 db = new PBL3();
            KhuyenMaiCT delS = db.KhuyenMaiCT.Find(MaKMCT); // tim theo thuoc tinh khoa chinh
            db.KhuyenMaiCT.Remove(delS);
            db.SaveChanges();
        }
        public List<Sach> GetAllSach()
        {
            PBL3 db = new PBL3();
            return db.Sach.ToList();
        }
        public List<KhuyenMai> GetAllKM()
        {
            PBL3 db = new PBL3();
            return db.KhuyenMai.ToList();
        }
    }
}
