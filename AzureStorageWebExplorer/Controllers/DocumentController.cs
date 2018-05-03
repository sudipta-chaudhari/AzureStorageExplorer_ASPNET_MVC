using System.Web;
using System.Web.Mvc;

namespace AzureStorageWebExplorer.Controllers
{
    public class DocumentController : Controller
    {
        public ActionResult Index()
        {
            if (Request.QueryString["fn"] != null)
            {
                ViewBag.FileName = HttpUtility.UrlDecode(Request.QueryString["fn"].ToString());
            }
            else
            {
                ViewBag.FileName = string.Empty;
            }

            return View();
        }
    }
}