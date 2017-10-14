using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebAppHttpBenchmark.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(long AllocInMB = 0, int SleepInMS = 0, int LoopSpins = 0)
        {
            long memory = AllocInMB;
            int sleep = SleepInMS;
            long iterations = LoopSpins;
            var rnd = new Random();

            // Allocate memory and fill it with random bytes
            byte[] bytes = new byte[memory * 1024 * 1024];
            rnd.NextBytes(bytes);

            // Do an async sleep
            await Task.Delay(sleep);

            // Spin the CPU
            for (long i = 0; i < iterations; i++) { }

            Response.Headers.Add("X-server", Environment.GetEnvironmentVariable("COMPUTERNAME"));
            return Json("Done", JsonRequestBehavior.AllowGet);
        }
    }
}
