using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using proj.Models;



namespace proj.Models
{
    public class Wastecollectiondb
    {
        //SqlConnection con = new  SqlConnection("Data Source=NW60426\\VINOD;Initial Catalog=RECYCLEMANAGEMENT;Integrated Security=True");

        //public string Saverecord(WasteCollection ws)
        //{

        //    try
        //    {
        //        SqlCommand com = new SqlCommand("Sp_WasteCollection", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("@Collection_Date", ws.EntryDate);
        //        com.Parameters.AddWithValue("@Collection_Category", ws.Category);
        //        com.Parameters.AddWithValue("@Collection_SubCategory", ws.Subcategory);
        //        com.Parameters.AddWithValue("@Weight", ws.WeightInLbs);
        //        com.Parameters.AddWithValue("@IsRecycled", ws.IsRecyclable);
        //        con.Open();
        //        com.ExecuteNonQuery();
        //        con.Close();
        //        return ("Ok");

        //    }
        //    catch(Exception ex) 
        //    {
        //        if(con.State== ConnectionState.Open)
        //        {
        //            con.Close();        
        //        }
        //        return (ex.Message.ToString());
        //    }






        //}

        SqlConnection con = new SqlConnection("Data Source=NW60426\\VINOD;Initial Catalog=RECYCLEMANAGEMENT;Integrated Security=True");

        // Method to get Waste Collection records
        public DataSet WasteCollectionGet(WasteCollection ws, out string msg)
        {
            DataSet ds = new DataSet();
            msg = "";

            try
            {
                using (SqlCommand com = new SqlCommand("Sp_WasteCollection", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Collection_id", ws.Collection_id);

                    if (ws.EntryDate.HasValue)
                    {
                        if (ws.EntryDate < new DateTime(1753, 1, 1) || ws.EntryDate > new DateTime(9999, 12, 31))
                        {
                            msg = "entry_Date must be between 1/1/1753 and 12/31/9999.";
                            return ds; // Exit early if date is invalid
                        }
                        com.Parameters.AddWithValue("@Collection_Date", ws.EntryDate.Value); // Use Value to extract DateTime
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@Collection_Date", DBNull.Value); // Handle null case
                    }

                    
                    com.Parameters.AddWithValue("@Collection_Category", string.IsNullOrEmpty(ws.Category) ? (object)DBNull.Value : ws.Category);
                    com.Parameters.AddWithValue("@Collection_SubCategory", string.IsNullOrEmpty(ws.Subcategory) ? (object)DBNull.Value : ws.Subcategory);
                    com.Parameters.AddWithValue("@Weight_in_lbs", ws.WeightInLbs);
                    com.Parameters.AddWithValue("@IsRecycled", ws.IsRecyclable);
                    com.Parameters.AddWithValue("@flag", ws.flag);

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


        // Method for insert and update operations
        public string WasteCollectionDML(WasteCollection ws, out string msg)
        {
            msg = "";

            try
            {
                using (SqlCommand com = new SqlCommand("Sp_WasteCollection", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Collection_id", ws.Collection_id);

                    if (ws.EntryDate.HasValue)
                    {
                        if (ws.EntryDate < new DateTime(1753, 1, 1) || ws.EntryDate > new DateTime(9999, 12, 31))
                        {
                            msg = "entry_Date must be between 1/1/1753 and 12/31/9999.";
                            return msg; // Exit early if date is invalid
                        }
                        com.Parameters.AddWithValue("@Collection_Date", ws.EntryDate.Value); // Use Value to extract DateTime
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@Collection_Date", DBNull.Value); // Handle null case
                    }

                    // Add other parameters to command
                    com.Parameters.AddWithValue("@Collection_Category", string.IsNullOrEmpty(ws.Category) ? (object)DBNull.Value : ws.Category);
                    com.Parameters.AddWithValue("@Collection_SubCategory", string.IsNullOrEmpty(ws.Subcategory) ? (object)DBNull.Value : ws.Subcategory);
                    com.Parameters.AddWithValue("@Weight_in_lbs", ws.WeightInLbs);
                    com.Parameters.AddWithValue("@IsRecycled", ws.IsRecyclable);
                    com.Parameters.AddWithValue("@flag", ws.flag);

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
