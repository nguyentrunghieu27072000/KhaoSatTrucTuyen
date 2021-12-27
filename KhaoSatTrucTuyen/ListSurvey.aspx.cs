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
    public partial class ListSurvey : System.Web.UI.Page
    {
        public DataTable list_loaicauhoi = new DataTable();
        public DataTable ds_cauhoi = new DataTable();
        public DataTable ds_dapan = new DataTable();
        public DataTable ds_user_send = new DataTable();
        public string ma_khaosat_active = "";
        public string list_servey = "";
        public string title_servey = "";
        public int count_question;
        public int check_send = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id_taikhoan = Session["login"].ToString();
            if (Request.QueryString["id"] != null)
            {
                ma_khaosat_active = Request.QueryString["id"];
                load_Kind_of_question();
                ds_cauhoi = GetQuestionServey(ma_khaosat_active);
                ds_dapan = GetAnswerQuestion(ds_cauhoi);
                GetUser(id_taikhoan);
            }
            GetListServey(id_taikhoan, ma_khaosat_active);
        }
        public void load_Kind_of_question()
        {
            var lstParameter = new List<KeyValuePair<string, string>>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("sp_get_koq", lstParameter);
            list_loaicauhoi = dt;
        }
        public void GetListServey(string id_taikhoan, string ma_khaosat_active)
        {
            var lstParameter = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string,string>("@id_taikhoan",id_taikhoan),
            };
            DataTable dt = DataProvider.Instance.ExecuteQuery("sp_get_maukhaosat", lstParameter);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ma_khaosat"].ToString() == ma_khaosat_active)
                    {
                        list_servey += "<div class='item-survey active-survey'><a href='ListSurvey.aspx?id=" + dr["ma_khaosat"] + "'>" + dr["ten_khaosat"] + "</a></div>";
                    }
                    else
                    {
                        list_servey += "<div class='item-survey'><a href='ListSurvey.aspx?id=" + dr["ma_khaosat"] + "'>" + dr["ten_khaosat"] + "</a></div>";
                    }

                }
            }
        }
        public DataTable GetQuestionServey(string ma_khaosat)
        {
            var lstParameter = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string,string>("@ma_khaosat",ma_khaosat),
            };
            DataTable dt = DataProvider.Instance.ExecuteQuery("sp_get_cauhoi_khaosat", lstParameter);
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("ListSurvey.aspx");
            }
            title_servey = dt.Rows[0]["ten_khaosat"].ToString();
            count_question = dt.Rows.Count;
            return dt;
        }
        public DataTable GetAnswerQuestion(DataTable ds_cauhoi)
        {
            DataTable dt_dapan = new DataTable();
            DataTable dt_cauhoi = new DataTable();
            dt_cauhoi.Columns.Add(new DataColumn("ma_cauhoi"));
            for (int i = 0; i < ds_cauhoi.Rows.Count; ++i)
            {
                dt_cauhoi.Rows.Add(ds_cauhoi.Rows[i]["ma_cauhoi"].ToString());
            }
            SqlConnection con = new SqlConnection(DataProvider.Instance.conStr);
            SqlCommand cmd = new SqlCommand("sp_getdapan", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@list_answer", dt_cauhoi);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt_dapan.Load(dr);
            con.Close();
            return dt_dapan;
        }
        public void GetUser(string id_taikhoan)
        {
            SqlConnection con = new SqlConnection(DataProvider.Instance.conStr);
            SqlCommand cmd = new SqlCommand("sp_get_user_send", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma_khaosat", ma_khaosat_active);
            cmd.Parameters.AddWithValue("@id_taikhoan", id_taikhoan);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ds_user_send.Load(dr);
            foreach (DataRow drdapan in ds_user_send.Rows)
            {
                if (!string.IsNullOrEmpty(drdapan["ma_phieukhaosat"].ToString()))
                {
                    check_send = 1;
                    break;
                }
            }
            Lv_User.DataSource = ds_user_send;
            Lv_User.DataBind();
            HiddenField1.Value = ma_khaosat_active;
        }
        protected void send_click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument.ToString();
            string ma_khaosat = HiddenField1.Value;
            var lstParameter = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("@ma_phieukhaosat",((Int32)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString()),
                    new KeyValuePair<string, string>("@thoigian",DateTime.Now.ToString("yyyy-MM-dd h:mm:ss")),
                    new KeyValuePair<string, string>("@id_taikhoan",id),
                    new KeyValuePair<string, string>("@ma_khaosat",ma_khaosat),
                    new KeyValuePair<string, string>("@trangthai","0"),
                };
            bool flg = DataProvider.Instance.ExecuteQueryNoReturn("sp_add_sheet_survey", lstParameter);
            if (flg)
            {
                ((Button)sender).Text = "Đã gửi";
                ((Button)sender).CssClass = "sent";
                ((Button)sender).Enabled = false;
            }
            System.Threading.Thread.Sleep(3000);
        }
    }
}