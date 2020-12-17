using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PullEvikeSpecials.Db;
using PullEvikeSpecials.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PullEvikeSpecials
{
    public class PullEvikeSpecials
    {
        private readonly Context _context;
        private readonly HttpClient httpClient;
        private readonly HtmlParser parser;

        public PullEvikeSpecials(Context context)
        {
            _context = context;
            httpClient = new HttpClient();
            parser = new HtmlParser();
        }

        //[FunctionName("PullSpecialsAndPushToDb")]
        //public async Task Run([TimerTrigger("0 0 * * * *")] TimerInfo myTimer, ILogger log, CancellationToken cts)
        //{
        //    log.LogInformation($"C# Timer trigger function started at: {DateTime.Now}");
        //    List<Special> specials = new List<Special>();
        //    string siteUrl = "https://www.evike.com/specials_00.php?page=";
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        var url = $"{siteUrl}{i}";
        //        log.LogInformation($"Fetching HTML from: {url}");

        //        IHtmlDocument document = await Fetch(url);

        //        IHtmlCollection<IElement> selected = ParseDocument(document);

        //        // Stop fetching and parsing if all specials are grabbed.
        //        if (specials.Any() && selected[^2].TextContent == specials.Last().Title) break;

        //        for (int x = 0; x < selected.Length; x += 2)
        //        {
        //            specials.Add(new Special() { 
        //                Title = selected[x].TextContent,
        //                // decimal parse works on windows, however, linux function throws exception when 
        //                // trying to parse decimal from string that contains $.
        //                Price = decimal.Parse(selected[x + 1].TextContent.Replace("$",""), NumberStyles.Currency) 
        //            });
        //        }
        //        log.LogInformation($"{(selected.Count()/2)} specials parsed");
        //    }

        //    log.LogInformation($"Adding {specials.Count} specials to db");
        //    await _context.Specials.AddRangeAsync(specials);
        //    await _context.SaveChangesAsync(cts);

        //    log.LogInformation($"C# Timer trigger function completed at: {DateTime.Now}");
        //}

        private async Task<IHtmlDocument> Fetch(string url)
        {
            HttpResponseMessage request = await httpClient.GetAsync(url);
            Stream response = await request.Content.ReadAsStreamAsync();
            return parser.ParseDocument(response);
        }

        private IHtmlCollection<IElement> ParseDocument(IHtmlDocument document)
        {
            return document.QuerySelectorAll("#content > div.wcol.epicdeals > div.dealcontainer > .dealitem > a > h3[id], #content > div.wcol.epicdeals > div.dealcontainer > div > a > div > h4:not([style])");
        }
    }
}