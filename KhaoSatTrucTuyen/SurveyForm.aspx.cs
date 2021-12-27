using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KhaoSatTrucTuyen
{
    public partial class Bieumau : System.Web.UI.Page
    {
        public string list_loaicauhoi = "";
        public string loaicauhoi_selected = "doan";
        protected void Page_Load(object sender, EventArgs e)
        {
            get_soluong_mau();
            if(Request.Form["save-survey"] == "save-survey")
            {
                save_survey();
            }
            if (Request.Form["save-survey"] != "save-survey" && Request.Form["save-survey"] != null)
            {
                string ma_khaosat = Request.Form["save-survey"];
                DeleteSurvey(ma_khaosat);
                save_survey();
            }
            list_loaicauhoi = load_Kind_of_question();
        }
        protected void DeleteSurvey(string ma_khaosat)
        {
            var lstParameter = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string,string>("@ma_khaosat",ma_khaosat),
            };
            DataProvider.Instance.ExecuteQueryNoReturn("sp_delete_survey", lstParameter);
        }
        public string load_Kind_of_question()
        {
            string list = "";
            var lstParameter = new List<KeyValuePair<string, string>>();
            DataTable dt = DataProvider.Instance.ExecuteQuery("sp_get_koq", lstParameter);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (Equals(loaicauhoi_selected, row["ma_loaicauhoi"]))
                        list += "<option value='" + row["ma_loaicauhoi"] + "'selected>" + row["ten_loaicauhoi"] + "</option>";
                    else
                        list += "<option value='" + row["ma_loaicauhoi"] + "'>" + row["ten_loaicauhoi"] + "</option>";
                }
            }
            return list;
        }
        public void save_survey()
        { 
            string ma = ((Int32)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            string id_taikhoan = Session["login"].ToString();
            string title_servey = Request.Form["title_survey"];
            int count_question = int.Parse(Request.Form["sl_question"]);
            var lstParameter = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("@ma_khaosat",ma),
                    new KeyValuePair<string, string>("@thoigian",DateTime.Now.ToString("yyyy-MM-dd")),
                    new KeyValuePair<string, string>("@ten_khaosat",title_servey),
                    new KeyValuePair<string, string>("@id_taikhoan",id_taikhoan),
                };
            bool flg = DataProvider.Instance.ExecuteQueryNoReturn("sp_add_survey", lstParameter);
            if (flg)
            {
                int[] arr_cauhoi = new int[100];
                // Datatable lưu trữ câu hỏi mẫu khỏa sát 
                DataTable dt_chks = new DataTable();
                dt_chks.Columns.Add(new DataColumn("ma_khaosat"));
                dt_chks.Columns.Add(new DataColumn("ma_cauhoi"));
                // Datatable lưu trữ câu hỏi 
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ma_cauhoi"));
                dt.Columns.Add(new DataColumn("noidung_cauhoi"));
                dt.Columns.Add(new DataColumn("thutu_cauhoi"));
                dt.Columns.Add(new DataColumn("ma_loaicauhoi"));
                for(int i = 1; i <= count_question; ++i)
                {
                    if(Request.Form["title_question_" + i] == null )
                    {
                        continue;
                    }
                    string ma_cauhoi = ((Int32)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString() + i;
                    string noidung_cauhoi = Request.Form["title_question_" + i];
                    string ma_loaicauhoi = Request.Form["Kind_of_question_" + i];
                    dt.Rows.Add(ma_cauhoi,noidung_cauhoi, i, ma_loaicauhoi);
                    dt_chks.Rows.Add(ma, ma_cauhoi);
                    answer_question(i, ma_loaicauhoi, ma_cauhoi);
                }
                SqlConnection con = new SqlConnection(DataProvider.Instance.conStr);
                SqlCommand cmd = new SqlCommand("sp_luucauhoi", con);
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tbl_cauhoi", dt);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
                cmd = new SqlCommand("sp_khaosat_cauhoi", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tbl_khaosat_cauhoi", dt_chks);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("ListSurvey.aspx?id="+ ma);
            }

        }
        private void answer_question(int i, string ma_loaicauhoi, string ma_cauhoi)
        {
            int count_answer = int.Parse(Request.Form["sl_answer" + i]);    
            DataTable dt_answer = new DataTable();
            dt_answer.Columns.Add(new DataColumn("tieude_dapan"));
            dt_answer.Columns.Add(new DataColumn("thutu_dapan")); 
            dt_answer.Columns.Add(new DataColumn("ma_cauhoi"));
            for (int j = 1; j <= count_answer; ++j)
            {
                if(Request.Form[ma_loaicauhoi + "_" + i + "_" + j] == null && ma_loaicauhoi != "doan")
                {
                    continue;
                }
                string tieude_dapan = Request.Form[ma_loaicauhoi + "_" + i + "_" + j];
                dt_answer.Rows.Add(tieude_dapan, j, ma_cauhoi);
            }
            SqlConnection con = new SqlConnection(DataProvider.Instance.conStr);
            SqlCommand cmd = new SqlCommand("sp_luudapan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tbl_dapan", dt_answer);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void get_soluong_mau()
        {
            /*string id_taikhoan = Session["login"].ToString();
            SqlConnection con = new SqlConnection(DataProvider.Instance.conStr);
            SqlCommand cmd = new SqlCommand("select COUNT(ma_khaosat) as soluong from tbl_maukhaosat where id_taikhoan = "+ id_taikhoan, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                sl_khaosat.InnerText = sdr["soluong"].ToString();
            }
            con.Close();*/
        }
    }
}