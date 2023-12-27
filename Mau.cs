using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _22_27_ShopQuanAo
{
    public partial class Mau : Form
    {
        int viTri = 0;
        DataSet ds = new DataSet();
        public Mau()
        {
            InitializeComponent();
        }
        clsShopQuanAo c = new clsShopQuanAo();
        //đẩy dữ liệu vào danhSach_datagridview
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
            btnCancel.Enabled = !t;
        }
        void XuLyTextBox(bool t)
        {
            txtTenMau.ReadOnly = !t;
            txtMaMau.ReadOnly = t;
        }
        void XuLyTrangThai(bool t)
        {
            cmbTinhTrang.Visible = t;
            label4.Visible = t;
        }
        int flag = 0;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            label1.Text = "THÊM MÀU";
            flag = 1;
            XuLyChucNang(false);
            XuLyTextBox(true);
            XuLyTrangThai(false);
            txtMaMau.Text = phatSinhMa();
            txtTenMau.Clear();
            txtTenMau.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            label1.Text = "SỮA MÀU";

            flag = 2;
            XuLyTextBox(true);
            XuLyChucNang(false);
            XuLyTrangThai(true);
            cmbTinhTrang.SelectedIndex = 0;
            this.cmbTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            label1.Text = "XÓA MÀU";

            XuLyChucNang(false);
            flag = 3;
            XuLyTrangThai(false);
        }
        Boolean f = false;
        private void Mau_Load(object sender, EventArgs e)
        {
            f = true;
            XuLyChucNang(true);
            danhSach_datagridview(dgv, "select * from mau where trangthai = 0");
            hienThiLenTextBox(ds, viTri);
            XuLyTrangThai(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void hienThiLenTextBox(DataSet ds, int vt)
        {
            if (ds.Tables[0].Rows.Count > vt && vt > -1)
            {
                txtMaMau.Text = ds.Tables[0].Rows[vt]["mamau"].ToString();
                txtTenMau.Text = ds.Tables[0].Rows[vt]["tenmau"].ToString();
            }
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            viTri = e.RowIndex;
            hienThiLenTextBox(ds, viTri);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            label1.Text = "DANH SÁCH MÀU SẢN PHẨM";
            string sql = "";
            if (flag == 1)
            {
                if (string.IsNullOrEmpty(txtTenMau.Text))
                {
                    MessageBox.Show("Bạn chưa nhập tên màu !!!");
                    txtTenMau.Focus();
                    XuLyChucNang(false);
                    return;
                }
                else
                {
                    sql = "INSERT INTO mau values('" + txtMaMau.Text + "', N'" + txtTenMau.Text + "', 0 )";
                }
            }
            if (flag == 2)
            {
                sql = "update mau set tenmau = N'" + txtTenMau.Text + "',  trangthai = " + cmbTinhTrang.SelectedIndex + " where mamau = '" + txtMaMau.Text + "'";
            }
            if (flag == 3)
            {
                sql = "update mau set trangthai = 1 where mamau = '" + txtMaMau.Text + "'";
            }
            if (c.UpdateData(sql) > 0)
            {
                MessageBox.Show("Cập nhật thành công");
                viTri = 0;
                Mau_Load(sender, e);
            }
            flag = 0;
            XuLyTextBox(true);
            txtTenMau.ReadOnly = true;
            XuLyChucNang(true);
            XuLyTrangThai(false);
        }
        string phatSinhMa()
        {
            DataSet dsma = c.TakeData("select * from mau");
            return "M0" + (dsma.Tables[0].Rows.Count + 1).ToString();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            label1.Text = "DANH SÁCH MÀU SẢN PHẨM";
            XuLyChucNang(true);
            XuLyTrangThai(false);
            Mau_Load(sender, e);
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string sql = "";
            //Khi form load xong mới chạy cái này
            if (f)
            {
                //từ cột 1 mới cho người ta sửa
                //nếu == 1 thì chỉ sửa được cột tensize
                if (e.ColumnIndex == 1)
                {
                    //lấy vị trí
                    int vt = dgv.CurrentRow.Index;
                    //.value để lấy giá trị
                    string mamau = dgv.CurrentRow.Cells[0].Value.ToString();
                    string tenmau = dgv.CurrentRow.Cells[1].Value.ToString();
                    //string trangthai = dgv.CurrentRow.Cells[2].Value.ToString();
                    sql = "update mau set tenmau = N'" + tenmau + "'  where mamau = '" + mamau + "'";
                    if (c.UpdateData(sql) > 0)
                    {
                        MessageBox.Show("Cập nhật thành công");
                        viTri = 0;
                        //lưu xong cập nhật dữ liệu hiện thị lên datagridview
                        Mau_Load(sender, e);
                    }
                }
            }
        }

        private void Mau_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = new DialogResult();
            kq = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
