using System.Web.Mvc;

namespace AzureStorageWebExplorer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
     
            return View("Index");
        }
    }
}
