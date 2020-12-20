using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PullEvikeSpecials.Db;
using System;

[assembly: FunctionsStartup(typeof(PullEvikeSpecials.Startup))]

namespace PullEvikeSpecials
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionStringTest");
            builder.Services.AddDbContext<Context>(
                options => options.UseSqlServer(SqlConnection, options => options.EnableRetryOnFailure()));
        }
    }
}