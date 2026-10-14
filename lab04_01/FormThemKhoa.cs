namespace lab04_01
{
    public partial class FormThemKhoa : Form
    {
        // Form cha (Form1) đọc property này SAU KHI ShowDialog() trả về DialogResult.OK
        public string TenKhoaMoi { get; private set; } = "";

        public FormThemKhoa()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            TenKhoaMoi = txtTenKhoa.Text.Trim();

            if (string.IsNullOrWhiteSpace(TenKhoaMoi))
            {
                MessageBox.Show("Vui lòng nhập tên Khoa!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhoa.Focus();
                return;
            }

            // Gán DialogResult -> form đang mở bằng ShowDialog() sẽ tự đóng và trả về OK
            this.DialogResult = DialogResult.OK;
        }

        // Nút Hủy không cần viết code: trong Designer đã đặt btnHuy.DialogResult = Cancel,
        // bấm vào là form tự đóng và trả về Cancel.
    }
}