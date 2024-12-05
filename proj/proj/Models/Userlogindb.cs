using System.Data.SqlClient;
using System.Data;

namespace proj.Models
{
    public class Userlogindb
    {
        SqlConnection con = new SqlConnection("Data Source=NW60426\\VINOD;Initial Catalog=RECYCLEMANAGEMENT;Integrated Security=True");
        
        public int LoginCheck(UserLogin ad)
        {
            try
            {
                using (SqlCommand com = new SqlCommand("Sp_Login", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@UserID", ad.UserID);
                    com.Parameters.AddWithValue("@Password", ad.password);
                    com.Parameters.AddWithValue("@LOGINTYPE", 'A');

                    SqlParameter oblogin = new SqlParameter
                    {
                        ParameterName = "@Isvalid",
                        SqlDbType = SqlDbType.Bit,
                        Direction = ParameterDirection.Output
                    };
                    com.Parameters.Add(oblogin);

                    con.Open();
                    com.ExecuteNonQuery();
                    int res = Convert.ToInt32(oblogin.Value);
                    return res;
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine($"SQL Error: {ex.Message}");
                return -1; // Or some other error code
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return -1; // Or some other error code
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close(); // Ensure the connection is closed
                }
            }
        }
        }
    }
