using System;
using System.Collections.Generic;
using System.Text;
using House.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace House.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductTypes> ProductTypeses { get; set; }
        public DbSet<SpecialTags> SpecialTagses { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
