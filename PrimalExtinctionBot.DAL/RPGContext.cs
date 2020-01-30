using Microsoft.EntityFrameworkCore;
using PrimalExtinctionBot.DAL.Models.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimalExtinctionBot.DAL
{
    public class RPGContext : DbContext
    {
        public RPGContext(DbContextOptions<RPGContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}
