using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KhaoSatTrucTuyen
{
    public partial class ketqua : System.Web.UI.Page
    {
        public DataTable UserSurvey = new DataTable();
        public DataTable Cauhoi = new DataTable();
        public DataTable DapAn = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] != null)
            {
                string ma_khaosat = Request.QueryString["id"];
                PreviewSurvey(ma_khaosat);
            }
        }

        private void PreviewSurvey(string ma_khaosat)
        {
            var lstParameter = new List<KeyValuePair<string, string>>
                {new KeyValuePair<string,string>("@ma_khaosat",ma_khaosat),
                };
            // dt chứa danh sách câu hỏi theo mã
            DataTable dt = DataProvider.Instance.ExecuteQuery("sp_get_cauhoi_trongmau", lstParameter);
            Cauhoi = dt;

            var lstParameter2 = new List<KeyValuePair<string, string>>
                {new KeyValuePair<string,string>("@ma_khaosat",ma_khaosat),
            };
            DataTable User_dt = DataProvider.Instance.ExecuteQuery("sp_get_user_datraloi", lstParameter2);
            UserSurvey = User_dt;

            var lstParameter3 = new List<KeyValuePair<string, string>>
                {new KeyValuePair<string,string>("@ma_khaosat",ma_khaosat),
            };
            DataTable dapan_dt = DataProvider.Instance.ExecuteQuery("get_dapan_all_survey", lstParameter2);
            DapAn = dapan_dt;
        }
    }
}