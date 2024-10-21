using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using proj.Models;
using System.Data;

namespace proj.Controllers
{
    public class RecyclingsalesController : Controller
    {
        Recyclingsalesdb dbop = new Recyclingsalesdb();

        public IActionResult CreateSales()
        {
            return View("~/Views/Home/CreateSales.cshtml"); // Return the waste collection form view
        }


        //[HttpPost]
        //public IActionResult save([Bind] Recyclingsales rs)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            string res = dbop.Saverecord(rs);
        //            TempData["msg"] = res;
        //            return View("CreateSales", rs);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    return View("CreateSales", rs);

        //}

        string msg;


        [HttpPost]
        public IActionResult Create([Bind] Recyclingsales rs)
        {
            try
            {
                rs.flag = "insert";
                dbop.SalesDML(rs, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("CreateSales");
        }


        public IActionResult ViewSales()
        {
            Recyclingsales rs = new Recyclingsales();
            rs.flag = "get";
            DataSet ds = dbop.SalesGet(rs, out msg);
            List<Recyclingsales> list = new List<Recyclingsales>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new Recyclingsales
                {
                    Revenue_id = Convert.ToInt32(dr["Revenue_id"]),
                    Sales_date = dr["Sales_date"] as DateTime?, // Nullable DateTime
                    Material_Category = dr["Material_Category"].ToString(),
                    Material_SubCategory = dr["Material_SubCategory"].ToString(),
                    Material_Weight_lbs = Convert.ToInt32(dr["Material_Weight_lbs"]),
                    Revenue_in_USD = Convert.ToDecimal(dr["Revenue_in_USD"]),
                    Buyer_Name = dr["Buyer_Name"].ToString(),
                });
            }
            return View("~/Views/Home/ViewSales.cshtml", list);
        }


        public IActionResult EditSales(int id)
        {
            Recyclingsales rs = new Recyclingsales();
            rs.Revenue_id = id;
            rs.flag = "getid";
            DataSet ds = dbop.SalesGet(rs, out msg);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                rs.Revenue_id = Convert.ToInt32(dr["Revenue_id"]);
                rs.Sales_date = dr["Sales_date"] as DateTime?;
                rs.Material_Category = dr["Material_Category"].ToString();
                rs.Material_SubCategory = dr["Material_SubCategory"].ToString();
                rs.Material_Weight_lbs = Convert.ToInt32(dr["Material_Weight_lbs"]);
                rs.Revenue_in_USD = Convert.ToDecimal(dr["Revenue_in_USD"]);
                rs.Buyer_Name = dr["Buyer_Name"].ToString();


            }
            return View("~/Views/Home/EditSales.cshtml", rs);
        }
        [HttpPost]
        public IActionResult EditSales(int id, [Bind] Recyclingsales rs)
        {
            try
            {
                rs.Revenue_id = id;
                rs.flag = "update";
                dbop.SalesDML(rs, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ViewSales");
        }

        public IActionResult DeleteSales(int id)
        {
            try
            {
                Recyclingsales rs = new Recyclingsales();
                rs.flag = "delete";
                rs.Revenue_id = id;
                dbop.SalesDML(rs, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ViewSales");
        }


    }
}
