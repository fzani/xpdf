using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xpdf;

namespace App.Pdf.Controllers
{
    public class XPdfTestController : Controller
    {
        public ActionResult Example1()
        {
            //Example 
            XPdf x = new XPdf();
            x.Path = @"c:\temp\pdf\";

            if (!Directory.Exists(x.Path))
                Directory.CreateDirectory(x.Path);

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

            x.HtmlToPdf(html, "teste"); 

            return View();
        }

        public ActionResult Example2()
        {
            //Example 2
            XPdf x = new XPdf();
            x.Path = @"c:\temp\pdf\";

            if (!Directory.Exists(x.Path))
                Directory.CreateDirectory(x.Path);


            System.Security.Policy.Url html = new System.Security.Policy.Url("http://www.google.com");

            x.UrlToPdf(html, "teste");

            return View();
        }

    }
}
