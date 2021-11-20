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
    public partial class DoSurvey : System.Web.UI.Page
    {
        public DataTable dt_list_phieu = new DataTable();
        public DataTable ds_cauhoi = new DataTable();
        public DataTable ds_dapan = new DataTable();
        public Dictionary<string, Array> chitiet_hopkiem = new Dictionary<string, Array>();
        public Dictionary<string, string> chitiet_conlai = new Dictionary<string, string>();
        public List<string> termsList = new List<string>();
        public string ma_khaosat = "";
        public string title_survey = "";
        public string status_survey = "";
        public string ma_phieu_khaosat = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string id_taikhoan = Session["login"].ToString();
            get_phieu_khaosat(id_taikhoan);
            if (Request.QueryString["id"] != null)
            {
                ma_phieu_khaosat = Request.QueryString["id"];
                foreach(DataRow dr in dt_list_phieu.Rows)
                {
                    if(dr["ma_phieukhaosat"].ToString() == ma_phieu_khaosat)
                    {
                        ma_khaosat = dr["ma_khaosat"].ToString();
                        title_survey = dr["ten_khaosat"].ToString();
                        status_survey = dr["trangthai"].ToString();
                    }
                }
                get_info_phieu(ma_phieu_khaosat, id_taikhoan);
                get_chitiet_phieu(ma_phieu_khaosat);
            }
            if(Request.Form["send"] == "send-survey-test")
            {
                DataTable dt_add_dapan = new DataTable();
                dt_add_dapan.Columns.Add(new DataColumn("ma_phieukhaosat"));
                dt_add_dapan.Columns.Add(new DataColumn("ma_cauhoi"));
                dt_add_dapan.Columns.Add(new DataColumn("ma_dapan"));
                dt_add_dapan.Columns.Add(new DataColumn("noidung_dapan"));
                foreach (DataRow dr_ch in ds_cauhoi.Rows)
                {
                    string dapan = Request.Form[dr_ch["ma_cauhoi"]+""].ToString();
                    switch(dr_ch["ma_loaicauhoi"])
                    {
                        case "doan":
                            string noidung_dapan = Request.Form[dapan + ""];
                            dt_add_dapan.Rows.Add(ma_phieu_khaosat, dr_ch["ma_cauhoi"], dapan, noidung_dapan);
                            break;
                        case "hopkiem":
                            foreach (string Item in dapan.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                dt_add_dapan.Rows.Add(ma_phieu_khaosat, dr_ch["ma_cauhoi"], Item, "");
                            }
                            break;
                        default:
                            dt_add_dapan.Rows.Add(ma_phieu_khaosat, dr_ch["ma_cauhoi"], dapan, "");
                            break;
                    }
                }
                SqlConnection con = new SqlConnection(DataProvider.Instance.conStr);
                SqlCommand cmd = new SqlCommand("sp_chitiet_phieu", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ma_phieukhaosat", ma_phieu_khaosat);
                cmd.Parameters.AddWithValue("@tbl_phieuchitiet", dt_add_dapan);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("DoSurvey.aspx?id="+ma_phieu_khaosat );
            }
        }
        public void get_phieu_khaosat(string id_taikhoan)
        {
            var lstParameter = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string,string>("@id_taikhoan",id_taikhoan),
            };
            DataTable dt = DataProvider.Instance.ExecuteQuery("sp_get_list_phieukhaosat", lstParameter);
            dt_list_phieu = dt;
        }
        public void get_info_phieu(string ma_phieu_khaosat,string id_taikhoan)
        {
            ListSurvey LS = new ListSurvey();
            ds_cauhoi = LS.GetQuestionServey(ma_khaosat);
            ds_dapan = LS.GetAnswerQuestion(ds_cauhoi);
        }
        public void get_chitiet_phieu(string ma_phieu_khaosat)
        {
            var lstParameter = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string,string>("@ma_phieukhaosat",ma_phieu_khaosat),
            };
            DataTable dt = DataProvider.Instance.ExecuteQuery("sp_get_chitiet_phieu", lstParameter);
            
            foreach (DataRow dr in dt.Rows)
            {
                string ma_cauhoi = dr["ma_cauhoi"].ToString();
                switch (dr["ma_loaicauhoi"])
                {
                    case "doan":
                        chitiet_conlai[ma_cauhoi] = dr["noidung_dapan"].ToString();
                        break;
                    case "hopkiem":
                        termsList.Add(dr["ma_dapan"].ToString());
                        break;
                    default:
                        chitiet_conlai[ma_cauhoi] = dr["ma_dapan"].ToString();
                        break;
                }
            }
        }
    }
}