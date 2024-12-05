using Microsoft.AspNetCore.Mvc;
using proj.Models;

using System.Data;

namespace proj.Controllers
{
    public class LandFillController : Controller
    {
        //Landfilldb dbop = new Landfilldb();

        //public IActionResult CreateLandfill()
        //{
        //    return View("~/Views/Home/CreateLandfill.cshtml");
        //}


        //[HttpPost]
        //public IActionResult saverecords([Bind] LandFill lf)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            string res = dbop.Saverecord(lf);
        //            TempData["msg"] = res;
        //            return View("CreateLandfill", lf);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    return View("CreateLandfill", lf);

        //}


        public IActionResult CreateLandfill()
        {
            return View("~/Views/Home/CreateLandfill.cshtml");
        }


        [HttpPost]
        public IActionResult Create([Bind] LandFill lf)
        {
            try
            {
                lf.flag = "insert";
                dbop.landfilldml(lf, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("CreateLandfill");
        }

        Landfilldb dbop = new Landfilldb();
        string msg;
        public IActionResult ViewLandfill()
        {
            // You may need to set the 'Hauler' here if needed
            LandFill lf = new LandFill { LANDFILL_HAULER_NAME = "Default LANDFILL_HAULER_NAME" }; // Initialize required member

            lf.flag = "get";
            DataSet ds = dbop.landfillget(lf, out msg);
            List<LandFill> list = new List<LandFill>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new LandFill
                {
                    landfill_id = Convert.ToInt32(dr["landfill_id"]),
                    Landfill_Date = Convert.ToDateTime(dr["Landfill_Date"]),
                    weight_In_Lbs = Convert.ToInt32(dr["weight_In_Lbs"]),
                    Expense_In_Usd = Convert.ToInt32(dr["Expense_In_Usd"]),
                    LANDFILL_HAULER_NAME = dr["LANDFILL_HAULER_NAME"] != DBNull.Value ? dr["LANDFILL_HAULER_NAME"].ToString() : "Unknown" // Ensure Hauler is set
                });
            }

            return View("~/Views/Home/ViewLandfill.cshtml", list);
        }



        public IActionResult EditLandfill(int id)
        {
            LandFill lf = new LandFill();
            lf.landfill_id = id;
            lf.flag = "getid";
            DataSet ds = dbop.landfillget(lf, out msg);

            // Check if the DataSet has any tables and rows
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return NotFound($"No landfill record found with ID {id}.");
            }

            // Mapping the retrieved data to the LandFill object
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lf.landfill_id = Convert.ToInt32(dr["landfill_id"]);
                lf.Landfill_Date = Convert.ToDateTime(dr["Landfill_Date"]);
                lf.weight_In_Lbs = Convert.ToInt32(dr["weight_In_Lbs"]);
                lf.Expense_In_Usd = Convert.ToInt32(dr["Expense_In_Usd"]);
                lf.LANDFILL_HAULER_NAME = dr["LANDFILL_HAULER_NAME"] != DBNull.Value ? dr["LANDFILL_HAULER_NAME"].ToString() : "Unknown"; // Ensure Hauler is set
            }

            // Ensure that LANDFILL_HAULER_NAME is set before calling the stored procedure
            if (string.IsNullOrEmpty(lf.LANDFILL_HAULER_NAME))
            {
                lf.LANDFILL_HAULER_NAME = "Unknown"; // Default value if not provided
            }

            // Here you would typically call a save or update method that uses the stored procedure.
            // Example:
            // dbop.UpdateLandfill(lf); // Ensure this method sends all required parameters to the stored procedure

            return View("~/Views/Home/EditLandfill.cshtml", lf);
        }

        [HttpPost]
        public IActionResult EditLandfill(int id, [Bind] LandFill lf)
        {
            try
            {
                lf.landfill_id = id;
                lf.flag = "update";
                dbop.landfilldml(lf, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ViewLandfill");
        }

        public IActionResult DeleteLandfill(int id)
        {
            try
            {
                LandFill lf = new LandFill();
                lf.flag = "delete";
                lf.landfill_id = id;
                dbop.landfilldml(lf, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ViewLandfill");
        }


    }
}
