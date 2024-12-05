
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace proj.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Yearly()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe", // Command Prompt
                        Arguments = "/c start http://nw60426:8080/MyReport/browse", // Open the URL in the default browser
                        Verb = "runas", // Request admin privileges
                        UseShellExecute = true, // Required for "runas" to work
                        WindowStyle = ProcessWindowStyle.Hidden // Optional: Hide the CMD window
                    }
                };

                process.Start(); // Starts the process
                return Content("The report link was opened successfully.");
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                // User denied admin access or an error occurred
                return Content($"Failed to open the link as administrator: {ex.Message}");
            }
        }

        //public IActionResult viewreport()
        //{
        //	// Logic for Yearly Report
        //	return View("~/Views/Home/Index.cshtml");
        //}

        //public IActionResult Monthly()
        //{
        //	// Logic for Monthly Report
        //	return View();
        //}

    }
}