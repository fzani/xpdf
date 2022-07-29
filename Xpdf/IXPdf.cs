using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Xpdf
{
    interface IXPdf
    {
        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="url">html type</param>
        /// <param name="fileName">Name of file</param>
        /// <returns>byte[] from file</returns>
        byte[] UrlToPdf(Url url, string fileName);

        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="html">html type</param>
        /// <param name="fileName">Name of file</param>
        /// <returns>byte[] from file</returns>
        byte[] HtmlToPdf(string html, string fileName);

        /// <summary>
        /// Call generator pdf from Url or Html
        /// </summary>
        /// <param name="html">html type</param>
        /// <param name="fileName">Name of file</param>
        /// <returns>byte[] from file</returns>
        byte[] HtmlToPdf(string html);

    }
}
