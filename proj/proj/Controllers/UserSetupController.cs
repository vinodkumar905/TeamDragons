using Microsoft.AspNetCore.Mvc;

using proj.Models;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;

namespace proj.Controllers
{
    public class UserSetupController : Controller
    {
        

        // GET: Create User
        public IActionResult CreateUser()
        {
            return View("~/Views/Home/CreateUser.cshtml");
        }

        //// POST: Save Records
        //[HttpPost]
        //public IActionResult SaveRecords([Bind] UserSetup userSetup)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string result = dbop.SaveRecord(userSetup);
        //        TempData["msg"] = result;
        //        return RedirectToAction("Index");  // Redirect to the Index after saving
        //    }

        //    // If model validation fails, return the same view with the user data
        //    return View("CreateUser", userSetup);
        //}


        UserSetupdb dbop = new UserSetupdb();
        string msg;
        public IActionResult ViewUser()
        {
            UserSetup usr = new UserSetup();
            usr.flag = "get";
            DataSet ds = dbop.Userget(usr, out msg);
            List<UserSetup> list = new List<UserSetup>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new UserSetup
                {
                    User_Id = Convert.ToInt32(dr["User_Id"]),
                    UserName = dr["UserName"].ToString(),
                    Password = dr["Password"].ToString(),
                    LoginType = dr["LoginType"].ToString(),
                    // Trim the value and check if it's "1" (true) or "0" (false)
                    Active = dr["Active"].ToString().Trim() == "1"
                });
            }

            return View("~/Views/Home/ViewUser.cshtml",list);
        }

   
        [HttpPost]
        public IActionResult Create([Bind] UserSetup usr)
        {
            try
            {
                usr.flag = "insert";
                dbop.Userdml(usr, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("CreateUser");
        }


        public IActionResult Edituser(int id)
        {
            UserSetup usr = new UserSetup();
            usr.User_Id = id;
            usr.flag = "getid";
            DataSet ds = dbop.Userget(usr, out msg);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
               usr.User_Id = Convert.ToInt32(dr["User_Id"]);
               usr.UserName = dr["UserName"].ToString();
               usr.Password = dr["Password"].ToString();
               usr.LoginType = dr["LoginType"].ToString();
                usr.Active = dr["Active"].ToString().Trim() == "1";


            }
            return View("~/Views/Home/Edituser.cshtml",usr);
        }
        [HttpPost]
        public IActionResult Edituser(int id, [Bind] UserSetup usr)
        {
            try
            {
                usr.User_Id = id;
                usr.flag = "update";
                dbop.Userdml(usr, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ViewUser");
        }

        public IActionResult DeleteUser(int id)
        {
            try
            {
                UserSetup usr = new UserSetup();
                usr.flag = "delete";
                usr.User_Id = id;
                dbop.Userdml(usr, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ViewUser");
        }
    }
}
