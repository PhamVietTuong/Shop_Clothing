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
    public partial class HoaDonNhap : Form
    {
        int viTri = 0;
        DataSet ds = new DataSet();
        public HoaDonNhap()
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
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            XuLyChucNang(false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            XuLyChucNang(false);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            XuLyChucNang(false);
        }

        private void HoaDonNhap_Load(object sender, EventArgs e)
        {
            XuLyChucNang(true);
            //đẩy dữ liệu vào bản
            //ds = c.TakeData("");
            danhSach_datagridview(dgv, "select * from hoadonnhap where trangthai = 0");
            hienThiLenTextBox(ds, viTri);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void hienThiLenTextBox(DataSet ds, int vt)
        {
            txtMaHDNhap.Text = ds.Tables[0].Rows[vt]["mahdnhap"].ToString();
            txtMaNCC.Text = ds.Tables[0].Rows[vt]["mancc"].ToString();
            txtMaNV.Text = ds.Tables[0].Rows[vt]["manv"].ToString();
            dateNow.Text = ds.Tables[0].Rows[vt]["ngaynhap"].ToString();
            txtThanhTien.Text = ds.Tables[0].Rows[vt]["thanhtien"].ToString();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            viTri = e.RowIndex;
            hienThiLenTextBox(ds, viTri);
        }
    }
}
