using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCode.DTO;
using XCode.Infrastructure;

namespace XCode.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var host = new ApiHost()
            {
                ApiName = "api/product/productlist",
                Domain = "http://localhost:8860/",
            };

            var res = XcHttpClient.Post<string, Response<List<ProductDto>>>(host, null).Result;

            if (res.Head.IsSuccess)
            {
                return View(res.Content);
            }
            return View(new List<ProductDto>());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}