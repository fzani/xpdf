using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xpdf;

namespace App.Pdf.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            XPdf x = new XPdf();
            x.Path = @"c:\temp\pdf\";
            var html = @"<!DOCTYPE html>
            <html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
            <head>
            <meta charset='utf-8' />
            <title>Teste Hello World</title>
            </head>
            <body>
            Teste Hello World!!!
            </body>
            </html>";
            //html = "http://www.google.com";
            byte[] filestream = x.HtmlToPdf(html, "teste");

            Response.ClearContent();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "inline; filename=" + "teste.PDF");
            Response.AddHeader("Content-Length", filestream.Length.ToString());
            Response.BinaryWrite(filestream);
            Response.End();

            return View();
        }
    }
}
