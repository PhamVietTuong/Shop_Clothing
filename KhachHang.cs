using System;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;
namespace _22_27_ShopQuanAo
{
    public partial class KhachHang : Form
    {
        //Giữ lại dữ li
        public string TenKH
        {
            get
            {
                return txtTenKH.Text;
            }
        }
        public string MaKH
        {
            get
            {
                return txtMaKH.Text;
            }
        }
        public string Phone
        {
            get
            {
                return txtPhone.Text;
            }
        }
        int viTri = 0;
        DataSet ds = new DataSet();
        public KhachHang()
        {
            InitializeComponent();
        }
        clsShopQuanAo c = new clsShopQuanAo();
        //đẩy dữ liệu vào danhSach_datagridview
        bool dinhDangSDT(string sdt)
        {
            if (!Regex.Match(sdt, @"^(\+[0-9]{1,3})?\d{9,10}$").Success || sdt.Length != 10)
            {

                return false;
            }
            return true;
        }
        bool dinhDangEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        void danhSach_datagridview(DataGridView d, string sql)
        {
            ds = c.TakeData(sql);
            d.DataSource = ds.Tables[0];
        }
        void XuLyChucNang(Boolean t)
        {
            btnAdd.Enabled = t;
            btnEdit.Enabled = t;
            btnRemove.Enabled = t;
            btnSave.Enabled = !t;
            button4.Enabled = !t;
        }
        void XuLyTextBox(bool t)
        {
            txtTenKH.ReadOnly = t;
            txtDiaChi.ReadOnly = t;
            txtEmail.ReadOnly = t;
            txtPhone.ReadOnly = t;
        }
        //txtDiaChi.ReadOnly = true;
        //txtEmail.ReadOnly = true;
        //txtPhone.ReadOnly = true;
        //txtTenKH.ReadOnly = true;
        //rdNam.Enabled = false;
        //rdNu.Enabled = false;
        void XuLyTxt(bool t)
        {
            txtDiaChi.ReadOnly = t;
            txtEmail.ReadOnly = t;
            txtPhone.ReadOnly = t;
            txtTenKH.ReadOnly = t;
            rdNam.Enabled = !t;
            rdNu.Enabled = !t;
        }
        void XuLyTrangThai(bool t)
        {
            cmbTinhTrang.Visible = t;
            label4.Visible = t;
        }
        int flag = 0;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            label1.Text = "THÊM KHÁCH HÀNG";
            flag = 1;
            XuLyChucNang(false);
            XuLyTextBox(false);
            XuLyTrangThai(false);
            txtMaKH.Text = phatSinhMa();
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtTenKH.Focus();
            rdNam.Enabled = !false;
            rdNu.Enabled = !false;
            dgv.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            label1.Text = "SỬA KHÁCH HÀNG";
            flag = 2;
            XuLyTextBox(false);
            XuLyChucNang(false);
            XuLyTrangThai(true);
            cmbTinhTrang.SelectedIndex = 0;
            this.cmbTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            rdNam.Enabled = !false;
            rdNu.Enabled = !false;
            dgv.Enabled = !false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            label1.Text = "XÓA KHÁCH HÀNG";
            flag = 3;
            XuLyChucNang(false);
            XuLyTrangThai(false);
            rdNam.Enabled = false;
            rdNu.Enabled = false;
            dgv.Enabled = !false;
        }
        Boolean f = false;
        private void KhachHang_Load(object sender, EventArgs e)
        {
            XuLyTrangThai(false);
            XuLyChucNang(true);
            danhSach_datagridview(dgv, "select * from khachhang where trangthai = 0");
            viTri = ds.Tables[0].Rows.Count - 1;
            hienThiLenTextBox(ds, viTri);
            rdNam.Checked = true;
            rdNam.Enabled = false;
            rdNu.Enabled = false;
            f = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void hienThiLenTextBox(DataSet ds, int vt)
        {
            if (ds.Tables[0].Rows.Count > vt && vt > -1)
            {
                txtMaKH.Text = ds.Tables[0].Rows[vt]["makh"].ToString();
                txtTenKH.Text = ds.Tables[0].Rows[vt]["tenkh"].ToString();
                string phai = ds.Tables[0].Rows[vt]["phai"].ToString();
                txtDiaChi.Text = ds.Tables[0].Rows[vt]["diachi"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[vt]["email"].ToString();
                txtPhone.Text = ds.Tables[0].Rows[vt]["phone"].ToString();
                if (phai == "Nam")
                {
                    rdNam.Checked = true;
                }
                else if (phai == "Nữ")
                {
                    rdNu.Checked = true;
                }
            }
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            viTri = e.RowIndex;
            hienThiLenTextBox(ds, viTri);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            XuLyTextBox(true);
            XuLyChucNang(true);

            label1.Text = "DANH SÁCH KHÁCH HÀNG";
            string sql = "";
            string gender = "";
            if (rdNam.Checked)
            {
                gender = "Nam";
            }
            else if (rdNu.Checked)
            {
                gender = "Nữ";
            }

            if (flag == 1)
            {
                if (string.IsNullOrEmpty(txtTenKH.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPhone.Text))
                {
                    MessageBox.Show("Bạn cần điền đầy đủ thông tin !!!");
                    XuLyChucNang(false);
                    XuLyTxt(false);
                    return;
                }
                else if (!dinhDangEmail(txtEmail.Text))
                {
                    MessageBox.Show("Định dạng email sai, vui lòng nhập lại");
                    txtEmail.Focus();
                    XuLyTxt(false);
                    XuLyChucNang(false);
                    return;
                }
                else if (!dinhDangSDT(txtPhone.Text))
                {
                    MessageBox.Show("Định dạng số điện thoại sai, vui lòng nhập lại");
                    txtPhone.Focus();
                    XuLyTxt(false);
                    XuLyChucNang(false);
                    return;
                }
                else
                {
                    sql = "INSERT INTO khachhang values('" + txtMaKH.Text + "', N'" + txtTenKH.Text + "', N'" + gender + "', N'" + txtDiaChi.Text + "', '" + txtEmail.Text + "', '" + txtPhone.Text + "',0)";
                }
            }
            if (flag == 2)
            {
                sql = "update khachhang set tenkh = N'" + txtTenKH.Text + "', phai = N'" + gender + "', diachi = N'" + txtDiaChi.Text + "',  email = '" + txtEmail.Text + "',  phone = '" + txtPhone.Text + "',  trangthai = " + cmbTinhTrang.SelectedIndex + "where makh = '" + txtMaKH.Text + "'";

            }
            if (flag == 3)
            {
                sql = "update khachhang set trangthai = 1 where makh = '" + txtMaKH.Text + "'";

            }
            if (c.UpdateData(sql) > 0)
            {
                MessageBox.Show("Cập nhật thành công");
                //lưu xong cập nhật dữ liệu hiện thị lên datagridview
                KhachHang_Load(sender, e);
            }
            flag = 0;
            XuLyTextBox(true);
            //txtDiaChi.ReadOnly = true;
            //txtEmail.ReadOnly = true;
            //txtPhone.ReadOnly = true;
            //txtTenKH.ReadOnly = true;
            //rdNam.Enabled = false;
            //rdNu.Enabled = false;
            dgv.Enabled = !false;
            XuLyChucNang(true);
            XuLyTrangThai(false);
        }
        string phatSinhMa()
        {
            DataSet dsma = c.TakeData("select * from khachhang");
            return "KH0" + (dsma.Tables[0].Rows.Count + 1).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "DANH SÁCH KHÁCH HÀNG";
            XuLyChucNang(true);
            txtDiaChi.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtPhone.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            rdNam.Enabled = false;
            rdNu.Enabled = false;
            XuLyTrangThai(false);
            dgv.Enabled = !false;
            KhachHang_Load(sender, e);
        }

        private void KhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = new DialogResult();
            kq = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
            {
                e.Handled = true;
            }
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (f)
            {
                if (e.ColumnIndex >= 1)
                {
                    int vt = dgv.CurrentRow.Index;
                    string makh = dgv.CurrentRow.Cells[0].Value.ToString();
                    string tenkh = dgv.CurrentRow.Cells[1].Value.ToString();
                    //string phai = dgvDanhSach.CurrentRow.Cells[2].Value.ToString();
                    string diachi = dgv.CurrentRow.Cells[3].Value.ToString();
                    string email = dgv.CurrentRow.Cells[4].Value.ToString();
                    string phone = dgv.CurrentRow.Cells[5].Value.ToString();
                    //string trangthai = dgvDanhSach.CurrentRow.Cells[6].Value.ToString();
                    string sql = "update khachhang set  tenkh= N'" + tenkh + "', diachi=N'" + diachi + "', email='" + email + "', phone='" + phone + "' where makh = '" + makh + "'";
                    if (c.UpdateData(sql) > 0)
                    {
                        MessageBox.Show("Cập nhật thành công");
                        //vitri = 0;
                        KhachHang_Load(sender, e);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.rdNu = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdNam = new System.Windows.Forms.RadioButton();
            this.cmbTinhTrang = new System.Windows.Forms.ComboBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.btnRemove);
            this.groupBox3.Controls.Add(this.btnEdit);
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(537, 63);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(245, 174);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chức năng";
            this.groupBox3.UseWaitCursor = true;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnClose.Location = new System.Drawing.Point(125, 117);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 40);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Thoát";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.UseWaitCursor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.button4.Location = new System.Drawing.Point(19, 117);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 40);
            this.button4.TabIndex = 4;
            this.button4.Text = "Hủy";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.UseWaitCursor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnSave.Location = new System.Drawing.Point(125, 71);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 40);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.UseWaitCursor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnRemove.Location = new System.Drawing.Point(19, 71);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(100, 40);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Xóa";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.UseWaitCursor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnEdit.Location = new System.Drawing.Point(125, 25);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Sữa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.UseWaitCursor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnAdd.Location = new System.Drawing.Point(19, 25);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.UseWaitCursor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // rdNu
            // 
            this.rdNu.AutoSize = true;
            this.rdNu.Location = new System.Drawing.Point(257, 98);
            this.rdNu.Name = "rdNu";
            this.rdNu.Size = new System.Drawing.Size(49, 21);
            this.rdNu.TabIndex = 3;
            this.rdNu.TabStop = true;
            this.rdNu.Text = "Nữ";
            this.rdNu.UseVisualStyleBackColor = true;
            this.rdNu.UseWaitCursor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdNu);
            this.groupBox1.Controls.Add(this.rdNam);
            this.groupBox1.Controls.Add(this.cmbTinhTrang);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.txtDiaChi);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtTenKH);
            this.groupBox1.Controls.Add(this.txtMaKH);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(35, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 262);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nhân viên";
            this.groupBox1.UseWaitCursor = true;
            // 
            // rdNam
            // 
            this.rdNam.AutoSize = true;
            this.rdNam.Location = new System.Drawing.Point(165, 98);
            this.rdNam.Name = "rdNam";
            this.rdNam.Size = new System.Drawing.Size(61, 21);
            this.rdNam.TabIndex = 2;
            this.rdNam.TabStop = true;
            this.rdNam.Text = "Nam";
            this.rdNam.UseVisualStyleBackColor = true;
            this.rdNam.UseWaitCursor = true;
            // 
            // cmbTinhTrang
            // 
            this.cmbTinhTrang.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.cmbTinhTrang.FormattingEnabled = true;
            this.cmbTinhTrang.Items.AddRange(new object[] {
            "Hoạt động",
            "Ngưng hoạt động"});
            this.cmbTinhTrang.Location = new System.Drawing.Point(165, 225);
            this.cmbTinhTrang.Name = "cmbTinhTrang";
            this.cmbTinhTrang.Size = new System.Drawing.Size(174, 24);
            this.cmbTinhTrang.TabIndex = 7;
            this.cmbTinhTrang.UseWaitCursor = true;
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.txtPhone.Location = new System.Drawing.Point(165, 193);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.ReadOnly = true;
            this.txtPhone.Size = new System.Drawing.Size(174, 22);
            this.txtPhone.TabIndex = 6;
            this.txtPhone.UseWaitCursor = true;
            this.txtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhone_KeyPress);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.txtEmail.Location = new System.Drawing.Point(165, 161);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(174, 22);
            this.txtEmail.TabIndex = 5;
            this.txtEmail.UseWaitCursor = true;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.txtDiaChi.Location = new System.Drawing.Point(165, 129);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.ReadOnly = true;
            this.txtDiaChi.Size = new System.Drawing.Size(174, 22);
            this.txtDiaChi.TabIndex = 4;
            this.txtDiaChi.UseWaitCursor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label9.Location = new System.Drawing.Point(58, 198);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Phone: ";
            this.label9.UseWaitCursor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label8.Location = new System.Drawing.Point(65, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Email: ";
            this.label8.UseWaitCursor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label7.Location = new System.Drawing.Point(56, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Địa chỉ: ";
            this.label7.UseWaitCursor = true;
            // 
            // txtTenKH
            // 
            this.txtTenKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.txtTenKH.Location = new System.Drawing.Point(165, 66);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.ReadOnly = true;
            this.txtTenKH.Size = new System.Drawing.Size(174, 22);
            this.txtTenKH.TabIndex = 1;
            this.txtTenKH.UseWaitCursor = true;
            // 
            // txtMaKH
            // 
            this.txtMaKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.txtMaKH.Location = new System.Drawing.Point(165, 34);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.ReadOnly = true;
            this.txtMaKH.Size = new System.Drawing.Size(174, 22);
            this.txtMaKH.TabIndex = 0;
            this.txtMaKH.UseWaitCursor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label5.Location = new System.Drawing.Point(47, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Giới tính: ";
            this.label5.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label4.Location = new System.Drawing.Point(34, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tình trạng: ";
            this.label4.UseWaitCursor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label3.Location = new System.Drawing.Point(51, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tên KH: ";
            this.label3.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label2.Location = new System.Drawing.Point(57, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã KH: ";
            this.label2.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(829, 60);
            this.label1.TabIndex = 19;
            this.label1.Text = "DANH SÁCH KHÁCH HÀNG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseWaitCursor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(29, 354);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(759, 215);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách khách hàng";
            this.groupBox2.UseWaitCursor = true;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(6, 19);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(747, 190);
            this.dgv.TabIndex = 0;
            this.dgv.UseWaitCursor = true;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // KhachHang
            // 
            this.ClientSize = new System.Drawing.Size(829, 594);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Name = "KhachHang";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KhachHang_FormClosing);
            this.Load += new System.EventHandler(this.KhachHang_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
