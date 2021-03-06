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
using PullEvikeSpecials.Models;

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
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string queryString = data.query;
            string query = $"\"{queryString.Replace("\"", "")}\"";

            if (string.IsNullOrEmpty(query))
            {
                return new BadRequestResult();
            }

            log.LogInformation($"C# HTTP trigger function processed a request. Query: ${query}");
            var response = _context.SpecialsGrouped.FromSqlInterpolated($"dbo.Search @Name = {query}").ToList();

            return new OkObjectResult(response);
        }
    }
}
