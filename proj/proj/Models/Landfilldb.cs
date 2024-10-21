using System.Data.SqlClient;
using System.Data;
using Microsoft.VisualBasic;

namespace proj.Models
{
    public class Landfilldb
    {

        SqlConnection con = new SqlConnection("Data Source=NW60426\\VINOD;Initial Catalog=RECYCLEMANAGEMENT;Integrated Security=True");

        public DataSet landfillget(LandFill lf, out string msg)
        {
            DataSet ds = new DataSet();
            msg = "";

            try
            {
                using (SqlCommand com = new SqlCommand("Sp_Landfill", con))
                {
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.AddWithValue("@landfill_id", lf.landfill_id);

                    // Check for valid LandFillDate
                    if (lf.Landfill_Date.HasValue)
                    {
                        if (lf.Landfill_Date < new DateTime(1753, 1, 1) || lf.Landfill_Date > new DateTime(9999, 12, 31))
                        {
                            msg = "Landfill_Date must be between 1/1/1753 and 12/31/9999.";
                            return ds; // Exit early if date is invalid
                        }
                        com.Parameters.AddWithValue("@Landfill_Date", lf.Landfill_Date.Value); // Use Value to extract DateTime
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@Landfill_Date", DBNull.Value); // Handle null case
                    }

                    // Add other parameters
                    com.Parameters.AddWithValue("@WEIGHT_IN_LBS", lf.weight_In_Lbs);
                    com.Parameters.AddWithValue("@EXPENSE_IN_USD", lf.Expense_In_Usd);

                    // Check if LANDFILL_HAULER_NAME is set, if not set to DBNull
                    if (string.IsNullOrEmpty(lf.LANDFILL_HAULER_NAME))
                    {
                        com.Parameters.AddWithValue("@LANDFILL_HAULER_NAME", DBNull.Value); // Handle null case
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@LANDFILL_HAULER_NAME", lf.LANDFILL_HAULER_NAME);
                    }

                    com.Parameters.AddWithValue("@flag", lf.flag);

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(ds);

                    // Check if the DataSet contains any tables before trying to access them
                    if (ds.Tables.Count == 0)
                    {
                        msg = "No data returned from the database.";
                        return ds; // Return early if no tables were added
                    }

                    msg = "ok";
                    return ds;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return ds; // Return empty dataset on exception
            }
        }




        public string landfilldml(LandFill lf, out string msg)
        {
            msg = "";
            try
            {
                SqlCommand com = new SqlCommand("Sp_Landfill", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@landfill_id", lf.landfill_id);
                com.Parameters.Add("@Landfill_Date", SqlDbType.DateTime).Value = (object)lf.Landfill_Date ?? DBNull.Value;
                com.Parameters.AddWithValue("@WEIGHT_IN_LBS", lf.weight_In_Lbs);
                com.Parameters.AddWithValue("@EXPENSE_IN_USD", lf.Expense_In_Usd);
                com.Parameters.AddWithValue("@LANDFILL_HAULER_NAME", lf.LANDFILL_HAULER_NAME ?? (object)DBNull.Value);
                com.Parameters.AddWithValue("@flag", lf.flag);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                msg = "ok";
                return msg;
            }
            catch (Exception ex)
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                msg = ex.Message;
                return msg;
            }
        }
    }
}
