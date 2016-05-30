﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OpenIddict;
using OpenIddict.Models;

namespace WEBAPI.Data
{
    public class ApplicationDbContext : OpenIddictContext<ApplicationUser, Application, ApplicationRole, string>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tags> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
