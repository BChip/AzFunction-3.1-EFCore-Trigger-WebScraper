using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PullEvikeSpecials.Db;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PullEvikeSpecials
{
    public class EvikeSpecialsHttp
    {
        private readonly Context _context;

        public EvikeSpecialsHttp(Context context)
        {
            _context = context;
        }

        [FunctionName("EvikeSpecialsHttp")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string query = req.Query["query"];
            log.LogInformation($"C# HTTP trigger function processed a request. Query: ${query}");
            if (string.IsNullOrEmpty(query))
            {
                return new BadRequestResult();
            }

            var response = _context.SpecialsGroupedByDate.Where(s => s.Title.Contains(query)).Take(30).OrderByDescending(s => s.Date).ToArray();

            return new OkObjectResult(response);
        }
    }
}
