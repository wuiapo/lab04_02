using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04_01.Models
{
    public class SinhVien
    {
        public int Id { get; set; }
        public string MaSV { get; set; } = "";
        public string HoTen { get; set; } = "";
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; } = "";
        public double? DiemTB { get; set; }
        public int KhoaId { get; set; } 
        public Khoa? Khoa { get; set; } 
    }
}
