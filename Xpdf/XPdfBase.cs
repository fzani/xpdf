using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using WkHtmlToXSharp;

namespace Xpdf
{
    public abstract class XPdfBase : IXPdf
    {

        #region Attributes

        private string _path;

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        #endregion


        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="url">html type</param>
        /// <param name="fileName">Name of file</param>
        /// <returns>byte[] from file</returns>
        public abstract byte[] UrlToPdf(Url url, string fileName);

        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="html">html type</param>
        /// <param name="fileName">Name of file</param>
        /// <returns>byte[] from file</returns>
        public abstract byte[] HtmlToPdf(string html, string fileName);

        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="html">html type</param>
        /// <returns>byte[] from file</returns>
        public abstract byte[] HtmlToPdf(string html);

        /// <summary>
        /// Generate byte[] from Url or Html Content
        /// </summary>
        /// <param name="page">Url or Html Content</param>
        /// <param name="fileName">Name of file</param>
        /// <param name="source">EnumType:Url or Html</param>
        /// <returns></returns>
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, Unrestricted = true)]
        protected byte[] CreatePdfFile(string page, string fileName, EnumPdfType source)
        {
            var fileBytes = CreatePdfFile(page, source);

            using (FileStream fs = new FileStream(string.Concat(Path, fileName.ToLower().Replace(".pdf", string.Empty), ".PDF"), FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
                fs.Close();
            }

            return fileBytes;
        }

        /// <summary>
        /// Generate byte[] from html string
        /// </summary>
        /// <param name="page">Url or Html Content</param>        
        /// <param name="source">EnumType:Url or Html</param>
        /// <returns></returns>
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, Unrestricted = true)]
        protected byte[] CreatePdfFile(string page, EnumPdfType source)
        {
            bool? isFailed;
            byte[] fileBytes;
            string _log = string.Empty;

            try
            {
                using (IHtmlToPdfConverter wkHtml = new MultiplexingConverter())
                {
                    isFailed = false;

                    wkHtml.GlobalSettings.Margin.Top = "0cm";
                    wkHtml.GlobalSettings.Margin.Bottom = "0cm";
                    wkHtml.GlobalSettings.Margin.Left = "0cm";
                    wkHtml.GlobalSettings.Margin.Right = "0cm";

                    wkHtml.ObjectSettings.Load.Proxy = "none";
                    wkHtml.ObjectSettings.Load.LoadErrorHandling = LoadErrorHandlingType.ignore;
                    wkHtml.ObjectSettings.Load.StopSlowScripts = false;
                    wkHtml.ObjectSettings.Load.Jsdelay = 1000;

                    //Credentials
                    wkHtml.ObjectSettings.Load.Username = UserName;
                    wkHtml.ObjectSettings.Load.Password = Password;

                    wkHtml.ObjectSettings.Web.EnablePlugins = true;
                    wkHtml.ObjectSettings.Web.EnableJavascript = true;

                    wkHtml.ObjectSettings.Page = page; // Some misg site requiring HTTP Basic auth.

                    wkHtml.Begin += (s, e) =>
                    {
                        _log += string.Format("==>> Begin: {0}\n", e.Value);
                        Console.WriteLine("==>> Begin: {0}", e.Value);
                    };
                    wkHtml.PhaseChanged += (s, e) =>
                    {
                        _log += string.Format("==>> New Phase: {0} ({1})\n", e.Value, e.Value2);
                        Console.WriteLine("==>> New Phase: {0} ({1})", e.Value, e.Value2);
                    };
                    wkHtml.ProgressChanged += (s, e) =>
                    {
                        _log += string.Format("==>> Progress: {0} ({1})\n", e.Value, e.Value2);
                        Console.WriteLine("==>> Progress: {0} ({1})", e.Value, e.Value2);
                    };
                    wkHtml.Error += (s, e) =>
                    {
                        _log += string.Format("==>> ERROR: {0}\n", e.Value);
                        isFailed = true;
                        Console.WriteLine("==>> ERROR: {0}", e.Value);
                    };
                    wkHtml.Finished += (s, e) => { Console.WriteLine("==>> WARN: {0}", e.Value); };
                    lock (wkHtml)
                    {
                        if (source == EnumPdfType.Html)
                            fileBytes = wkHtml.Convert(page);
                        else
                            fileBytes = wkHtml.Convert();
                    }

                    return fileBytes;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
