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
    public partial class Main : Form
    {
        private int childFormNumber = 0;

        public Main()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private void frmNCC_Click(object sender, EventArgs e)
        {
            NhaCungCap frm = new NhaCungCap();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmLoaiSP_Click(object sender, EventArgs e)
        {
            LoaiSanPham frm = new LoaiSanPham();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmNhanVien_Click(object sender, EventArgs e)
        {
            NhanVien frm = new NhanVien();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmHoaDonNhap_Click(object sender, EventArgs e)
        {
            //HoaDonNhap frm = new HoaDonNhap();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void frmSize_Click(object sender, EventArgs e)
        {
            Size frm = new Size();
            frm.MdiParent = this;
            frm.Show();
        }

        private void màuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mau frm = new Mau();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmHoaDonBan_Click(object sender, EventArgs e)
        {
            HoaDonBan frm = new HoaDonBan();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmKhachHang_Click(object sender, EventArgs e)
        {
            KhachHang frm = new KhachHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmThongTinSP_Click(object sender, EventArgs e)
        {
            frmTimKiemTheoLoaiSP frm = new frmTimKiemTheoLoaiSP();
            frm.MdiParent = this;
            frm.Show();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SanPham frm = new SanPham();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
