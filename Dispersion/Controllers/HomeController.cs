using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Dispersion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }        
        //public string GenertePDF(string pdfData)
        //{
        //    StringWriter sw = new StringWriter();
        //    //string css = System.IO.File.ReadAllText(Server.MapPath("~/Content/bootstrap.min.css"));
        //    //string responsive = System.IO.File.ReadAllText(Server.MapPath("~/Content/responsive.css"));
        //    string html = pdfData;
        //    //"<html><head><style>"
        //    //    //+ css
        //    //    + "#header{display:none;}#printbtn{display:none;}@media screen and (max-width:768px){body{font-size:11px!important;}#mainDiv{font-size:11px!important;}.s-p-sign>.p-sign{min-width: 22%;padding: 8px 4px; margin-right:2px; font-size: 10px;text-align: center;word-wrap: break-word;width: auto;}.legendmilestone2 {font-weight: normal; font-size: 11px;padding: 8px 4px; margin-right:2px; margin-top:10px;display:inline-block;}.fa-stop{transform: rotate(45deg);}}</style></head><body >"
        //    //    + pdfData
        //    //    + "</body></html>";

        //    string path = Server.MapPath("~/Temp/PDF" + Guid.NewGuid().ToString().Substring(0, 6) + ".html");

        //    using (FileStream fs = new FileStream(path, FileMode.Create))
        //    {
        //        Byte[] info = new UTF8Encoding(true).GetBytes(html);
        //        fs.Write(info, 0, info.Length);

        //    }
        //    string pdfFileName = "ProjectSummaryDetail" + Guid.NewGuid().ToString().Substring(0, 6) + ".pdf";
        //    string filePath = Server.MapPath("~/Temp/" + pdfFileName);
        //    StringBuilder sb = new StringBuilder();
        //    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
        //    htmlToPdf.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
        //    var coverHTML = "";
        //    htmlToPdf.GeneratePdfFromFile(path, coverHTML, filePath);
        //    System.IO.File.Delete(path);
        //    return filePath;
        //}
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            
            return View();
        }

        public ActionResult Contact()
        {
            //ExportToPDF();
            //var htmlContent = String.Format("<body>Hello world: {0}</body>",DateTime.Now);
            //GenertePDF(htmlContent);
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}