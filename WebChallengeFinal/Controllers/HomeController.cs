using Jint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebChallengeFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public void Test()
        {
            string code = @"var xmlHttp = new XMLHttpRequest();
                            xmlHttp.open( 'GET', 'http://localhost:53715/Home/Test', false ); // false for synchronous request
                            xmlHttp.send(null); ";
            var jintEngine = new Engine();
            jintEngine.Execute(code);
        }
    }
}