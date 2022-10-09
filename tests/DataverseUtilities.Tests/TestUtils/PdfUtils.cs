using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malaker.DataverseUtilities.Tests.TestUtils
{
    public class PdfUtils
    {
        public static void OpenAndClosePdf(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);

            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0L;
                PdfReader pdfReader = new PdfReader(ms);
                PdfDocument doc = new PdfDocument(pdfReader);
            }
        }


        public static void OpenAndSaveDocument(string base64, string destinationPath)
        {
            byte[] bytes = Convert.FromBase64String(base64);

            using (var ms = new MemoryStream())
            {
                using (var fs = new FileStream(destinationPath, FileMode.OpenOrCreate))
                {
                    PdfWriter pdfWriter = new PdfWriter(fs);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Position = 0L;
                    PdfReader pdfReader = new PdfReader(ms);
                    PdfDocument doc = new PdfDocument(pdfReader, pdfWriter);
                    doc.Close();
                }
            }
        }
    }
}
