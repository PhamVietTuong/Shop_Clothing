using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace _22_27_ShopQuanAo
{
    public partial class clsShopQuanAo : Form
    {
        SqlConnection con = new SqlConnection();
        void Connect()
        {
            con.ConnectionString = @"Data source = PhamVietTuong\SQLEXPRESS; Initial Catalog = QuanLyQuanAo; integrated Security = True";

            if (con.State == ConnectionState.Closed)
                con.Open();
        }
        void CloseConnect()
        {
            con.Close();
        }
        //Phương thức khởi tạo
        public clsShopQuanAo()
        {
            Connect();
        }
        //chứa các bản
        //datatable chỉ chưa 1 bản
        //dataset chưa nhiều bản
        public DataSet TakeData(String sql)
        {
            DataSet ds = new DataSet();
            //để kết nối db đưa vào dataset cần 1 thằng trung gian đó là SqlDataAdapter
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            //load dữ liệu vào dataset
            da.Fill(ds);
            return ds;
        }
        public int UpdateData(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            return cmd.ExecuteNonQuery();
        }
        private void clsShopQuanAo_Load(object sender, EventArgs e)
        {

        }
        //public clsShopQuanAo()
        //{
        //    InitializeComponent();
        //}
    }
}
