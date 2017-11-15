using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAppHttpBenchmark.Controllers
{
    public class HttpTestController : ApiController
    {
        private static HttpClient client = new HttpClient();

        public async Task<HttpResponseMessage> Get(long AllocInMB = 0, int SleepInMS = 0, int LoopSpins = 0, int MatrixSize = 0, int Requests = 0)
        {
            long memory = AllocInMB;
            int sleep = SleepInMS;
            long iterations = LoopSpins;
            int matrixSize = MatrixSize;
            int requests = Requests;
            var rnd = new Random();

            // Allocate memory and fill it with random bytes
            byte[] bytes = new byte[memory * 1024 * 1024];
            rnd.NextBytes(bytes);

            // Do an async sleep
            await Task.Delay(sleep);

            // Spin the CPU
            for (long i = 0; i < iterations; i++) { }

            // Do matrix multiplications
            if (matrixSize > 0)
            {
                Matrix.DoMatrixMultiplication(matrixSize);
            }

            // Make some outbound http requests in parallel
            if (requests > 0)
            {
                var tasks = new List<Task>();
                for (int i = 0; i < requests; i++)
                {
                    tasks.Add(client.GetAsync("http://microsoft.com"));
                }
                await Task.WhenAll(tasks);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK, "Done");
            response.Headers.Add("X-server", Environment.GetEnvironmentVariable("COMPUTERNAME"));
            return response;
        }
    }
}
