//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PBL3
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            this.HoaDonCT = new HashSet<HoaDonCT>();
            this.KhuyenMaiCT = new HashSet<KhuyenMaiCT>();
            this.PhieuNhapCT = new HashSet<PhieuNhapCT>();
        }
    
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string DVT { get; set; }
        public int SoLuongTon { get; set; }
        public double DonGia { get; set; }
        public double GiaNhap { get; set; }
        public int MaMH { get; set; }
        public int MaNSX { get; set; }
        public string MoTa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonCT> HoaDonCT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhuyenMaiCT> KhuyenMaiCT { get; set; }
        public virtual MatHang MatHang { get; set; }
        public virtual NhaSX NhaSX { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuNhapCT> PhieuNhapCT { get; set; }
    }
}
