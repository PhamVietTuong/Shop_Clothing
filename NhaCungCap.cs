using System;
using System.Data;
using System.Windows.Forms;

namespace _22_27_ShopQuanAo
{
    public partial class NhaCungCap : Form
    {
        int viTri = 0;
        DataSet dsNCC = new DataSet();
        DataSet dsLoaiSp = new DataSet();
        DataView dvLoaiSP = new DataView();
        int flag = 0; // thêm sửa xóa
        Boolean f = false; // xử lý việc load dữ liệu
        clsShopQuanAo c = new clsShopQuanAo();

        public NhaCungCap()
        {
            InitializeComponent();
        }
        //đẩy dữ liệu vào danhSach_datagridview
        void danhSach_datagridview(DataGridView d, string sql)
        {
            dsNCC = c.TakeData(sql);
            d.DataSource = dsNCC.Tables[0];
        }
        void XuLyChucNang(Boolean t)
        {
            btnAdd.Enabled = t;
            btnEdit.Enabled = t;
            btnRemove.Enabled = t;
            btnSave.Enabled = !t;
        }
        void XuLyTextBox(bool t)
        {
            txtTenNCC.ReadOnly = !t;
            txtMaNCC.ReadOnly = t;
            txtTenNHang.ReadOnly = !t;
            txtPhone.ReadOnly = !t;
        }
        void XuLyTrangThai(bool t)
        {
            cmbTinhTrang.Visible = t;
            label8.Visible = t;
        }
        void clear()
        {
            txtTenNCC.Clear();
            txtTenNHang.Clear();
            txtPhone.Clear();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            label1.Text = "THÊM NHÀ CUNG CẤP";
            flag = 1;

            dsLoaiSp = c.TakeData("select * from loaisanpham where trangthai = 0");
            hienThiCombobox(cmbLoaiSP, dsLoaiSp, "tenloai", "maloai");

            XuLyChucNang(false);
            XuLyTextBox(true);
            XuLyTrangThai(false);
            txtMaNCC.Text = phatSinhMa();
            clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            label1.Text = "SỬA NHÀ CUNG CẤP";
            flag = 2;
            XuLyTextBox(false);
            XuLyChucNang(false);
            XuLyTrangThai(true);
            cmbTinhTrang.SelectedIndex = 0;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            label1.Text = "XÓA NHÀ CUNG CẤP";
            flag = 3;
            XuLyChucNang(false);
            XuLyTrangThai(false);
        }
        private void NhaCungCap_Load(object sender, EventArgs e)
        {
            XuLyChucNang(true);
            XuLyTrangThai(false);

            danhSach_datagridview(dgv, "select * from nhacungcap where trangthai = 0");

            dsLoaiSp = c.TakeData("select * from loaisanpham where trangthai = 0");
            hienThiCombobox(cmbLoaiSP, dsLoaiSp, "tenloai", "maloai");

            //loadDuLieu_datagrid(,dsNCC)
            //cmbLoaiCH.DataSource = dsLoaiCH.Tables[0]; //load dữ liệu lên cmb
            //cmbLoaiCH.DisplayMember = "tench"; // hiển thị lên cmb cho người dùng xem
            //cmbLoaiCH.ValueMember = "mach";// lưu giá trị người dùng
            //cmbLoaiCH.SelectedIndex = -1; // cho cmb về giá trị chưa chọn

            hienThiLenTextBox(dsNCC, viTri);

            f = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void locDuLieuCombobox(DataSet ds, ComboBox cmb, string ten, string ma, string giatrima)
        {
            //dv.Table = dsLoaiSp.Tables[0];
            //dv.RowFilter = "maloai = '" + ma + "'";
            //cmb.DataSource = dv;
            //cmb.DisplayMember = "tenloai";
            //cmb.ValueMember = "maloai";
            DataView dv = new DataView();
            dv.Table = ds.Tables[0];
            dv.RowFilter = ma + " = '" + giatrima + "'";
            cmb.DataSource = dv;
            cmb.DisplayMember = ten;
            cmb.ValueMember = ma;

            //rowfilter dùng lọc theo đk
            //dataview không cần .table0 nó chỉ lưu 1 bản còn dataset phải .table0 vì nó lưu n bản
        }
        void loadDuLieu_datagrid(DataGridView d, DataSet ds)
        {
            d.DataSource = ds.Tables[0];
        }
        void hienThiLenTextBox(DataSet ds, int vt)
        {
            if (ds.Tables[0].Rows.Count > vt && vt > -1)
            {
                txtMaNCC.Text = ds.Tables[0].Rows[vt]["mancc"].ToString();
                txtTenNCC.Text = ds.Tables[0].Rows[vt]["tenncc"].ToString();
                txtTenNHang.Text = ds.Tables[0].Rows[vt]["tennganhang"].ToString();
                txtPhone.Text = ds.Tables[0].Rows[vt]["phone"].ToString();

                string maloai = ds.Tables[0].Rows[vt]["maloai"].ToString();

                locDuLieuCombobox(dsLoaiSp, cmbLoaiSP, "tenloai", "maloai", maloai);
            }
        }
        void hienThiCombobox(ComboBox cmb, DataSet ds, string ten, string ma)
        {
            cmb.DataSource = ds.Tables[0];
            cmb.DisplayMember = ten;
            cmb.ValueMember = ma;
            cmb.SelectedIndex = -1;
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            viTri = e.RowIndex;
            hienThiLenTextBox(dsNCC, viTri);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            label1.Text = "DANH SÁCH NHÀ CUNG CẤP";
            XuLyTextBox(true);
            XuLyChucNang(true);
            if (string.IsNullOrEmpty(txtTenNCC.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhà cung cấp !!!");
                btnAdd_Click(sender, e);
            }
            else
            {
                string sql = "";
                if (flag == 1)
                {
                    sql = "INSERT INTO nhacungcap values('" + txtMaNCC.Text + "', N'" + txtTenNCC.Text + "', N'" + txtTenNHang.Text + "', '" + txtPhone.Text + "', '" + cmbLoaiSP.SelectedValue.ToString() + "', 0 )";
                }
                if (flag == 2)
                {
                    sql = "update nhacungcap set tenncc = N'" + txtTenNCC.Text + "',  tennganhang = " + txtTenNCC.Text + "',  phone  = " + txtPhone.Text + "',  maloai  = " + cmbLoaiSP.SelectedValue.ToString() + "',  trangthai = " + cmbTinhTrang.SelectedIndex + " where mancc = '" + txtMaNCC.Text + "'";
                }
                if (flag == 3)
                {
                    sql = "update nhacungcap set trangthai = 1 where mancc = '" + txtMaNCC.Text + "'";
                }
                //Kiểm tra không bao giờ sai thì vô nàyx
                if (c.UpdateData(sql) > 0)
                {
                    MessageBox.Show("Cập nhật thành công");
                    viTri = 0;
                    //lưu xong cập nhật dữ liệu hiện thị lên datagridview
                    NhaCungCap_Load(sender, e);
                }
            }
            XuLyTrangThai(false);
        }
        string phatSinhMa()
        {
            DataSet dsma = c.TakeData("select * from nhanvien");
            return "NCC0" + (dsma.Tables[0].Rows.Count + 1).ToString();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            label1.Text = "DANH SÁCH NHÀ CUNG CẤP";
            XuLyChucNang(true);
            XuLyTrangThai(false);
            NhaCungCap_Load(sender, e);
            txtTenNCC.ReadOnly = true;
            txtTenNHang.ReadOnly = true;
            txtPhone.ReadOnly = true;
        }
        //Sửa trực tiếp trên dgb
        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ////Khi form load xong mới chạy cái này
            if (f)
            {
                //từ cột 1 mới cho người ta sửa
                //nếu == 1 thì chỉ sửa được cột tensize
                if (e.ColumnIndex >= 1)
                {
                    int vt = dgv.CurrentRow.Index;
                    string mancc = dgv.CurrentRow.Cells[0].Value.ToString();
                    string tenncc = dgv.CurrentRow.Cells[1].Value.ToString();
                    string tennganhang = dgv.CurrentRow.Cells[2].Value.ToString();
                    string phone = dgv.CurrentRow.Cells[3].Value.ToString();
                    string maloai = dgv.CurrentRow.Cells[4].Value.ToString(); // chưa xử lý
                    string sql = "update nhacungcap set tenncc= N'" + tenncc + "', tennganhang=N'" + tennganhang + "', phone='" + phone + "' where manv = '" + mancc + "'";
                    if (c.UpdateData(sql) > 0)
                    {
                        MessageBox.Show("Cập nhật thành công");
                        //vitri = 0;
                        NhaCungCap_Load(sender, e);
                    }
                }
            }
        }

        private void NhaCungCap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = new DialogResult();
            kq = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        //private void cmbLoaiCH_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (f)
        //    {
        //        if (cmbLoaiCH.SelectedIndex!=-1)
        //        {
        //            string mach = cmbLoaiCH.SelectedValue.ToString();
        //            dsLoaiSp = c.TakeData("select * from loaisanpham where mach= '" + mach + "'");
        //        }
        //    }
        //}

    }
}
