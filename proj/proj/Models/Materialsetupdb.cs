using proj.Controllers;
using System.Data.SqlClient;
using System.Data;

namespace proj.Models
{
    public class Materialsetupdb
    {
        SqlConnection con = new SqlConnection("Data Source=NW60426\\VINOD;Initial Catalog=RECYCLEMANAGEMENT;Integrated Security=True");

        // Method to fetch Category and Sub-Category data
        public DataSet GetMaterial(Materialsetup usr, out string msg)
        {
            DataSet ds = new DataSet();
            msg = "";
            try
            {
                SqlCommand com = new SqlCommand("Sp_Material", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Material_ID", usr.MaterialID);
                com.Parameters.AddWithValue("@Category", usr.Category);
                com.Parameters.AddWithValue("@Sub_Category", usr.Sub_Category);
                com.Parameters.AddWithValue("@flag", usr.flag);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds);
                msg = "OK";
                return ds;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return ds;
            }
        }

        // Method to insert/update MaterialSetup details (DML Operation)
        public string MaterialSetupDML(Materialsetup ms, out string msg)
        {
            msg = "";

            try
            {
                SqlCommand com = new SqlCommand("Sp_Material", con);
                com.CommandType = CommandType.StoredProcedure;

                // Add parameters
                com.Parameters.AddWithValue("@Material_ID", ms.MaterialID);
                com.Parameters.AddWithValue("@Category", ms.Category ?? (object)DBNull.Value);
                com.Parameters.AddWithValue("@Sub_Category", ms.Sub_Category ?? (object)DBNull.Value);
                com.Parameters.AddWithValue("@flag", ms.flag);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                msg = "ok";
                return msg;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                msg = ex.Message;
                return msg;
            }
        }
    }
}
