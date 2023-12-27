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
    public partial class frmSize : Form
    {
        public frmSize()
        {
            InitializeComponent();
        }
        clsShopQuanAo c = new clsShopQuanAo();
        DataSet dsSize = new DataSet();
        void HienThi()
        {
            dsSize = c.TakeData("select * from size where trangthai = 0");
            for (int i = 0; i < dsSize.Tables[0].Rows.Count; i++)
            {
                CheckBox chk = new CheckBox();
                chk.Name = dsSize.Tables[0].Rows[i]["masize"].ToString();
                chk.Text = dsSize.Tables[0].Rows[i]["tensize"].ToString();
                //chk.Width = 300;
                //chk.Height = 300;
                //chk.AutoSize = false;
                flpSize.Controls.Add(chk);
                
            }
        }
        private void frmSize_Load(object sender, EventArgs e)
        {
            HienThi();
            
        }
    }
}
