using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Homework_4_29_2019.Data
{
    public class ImageContext: DbContext
    {
        private string _connectionstring;

        public ImageContext(string _ConnectionString)
        {
            _connectionstring = _ConnectionString;
        }

        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionstring);
        }
    }
}
