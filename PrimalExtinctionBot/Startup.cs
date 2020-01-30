using DSharpPlus.CommandsNext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrimalExtinction.Bots;
using PrimalExtinctionBot.DAL;
using PrimalExtinctionBots.Core.Services.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimalExtinctionBot
{
        public class Startups
    {
        public void ConfigureServices(IServiceCollection services)
            {
                services.AddDbContext<RPGContext>(options =>
                {
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RPGContext;Trusted_Connection=True;MultipleActiveResultSets=true",
                        x => x.MigrationsAssembly("PrimalExtinctionBot.DAL.Migrations"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

            services.AddScoped<IItemService, ItemService>();

            var serviceProvider = services.BuildServiceProvider();

                var bot = new Bot(serviceProvider);
                services.AddSingleton(bot);
            }

        

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {

            }
        }
    }
