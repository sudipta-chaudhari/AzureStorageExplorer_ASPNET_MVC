using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AzureStorageWebExplorer.Models
{
    public class AzureBlob
    {
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public decimal Size { get; set; }
        public string SizeString
        {
            get
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                int order = 0;
                while (Size >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    Size = Size / 1024;
                }

                // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
                // show a single decimal place, and no space.
                return string.Format("{0:0.##} {1}", Size, sizes[order]);
            }
        }
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] Files { get; set; }
    }
}