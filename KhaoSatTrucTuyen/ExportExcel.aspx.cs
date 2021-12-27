using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;

namespace KhaoSatTrucTuyen
{
    public partial class ExportExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] != null)
            { 
                EpExcel();
            }
        }
        public void EpExcel()
        {
            // lấy mã khảo sát -- thay hiddenfield1 bằng mã
            string ma_khaosat = Request.QueryString["id"];
            var lstParameter = new List<KeyValuePair<string, string>>
                {new KeyValuePair<string,string>("@ma_khaosat",ma_khaosat),
                };
            // dt chứa danh sách câu hỏi theo mã
            DataTable dt = DataProvider.Instance.ExecuteQuery("sp_get_cauhoi_trongmau", lstParameter);

            var lstParameter2 = new List<KeyValuePair<string, string>>
                {new KeyValuePair<string,string>("@ma_khaosat",ma_khaosat),
                };
            // User_dt có id_taikhoan,ten_taikhoan
            // user_dt chứa user làm phiếu khảo sát(đã làm)
            DataTable User_dt = DataProvider.Instance.ExecuteQuery("sp_get_user_datraloi", lstParameter2);
            if (dt.Rows.Count == 0)
            {
                // mẫu khảo sát k có câu hỏi nào 
                // test
                Response.Write(ma_khaosat);
            }
            else
            {
                /*Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];*/
                


                Excel.Application excel = new Excel.Application();
                Excel.Workbook workbook = excel.Workbooks.Add(1);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)workbook.Worksheets[1];

                xlWorkSheet.Cells[1][1] = "Tài khoản";
                int j = 2;
                foreach (DataRow dr in dt.Rows)
                {
                    // dong` + cot
                    xlWorkSheet.Cells[j][1] = dr["noidung_cauhoi"].ToString();
                    int a = 2;
                    foreach (DataRow user_dr in User_dt.Rows)
                    {
                        var lstParameter3 = new List<KeyValuePair<string, string>>
                            {new KeyValuePair<string,string>("@ma_cauhoi",dr["ma_cauhoi"].ToString()),
                                new KeyValuePair<string,string>("@id_taikhoan",user_dr["id_taikhoan"].ToString()),
                            };
                        // tabledapan có ma_dapan,tieude_dapan
                        // lấy câu trả lời cho từng đáp án
                        DataTable tabledapan = DataProvider.Instance.ExecuteQuery("sp_get_dapan_datraloi", lstParameter3);
                        if (tabledapan.Rows.Count == 1)
                        {
                            // trường hợp 1 đáp án có 1 câu trả lời
                            if (tabledapan.Rows[0]["tieude_dapan"].ToString().Length == 0)
                            {
                                // đáp án null do loại đáp án là đoạn
                                var lstParameter4 = new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string,string>("@ma_dapan",tabledapan.Rows[0]["ma_dapan"].ToString()),
                                new KeyValuePair<string,string>("@ma_cauhoi",dr["ma_cauhoi"].ToString()),
                                new KeyValuePair<string,string>("@id_taikhoan",user_dr["id_taikhoan"].ToString()),
                            };
                                // tabledapan có ma_dapan,tieude_dapan
                                DataTable tabledoan = DataProvider.Instance.ExecuteQuery("sp_get_noidung_doan", lstParameter4);
                                xlWorkSheet.Cells[j][a] = tabledoan.Rows[0]["noidung_dapan"].ToString();
                            }
                            else
                            {
                                // lấy luôn nội dung đáp án đối với n~ đáp án k phải đoạn
                                xlWorkSheet.Cells[j][a] = tabledapan.Rows[0]["tieude_dapan"].ToString();
                            }

                        }
                        else
                        {
                            // 1 đáp án có nhiều câu trả lời
                            string dapan = "";
                            foreach (DataRow dapan_row in tabledapan.Rows)
                            {
                                dapan = dapan + dapan_row["tieude_dapan"].ToString() + ";";
                            }
                            if (dapan.Length == 0)
                                dapan = "";
                            else
                                dapan = dapan.Remove(dapan.Length - 1, 1);
                            xlWorkSheet.Cells[j][a] = dapan;
                        }
                        xlWorkSheet.Cells[1][a] = user_dr["ten_taikhoan"].ToString();
                        a++;
                    }
                    j++;
                }
                /*   wb.SaveAs("ABCD.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, Type.Missing);
                   wb.Close(false, Type.Missing, Type.Missing);*/
                /*string filename = ((Int32)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString() + ".xls";
                excel.Workbooks.Add("D:\exportexcel\a.xls");
                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);*/
                excel.Quit();
                // System.Diagnostics.Process.Start("ABCD.xls");


            }

        }
    }
}