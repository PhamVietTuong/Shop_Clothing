using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _22_27_ShopQuanAo
{
    public partial class SanPham : Form
    {
        clsShopQuanAo c = new clsShopQuanAo();
        int viTri = 0;
        DataSet dsSanPham = new DataSet();
        DataSet dsLoaiSP = new DataSet();
        DataSet dsNCC = new DataSet();
        DataSet dsMau = new DataSet();
        DataSet dsSize = new DataSet();
        //DataSet dsLoaiCH = new DataSet();
        Boolean f = false; // xử lý việc load dữ liệu
        public SanPham()
        {
            InitializeComponent();
        }
        //đẩy dữ liệu vào danhSach_datagridview
        void danhSach_datagridview(DataGridView d, string sql)
        {
            dsSanPham = c.TakeData(sql);
            d.DataSource = dsSanPham.Tables[0];
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
            txtMaSP.ReadOnly = !t;
            txtTenSP.ReadOnly = t;
            txtSoLuong.ReadOnly = t;
            txtGiaBan.ReadOnly = !t;
            txtGiaNhap.ReadOnly = t;
            txtMoTa.ReadOnly = t;
        }
        void XuLyTrangThai(bool t)
        {
            cmbTinhTrang.Visible = t;
            label4.Visible = t;
        }
        int flag = 0;
        void clear()
        {
            txtTenSP.Clear();
            txtMaSP.Clear();
            //txtMau.Clear();
            //txtSize.Clear();
            txtSoLuong.Clear();
            txtGiaBan.Clear();
            txtGiaNhap.Clear();
            txtMoTa.Clear();
            txtTenSP.Focus();
        }
        void CreateColumnSanPham()
        {
            dgv.Columns.Clear();
            dgv.Columns.Add("maloai", "Mã loại");
            dgv.Columns.Add("mancc", "Mã nhà cung cấp");
            dgv.Columns.Add("masp", "Mã sản phẩm");
            dgv.Columns.Add("tensp", "Tên sản phẩm");
            dgv.Columns.Add("mamau", "Màu");
            dgv.Columns.Add("masize", "Size");
            dgv.Columns.Add("hinh", "Hình");
            dgv.Columns.Add("soluong", "Số lượng");
            dgv.Columns.Add("gianhap", "Giá nhập");
            dgv.Columns.Add("giaban", "Giá bán");
            dgv.Columns.Add("mota", "Mô tả");
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "THÊM SẢN PHẨM";
            flag = 1;
            XuLyChucNang(false);
            XuLyTextBox(false);
            XuLyTrangThai(false);
            cmbNCC.Text = "";
            this.cmbNCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoaiSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMau.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            btnSave.Enabled = false;
            btnThem.Visible = true;
            btnLoadImg.Visible = true;
            LoadImg.Image = null;
            dsLoaiSP = c.TakeData("select * from loaisanpham where trangthai = 0");
            dsMau = c.TakeData("select * from mau where trangthai = 0");
            dsSize = c.TakeData("select * from size where trangthai = 0");

            hienThiCombobox(cmbLoaiSP, dsLoaiSP, "tenloai", "maloai");
            hienThiCombobox(cmbMau, dsMau, "tenmau", "mamau");
            hienThiCombobox(cmbSize, dsSize, "tensize", "masize");

            clear();
            dgv.DataSource = null;
            CreateColumnSanPham();
            flag = 1;
        }
        string phatSinhMa()
        {
            return "MaHDBan" + (dsSanPham.Tables[0].Rows.Count + 1).ToString();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "SỬA SẢN PHẨM";
            flag = 2;
            XuLyTextBox(false);
            XuLyChucNang(false);
            XuLyTrangThai(true);
            cmbTinhTrang.SelectedIndex = 0;
            btnLoadImg.Visible = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "XÓA SẢN PHẨM";
            flag = 3;
            XuLyChucNang(false);
            XuLyTrangThai(false);
        }

        private void SanPham_Load(object sender, EventArgs e)
        {
            XuLyTrangThai(false);
            XuLyChucNang(true);
            btnLoadImg.Visible = false;
            btnThem.Visible = false;
            dgv.ReadOnly = true;
            danhSach_datagridview(dgv, "select * from sanpham where trangthai = 0");


            dsLoaiSP = c.TakeData("select * from loaisanpham");
            dsMau = c.TakeData("select * from mau");
            dsSize = c.TakeData("select * from size");
            dsNCC = c.TakeData("select * from nhacungcap");

            hienThiCombobox(cmbLoaiSP, dsLoaiSP, "tenloai", "maloai");
            hienThiCombobox(cmbMau, dsMau, "tenmau", "mamau");
            hienThiCombobox(cmbSize, dsSize, "tensize", "masize");
            hienThiCombobox(cmbNCC, dsNCC, "tenncc", "mancc");

            hienThiLenTextBox(dsSanPham, viTri);

            f = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void locDuLieuCombobox(DataSet ds, ComboBox cmb, string ten, string ma, string giatrima)
        {
            DataView dv = new DataView();
            dv.Table = ds.Tables[0];
            dv.RowFilter = ma + "='" + giatrima + "'";
            cmb.DataSource = dv;
            cmb.DisplayMember = ten;
            cmb.ValueMember = ma;
        }
        void hienThiLenTextBox(DataSet ds, int vt)
        {
            if (ds.Tables[0].Rows.Count > vt && vt > -1)
            {
                txtMaSP.Text = ds.Tables[0].Rows[vt]["masp"].ToString();
                txtTenSP.Text = ds.Tables[0].Rows[vt]["tensp"].ToString();
                txtSoLuong.Text = ds.Tables[0].Rows[vt]["soluong"].ToString();
                txtGiaNhap.Text = ds.Tables[0].Rows[vt]["gianhap"].ToString();
                txtGiaBan.Text = ds.Tables[0].Rows[vt]["giaban"].ToString();
                txtMoTa.Text = ds.Tables[0].Rows[vt]["mota"].ToString();

                string maloai = ds.Tables[0].Rows[vt]["maloai"].ToString();
                string mancc = ds.Tables[0].Rows[vt]["mancc"].ToString();
                string mau = ds.Tables[0].Rows[vt]["mamau"].ToString();
                string size = ds.Tables[0].Rows[vt]["masize"].ToString();

                string tenhinh = ds.Tables[0].Rows[vt]["hinh"].ToString();
                string tenfile = Path.GetFullPath("img") + @"\" + tenhinh;
                taoanh_tufileanh(LoadImg, tenfile);

                locDuLieuCombobox(dsLoaiSP, cmbLoaiSP, "tenloai", "maloai", maloai);
                locDuLieuCombobox(dsNCC, cmbNCC, "tenncc", "mancc", mancc);
                locDuLieuCombobox(dsMau, cmbMau, "tenmau", "mamau", mau);
                locDuLieuCombobox(dsSize, cmbSize, "tensize", "masize", size);

                if (cmbNCC.SelectedIndex != -1)
                {
                    txtMaSP.Text = ds.Tables[0].Rows[vt]["masp"].ToString();
                }
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
            if (f)
            {
                //if (flag != -1)
                //{
                    viTri = e.RowIndex;
                    hienThiLenTextBox(dsSanPham, viTri);
                //}
            }
            
        }
        void taoanh_tufileanh(PictureBox p, string filename)
        {
            Bitmap a = new Bitmap(filename);
            p.Image = a;
            p.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.InitialDirectory = Path.GetFullPath("img") + @"\";
            if (o.ShowDialog() == DialogResult.OK)
            {
                string tenFile = o.FileName;
                string[] tenhinh = tenFile.Split('\\'); //Cắt đường dẫn ảnh
                lblTenHinh.Text = tenhinh[9] + "\\" + tenhinh[10]; // Chọn đường dẫn ảnh cần lưu
                taoanh_tufileanh(LoadImg, tenFile);
            }
            else
            {
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "DANH SÁCH SẢN PHẨM";
            //if (string.IsNullOrEmpty(txtTenSP.Text))
            //{
            //    MessageBox.Show("Bạn chưa nhập tên sản phẩm !!!");
            //    btnAdd_Click(sender, e);
            //}
            //else
            //{
            string sql = "";
            if (flag == 1)
            {
                int dem = 0;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    string maloai = dgv.Rows[i].Cells[0].Value.ToString();
                    string mancc = dgv.Rows[i].Cells[1].Value.ToString();
                    string masp = dgv.Rows[i].Cells[2].Value.ToString();
                    string tensp = dgv.Rows[i].Cells[3].Value.ToString();
                    string mamau = dgv.Rows[i].Cells[4].Value.ToString();
                    string masize = dgv.Rows[i].Cells[5].Value.ToString();
                    string hinh = dgv.Rows[i].Cells[6].Value.ToString();
                    string soluong = dgv.Rows[i].Cells[7].Value.ToString();
                    string gianhap = dgv.Rows[i].Cells[8].Value.ToString();
                    string giaban = dgv.Rows[i].Cells[9].Value.ToString();
                    string mota = dgv.Rows[i].Cells[10].Value.ToString();
                    string trangthai = "0";

                    sql = "insert into sanpham values('" + masp + "',N'" + tensp + "','" + mancc + "','" + maloai + "',N'" + mamau + "',N'" + masize + "',N'" + mota + "','" + hinh + "'," + soluong + "," + gianhap + "," + giaban + ", " + trangthai + ")";

                    if (c.UpdateData(sql) > 0)
                    {
                        dem++;
                    }
                }
                if (dem != 0)
                {
                    MessageBox.Show("Cập nhật thành công");
                    dgv.Columns.Clear();
                }
                dgv.Columns.Clear();
                SanPham_Load(sender, e);
            }
            else if (flag == 2)
            {

                sql = "update sanpham set tensp= N'" + txtTenSP.Text + "', mota=N'" + txtMoTa.Text + "', hinh='" + lblTenHinh.Text +"', soluong=" + txtSoLuong.Text + ", gianhap=" + txtGiaNhap.Text + ", giaban=" + txtGiaBan.Text + ", trangthai=" + cmbTinhTrang.SelectedIndex + "  where masp = '" + txtMaSP.Text + "'";

                if (c.UpdateData(sql) > 0)
                {
                    MessageBox.Show("Cập nhật thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    viTri = 0;
                    SanPham_Load(sender, e);
                }
            }

            else if(flag == 3)
            {
                sql = "update sanpham set trangthai = 1 where masp='" + txtMaSP.Text + "'";
                    if (c.UpdateData(sql) > 0)
                    {

                        MessageBox.Show("Cập nhật thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        viTri = 0;
                        SanPham_Load(sender, e);
                    }
            }
            SanPham_Load(sender, e);
            txtTenSP.ReadOnly = true;
            txtSoLuong.ReadOnly = true;
            txtGiaNhap.ReadOnly = true;
            txtMoTa.ReadOnly = true;
        }


        private void cmbNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (f)
            {
                string masp = "";

                if (cmbNCC.SelectedIndex != -1)
                {
                    string mancc = cmbNCC.SelectedValue.ToString();
                    DataSet ds = c.TakeData("select masp from sanpham where mancc ='" + mancc + "'");
                    if (ds.Tables[0].Rows.Count < 9)
                    {
                        masp = mancc + "0" + (ds.Tables[0].Rows.Count + 1).ToString();
                    }
                    else
                    {
                        masp = mancc + (ds.Tables[0].Rows.Count + 1).ToString();
                    }
                }
                txtMaSP.Text = masp;
            }
        }
        //Dùng để chọn một cmb nào đó sẽ hiển thị thông tin theo cmb đó

        private void cmbLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (f)
            {
                if (cmbLoaiSP.SelectedIndex != -1)
                {
                    string maloai = cmbLoaiSP.SelectedValue.ToString();
                    dsNCC = c.TakeData("select * from nhacungcap where maloai ='" + maloai + "'");
                    hienThiCombobox(cmbNCC, dsNCC, "tenncc", "mancc");
                }
            }
        }
        private void txtGiaNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtGiaNhap.Text!="")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    float giaNhap = int.Parse(txtGiaNhap.Text);
                    txtGiaBan.Text = (giaNhap * 2).ToString();
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "DANH SÁCH SẢN PHẨM";
            XuLyChucNang(true);
            XuLyTrangThai(false);
            dgv.Columns.Clear();
            SanPham_Load(sender, e);
            txtTenSP.ReadOnly = true;
            txtSoLuong.ReadOnly = true;
            txtGiaNhap.ReadOnly = true;
            txtMoTa.ReadOnly = true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            //dgv.Columns.Add("maloai", "Mã loại");
            //dgv.Columns.Add("mancc", "Mã nhà cung cấp");
            //dgv.Columns.Add("masp", "Mã sản phẩm");
            //dgv.Columns.Add("tensp", "Tên sản phẩm");
            //dgv.Columns.Add("mau", "Màu");
            //dgv.Columns.Add("size", "Size");
            //dgv.Columns.Add("hinh", "Hình");
            //dgv.Columns.Add("soluong", "Số lượng");
            //dgv.Columns.Add("gianhap", "Giá nhập");
            //dgv.Columns.Add("giaban", "Giá bán");
            //dgv.Columns.Add("mota", "Mô tả");
            object[] d = new object[11];
            if (string.IsNullOrEmpty(txtTenSP.Text) || string.IsNullOrEmpty(txtSoLuong.Text) || string.IsNullOrEmpty(txtGiaNhap.Text) || string.IsNullOrEmpty(txtGiaBan.Text) || string.IsNullOrEmpty(cmbLoaiSP.Text) || string.IsNullOrEmpty(cmbNCC.Text) || string.IsNullOrEmpty(cmbMau.Text) || string.IsNullOrEmpty(cmbSize.Text))
            {
                MessageBox.Show("Bạn cần điền đầy đủ thông tin !!!");
                XuLyChucNang(false);
                btnSave.Enabled = false;
                return;
            }
            else
            {
                d[0] = cmbLoaiSP.SelectedValue.ToString();
                d[1] = cmbNCC.SelectedValue.ToString();
                d[2] = txtMaSP.Text + "." + cmbMau.SelectedValue.ToString() + "." + cmbSize.SelectedValue.ToString();
                d[3] = txtTenSP.Text;
                d[4] = cmbMau.SelectedValue.ToString();
                d[5] = cmbSize.SelectedValue.ToString();
                d[6] = lblTenHinh.Text;
                d[7] = txtSoLuong.Text;
                d[8] = txtGiaNhap.Text;
                d[9] = txtGiaBan.Text;
                d[10] = txtMoTa.Text;
                dgv.Rows.Add(d);
                btnSave.Enabled = true;
            }
            //d[0] = txtMaHDBan.Text + "." + txtMaSP.Text+"."+cmbTenNV.SelectedValue.ToString();//mã cthdb
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (f)
            {
                if (flag == 1)//Thêm
                {
                    if (e.ColumnIndex == 7)
                    {
                        int TongSoLuong = 0;
                        for (int i = 0; i < dgv.Rows.Count - 1; i++)
                        {
                            TongSoLuong += int.Parse(dgv.Rows[i].Cells[4].Value.ToString());
                        }
                        txtSoLuong.Text = TongSoLuong.ToString();

                        //int vt = dgv.CurrentRow.Index;
                        //string masp = dgv.CurrentRow.Cells[0].Value.ToString();
                        //string maloai = dgv.CurrentRow.Cells[1].Value.ToString();
                        //string mancc = dgv.CurrentRow.Cells[2].Value.ToString();
                        //string tensp = dgv.CurrentRow.Cells[3].Value.ToString();
                        //string mau = dgv.CurrentRow.Cells[4].Value.ToString();
                        //string size = dgv.CurrentRow.Cells[5].Value.ToString();
                        //string mota = dgv.CurrentRow.Cells[6].Value.ToString();
                        //string hinh = dgv.CurrentRow.Cells[7].Value.ToString();
                        //string soluong = dgv.CurrentRow.Cells[8].Value.ToString();
                        //string gianhap = dgv.CurrentRow.Cells[9].Value.ToString();
                        //string giaban = dgv.CurrentRow.Cells[10].Value.ToString();
                        ////string trangthai = dgvDanhSach.CurrentRow.Cells[6].Value.ToString();
                        //string sql = "update sanpham set  tennv= N'" + tennv + "', ngaysinh='" + ngaysinh + "', diachi=N'" + diachi + "', email='" + email + "', phone='" + phone + "' where manv = '" + manv + "'";
                        //if (c.UpdateData(sql) > 0)
                        //{
                        //    MessageBox.Show("Cập nhật thành công");
                        //    //vitri = 0;
                        //    SanPham_Load(sender, e);
                        //}
                    }
                }
            }
        }
        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
            {
                e.Handled = true;
            }
        }
        private void SanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = new DialogResult();
            kq = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.No)
            {
                e.Cancel = true;
            }
        }


        private void txtGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
            {
                e.Handled = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
