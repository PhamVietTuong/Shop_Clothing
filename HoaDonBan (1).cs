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
using System.Text.RegularExpressions;

namespace _22_27_ShopQuanAo
{
    public partial class HoaDonBan : Form
    {
        clsShopQuanAo c = new clsShopQuanAo();
        int viTri = 0;
        DataSet dsHoaDonBan = new DataSet();
        DataSet dsChiTietHoaDonBan = new DataSet();

        DataSet dsNhanVien = new DataSet();
        DataSet dsSanPham = new DataSet();
        public HoaDonBan()
        {
            InitializeComponent();
        }
        //đẩy dữ liệu vào danhSach_datagridview
        void danhSach_datagridview(DataGridView d, string sql)
        {
            dsHoaDonBan = c.TakeData(sql);
            d.DataSource = dsHoaDonBan.Tables[0];
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
            txtMaHDBan.ReadOnly = !t;
            txtTongTien.ReadOnly = !t;
            txtSĐT.ReadOnly = t;
            txtMaCTHDBan.ReadOnly = !t;
            //txtTenSP.ReadOnly = t;
            txtGiaBan.ReadOnly = !t;
            txtSoLuong.ReadOnly = t;
            txtKhuyenMai.ReadOnly = t;
            //txtThanhTien.ReadOnly = !t;
        }
        void XuLyTrangThai(bool t)
        {
            cmbTinhTrang.Visible = t;
            label4.Visible = t;
        }
        void CreateColumnCTHDBan()
        {
            dgv_CTHoaDon.Columns.Clear();
            dgv_CTHoaDon.Columns.Add("macthdnban", "Mã CTHĐBan");
            dgv_CTHoaDon.Columns.Add("mahdban", "Mã HĐBan");
            dgv_CTHoaDon.Columns.Add("masp", "Mã sản phẩm");
            dgv_CTHoaDon.Columns.Add("soluong", "Số lượng");
            dgv_CTHoaDon.Columns.Add("dongia", "Đơn giá");
            dgv_CTHoaDon.Columns.Add("thanhtien", "Thành tiền");
            dgv_CTHoaDon.Columns.Add("khuyenmai", "Khuyến mãi");
        }
        void CreateColumnHDBan()
        {
            dgv_HoaDon.Columns.Clear();
            dgv_HoaDon.Columns.Add("mahdban", "Mã HĐBan");
            dgv_HoaDon.Columns.Add("manv", "Mã nhân viên");
            dgv_HoaDon.Columns.Add("makh", "Mã khách hàng");
            dgv_HoaDon.Columns.Add("ngaylaphd", "Ngày lập hóa đơn");
            dgv_HoaDon.Columns.Add("tongtien", "Tổng tiền");
            dgv_HoaDon.Columns.Add("phone", "SĐT");
        }
        void locDuLieuCombobox(DataSet ds, ComboBox cmb, string ten, string ma, string giatrima)
        {
            DataView dv = new DataView();
            dv.Table = ds.Tables[0];
            dv.RowFilter = ma + " = '" + giatrima + "'";
            cmb.DataSource = dv;
            cmb.DisplayMember = ten;
            cmb.ValueMember = ma;
            //rowfilter dùng lọc theo đk
            //dataview không cần .table0 nó chỉ lưu 1 bản còn dataset phải .table0 vì nó lưu n bản
        }
        int flag = 0;
        void clear() {
            lblTenKhachHang.Text = "";
            txtSĐT.Clear();
            txtTongTien.Clear();
            //txtTenSP.Clear();
            txtGiaBan.Clear();
            txtSoLuong.Clear();
            txtKhuyenMai.Clear();
            txtSĐT.Focus();
        }
        void load_CTHDBanTheoMaHDN(string mahdn)
        {
            string sql = "select * from chitiethoadonban where mahdban='" + mahdn + "'";
            dsChiTietHoaDonBan = c.TakeData(sql);
            dgv_CTHoaDon.DataSource = null;
            dgv_CTHoaDon.Columns.Clear();
            dgv_CTHoaDon.DataSource = dsChiTietHoaDonBan.Tables[0];
        }
        string MAKH = "";
        Boolean TimKhachHang()
        {
            DataSet ds = new DataSet();
            ds = c.TakeData("select * from khachhang where phone = '" + txtSĐT.Text + "'");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MAKH = ds.Tables[0].Rows[0]["makh"].ToString();
                    lblTenKhachHang.Text = ds.Tables[0].Rows[0]["tenkh"].ToString();
                    return true;
                }
            }
            return false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            label1.Text = "THÊM HÓA ĐƠN BÁN HÀNG";

            flag = 1;

            XuLyChucNang(false);
            XuLyTextBox(false);
            XuLyTrangThai(false);

            dgv_HoaDon.DataSource = null;
            dgv_CTHoaDon.DataSource = null;

            CreateColumnHDBan();
            CreateColumnCTHDBan();

            clear();

            dsNhanVien = c.TakeData("select * from nhanvien where trangthai = 0");
            dsSanPham = c.TakeData("select * from sanpham where trangthai = 0");

            hienThiCombobox(cmbTenNV, dsNhanVien, "tennv", "manv");
            hienThiCombobox(cmbTenSP, dsSanPham, "tensp", "masp");

            cmbTenNV.SelectedIndex = 0;
            cmbTenSP.SelectedIndex = 0;


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            label1.Text = "SỬA HÓA ĐƠN BÁN HÀNG";
            flag = 2;
            XuLyTextBox(false);
            XuLyChucNang(false);
            XuLyTrangThai(true);
            cmbTinhTrang.SelectedIndex = 0;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            label1.Text = "XÓA HÓA ĐƠN BÁN HÀNG";
            flag = 3;
            XuLyChucNang(false);
            XuLyTrangThai(false);
        }
        Boolean f = false;
        private void HoaDonBan_Load(object sender, EventArgs e)
        {
            XuLyTrangThai(false);
            XuLyChucNang(true);
            danhSach_datagridview(dgv_HoaDon, "select * from hoadonban where trangthai = 0");

            dsNhanVien = c.TakeData("select * from nhanvien");
            dsSanPham = c.TakeData("select * from sanpham");

            hienThiLenTextBoxhdban(dsHoaDonBan, viTri);
            hienThiLenTextBoxcthdban(dsChiTietHoaDonBan, viTri);
            f = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void hienThiCombobox(ComboBox cmb, DataSet ds, string ten, string ma)
        {
            cmb.DataSource = ds.Tables[0];
            cmb.DisplayMember = ten;
            cmb.ValueMember = ma;
            cmb.SelectedIndex = -1;
        }
        void hienThiLenTextBoxhdban(DataSet ds, int vt)
        {

            if (ds.Tables[0].Rows.Count > vt && vt > -1)
            {
                txtMaHDBan.Text = ds.Tables[0].Rows[vt]["mahdban"].ToString();
                txtTongTien.Text = ds.Tables[0].Rows[vt]["tongtien"].ToString();
                dateNow.Text = ds.Tables[0].Rows[vt]["ngaylaphd"].ToString();
                txtSĐT.Text = ds.Tables[0].Rows[vt]["phone"].ToString();
                lblTenKhachHang.Text = ds.Tables[0].Rows[vt]["makh"].ToString();

                string manv = ds.Tables[0].Rows[vt]["manv"].ToString();
                locDuLieuCombobox(dsNhanVien, cmbTenNV, "tennv", "manv", manv);

                //string makh = ds.Tables[0].Rows[vt]["makh"].ToString();
                //locDuLieuCombobox(dsKhachHang, cmbKhachHang, "tenkh", "makh", makh);
                load_CTHDBanTheoMaHDN(ds.Tables[0].Rows[vt]["mahdban"].ToString());

                if (cmbTenNV.SelectedIndex != -1)
                {
                    txtMaHDBan.Text = ds.Tables[0].Rows[vt]["mahdban"].ToString();
                }
            }
        }
        void hienThiLenTextBoxcthdban(DataSet ds, int vt)
        {

            if (ds.Tables[0].Rows.Count > vt && vt > -1)
            {
                txtMaCTHDBan.Text = ds.Tables[0].Rows[vt]["macthdnban"].ToString();
                txtGiaBan.Text = ds.Tables[0].Rows[vt]["dongia"].ToString();
                txtSoLuong.Text = ds.Tables[0].Rows[vt]["soluong"].ToString();
                txtKhuyenMai.Text = ds.Tables[0].Rows[vt]["khuyenmai"].ToString();
                //txtThanhTien.Text = ds.Tables[0].Rows[vt]["thanhtien"].ToString();

                string masp = ds.Tables[0].Rows[vt]["masp"].ToString();
                locDuLieuCombobox(dsSanPham, cmbTenSP, "tensp", "masp", masp);

                if (cmbTenSP.SelectedIndex != -1)
                {
                    txtMaCTHDBan.Text = ds.Tables[0].Rows[vt]["macthdnban"].ToString();
                }
            }
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            viTri = e.RowIndex;
            hienThiLenTextBoxhdban(dsHoaDonBan, viTri);

        }
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            KhachHang frm = new KhachHang();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                MAKH = frm.MaKH;
                lblTenKhachHang.Text = frm.TenKH;
                txtSĐT.Text = frm.Phone;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "DANH SÁCH HÓA ĐƠN BÁN HÀNG";
            XuLyChucNang(true);
            XuLyTrangThai(false);
            HoaDonBan_Load(sender, e);
        }
        double tongTienHĐ = 0;

        void taoCTHoaDon(object[] d, float thanhtien)
        {
            d[0] = txtMaCTHDBan.Text /*+ "." + txt.Text + "." + cmbTenNV.SelectedValue.ToString()*/;//mã cthdb
            d[1] = txtMaHDBan.Text;
            d[2] = cmbTenSP.SelectedValue.ToString();
            d[3] = txtSoLuong.Text;
            d[4] = txtGiaBan.Text;
            thanhtien = (float.Parse(txtSoLuong.Text) * float.Parse(txtGiaBan.Text)) - float.Parse(txtKhuyenMai.Text);
            d[5] = thanhtien.ToString();
            d[6] = txtKhuyenMai.Text;
            d[7] = '0';

            tongTienHĐ += thanhtien;
            dgv_CTHoaDon.Rows.Add(d);
            txtTongTien.Text = tongTienHĐ.ToString();
        }

        void taoHoaDon(object[] d, float thanhtien)
        {
            d[0] = txtMaHDBan.Text;
            d[1] = cmbTenNV.SelectedValue.ToString();
            d[2] = MAKH.ToString();
            d[3] = dateNow.Text;
            d[4] = txtTongTien.Text;
            d[5] = txtSĐT.Text;
            d[6] = '0';

            tongTienHĐ += thanhtien;
            dgv_HoaDon.Rows.Add(d);
            txtTongTien.Text = tongTienHĐ.ToString();
        }
        bool xuly_HD(DataGridView dgv)
        {
            for (int j = 0; j < dgv.Rows.Count - 1; j++)
            {
                if (dgv.Rows[j].Cells[0].Value.ToString() == txtMaHDBan.Text)
                {
                    dgv.Rows[j].Cells[4].Value = txtTongTien.Text;
                    return true;
                }

            }
            return false;
        }
        bool xuly_CTHD(DataGridView dgv, float thanhtien)
        {
            for (int i = 0; i < dgv.Rows.Count - 1; i++)
            {
                //manv0102.mancc0204.mamau01.masize02
                if (dgv.Rows[i].Cells[0].Value.ToString() == txtMaCTHDBan.Text && dgv.Rows[i].Cells[1].Value.ToString() == txtMaHDBan.Text)
                {
                    int soluong = int.Parse(dgv.Rows[i].Cells[3].Value.ToString());
                    float dongia = float.Parse(dgv.Rows[i].Cells[4].Value.ToString());

                    soluong += int.Parse(txtSoLuong.Text);
                    dgv.Rows[i].Cells[3].Value = soluong;

                    thanhtien += (soluong * dongia);

                    dgv.Rows[i].Cells[5].Value = thanhtien;
                    tongTienHĐ = thanhtien;
                    txtTongTien.Text = tongTienHĐ.ToString();

                    xuly_HD(dgv_HoaDon);
                    return true;
                }
            }
            return false;
        }

        private void btnThemCTHD_Click(object sender, EventArgs e)
        {
            //object[] CTHD = new object[8];
            //object[] HD = new object[8];
            //float thanhtien = 0;
            //CTHD[0] = txtMaCTHDBan.Text /*+ "." + txt.Text + "." + cmbTenNV.SelectedValue.ToString()*/;//mã cthdb
            //CTHD[1] = txtMaHDBan.Text;
            //CTHD[2] = cmbTenSP.SelectedValue.ToString();
            //CTHD[3] = txtSoLuong.Text;
            //CTHD[4] = txtGiaBan.Text;
            //thanhtien = (float.Parse(txtSoLuong.Text) * float.Parse(txtGiaBan.Text)) - float.Parse(txtKhuyenMai.Text);
            //CTHD[5] = thanhtien.ToString();
            //CTHD[6] = txtKhuyenMai.Text;
            //CTHD[7] = '0';

            //dgv_CTHoaDon.Rows.Add(CTHD);

            //HD[0] = txtMaHDBan.Text;
            //HD[1] = cmbTenNV.SelectedValue.ToString();
            //HD[2] = makh.ToString();
            //HD[3] = dateNow.Text;
            //HD[4] = txtTongTien.Text;
            //HD[5] = txtSĐT.Text;
            //HD[6] = '0';

            //tongTienHĐ += thanhtien;
            //dgv_HoaDon.Rows.Add(HD);
            //txtTongTien.Text = tongTienHĐ.ToString();

            object[] CTHD = new object[8];
            object[] HD = new object[7];
            float thanhtien = 0;
            if (xuly_CTHD(dgv_CTHoaDon, thanhtien))
            {
                return;
            }

            taoCTHoaDon(CTHD, thanhtien);

            if (xuly_HD(dgv_HoaDon))
            {
                return;
            }
            else
            {
                taoHoaDon(HD, thanhtien);
            }
            //object[] d = new object[8];
            //d[0] = txtMaCTHDBan.Text /*+ "." + txt.Text + "." + cmbTenNV.SelectedValue.ToString()*/;//mã cthdb
            //d[1] = txtMaHDBan.Text;
            //d[2] = cmbTenSP.SelectedValue.ToString();
            //d[3] = txtSoLuong.Text;
            //d[4] = txtGiaBan.Text;
            //double thanhtien = (float.Parse(txtSoLuong.Text) * float.Parse(txtGiaBan.Text)) - float.Parse(txtKhuyenMai.Text);
            //d[5] = thanhtien.ToString();
            //d[6] = txtKhuyenMai.Text;
            //d[7] = '0';
            //dgv_CTHoaDon.Rows.Add(d);

            //tongTien += thanhtien;
            //txtTongTien.Text = tongTien.ToString();
            //taoCTHoaDon(CTHD);
        }

        private void cmbTenNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (f)
            {
                string mahdban = "";

                if (cmbTenNV.SelectedIndex != -1)
                {
                    string manv = cmbTenNV.SelectedValue.ToString();
                    DataSet ds = c.TakeData("select mahdban from hoadonban where manv ='" + manv + "'");
                    if (ds.Tables[0].Rows.Count < 9)
                    {
                        mahdban = manv + "0" + (ds.Tables[0].Rows.Count + 1).ToString();
                    }
                    else
                    {
                        mahdban = manv + (ds.Tables[0].Rows.Count + 1).ToString();
                    }
                }
                txtMaHDBan.Text = mahdban;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            label1.Text = "DANH SÁCH SẢN PHẨM";
            if (string.IsNullOrEmpty(cmbTenNV.SelectedValue.ToString()))
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên !!!");
                btnAdd_Click(sender, e);
            }
            else
            {
                if (flag == 1)
                {
                    string masp = cmbTenSP.SelectedValue.ToString();
                    dsSanPham = c.TakeData("select * from sanpham where masp ='" + masp + "'");
                    double tongSoLuong = double.Parse(dsSanPham.Tables[0].Rows[0]["soluong"].ToString());
                    //Thêm hd
                    string mahdban = txtMaHDBan.Text;
                    string manv = cmbTenNV.SelectedValue.ToString();
                    string makh = MAKH;
                    string ngaylaphd = dateNow.Value.ToShortDateString();
                    string tongtien = txtTongTien.Text;
                    string phone = txtSĐT.Text;
                    string trangthai = "0";
                    //hoadonban
                    //Số không ''
                    string sqlHoaDonBan = "insert into hoadonban values('" + mahdban + "','" + manv + "','" + makh + "','" + ngaylaphd + "'," + tongtien + "," + phone + ", " + trangthai + ")";

                    //CTHĐBAN
                    if (c.UpdateData(sqlHoaDonBan) > 0)
                    {
                        MessageBox.Show("Cập nhật HĐB thành công");
                        for (int i = 0; i < dgv_CTHoaDon.Rows.Count - 1; i++)
                        {
                            //dgv_CTHoaDon.Columns.Clear();
                            //dgv_CTHoaDon.Columns.Add("ctmahdban", "ctMã hóa đơn bán");
                            //dgv_CTHoaDon.Columns.Add("mahdban", "Mã hóa đơn bán");
                            //dgv_CTHoaDon.Columns.Add("masp", "Mã sản phẩm");
                            //dgv_CTHoaDon.Columns.Add("soluong", "Số lượng");
                            //dgv_CTHoaDon.Columns.Add("dongia", "Đơn giá");
                            //dgv_CTHoaDon.Columns.Add("thanhtien", "Thành tiền");
                            //dgv_CTHoaDon.Columns.Add("khuyenmai", "Khuyến mãi");

                            string macthdban = dgv_CTHoaDon.Rows[i].Cells[0].Value.ToString();
                            string mactsp = dgv_CTHoaDon.Rows[i].Cells[2].Value.ToString();
                            string soluong = dgv_CTHoaDon.Rows[i].Cells[3].Value.ToString();
                            string dongia = dgv_CTHoaDon.Rows[i].Cells[4].Value.ToString();
                            string khuyenmai = dgv_CTHoaDon.Rows[i].Cells[5].Value.ToString();
                            string thanhtien = dgv_CTHoaDon.Rows[i].Cells[6].Value.ToString();

                            string sqlchitiethoadonban = "insert into chitiethoadonban values('" + macthdban + "','" + mahdban + "','" + mactsp + "'," + soluong + "," + dongia + "," + khuyenmai + ", " + thanhtien + ", " + trangthai + ")";

                            if (c.UpdateData(sqlchitiethoadonban) > 0)
                            {
                                //Nếu có ctsp thì cập nhật bên ctsp và sp, không có ctsp thì chỉ cần cập nhật bên sp
                                //string[] maspUpdate = macthdban.Split('.');

                                //string update = "update ctsanpham set soluong = soluong - " + soluong + " where ctmasp = '"+macthdban+"'";

                                //c.UpdateData(update);

                                string update = "update sanpham set soluong -= " + soluong + " where masp = '" + masp + "'";

                                c.UpdateData(update);

                                MessageBox.Show("Cập nhật CTHĐB thành công");
                                //lưu xong cập nhật dữ liệu hiện thị lên datagridview
                            }
                        }
                        dgv_CTHoaDon.Columns.Clear();
                        dgv_HoaDon.Columns.Clear();
                        HoaDonBan_Load(sender, e);
                    }
                    flag = 0;
                }
                if (flag == 2)
                {

                    string sql = "update hoadonban set manv = '" + cmbTenNV.SelectedValue + "', makh = '" + lblTenKhachHang.Text + "', ngaylaphd = '" + dateNow.Text + "',  tongtien  = " + txtTongTien.Text + ",  phone  = " + txtSĐT.Text + ", trangthai = " + cmbTinhTrang.SelectedIndex + "where mahdban = '" + txtMaHDBan.Text + "'";

                    if (c.UpdateData(sql) > 0)
                    {
                        MessageBox.Show("Cập nhật thành công");
                        HoaDonBan_Load(sender, e);
                    }

                }
                if (flag == 3)
                {
                    string sql = "update hoadonban set trangthai = 1 where mahdban = '" + txtMaHDBan.Text + "'";

                    if (c.UpdateData(sql) > 0)
                    {
                        MessageBox.Show("Cập nhật thành công");
                        HoaDonBan_Load(sender, e);
                    }

                }
                //DialogResult result = new DialogResult();
                //result = MessageBox.Show("Bạn có muốn " + ChucNang, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //if (result == DialogResult.Yes)
                //{
                //    if (c.UpdateData(sql) > 0)
                //    {
                //        MessageBox.Show(ChucNang + " thành công");
                //        //lưu xong cập nhật dữ liệu hiện thị lên datagridview
                //        dgv_HoaDon.Columns.Clear();
                //    }
                //}
            }
        }
        void taoanh_tufileanh(PictureBox p, string filename)
        {
            Bitmap a = new Bitmap(filename);
            p.Image = a;
            p.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void cmbTenSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (f)
            {
                if (cmbTenSP.SelectedIndex != -1)
                {
                    string masp = cmbTenSP.SelectedValue.ToString();
                    dsSanPham = c.TakeData("select * from sanpham where masp ='" + masp + "'");
                    txtGiaBan.Text = dsSanPham.Tables[0].Rows[0]["giaban"].ToString();
                    string mahdban = txtMaHDBan.Text;
                    txtMaCTHDBan.Text = mahdban + "." + masp;

                    string tenhinh = dsSanPham.Tables[0].Rows[0]["hinh"].ToString();
                    string tenfile = Path.GetFullPath("img") + @"\" + tenhinh;
                    taoanh_tufileanh(LoadImg, tenfile);
                }
            }
        }

        //private void txtSoLuong_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtThanhTien.Text = ((float.Parse(txtSoLuong.Text) * float.Parse(txtGiaBan.Text))- float.Parse(txtKhuyenMai.Text)).ToString();
        //    }
        //}

        private void dgv_CTHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            viTri = e.RowIndex;
            hienThiLenTextBoxcthdban(dsChiTietHoaDonBan, viTri);
        }

        bool dinhDangSDT(string sdt)
        {
            if (!Regex.Match(sdt, @"^(\+[0-9]{1,3})?\d{9,10}$").Success || sdt.Length!=10)
            {
                MessageBox.Show("Định dạng số điện thoại sai, vui lòng nhập lại");
                txtSĐT.Clear();
                return false;
            }
            return true;
        }
        private void btnTimSDT_Click(object sender, EventArgs e)
        {
            if (txtSĐT.Text!="")
            {
                if (dinhDangSDT(txtSĐT.Text))
                {
                    if (!TimKhachHang())
                    {
                        MessageBox.Show("Không tìm thấy số điện thoại");
                    }
                }
                
            }
        }

        private void txtKhuyenMai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
            {
                e.Handled = true;
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
            {
                e.Handled = true;
            }
        }

        private void txtSĐT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
            {
                e.Handled = true;
            }
        }
    }
}

