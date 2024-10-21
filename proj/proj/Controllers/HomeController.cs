using Microsoft.AspNetCore.Mvc;
using proj.Models;
using proj.Models.Models;
using System.Data;
using System.Diagnostics;

namespace proj.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly Db _dbop; // Declare _dbop for login operations

        // Constructor to accept ILogger and Db instances
        public HomeController(ILogger<HomeController> logger, Db db)
        {
            _logger = logger;
            _dbop = db; // Assign the db parameter to _dbop
          
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Index()
        {
            return View("~/Views/Home/Homepage.cshtml"); // Return the login view
        }

        // Index action for login page (GET)
        public IActionResult Index1()
        {
            return View(new Login()); // Return the login view
        }

        // Login action (POST)





        [HttpPost]
        public IActionResult Index1([Bind] Login loginModel)
        {
            if (ModelState.IsValid) // Check if the login model is valid
            {
                int res = _dbop.LoginCheck(loginModel);
                if (res == 1)
                {
                    TempData["msg"] = "You are welcome to the Admin Section";
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["msg"] = "Admin ID or Password is wrong!";
                }
            }
            return View(loginModel); // Return the login view with validation errors
        }

        // Dashboard action (GET)
        public IActionResult Dashboard()
        {
            return View(); // Return the dashboard view
        }

        //// Create User (GET)
        //public IActionResult CreateUser()
        //{
        //    return View(); // Return the CreateUser.cshtml view
        //}




        //// Alias for CreateUser (GET)
        //[HttpGet("Create")]
        //public IActionResult Create()
        //{
        //    return View("CreateUser"); // Redirect to CreateUser view
        //}

        //// Waste Collection Form (GET)
        //public IActionResult CreateWasteCollection()
        //{
        //    return View("Createwastecollection"); // Return the waste collection form view
        //}

        //public IActionResult CreateSales()
        //{
        //    return View("CreateSales"); // Return the waste collection form view
        //}

        //public IActionResult CreateLandfill()
        //{
        //    return View("CreateLandfill"); // Return the waste collection form view
        //}

        //public IActionResult edit()
        //{
        //    return View(); // Return the edit form view
        //}

        public IActionResult CreateLandfill()
        {
            return View("~/Views/Home/ViewLandfill.cshtml");
        }



    }
}
