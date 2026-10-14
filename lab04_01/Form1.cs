using System.Globalization;
using lab04_01.Models;
using Microsoft.EntityFrameworkCore;

namespace lab04_01
{
    public partial class Form1 : Form
    {
        // Id của sinh viên đang được chọn để Sửa/Xóa (null = đang ở chế độ Thêm mới)
        private int? idDangSua = null;

        public Form1()
        {
            InitializeComponent();

            // Gắn sự kiện bằng code -> KHÔNG cần double-click các nút trong Designer
            Load += Form1_Load;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnCong.Click += btnCong_Click;                 // nút "+" cạnh Khoa
            dataGridView1.CellClick += dataGridView1_CellClick;

            // Menu Dữ liệu -> Quản lý khoa (bấm chuột hoặc nhấn F1)
            mnuQuanLyKhoa.Click += mnuQuanLyKhoa_Click;
        }

        // 2 hàm rỗng này do lỡ double-click trong Designer tạo ra.
        // PHẢI giữ lại, vì Form1.Designer.cs đang tham chiếu tới chúng (xóa sẽ lỗi build).
        private void lblDiemTB_Click(object sender, EventArgs e) { }
        private void gpbThongTinSinhVien_Enter(object sender, EventArgs e) { }

        // ============================== KHỞI TẠO ==============================
        private void Form1_Load(object? sender, EventArgs e)
        {
            CauHinhGiaoDien();
            TaoMenu();
            LayDanhSachKhoa();
            LamMoiForm();
            LayDanhSachSinhVien();
        }

        private void CauHinhGiaoDien()
        {
            Text = "Quản Lý Sinh Viên";

            // Tiêu đề luôn nằm giữa header kể cả khi phóng to form
            lblQuanLyThongTinSV.AutoSize = false;
            lblQuanLyThongTinSV.Dock = DockStyle.Fill;
            lblQuanLyThongTinSV.TextAlign = ContentAlignment.MiddleCenter;

            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.MaxDate = DateTime.Today;

            cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;   // không cho gõ tay tên khoa

            // Bảng chiếm hết phần còn lại bên phải (trong Designer đang có kích thước cố định 877x143)
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Thêm mục con vào 2 menu "Tệp" và "Dữ liệu" đã vẽ sẵn
        private void TaoMenu()
        {
            tệpToolStripMenuItem.DropDownItems.Add("Thoát", null, (s, e) => Close());
            dữLiệuToolStripMenuItem.DropDownItems.Add("Thêm khoa mới", null, btnCong_Click);
            dữLiệuToolStripMenuItem.DropDownItems.Add("Xóa khoa đang chọn", null, (s, e) => XoaKhoaDangChon());
        }

        // ============================== ĐỌC DỮ LIỆU ==============================
        private void LayDanhSachKhoa(int? idKhoaCanChon = null)
        {
            try
            {
                using var db = new QuanLySinhVienDbContext();
                List<Khoa> dsKhoa = db.Khoas.OrderBy(k => k.TenKhoa).ToList();

                cboKhoa.DisplayMember = "TenKhoa";   // chữ hiển thị
                cboKhoa.ValueMember = "Id";          // giá trị lấy ra qua SelectedValue
                cboKhoa.DataSource = dsKhoa;

                if (idKhoaCanChon.HasValue)
                    cboKhoa.SelectedValue = idKhoaCanChon.Value;
            }
            catch (Exception ex)
            {
                BaoLoiCSDL("Lỗi kết nối", ex);
            }
        }

        private void LayDanhSachSinhVien()
        {
            try
            {
                using var db = new QuanLySinhVienDbContext();

                // Include: nạp kèm Khoa để lấy được TenKhoa (quan hệ 1-N)
                // ToList: chạy truy vấn NGAY, lấy dữ liệu về bộ nhớ trước khi DbContext bị hủy (using)
                List<SinhVien> dsSinhVien = db.SinhViens
                    .Include(sv => sv.Khoa)
                    .OrderBy(sv => sv.MaSV)
                    .ToList();

                dataGridView1.Rows.Clear();
                foreach (SinhVien sv in dsSinhVien)
                {
                    // Thứ tự khớp 6 cột trong Designer: Mã SV | Họ và tên | Ngày sinh | Giới tính | Khoa | Điểm TB
                    int dong = dataGridView1.Rows.Add(
                        sv.MaSV,
                        sv.HoTen,
                        sv.NgaySinh.ToString("dd/MM/yyyy"),
                        sv.GioiTinh,
                        sv.Khoa?.TenKhoa ?? "",
                        sv.DiemTB.HasValue ? sv.DiemTB.Value.ToString("0.##") : "Chưa có");

                    dataGridView1.Rows[dong].Tag = sv.Id;   // giấu Id vào Tag để dùng khi click chọn dòng
                }

                dataGridView1.ClearSelection();
                tslblTongSo.Text = $"Tổng số sinh viên: {dsSinhVien.Count}";
                tslblSanSang.Text = "Sẵn sàng";
            }
            catch (Exception ex)
            {
                BaoLoiCSDL("Lỗi kết nối", ex);
            }
        }

        // ============================== CLICK CHỌN DÒNG ==============================
        private void dataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;                                     // click vào tiêu đề cột
            if (dataGridView1.Rows[e.RowIndex].Tag is not int id) return;

            try
            {
                using var db = new QuanLySinhVienDbContext();
                SinhVien? sv = db.SinhViens.Find(id);
                if (sv == null) return;

                txtMaSoSV.Text = sv.MaSV;
                txtHoTen.Text = sv.HoTen;
                dtpNgaySinh.Value = sv.NgaySinh;
                radNam.Checked = sv.GioiTinh == "Nam";
                radNu.Checked = sv.GioiTinh == "Nữ";
                cboKhoa.SelectedValue = sv.KhoaId;      // chọn theo Id, không theo object
                txtDiemTB.Text = sv.DiemTB.HasValue
                    ? sv.DiemTB.Value.ToString(CultureInfo.InvariantCulture)
                    : "";

                idDangSua = sv.Id;
                txtMaSoSV.Enabled = false;              // Mã SV là khóa định danh, không cho sửa
                tslblSanSang.Text = $"Đang chọn: {sv.MaSV} - {sv.HoTen}";
            }
            catch (Exception ex)
            {
                BaoLoiCSDL("Lỗi đọc dữ liệu", ex);
            }
        }

        // ============================== THÊM ==============================
        private void btnThem_Click(object? sender, EventArgs e)
        {
            if (!DocDuLieuNhap(out string maSV, out string hoTen, out int khoaId, out double? diemTB))
                return;

            try
            {
                using var db = new QuanLySinhVienDbContext();

                // Kiểm tra trùng Mã SV bằng truy vấn thật trong database
                if (db.SinhViens.Any(sv => sv.MaSV == maSV))
                {
                    CanhBao($"Mã SV \"{maSV}\" đã tồn tại!", txtMaSoSV);
                    return;
                }

                db.SinhViens.Add(new SinhVien
                {
                    MaSV = maSV,
                    HoTen = hoTen,
                    NgaySinh = dtpNgaySinh.Value.Date,
                    GioiTinh = radNam.Checked ? "Nam" : "Nữ",
                    KhoaId = khoaId,
                    DiemTB = diemTB
                });
                db.SaveChanges();   // chưa gọi thì chưa ghi xuống database

                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiForm();
                LayDanhSachSinhVien();
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
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DocDuLieuNhap(out _, out string hoTen, out int khoaId, out double? diemTB))
                return;

            try
            {
                using var db = new QuanLySinhVienDbContext();

                // Tìm đúng bản ghi gốc theo khóa chính
                SinhVien? sv = db.SinhViens.Find(idDangSua.Value);
                if (sv == null)
                {
                    MessageBox.Show("Sinh viên này không còn tồn tại trong CSDL!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LamMoiForm();
                    LayDanhSachSinhVien();
                    return;
                }

                // Không gán lại Id và MaSV
                sv.HoTen = hoTen;
                sv.NgaySinh = dtpNgaySinh.Value.Date;
                sv.GioiTinh = radNam.Checked ? "Nam" : "Nữ";
                sv.KhoaId = khoaId;
                sv.DiemTB = diemTB;
                db.SaveChanges();

                MessageBox.Show("Cập nhật thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiForm();
                LayDanhSachSinhVien();
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
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var xacNhan = MessageBox.Show(
                $"Bạn có chắc muốn xóa sinh viên {txtMaSoSV.Text} - {txtHoTen.Text}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            try
            {
                using var db = new QuanLySinhVienDbContext();
                SinhVien? sv = db.SinhViens.Find(idDangSua.Value);
                if (sv != null)
                {
                    db.SinhViens.Remove(sv);
                    db.SaveChanges();
                }

                LamMoiForm();
                LayDanhSachSinhVien();
                tslblSanSang.Text = "Đã xóa sinh viên";
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
            txtMaSoSV.Clear();
            txtHoTen.Clear();
            txtDiemTB.Clear();
            dtpNgaySinh.Value = new DateTime(2000, 1, 1);
            radNam.Checked = true;
            if (cboKhoa.Items.Count > 0) cboKhoa.SelectedIndex = 0;

            txtMaSoSV.Enabled = true;
            idDangSua = null;
            dataGridView1.ClearSelection();
            txtMaSoSV.Focus();
        }

        // ============================== KHOA ==============================

        // Mở form Quản lý khoa. ShowDialog: phải đóng form này mới quay lại Form1
        private void mnuQuanLyKhoa_Click(object? sender, EventArgs e)
        {
            using var formQuanLyKhoa = new Quản_lý_khoa();
            formQuanLyKhoa.ShowDialog(this);

            // Bên form kia có thể đã thêm/sửa/xóa khoa -> nạp lại ComboBox Khoa và bảng sinh viên
            LayDanhSachKhoa();
            LamMoiForm();
            LayDanhSachSinhVien();
        }

        private void btnCong_Click(object? sender, EventArgs e)
        {
            using var formThemKhoa = new FormThemKhoa();
            if (formThemKhoa.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                using var db = new QuanLySinhVienDbContext();

                // Kiểm tra trùng tên Khoa bằng truy vấn database
                if (db.Khoas.Any(k => k.TenKhoa == formThemKhoa.TenKhoaMoi))
                {
                    MessageBox.Show($"Khoa \"{formThemKhoa.TenKhoaMoi}\" đã tồn tại!", "Trùng dữ liệu",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var khoaMoi = new Khoa { TenKhoa = formThemKhoa.TenKhoaMoi };
                db.Khoas.Add(khoaMoi);
                db.SaveChanges();                     // sau dòng này khoaMoi.Id đã có giá trị

                LayDanhSachKhoa(khoaMoi.Id);          // nạp lại và chọn luôn khoa vừa thêm
                MessageBox.Show("Thêm khoa mới thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                BaoLoiCSDL("Thêm khoa thất bại", ex);
            }
        }

        // Yêu cầu 7: không cho xóa Khoa khi vẫn còn sinh viên thuộc Khoa đó
        private void XoaKhoaDangChon()
        {
            if (cboKhoa.SelectedValue is not int khoaId)
            {
                MessageBox.Show("Chưa có khoa nào để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string tenKhoa = cboKhoa.Text;

            try
            {
                using var db = new QuanLySinhVienDbContext();

                int soSinhVien = db.SinhViens.Count(sv => sv.KhoaId == khoaId);
                if (soSinhVien > 0)
                {
                    MessageBox.Show(
                        $"Không thể xóa khoa \"{tenKhoa}\" vì đang có {soSinhVien} sinh viên thuộc khoa này.\n" +
                        "Hãy chuyển các sinh viên sang khoa khác (hoặc xóa họ) trước.",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var xacNhan = MessageBox.Show($"Bạn có chắc muốn xóa khoa \"{tenKhoa}\"?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xacNhan != DialogResult.Yes) return;

                Khoa? khoa = db.Khoas.Find(khoaId);
                if (khoa != null)
                {
                    db.Khoas.Remove(khoa);
                    db.SaveChanges();
                }

                LayDanhSachKhoa();
                LamMoiForm();
                tslblSanSang.Text = "Đã xóa khoa";
            }
            catch (Exception ex)
            {
                BaoLoiCSDL("Xóa khoa thất bại", ex);
            }
        }

        // ============================== HÀM PHỤ ==============================

        // Đọc + validate dữ liệu trên form. Trả về false nếu có lỗi (đã báo lỗi và Focus vào ô sai)
        private bool DocDuLieuNhap(out string maSV, out string hoTen, out int khoaId, out double? diemTB)
        {
            maSV = txtMaSoSV.Text.Trim();
            hoTen = txtHoTen.Text.Trim();
            khoaId = 0;
            diemTB = null;

            if (maSV == "")
            {
                CanhBao("Vui lòng nhập Mã SV!", txtMaSoSV);
                return false;
            }
            if (hoTen == "")
            {
                CanhBao("Vui lòng nhập Họ tên!", txtHoTen);
                return false;
            }
            if (cboKhoa.SelectedValue is not int idKhoa)
            {
                CanhBao("Chưa có Khoa nào. Bấm nút + để thêm Khoa trước!", cboKhoa);
                return false;
            }
            khoaId = idKhoa;

            string diemText = txtDiemTB.Text.Trim();
            if (diemText != "")   // để trống = chưa có điểm (null)
            {
                // Chấp nhận cả "8.5" và "8,5". Dùng InvariantCulture vì máy cài tiếng Việt
                // sẽ hiểu "8.5" thành 85 nếu parse theo culture mặc định.
                diemText = diemText.Replace(',', '.');
                if (!double.TryParse(diemText, NumberStyles.Float, CultureInfo.InvariantCulture, out double diem))
                {
                    CanhBao("Điểm TB phải là số hợp lệ!", txtDiemTB);
                    return false;
                }
                if (diem < 0 || diem > 10)
                {
                    CanhBao("Điểm TB phải nằm trong khoảng 0 - 10!", txtDiemTB);
                    return false;
                }
                diemTB = diem;
            }

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
                "Kiểm tra: đã chạy Update-Database chưa? SQL Server có đang chạy không?",
                tieuDe, MessageBoxButtons.OK, MessageBoxIcon.Error);
            tslblSanSang.Text = "Lỗi kết nối CSDL";
        }
    }
}