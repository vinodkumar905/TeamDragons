using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.WebForms;


public class ReportsController : Controller
{
    public IActionResult ReportViewer()
    {
        // Create a new instance of the ReportViewer
        var reportViewer = new ReportViewer
        {
            ProcessingMode = ProcessingMode.Remote,  // Set remote processing mode
            SizeToReportContent = true,              // Dynamically adjusts to report size
            AsyncRendering = false                   // Disable asynchronous rendering
        };

        // Set the Report Server URL (SSRS Web Service URL)
        reportViewer.ServerReport.ReportServerUrl = new Uri("http://<YourReportServer>/ReportServer");

        // Set the path of the report on the Report Server
        reportViewer.ServerReport.ReportPath = "/YourFolder/YourReportName";

        // Pass ReportViewer instance to the View using ViewBag
        ViewBag.ReportViewer = reportViewer;

        return View();  // Return a view to render the ReportViewer
    }
}
