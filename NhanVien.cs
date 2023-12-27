using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace _22_27_ShopQuanAo
{
    public partial class NhanVien : Form
    {
        int viTri = 0;
        DataSet ds = new DataSet();
        public NhanVien()
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
            txtTenNV.ReadOnly = t;
            txtDiaChi.ReadOnly = t;
            txtEmail.ReadOnly = t;
            txtPhone.ReadOnly = t;
        }
        void XuLyTrangThai(bool t)
        {
            cmbTinhTrang.Visible = t;
            label4.Visible = t;
        }
        int flag = 0;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            label1.Text = "THÊM NHÂN VIÊN";
            flag = 1;
            XuLyChucNang(false);
            XuLyTextBox(false);
            XuLyTrangThai(false);
            txtMaNV.Text = phatSinhMa();
            dateNow.Value = DateTime.Now;
            txtTenNV.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtTenNV.Focus();
            rdNam.Checked = true;
            rdNam.Enabled = !false;
            rdNu.Enabled = !false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            label1.Text = "SỬA NHÂN VIÊN";
            flag = 2;
            XuLyTextBox(false);
            XuLyChucNang(false);
            XuLyTrangThai(true);
            cmbTinhTrang.SelectedIndex = 0;
            this.cmbTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            rdNam.Enabled = !false;
            rdNu.Enabled = !false;
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            label1.Text = "XÓA NHÂN VIÊN";
            flag = 3;
            XuLyChucNang(false);
            XuLyTrangThai(false);
            rdNam.Enabled = false;
            rdNu.Enabled = false;
        }
        Boolean f = false;
        private void NhanVien_Load(object sender, EventArgs e)
        {
            f = true;
            XuLyTrangThai(false);
            XuLyChucNang(true);
            danhSach_datagridview(dgv, "select * from nhanvien where trangthai = 0");
            hienThiLenTextBox(ds, viTri);
            rdNu.Checked = true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void hienThiLenTextBox(DataSet ds, int vt)
        {
            if (ds.Tables[0].Rows.Count > vt && vt > -1)
            {
                txtMaNV.Text = ds.Tables[0].Rows[vt]["manv"].ToString();
                txtTenNV.Text = ds.Tables[0].Rows[vt]["tennv"].ToString();
                string phai = ds.Tables[0].Rows[vt]["phai"].ToString();
                dateNow.Text = ds.Tables[0].Rows[vt]["ngaysinh"].ToString();
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
            label1.Text = "DANH SÁCH NHÂN VIÊN";
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

                if (string.IsNullOrEmpty(txtTenNV.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtPhone.Text))
                {
                    MessageBox.Show("Bạn cần điền đầy đủ thông tin !!!");
                    XuLyChucNang(false);
                    return;
                }
                else if (!dinhDangEmail(txtEmail.Text))
                {
                    MessageBox.Show("Định dạng email sai, vui lòng nhập lại");
                    txtEmail.Focus();
                    XuLyChucNang(false);
                    return;
                }
                else if (!dinhDangSDT(txtPhone.Text))
                {
                    MessageBox.Show("Định dạng số điện thoại sai, vui lòng nhập lại");
                    txtPhone.Focus();
                    XuLyChucNang(false);
                    return;
                }
                else
                {
                    sql = "INSERT INTO nhanvien values('" + txtMaNV.Text + "', N'" + txtTenNV.Text + "', N'" + gender + "', '" + dateNow.Text + "', N'" + txtDiaChi.Text + "', '" + txtEmail.Text + "', '" + txtPhone.Text + "',0)";
                }
            }
            if (flag == 2)
            {
                sql = "update nhanvien set tennv = N'" + txtTenNV.Text + "', phai = N'" + gender + "', ngaysinh = '" + dateNow.Text + "', diachi = N'" + txtDiaChi.Text + "',  email = '" + txtEmail.Text + "',  phone = '" + txtPhone.Text + "',  trangthai = " + cmbTinhTrang.SelectedIndex + "where manv = '" + txtMaNV.Text + "'";

            }
            if (flag == 3)
            {
                sql = "update nhanvien set trangthai = 1 where manv = '" + txtMaNV.Text + "'";

            }

            if (c.UpdateData(sql) > 0)
            {
                MessageBox.Show("Cập nhật thành công");
                viTri = 0;
                NhanVien_Load(sender, e);
            }
            NhanVien_Load(sender, e);
            flag = 0;
            rdNam.Enabled = false;
            rdNu.Enabled = false;
            XuLyTextBox(true);
            XuLyChucNang(true);
            XuLyTrangThai(false);
        }
        string phatSinhMa()
        {
            DataSet dsma = c.TakeData("select * from nhanvien");
            return "NV0" + (dsma.Tables[0].Rows.Count + 1).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "DANH SÁCH NHÂN VIÊN";
            XuLyChucNang(true);
            XuLyTrangThai(false);
            rdNam.Enabled = false;
            rdNu.Enabled = false;
            NhanVien_Load(sender, e);
        }

        private void NhanVien_FormClosing(object sender, FormClosingEventArgs e)
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
                    string manv = dgv.CurrentRow.Cells[0].Value.ToString();
                    string tennv = dgv.CurrentRow.Cells[1].Value.ToString();
                    string ngaysinh = dgv.CurrentRow.Cells[2].Value.ToString();
                    string diachi = dgv.CurrentRow.Cells[3].Value.ToString();
                    string email = dgv.CurrentRow.Cells[4].Value.ToString();
                    string phone = dgv.CurrentRow.Cells[5].Value.ToString();
                    //string trangthai = dgvDanhSach.CurrentRow.Cells[6].Value.ToString();
                    string sql = "update nhanvien set  tennv= N'" + tennv + "', ngaysinh='" + ngaysinh + "', diachi=N'" + diachi + "', email='" + email + "', phone='" + phone + "' where manv = '" + manv + "'";
                    if (c.UpdateData(sql) > 0)
                    {
                        MessageBox.Show("Cập nhật thành công");
                        //vitri = 0;
                        NhanVien_Load(sender, e);
                    }
                }
            }
        }


    }
}
