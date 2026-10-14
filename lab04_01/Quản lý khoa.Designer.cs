namespace lab04_01
{
    partial class Quản_lý_khoa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlTieuDe = new Panel();
            lblTieuDe = new Label();
            gpbThongTinKhoa = new GroupBox();
            btnLamMoi = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnThem = new Button();
            nudTongSoGV = new NumericUpDown();
            chkGVChuaRo = new CheckBox();
            nudNamThanhLap = new NumericUpDown();
            chkNamChuaRo = new CheckBox();
            txtTenKhoa = new TextBox();
            lblNamThanhLap = new Label();
            lblTongSoGV = new Label();
            lblTenKhoa = new Label();
            dgvKhoa = new DataGridView();
            ssTrangThai = new StatusStrip();
            tslblTrangThai = new ToolStripStatusLabel();
            tslblTongSo = new ToolStripStatusLabel();
            pnlTieuDe.SuspendLayout();
            gpbThongTinKhoa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTongSoGV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNamThanhLap).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).BeginInit();
            ssTrangThai.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTieuDe
            // 
            pnlTieuDe.Controls.Add(lblTieuDe);
            pnlTieuDe.BackColor = Color.FromArgb(232, 240, 254);
            pnlTieuDe.Dock = DockStyle.Top;
            pnlTieuDe.Location = new Point(0, 0);
            pnlTieuDe.Name = "pnlTieuDe";
            pnlTieuDe.Size = new Size(1127, 99);
            pnlTieuDe.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            lblTieuDe.Dock = DockStyle.Fill;
            lblTieuDe.ForeColor = Color.FromArgb(13, 71, 161);
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTieuDe.Location = new Point(463, 25);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(203, 41);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ KHOA";
            // 
            // gpbThongTinKhoa
            // 
            gpbThongTinKhoa.Controls.Add(btnLamMoi);
            gpbThongTinKhoa.Controls.Add(btnXoa);
            gpbThongTinKhoa.Controls.Add(btnSua);
            gpbThongTinKhoa.Controls.Add(btnThem);
            gpbThongTinKhoa.Controls.Add(nudTongSoGV);
            gpbThongTinKhoa.Controls.Add(chkGVChuaRo);
            gpbThongTinKhoa.Controls.Add(nudNamThanhLap);
            gpbThongTinKhoa.Controls.Add(chkNamChuaRo);
            gpbThongTinKhoa.Controls.Add(txtTenKhoa);
            gpbThongTinKhoa.Controls.Add(lblNamThanhLap);
            gpbThongTinKhoa.Controls.Add(lblTongSoGV);
            gpbThongTinKhoa.Controls.Add(lblTenKhoa);
            gpbThongTinKhoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gpbThongTinKhoa.Dock = DockStyle.Left;
            gpbThongTinKhoa.Location = new Point(0, 99);
            gpbThongTinKhoa.Name = "gpbThongTinKhoa";
            gpbThongTinKhoa.Size = new Size(371, 601);
            gpbThongTinKhoa.TabIndex = 1;
            gpbThongTinKhoa.TabStop = false;
            gpbThongTinKhoa.Text = "Thông Tin Khoa";
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(185, 410);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(140, 45);
            btnLamMoi.TabIndex = 12;
            btnLamMoi.BackColor = Color.FromArgb(108, 117, 125);
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.Text = "↻ Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(21, 410);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(140, 45);
            btnXoa.TabIndex = 11;
            btnXoa.BackColor = Color.FromArgb(220, 53, 69);
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.ForeColor = Color.White;
            btnXoa.Text = "✕ Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(185, 351);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(140, 45);
            btnSua.TabIndex = 10;
            btnSua.BackColor = Color.FromArgb(13, 110, 253);
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.ForeColor = Color.White;
            btnSua.Text = "✎ Sửa";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(21, 351);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(140, 45);
            btnThem.TabIndex = 9;
            btnThem.BackColor = Color.FromArgb(25, 135, 84);
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.ForeColor = Color.White;
            btnThem.Text = "+ Thêm";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // nudTongSoGV
            // 
            nudTongSoGV.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudTongSoGV.Location = new Point(21, 279);
            nudTongSoGV.Name = "nudTongSoGV";
            nudTongSoGV.Size = new Size(150, 27);
            nudTongSoGV.TabIndex = 8;
            // 
            // chkGVChuaRo
            // 
            chkGVChuaRo.AutoSize = true;
            chkGVChuaRo.Location = new Point(188, 282);
            chkGVChuaRo.Name = "chkGVChuaRo";
            chkGVChuaRo.Size = new Size(106, 24);
            chkGVChuaRo.TabIndex = 7;
            chkGVChuaRo.Text = "Chưa rõ";
            chkGVChuaRo.UseVisualStyleBackColor = true;
            // 
            // nudNamThanhLap
            // 
            nudNamThanhLap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudNamThanhLap.Location = new Point(21, 178);
            nudNamThanhLap.Name = "nudNamThanhLap";
            nudNamThanhLap.Size = new Size(150, 27);
            nudNamThanhLap.TabIndex = 6;
            // 
            // chkNamChuaRo
            // 
            chkNamChuaRo.AutoSize = true;
            chkNamChuaRo.Location = new Point(188, 181);
            chkNamChuaRo.Name = "chkNamChuaRo";
            chkNamChuaRo.Size = new Size(106, 24);
            chkNamChuaRo.TabIndex = 5;
            chkNamChuaRo.Text = "Chưa rõ";
            chkNamChuaRo.UseVisualStyleBackColor = true;
            // 
            // txtTenKhoa
            // 
            txtTenKhoa.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTenKhoa.Location = new Point(21, 84);
            txtTenKhoa.Name = "txtTenKhoa";
            txtTenKhoa.Size = new Size(323, 27);
            txtTenKhoa.TabIndex = 3;
            // 
            // lblNamThanhLap
            // 
            lblNamThanhLap.AutoSize = true;
            lblNamThanhLap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNamThanhLap.Location = new Point(21, 144);
            lblNamThanhLap.Name = "lblNamThanhLap";
            lblNamThanhLap.Size = new Size(51, 20);
            lblNamThanhLap.TabIndex = 2;
            lblNamThanhLap.Text = "Năm thành lập:";
            // 
            // lblTongSoGV
            // 
            lblTongSoGV.AutoSize = true;
            lblTongSoGV.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTongSoGV.Location = new Point(21, 244);
            lblTongSoGV.Name = "lblTongSoGV";
            lblTongSoGV.Size = new Size(51, 20);
            lblTongSoGV.TabIndex = 1;
            lblTongSoGV.Text = "Tổng số Giảng viên:";
            // 
            // lblTenKhoa
            // 
            lblTenKhoa.AutoSize = true;
            lblTenKhoa.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTenKhoa.Location = new Point(21, 47);
            lblTenKhoa.Name = "lblTenKhoa";
            lblTenKhoa.Size = new Size(75, 20);
            lblTenKhoa.TabIndex = 0;
            lblTenKhoa.Text = "Tên Khoa:";
            // 
            // dgvKhoa
            // 
            dgvKhoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhoa.Dock = DockStyle.Fill;
            dgvKhoa.Location = new Point(371, 99);
            dgvKhoa.Name = "dgvKhoa";
            dgvKhoa.RowHeadersWidth = 51;
            dgvKhoa.Size = new Size(756, 601);
            dgvKhoa.TabIndex = 2;
            // 
            // ssTrangThai
            // 
            ssTrangThai.ImageScalingSize = new Size(20, 20);
            ssTrangThai.Items.AddRange(new ToolStripItem[] { tslblTrangThai, tslblTongSo });
            ssTrangThai.Location = new Point(0, 700);
            ssTrangThai.Name = "ssTrangThai";
            ssTrangThai.Size = new Size(1127, 26);
            ssTrangThai.TabIndex = 3;
            ssTrangThai.Text = "ssTrangThai";
            // 
            // tslblTrangThai
            // 
            tslblTrangThai.Name = "tslblTrangThai";
            tslblTrangThai.Size = new Size(1015, 20);
            tslblTrangThai.Spring = true;
            tslblTrangThai.Text = "Sẵn sàng";
            tslblTrangThai.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tslblTongSo
            // 
            tslblTongSo.Name = "tslblTongSo";
            tslblTongSo.Size = new Size(97, 20);
            tslblTongSo.Text = "Tổng số Khoa: 0";
            // 
            // Quản_lý_khoa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1127, 726);
            Controls.Add(dgvKhoa);
            Controls.Add(gpbThongTinKhoa);
            Controls.Add(pnlTieuDe);
            Controls.Add(ssTrangThai);
            Name = "Quản_lý_khoa";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý Khoa";
            Load += Quản_lý_khoa_Load;
            pnlTieuDe.ResumeLayout(false);
            pnlTieuDe.PerformLayout();
            gpbThongTinKhoa.ResumeLayout(false);
            gpbThongTinKhoa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTongSoGV).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNamThanhLap).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).EndInit();
            ssTrangThai.ResumeLayout(false);
            ssTrangThai.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlTieuDe;
        private GroupBox gpbThongTinKhoa;
        private DataGridView dgvKhoa;
        private StatusStrip ssTrangThai;
        private ToolStripStatusLabel tslblTrangThai;
        private ToolStripStatusLabel tslblTongSo;
        private Label lblTieuDe;
        private Label lblNamThanhLap;
        private Label lblTongSoGV;
        private Label lblTenKhoa;
        private CheckBox chkNamChuaRo;
        private TextBox txtTenKhoa;
        private NumericUpDown nudTongSoGV;
        private CheckBox chkGVChuaRo;
        private NumericUpDown nudNamThanhLap;
        private Button btnLamMoi;
        private Button btnXoa;
        private Button btnSua;
        private Button btnThem;
    }
}