using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04_01.Models
{
    public class Khoa
    {
        public int Id { get; set; }                     // Khóa chính (EF Core tự nhận diện tên "Id")
        public string TenKhoa { get; set; } = "";

        // Bài tập 2: thêm 2 thông tin mới.
        // Kiểu int? (cho phép null) vì các khoa đã có trong database chưa có dữ liệu này -> "Chưa có".
        // Sau khi sửa file này PHẢI chạy: Add-Migration ThemThongTinKhoa  rồi  Update-Database
        public int? NamThanhLap { get; set; }
        public int? TongSoGiangVien { get; set; }

        public override string ToString() => TenKhoa;
    }
}
