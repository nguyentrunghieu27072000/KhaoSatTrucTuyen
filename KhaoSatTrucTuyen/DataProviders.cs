using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KhaoSatTrucTuyen
{
    public class DataProvider
    {
        #region Parameters
        private static DataProvider instance;
        public string conStr = @"Data Source=DESKTOP-IPHDBTQ\SQLEXPRESS;Initial Catalog=QuanLyKhaoSatTrucTiep;Integrated Security=True";
        //private string conStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        #endregion
        #region Initializations
        internal static DataProvider Instance { get => instance != null ? instance : new DataProvider(); private set => instance = value; }
        private DataProvider() { }
        #endregion
        #region Methods
        public DataTable ExecuteQuery(string sp, List<KeyValuePair<string, string>> lst = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (KeyValuePair<string, string> item in lst)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    return dt;
                }
            }
        }
        public bool ExecuteQueryNoReturn(string sp, List<KeyValuePair<string, string>> lst = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (KeyValuePair<string, string> item in lst)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable GetView(string viewName, string modifyName = null)
        {
            string sql = modifyName != null ? modifyName + viewName : "select * from " + viewName;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        #endregion

    }
}