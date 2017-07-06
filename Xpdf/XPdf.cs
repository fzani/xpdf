using System;
using System.Security.Policy;
using WkHtmlToXSharp;

namespace Xpdf
{
    public class XPdf : XPdfBase, IDisposable
    {

        #region Attributes

        private bool disposed = false;

        private INativeLibraryBundle nativeBundle { get; set; }

        #endregion

        #region Public Methods

        public XPdf()
        {
            if (nativeBundle == null)
            {
                if (WkHtmlToXLibrariesManager.RunningIn64Bits)
                {
                    nativeBundle = new Win64NativeBundle();
                }
                else
                {
                    nativeBundle = new Win32NativeBundle();
                }

                WkHtmlToXLibrariesManager.Register(nativeBundle);
            }
        }

        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="url">html type</param>
        /// <param name="fileName">Name of file</param>
        /// <returns>byte[] from file</returns>
        public override byte[] UrlToPdf(Url url, string fileName)
        {
            return base.CreatePdfFile(url.Value, fileName, EnumPdfType.Url);
        }

        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="html">html type</param>
        /// <param name="fileName">Name of file</param>
        /// <returns>byte[] from file</returns>
        public override byte[] HtmlToPdf(string html, string fileName)
        {
            return base.CreatePdfFile(html, fileName, EnumPdfType.Html);
        }

        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="html">html type</param>
        /// <returns>byte[] from file</returns>
        public override byte[] HtmlToPdf(string html)
        {
            return base.CreatePdfFile(html, EnumPdfType.Html);
        }

        #endregion

        #region Private Methods


        #endregion

        #region Implement IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //managed objects
                }
                // set fields to null
                disposed = true;
            }
        }

        ~XPdf()
        {
            Dispose(false);
        }

        #endregion

    }
}
