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
    public partial class Size : Form
    {
        int viTri = 0;
        DataSet ds = new DataSet();
        public Size()
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
            btnHuy.Enabled = !t;
        }
        void XuLyTextBox(bool t)
        {
            txtTenSize.ReadOnly = !t;
            txtMaSize.ReadOnly = t;
        }
        void XuLyTrangThai(bool t)
        {
            cmbTinhTrang.Visible = t;
            label4.Visible = t;
        }
        int flag = 0;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            label1.Text = "THÊM SIZE";
            XuLyChucNang(false);
            XuLyTextBox(true);
            XuLyTrangThai(false);
            txtMaSize.Text = phatSinhMa();
            txtTenSize.Clear();
            txtTenSize.Focus();
            flag = 1;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            label1.Text = "SỮA SIZE";

            flag = 2;
            XuLyTextBox(true);
            XuLyChucNang(false);
            XuLyTrangThai(true);
            cmbTinhTrang.SelectedIndex = 0;
            this.cmbTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            label1.Text = "XÓA SIZE";

            XuLyChucNang(false);
            flag = 3;
            XuLyTrangThai(false);
        }
        Boolean f = false;
        private void Size_Load(object sender, EventArgs e)
        {
            f = true;
            XuLyChucNang(true);
            //đẩy dữ liệu vào bản
            //ds = c.TakeData("");
            danhSach_datagridview(dgv, "select * from size where trangthai = 0");
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
                txtMaSize.Text = ds.Tables[0].Rows[vt]["masize"].ToString();
                txtTenSize.Text = ds.Tables[0].Rows[vt]["tensize"].ToString();
            }
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            viTri = e.RowIndex;
            hienThiLenTextBox(ds, viTri);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            label1.Text = "DANH SÁCH SIZE SẢN PHẨM";
            string sql = "";
            if (flag == 1)
            {
                if (string.IsNullOrEmpty(txtTenSize.Text))
                {
                    MessageBox.Show("Bạn chưa nhập tên size !!!");
                    txtTenSize.Focus();
                    XuLyChucNang(false);
                    return;
                }
                else
                {
                    sql = "INSERT INTO size values('" + txtMaSize.Text + "', N'" + txtTenSize.Text + "', 0 )";
                }
            }
            else if (flag == 2)
            {
                sql = "update size set tensize = N'" + txtTenSize.Text + "',  trangthai = " + cmbTinhTrang.SelectedIndex + " where masize = '" + txtMaSize.Text + "'";
            }
            else if (flag == 3)
            {
                sql = "update size set trangthai = 1 where masize = '" + txtMaSize.Text + "'";
            }
            if (c.UpdateData(sql) > 0)
            {
                MessageBox.Show("Cập nhật thành công");
                viTri = 0;
                Size_Load(sender, e);
            }
            flag = 0;
            XuLyTextBox(true);
            txtTenSize.ReadOnly = true;
            XuLyChucNang(true);
            XuLyTrangThai(false);
        }
        string phatSinhMa()
        {
            DataSet dsma = c.TakeData("select * from size");
            return "S0" + (dsma.Tables[0].Rows.Count + 1).ToString();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            label1.Text = "DANH SÁCH SIZE SẢN PHẨM";
            XuLyChucNang(true);
            XuLyTrangThai(false);
            Size_Load(sender, e);
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
                    string masize = dgv.CurrentRow.Cells[0].Value.ToString();
                    string tensize = dgv.CurrentRow.Cells[1].Value.ToString();
                    //string trangthai = dgv.CurrentRow.Cells[2].Value.ToString();
                    sql = "update size set tensize = '" + tensize + "'  where masize = '" + masize + "'";
                    if (c.UpdateData(sql) > 0)
                    {
                        MessageBox.Show("Cập nhật thành công");
                        viTri = 0;
                        //lưu xong cập nhật dữ liệu hiện thị lên datagridview
                        Size_Load(sender, e);
                    }
                }
            }
        }
        private void Size_FormClosing(object sender, FormClosingEventArgs e)
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
