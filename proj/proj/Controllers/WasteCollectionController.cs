using Microsoft.AspNetCore.Mvc;
using proj.Models;
using System.Data;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using proj.Models.Models;


namespace proj.Controllers
{
    public class WasteCollectionController : Controller
    {
        

        public IActionResult CreateWasteCollection()
        {
            return View("~/Views/Home/CreateWasteCollection.cshtml"); // Return the waste collection form view
        }


        //[HttpPost]
        //public IActionResult save([Bind] WasteCollection ws)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            string res = dbop.Saverecord(ws);
        //            TempData["msg"] = res;
        //            return View("Createwastecollection", ws);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    return View("Createwastecollection", ws);

        //}


        Wastecollectiondb dbop = new Wastecollectiondb();
        string msg;
        public IActionResult ViewCollection()
        {
            WasteCollection ws = new WasteCollection();
            ws.flag = "get";
            DataSet ds = dbop.WasteCollectionGet(ws, out msg);
            List<WasteCollection> list = new List<WasteCollection>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new WasteCollection
                {
                Collection_id = Convert.ToInt32(dr["Collection_id"]),
                EntryDate = Convert.ToDateTime(dr["Collection_Date"]), 
                Category = dr["Collection_Category"].ToString(),
                Subcategory = dr["Collection_SubCategory"].ToString(),
                WeightInLbs = Convert.ToInt32(dr["Weight_in_lbs"]),
                    IsRecyclable = dr["IsRecycled"].ToString() == "1", // 
                });
            }

            return View("~/Views/Home/ViewCollection.cshtml", list);
        }


        [HttpPost]
        public IActionResult Create([Bind] WasteCollection ws)
        {
            try
            {
                ws.flag = "insert";
                dbop.WasteCollectionDML(ws, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("Createwastecollection");
        }


        public IActionResult EditCollection(int id)
        {
            WasteCollection ws = new WasteCollection();
            ws.Collection_id = id;
            ws.flag = "getid";
            DataSet ds = dbop.WasteCollectionGet(ws, out msg);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ws.Collection_id = Convert.ToInt32(dr["Collection_id"]);
                ws.EntryDate = Convert.ToDateTime(dr["Collection_Date"]);
                ws.Category = dr["Collection_Category"].ToString();
                ws.Subcategory = dr["Collection_SubCategory"].ToString();
                ws.WeightInLbs = Convert.ToInt32(dr["Weight_in_lbs"]);
                ws.IsRecyclable = dr["IsRecycled"].ToString() == "1";


            }
            return View("~/Views/Home/EditCollection.cshtml", ws);
        }
        [HttpPost]
        public IActionResult EditCollection(int id, [Bind] WasteCollection ws)
        {
            try
            {
                ws.Collection_id = id;
                ws.flag = "update";
                dbop.WasteCollectionDML(ws, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ViewCollection");
        }

        public IActionResult DeleteCollection(int id)
        {
            try
            {
                WasteCollection ws = new WasteCollection();
                ws.flag = "delete";
                ws.Collection_id = id;
                dbop.WasteCollectionDML(ws, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ViewCollection");
        }


    }
}
