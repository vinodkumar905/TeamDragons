using System.Data.SqlClient;
using System.Data;

namespace proj.Models
{
    public class Recyclingsalesdb
    {

        SqlConnection con = new SqlConnection("Data Source=NW60426\\VINOD;Initial Catalog=RECYCLEMANAGEMENT;Integrated Security=True");

        //public string Saverecord(Recyclingsales rs)
        //{

        //    try
        //    {
        //        SqlCommand com = new SqlCommand("Sp_Recycling_Revenue", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("@Sales_date", rs.Sales_date);
        //        com.Parameters.AddWithValue("@Material_Category", rs.Material_Category);
        //        com.Parameters.AddWithValue("@Material_SubCategory", rs.MaterialSubCategory);
        //        com.Parameters.AddWithValue("@Material_Weight_lbs", rs.Material_Weight_lbs);
        //        com.Parameters.AddWithValue("@Revenue_in_USD", rs.Revenue_in_USD);
        //        com.Parameters.AddWithValue("@Buyer_Name", rs.Buyer_Name);
        //        con.Open();
        //        com.ExecuteNonQuery();
        //        con.Close();
        //        return ("Ok");

        //    }
        //    catch (Exception ex)
        //    {
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return (ex.Message.ToString());
        //    }


        //}


        public DataSet SalesGet(Recyclingsales rs, out string msg)
        {
            DataSet ds = new DataSet();
            msg = "";

            try
            {
                using (SqlCommand com = new SqlCommand("Sp_Recycling_Revenue", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Revenue_id", rs.Revenue_id);

                    // Handle Sales_date parameter
                    if (rs.Sales_date.HasValue) // Check if Sales_date has a value
                    {
                        if (rs.Sales_date < new DateTime(1753, 1, 1) || rs.Sales_date > new DateTime(9999, 12, 31))
                        {
                            msg = "Sales_date must be between 1/1/1753 and 12/31/9999.";
                            return ds; // Exit early if date is invalid
                        }
                        com.Parameters.AddWithValue("@Sales_date", rs.Sales_date.Value); // Use Value to extract DateTime
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@Sales_date", DBNull.Value); // Handle null case
                    }

                    com.Parameters.AddWithValue("@Material_Category", string.IsNullOrEmpty(rs.Material_Category) ? (object)DBNull.Value : rs.Material_Category);
                    com.Parameters.AddWithValue("@Material_SubCategory", string.IsNullOrEmpty(rs.Material_SubCategory) ? (object)DBNull.Value : rs.Material_SubCategory);
                    com.Parameters.AddWithValue("@Material_Weight_lbs", rs.Material_Weight_lbs);
                    com.Parameters.AddWithValue("@Revenue_in_USD", rs.Revenue_in_USD);
                    com.Parameters.AddWithValue("@Buyer_Name", string.IsNullOrEmpty(rs.Buyer_Name) ? (object)DBNull.Value : rs.Buyer_Name);
                    com.Parameters.AddWithValue("@flag", rs.flag);

                    // Execute and fill dataset
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(ds);
                    }

                    msg = "OK";
                }
            }
            catch (ArgumentOutOfRangeException aoex)
            {
                msg = aoex.Message; // Specific handling for argument out of range
            }
            catch (Exception ex)
            {
                msg = ex.Message; // General exception handling
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return ds;
        }


        public string SalesDML(Recyclingsales rs, out string msg)
        {
            msg = "";

            try
            {
                using (SqlCommand com = new SqlCommand("Sp_Recycling_Revenue", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Revenue_id", rs.Revenue_id);

                    // Handle Sales_date parameter
                    if (rs.Sales_date.HasValue) // Check if Sales_date has a value
                    {
                        if (rs.Sales_date < new DateTime(1753, 1, 1) || rs.Sales_date > new DateTime(9999, 12, 31))
                        {
                            msg = "Sales_date must be between 1/1/1753 and 12/31/9999.";
                            return msg; // Exit early if date is invalid
                        }
                        com.Parameters.AddWithValue("@Sales_date", rs.Sales_date.Value); // Use Value to extract DateTime
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@Sales_date", DBNull.Value); // Handle null case
                    }

                    // Add other parameters to command
                    com.Parameters.AddWithValue("@Material_Category", string.IsNullOrEmpty(rs.Material_Category) ? (object)DBNull.Value : rs.Material_Category);
                    com.Parameters.AddWithValue("@Material_SubCategory", string.IsNullOrEmpty(rs.Material_SubCategory) ? (object)DBNull.Value : rs.Material_SubCategory);
                    com.Parameters.AddWithValue("@Material_Weight_lbs", rs.Material_Weight_lbs);
                    com.Parameters.AddWithValue("@Revenue_in_USD", rs.Revenue_in_USD);
                    com.Parameters.AddWithValue("@Buyer_Name", string.IsNullOrEmpty(rs.Buyer_Name) ? (object)DBNull.Value : rs.Buyer_Name);
                    com.Parameters.AddWithValue("@flag", rs.flag);

                    // Open connection and execute command
                    con.Open();
                    com.ExecuteNonQuery();
                    msg = "OK";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return msg;
        }
    }
}
