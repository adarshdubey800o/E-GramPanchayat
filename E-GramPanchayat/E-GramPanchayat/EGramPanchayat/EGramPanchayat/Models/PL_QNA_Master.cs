using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace EGramPanchayat.Models
{
    public class PL_QNA_Master /*: DataConnection*/
    {
        //SqlConnection conn;
        //public PL_QNA_Master() 
        //{
        //   conn=new SqlConnection("Data Source=HP_ATULPANDEY/SQLEXPRESS;Initial Catalog=  ;Integrated Security=true;");
        //}
        public int AutoId { get; set; }
        public string URL { set; get; }
        public string Question { set; get; }
        public string Answer { set; get; }
        public string IsActive { get; set; }
        public string CreatedBy { set; get; }
        public string exceptionMessage { get; set; }
        public bool isException { get; set; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public int RecordCount { set; get; }

        public int Opcode { get; set; }
        public DataSet Ds;

    }
    public class DL_QNA_Master
    {
        public static void returntable(PL_QNA_Master pobj)
        {
            try
            {
                //SqlConnection con = DataConnection.GetConnectionML_Website();
                SqlConnection con = new SqlConnection("Data Source=HP_ATULPANDEY\\SQLEXPRESS;Initial Catalog= Panchyat_DB ;Integrated Security=true");
                SqlCommand cmd = new SqlCommand("SP_ContantForQNA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcode", pobj.Opcode);
                cmd.Parameters.AddWithValue("@AutoId", pobj.AutoId);
                cmd.Parameters.AddWithValue("@URL", pobj.URL);
                cmd.Parameters.AddWithValue("@Question", pobj.Question);
                cmd.Parameters.AddWithValue("@Answer", pobj.Answer);
                cmd.Parameters.AddWithValue("@IsActive", pobj.IsActive);
                //cmd.Parameters.AddWithValue("@CreatedBy", pobj.CreatedBy);
                cmd.Parameters.AddWithValue("@PageIndex", pobj.PageIndex);
                cmd.Parameters.AddWithValue("@PageSize", pobj.PageSize);
                cmd.Parameters.AddWithValue("@RecordCount", pobj.RecordCount);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                pobj.Ds = new DataSet();
                da.Fill(pobj.Ds);
                pobj.isException = false;

            }
            catch (Exception ex)
            {
                pobj.isException = true;
                pobj.exceptionMessage = ex.Message;
            }
        }
    }
    public class BL_QNA_Master
    {
        public static void save(PL_QNA_Master pobj)
        {
            pobj.Opcode = 11;
            DL_QNA_Master.returntable(pobj);
        }
        public static void Show(PL_QNA_Master pobj)
        {
            pobj.Opcode = 12;
            DL_QNA_Master.returntable(pobj);
        }
        public static void removeQNA(PL_QNA_Master pobj)
        {
            pobj.Opcode = 13;
            DL_QNA_Master.returntable(pobj);
        }
        public static void editQNA(PL_QNA_Master pobj)
        {
            pobj.Opcode = 14;
            DL_QNA_Master.returntable(pobj);
        }
        public static void UpdateQNA(PL_QNA_Master pobj)
        {
            pobj.Opcode = 15;
            DL_QNA_Master.returntable(pobj);
        }

    }
}