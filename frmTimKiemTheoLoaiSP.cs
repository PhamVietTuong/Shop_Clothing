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
    public partial class frmTimKiemTheoLoaiSP : Form
    {
        public frmTimKiemTheoLoaiSP()
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
     
        private void frmTimKiemTheoLoaiSP_Load(object sender, EventArgs e)
        {
            dsLoaiSP = c.TakeData("select * from loaisanpham where trangthai = 0");
            HienThiComboBox(cmbLoaiSP, dsLoaiSP, "tenloai", "maloai");
            f = true;
        }

        private void cmbLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (f)
            {
                if (cmbLoaiSP.SelectedIndex != -1)
                {
                    string maloai = cmbLoaiSP.SelectedValue.ToString();
                    string sql = "select * from sanpham where maloai = '" + maloai + "'AND trangthai = 0";
                    dsSanPham = c.TakeData(sql);
                    dgv.DataSource = dsSanPham.Tables[0];
                }
            }
        }
    }
}
