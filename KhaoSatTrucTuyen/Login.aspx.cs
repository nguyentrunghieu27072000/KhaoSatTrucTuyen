using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KhaoSatTrucTuyen
{
    public partial class Dangnhap : System.Web.UI.Page
    {
        public static string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public static SqlConnection connectDatabase()
        {
            SqlConnection myCnn = new SqlConnection(strCon);
            myCnn.Open();
            return myCnn;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string user = username.Text;
            string pass = password.Text;
            SqlConnection myCnn = connectDatabase();
            SqlCommand cmd = new SqlCommand("proc_login", myCnn);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.CommandType = CommandType.StoredProcedure;
            using (SqlDataReader data = cmd.ExecuteReader())
            {
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        if (Equals(data["matkhau"], pass))
                        {
                            Session["login"] = data["id_taikhoan"];
                            Response.Redirect("Home.aspx");
                        }
                    }
                    string Mesg = "Username or password false!";
                    errorLogin.Text = Mesg;
                }
                else
                {
                    string Mesg = "Account does not exist!";
                    errorLogin.Text = Mesg;
                }
            }
        }
    }
}