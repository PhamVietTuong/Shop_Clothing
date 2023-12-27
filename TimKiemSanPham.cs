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
    public partial class TimKiemSanPham : Form
    {
        public TimKiemSanPham()
        {
            InitializeComponent();
        }
        clsShopQuanAo c = new clsShopQuanAo();
        DataSet dsLoaiSP = new DataSet();
        DataSet dsSanPham = new DataSet();
        DataSet dsNCC = new DataSet();
        Boolean f = false;
        int viTri = 0;
        void HienThiComboBox(ComboBox cmb, DataSet ds, string ten, string ma)
        {
            cmb.DataSource = ds.Tables[0];
            cmb.DisplayMember = ten;
            cmb.ValueMember = ma;
            cmb.SelectedIndex = -1;
        }
        void hienthianh(DataSet ds, int vt)
        {
            if (ds.Tables[0].Rows.Count > vt && vt > -1)
            {
                string tenhinh = ds.Tables[0].Rows[vt]["hinh"].ToString();
                string tenfile = Path.GetFullPath("img") + @"\" + tenhinh;
                taoanh_tufileanh(LoadImg, tenfile);
            }
        }
        void taoanh_tufileanh(PictureBox p, string filename)
        {
            Bitmap a = new Bitmap(filename);
            p.Image = a;
            p.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void cmbCacLoaiTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maloai = "";
            string sql = "";
            if (f)
            {
                if (cmbTimTheo.SelectedIndex == 0)
                {
                    if (cmbCacLoaiTimKiem.SelectedIndex != -1)
                    {
                        maloai = cmbCacLoaiTimKiem.SelectedValue.ToString();
                        sql = "select * from sanpham where masp = '" + maloai + "'";
                        dsSanPham = c.TakeData(sql);
                    }
                }
                if (cmbTimTheo.SelectedIndex == 1)
                {
                    if (cmbCacLoaiTimKiem.SelectedIndex != -1)
                    {
                        maloai = cmbCacLoaiTimKiem.SelectedValue.ToString();
                        sql = "select * from sanpham where maloai = '" + maloai + "'";
                        dsSanPham = c.TakeData(sql);
                    }
                }
                if (cmbTimTheo.SelectedIndex == 2)
                {
                    if (cmbCacLoaiTimKiem.SelectedIndex != -1)
                    {
                        maloai = cmbCacLoaiTimKiem.SelectedValue.ToString();
                        sql = "select * from sanpham where mancc = '" + maloai + "'";
                        dsSanPham = c.TakeData(sql);
                    }
                }
                dgv.DataSource = dsSanPham.Tables[0];
                label3.Visible = true;
                LoadImg.Visible = true;
            }
        }

        private void cmbTimTheo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lab = "";
            if (f)
            {
                if (cmbTimTheo.SelectedIndex == 0)
                {
                    lab = "Chọn tên sản phẩm";
                    dsSanPham = c.TakeData("select * from sanpham where trangthai = 0");
                    HienThiComboBox(cmbCacLoaiTimKiem, dsSanPham, "tensp", "masp");
                }
                if (cmbTimTheo.SelectedIndex == 1)
                {
                    lab = "Chọn tên loại sản phẩm";

                    dsLoaiSP = c.TakeData("select * from loaisanpham where trangthai = 0");
                    HienThiComboBox(cmbCacLoaiTimKiem, dsLoaiSP, "tenloai", "maloai");

                }
                if (cmbTimTheo.SelectedIndex == 2)
                {
                    lab = "Chọn tên nhà cung cấp";
                    dsNCC = c.TakeData("select * from nhacungcap where trangthai = 0");
                    HienThiComboBox(cmbCacLoaiTimKiem, dsNCC, "tenncc", "mancc");
                }
            }

            label2.Visible = true;
            label2.Text = lab;
            cmbCacLoaiTimKiem.Visible = true;
        }

        private void TimKiemSanPham_Load(object sender, EventArgs e)
        {
            dsSanPham = c.TakeData("select * from sanpham where trangthai = 0");
            this.cmbCacLoaiTimKiem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimTheo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            hienthianh(dsSanPham, viTri);

            dgv.ReadOnly = true;
            label2.Visible = false;
            cmbCacLoaiTimKiem.Visible = false;
            label3.Visible = false;
            LoadImg.Visible = false;
            f = true;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (f)
            {
                viTri = e.RowIndex;
                hienthianh(dsSanPham, viTri);
            }
        }
    }
}
