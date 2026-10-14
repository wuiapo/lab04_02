using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab04_01.Models;

namespace lab04_01
{
    public partial class Quản_lý_khoa : Form
    {
        // Id của khoa đang được chọn để Sửa/Xóa (null = đang ở chế độ Thêm mới)
        private int? idDangSua = null;

        public Quản_lý_khoa()
        {
            InitializeComponent();

            // Gắn sự kiện bằng code (giống Form1). Sự kiện Load đã được Designer gắn sẵn.
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            dgvKhoa.CellClick += dgvKhoa_CellClick;
            chkNamChuaRo.CheckedChanged += (s, e) => nudNamThanhLap.Enabled = !chkNamChuaRo.Checked;
            chkGVChuaRo.CheckedChanged += (s, e) => nudTongSoGV.Enabled = !chkGVChuaRo.Checked;
            dgvKhoa.DataBindingComplete += (s, e) => dgvKhoa.ClearSelection();   // không tự chọn dòng đầu
        }

        // ============================== KHỞI TẠO ==============================
        private void Quản_lý_khoa_Load(object sender, EventArgs e)
        {
            CauHinhGiaoDien();
            LamMoiForm();
            LayDanhSachKhoa();
        }

        private void CauHinhGiaoDien()
        {
            // Đặt Maximum trước rồi mới đặt Minimum (mặc định Maximum = 100 < 1900)
            nudNamThanhLap.Maximum = DateTime.Today.Year;
            nudNamThanhLap.Minimum = 1900;
            nudTongSoGV.Minimum = 0;
            nudTongSoGV.Maximum = 10000;

            dgvKhoa.ReadOnly = true;
            dgvKhoa.AllowUserToAddRows = false;
            dgvKhoa.AllowUserToDeleteRows = false;
            dgvKhoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhoa.MultiSelect = false;
            dgvKhoa.RowHeadersVisible = false;
            dgvKhoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhoa.BackgroundColor = Color.White;

            // Tiêu đề cột nền xanh chữ trắng giống mẫu
            dgvKhoa.EnableHeadersVisualStyles = false;
            dgvKhoa.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(13, 71, 161);
            dgvKhoa.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKhoa.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }

        // ============================== ĐỌC DỮ LIỆU ==============================
        private void LayDanhSachKhoa()
        {
            try
            {
                using var db = new QuanLySinhVienDbContext();
                List<Khoa> dsKhoa = db.Khoas.OrderBy(k => k.TenKhoa).ToList();

                dgvKhoa.DataSource = dsKhoa;

                // Cột được tạo tự động theo thuộc tính của Khoa -> đặt lại tiêu đề, ẩn Id
                dgvKhoa.Columns[nameof(Khoa.Id)].Visible = false;
                dgvKhoa.Columns[nameof(Khoa.TenKhoa)].HeaderText = "Tên Khoa";
                dgvKhoa.Columns[nameof(Khoa.NamThanhLap)].HeaderText = "Năm thành lập";
                dgvKhoa.Columns[nameof(Khoa.TongSoGiangVien)].HeaderText = "Tổng số Giảng viên";

                // Giá trị null hiển thị "Chưa có" thay vì ô trống
                dgvKhoa.Columns[nameof(Khoa.NamThanhLap)].DefaultCellStyle.NullValue = "Chưa có";
                dgvKhoa.Columns[nameof(Khoa.TongSoGiangVien)].DefaultCellStyle.NullValue = "Chưa có";

                tslblTongSo.Text = $"Tổng số Khoa: {dsKhoa.Count}";
                tslblTrangThai.Text = "Sẵn sàng";
            }
            catch (Exception ex)
            {
                BaoLoiCSDL("Lỗi kết nối", ex);
            }
        }

        // ============================== CLICK CHỌN DÒNG ==============================
        private void dgvKhoa_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;                                          // click vào tiêu đề cột
            if (dgvKhoa.Rows[e.RowIndex].DataBoundItem is not Khoa khoa) return;

            txtTenKhoa.Text = khoa.TenKhoa;

            chkNamChuaRo.Checked = !khoa.NamThanhLap.HasValue;
            if (khoa.NamThanhLap.HasValue)
                nudNamThanhLap.Value = khoa.NamThanhLap.Value;

            chkGVChuaRo.Checked = !khoa.TongSoGiangVien.HasValue;
            if (khoa.TongSoGiangVien.HasValue)
                nudTongSoGV.Value = khoa.TongSoGiangVien.Value;

            idDangSua = khoa.Id;
            tslblTrangThai.Text = $"Đang chọn: {khoa.TenKhoa}";
        }

        // ============================== THÊM ==============================
        private void btnThem_Click(object? sender, EventArgs e)
        {
            if (!DocDuLieuNhap(out string tenKhoa, out int? namThanhLap, out int? tongSoGV))
                return;

            try
            {
                using var db = new QuanLySinhVienDbContext();

                if (db.Khoas.Any(k => k.TenKhoa == tenKhoa))
                {
                    CanhBao($"Khoa \"{tenKhoa}\" đã tồn tại!", txtTenKhoa);
                    return;
                }

                db.Khoas.Add(new Khoa
                {
                    TenKhoa = tenKhoa,
                    NamThanhLap = namThanhLap,
                    TongSoGiangVien = tongSoGV
                });
                db.SaveChanges();

                MessageBox.Show("Thêm khoa thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiForm();
                LayDanhSachKhoa();
            }
            catch (Exception ex)
            {
                BaoLoiCSDL("Thêm thất bại", ex);
            }
        }

        // ============================== SỬA ==============================
        private void btnSua_Click(object? sender, EventArgs e)
        {
            if (idDangSua == null)
            {
                MessageBox.Show("Vui lòng chọn khoa cần sửa!", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DocDuLieuNhap(out string tenKhoa, out int? namThanhLap, out int? tongSoGV))
                return;

            int id = idDangSua.Value;
            try
            {
                using var db = new QuanLySinhVienDbContext();

                // Trùng tên với một khoa KHÁC (không tính chính nó)
                if (db.Khoas.Any(k => k.TenKhoa == tenKhoa && k.Id != id))
                {
                    CanhBao($"Khoa \"{tenKhoa}\" đã tồn tại!", txtTenKhoa);
                    return;
                }

                Khoa? khoa = db.Khoas.Find(id);
                if (khoa == null)
                {
                    MessageBox.Show("Khoa này không còn tồn tại trong CSDL!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LamMoiForm();
                    LayDanhSachKhoa();
                    return;
                }

                khoa.TenKhoa = tenKhoa;
                khoa.NamThanhLap = namThanhLap;
                khoa.TongSoGiangVien = tongSoGV;
                db.SaveChanges();

                MessageBox.Show("Cập nhật thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiForm();
                LayDanhSachKhoa();
            }
            catch (Exception ex)
            {
                BaoLoiCSDL("Cập nhật thất bại", ex);
            }
        }

        // ============================== XÓA ==============================
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            if (idDangSua == null)
            {
                MessageBox.Show("Vui lòng chọn khoa cần xóa!", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = idDangSua.Value;
            try
            {
                using var db = new QuanLySinhVienDbContext();
                Khoa? khoa = db.Khoas.Find(id);
                if (khoa == null)
                {
                    LamMoiForm();
                    LayDanhSachKhoa();
                    return;
                }

                // Không cho xóa khoa khi vẫn còn sinh viên thuộc khoa đó
                int soSinhVien = db.SinhViens.Count(sv => sv.KhoaId == id);
                if (soSinhVien > 0)
                {
                    MessageBox.Show(
                        $"Không thể xóa khoa \"{khoa.TenKhoa}\" vì đang có {soSinhVien} sinh viên thuộc khoa này.\n" +
                        "Hãy chuyển các sinh viên sang khoa khác (hoặc xóa họ) trước.",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var xacNhan = MessageBox.Show($"Bạn có chắc muốn xóa khoa \"{khoa.TenKhoa}\"?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xacNhan != DialogResult.Yes) return;

                db.Khoas.Remove(khoa);
                db.SaveChanges();

                LamMoiForm();
                LayDanhSachKhoa();
                tslblTrangThai.Text = "Đã xóa khoa";
            }
            catch (Exception ex)
            {
                BaoLoiCSDL("Xóa thất bại", ex);
            }
        }

        // ============================== LÀM MỚI ==============================
        private void btnLamMoi_Click(object? sender, EventArgs e) => LamMoiForm();

        private void LamMoiForm()
        {
            txtTenKhoa.Clear();
            nudNamThanhLap.Value = nudNamThanhLap.Minimum;
            nudTongSoGV.Value = 0;
            chkNamChuaRo.Checked = true;      // mặc định "Chưa rõ" giống hình mẫu
            chkGVChuaRo.Checked = true;
            nudNamThanhLap.Enabled = false;   // gán lại cho chắc (nếu Checked vốn đã true thì CheckedChanged không chạy)
            nudTongSoGV.Enabled = false;

            idDangSua = null;
            dgvKhoa.ClearSelection();
            tslblTrangThai.Text = "Sẵn sàng";
            txtTenKhoa.Focus();
        }

        // ============================== HÀM PHỤ ==============================

        // Đọc + validate dữ liệu trên form. Trả về false nếu có lỗi
        private bool DocDuLieuNhap(out string tenKhoa, out int? namThanhLap, out int? tongSoGV)
        {
            tenKhoa = txtTenKhoa.Text.Trim();
            namThanhLap = chkNamChuaRo.Checked ? null : (int)nudNamThanhLap.Value;
            tongSoGV = chkGVChuaRo.Checked ? null : (int)nudTongSoGV.Value;

            if (tenKhoa == "")
            {
                CanhBao("Vui lòng nhập Tên Khoa!", txtTenKhoa);
                return false;
            }
            // Năm (1900 -> năm hiện tại) và số giảng viên (>= 0) đã được Minimum/Maximum của NumericUpDown chặn sẵn
            return true;
        }

        private static void CanhBao(string thongBao, Control oBiLoi)
        {
            MessageBox.Show(thongBao, "Dữ liệu chưa hợp lệ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            oBiLoi.Focus();
        }

        private void BaoLoiCSDL(string tieuDe, Exception ex)
        {
            MessageBox.Show(
                $"{ex.GetBaseException().Message}\n\n" +
                "Kiểm tra: đã chạy Add-Migration + Update-Database sau khi thêm 2 cột mới chưa?",
                tieuDe, MessageBoxButtons.OK, MessageBoxIcon.Error);
            tslblTrangThai.Text = "Lỗi kết nối CSDL";
        }
    }
}
