using Microsoft.AspNetCore.Mvc;
using proj.Models;
using System.Data;

namespace proj.Controllers
{
    public class MaterialSetupController : Controller
    {
  
            
            public IActionResult CreateMaterialsetup()
            {
                return View("~/Views/Home/CreateMaterialsetup.cshtml");
            }



            Materialsetupdb dbop = new Materialsetupdb();
            string msg;
            public IActionResult ViewMaterialsetup()
            {
                Materialsetup usr = new Materialsetup();
                usr.flag = "get";
                DataSet ds = dbop.GetMaterial(usr, out msg);
                List<Materialsetup> list = new List<Materialsetup>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(new Materialsetup
                    {
                        
                        Category = dr["Category"].ToString(),
                        Sub_Category = dr["Sub_Category"].ToString(),
                     
                    });
                }

                return View("~/Views/Home/ViewMaterialsetup.cshtml", list);
            }


            [HttpPost]
            public IActionResult Create([Bind] Materialsetup usr)
            {
                try
                {
                    usr.flag = "insert";
                    dbop.MaterialSetupDML(usr, out msg);
                    TempData["msg"] = msg;
                }
                catch (Exception ex)
                {
                    TempData["msg"] = ex.Message;
                }
                return RedirectToAction("CreateMaterialsetup");
            }


            public IActionResult EditMaterialsetup(int id)
            {
                Materialsetup usr = new Materialsetup();
                
                usr.flag = "getid";
                DataSet ds = dbop.GetMaterial(usr, out msg);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    usr.Category = dr["Category"].ToString();
                    usr.Sub_Category = dr["Sub_Category"].ToString();
                   


                }
                return View("~/Views/Home/Edituser.cshtml", usr);
            }
            [HttpPost]
            public IActionResult Edituser(int id, [Bind] Materialsetup usr)
            {
                try
                {
                 
                    usr.flag = "update";
                    dbop.MaterialSetupDML(usr, out msg);
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
                    Materialsetup usr = new Materialsetup();
                    usr.flag = "delete";
                    
                    dbop.MaterialSetupDML(usr, out msg);
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
