using System.Data.SqlClient;
using System.Data;

namespace proj.Models
{
    public class UserSetupdb
    {
        SqlConnection con = new SqlConnection("Data Source=NW60426\\VINOD;Initial Catalog=RECYCLEMANAGEMENT;Integrated Security=True");

        // Method to Save a User record
        //public string SaveRecord(UserSetup us, out string msg)
        //{
        //    msg = "";
        //    try
        //    {
        //        using (SqlCommand com = new SqlCommand("User_Setup", con))
        //        {
        //            com.CommandType = CommandType.StoredProcedure;
        //            com.Parameters.AddWithValue("@UserName", us.UserName);
        //            com.Parameters.AddWithValue("@Password", us.Password);
        //            com.Parameters.AddWithValue("@LoginType", us.LoginType);
        //            com.Parameters.AddWithValue("@Active", us.Active);

        //            con.Open();
        //            com.ExecuteNonQuery();
        //            con.Close();

        //            msg = "Ok";
        //            return msg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return msg;
        //    }
        //}

        public DataSet Userget(UserSetup usr, out string msg)
        {
            DataSet ds = new DataSet();
            msg = "";
            try
            {
                SqlCommand com = new SqlCommand("User_Setup", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@User_Id", usr.User_Id);
                com.Parameters.AddWithValue("@UserName", usr.UserName);
                com.Parameters.AddWithValue("@Password", usr.Password);
                com.Parameters.AddWithValue("@LoginType", usr.LoginType);
                com.Parameters.AddWithValue("@Active", usr.Active);
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

        //For insert and update
        public string Userdml(UserSetup usr, out string msg)
        {
            msg = "";
            try
            {
                SqlCommand com = new SqlCommand("User_Setup", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@User_Id", usr.User_Id);
                com.Parameters.AddWithValue("@UserName", usr.UserName);
                com.Parameters.AddWithValue("@Password", usr.Password);
                com.Parameters.AddWithValue("@LoginType", usr.LoginType);
                com.Parameters.AddWithValue("@Active", usr.Active);
                com.Parameters.AddWithValue("@flag", usr.flag);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                msg = "OK";
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
