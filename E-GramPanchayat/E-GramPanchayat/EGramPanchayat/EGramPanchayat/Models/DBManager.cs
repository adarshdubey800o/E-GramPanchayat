using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace EGramPanchayat.Models
{
    public class DBManager
    {
        protected string MyCommandText;
        SqlConnection con = new SqlConnection("data source = DESKTOP-PPLM9GB; initial catalog=Panchyat_DB;Integrated Security = true;");
        SqlCommand cmd = new SqlCommand();
        public bool IsInsertedUpdatedorDeleted(string MyCommandText)
        {
            cmd.CommandText = MyCommandText;
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            int status = cmd.ExecuteNonQuery();
            if (status > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable executeQuery(string MyCommandText)
        {
            SqlDataAdapter sq = new SqlDataAdapter(MyCommandText, con);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            return dt;
        }
    }
}