using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using AzureStorageWebExplorer.Models;

namespace AzureStorageWebExplorer.Controllers
{
    public class ExploreController : Controller
    {
        private static List<AzureBlob> lstBlobs;

        public ActionResult Index()
        {
            return View(DisplayBlobFiles());
        }
        private List<AzureBlob> DisplayBlobFiles()
        {
            lstBlobs = new List<AzureBlob>();

            string blobContainerSasUri = ConfigurationManager.AppSettings["BlobContainerSasUri"].ToString();

            Uri blobContainerUri = new Uri(blobContainerSasUri);
            CloudBlobContainer blobContainer = new CloudBlobContainer(blobContainerSasUri);

            var listOfBlobItems = blobContainer.ListBlobs().OfType<CloudBlockBlob>().Where(b => b.Name.ToLower().EndsWith(".pdf"))
                .OrderByDescending(b => b.Properties.LastModifiedUtc);

            lstBlobs.AddRange(listOfBlobItems.Select(x => new AzureBlob() { Name = x.Name, LastModified = x.Properties.LastModifiedUtc,
                Size = x.Properties.Length }));

            return lstBlobs;
        }
        public ActionResult Delete(string fileName)
        {
            bool res = DeleteFileFromBlob(fileName);
            return RedirectToAction("Index");
        }
        public static bool DeleteFileFromBlob(string fileName)
        {
            string blobContainerSasUri = ConfigurationManager.AppSettings["BlobContainerSasUri"].ToString();
            Uri blobContainerUri = new Uri(blobContainerSasUri);
            CloudBlobContainer blobContainer = new CloudBlobContainer(blobContainerSasUri);
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName);
            bool blobExisted = blob.DeleteIfExists();
            if (blobExisted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}