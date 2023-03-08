using DataLayer;
using System.Web.Mvc;

namespace MyEshop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        MyEshop_DBEntities db = new MyEshop_DBEntities();

        //public ActionResult ShowGroups()
        //{
        //    return PartialView(db.Product_Group.ToList());
        //}
    }
}