using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AzureStorageWebExplorer.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles(List<HttpPostedFileBase> postedFiles)
        {
            foreach (HttpPostedFileBase postedFile in postedFiles)
            {
                if (postedFile != null)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    byte[] bytes = new byte[postedFile.ContentLength];
                    postedFile.InputStream.Read(bytes,0,bytes.Length);
                    MemoryStream ms = new MemoryStream(bytes);
                    UploadPDFToBlob(ms, fileName);
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                    GC.Collect();
                }
            }

            return View("Index");
        }

        private static void UploadPDFToBlob(MemoryStream ms, string fileName)
        {
            string blobContainerSasUri = ConfigurationManager.AppSettings["BlobContainerSasUri"].ToString();

            Uri blobContainerUri = new Uri(blobContainerSasUri);
            CloudBlobContainer blobContainer = new CloudBlobContainer(blobContainerSasUri);
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = "application/pdf";
            blob.UploadFromStream(ms);
        }
    }
}