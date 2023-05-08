//// -----------------------------------------------------------------------
//// <copyright file="DbContextLocal.cs" company="Fluent.Infrastructure">
////     Copyright © Fluent.Infrastructure. All rights reserved.
//// </copyright>
////-----------------------------------------------------------------------
/// This is a test file created by Fluent Infrastructure in order to 
/// illustrate its operation.
/// See more at: https://github.com/dn32/Fluent.Infrastructure/wiki
////-----------------------------------------------------------------------

using System.Data.Entity;
using Fluent.Infrastructure.FluentDBContext;
using B2C_API.Models;
using Microsoft.Ajax.Utilities;

namespace B2C_API.DataBase
{
    public class DbContextLocal : DBContext
    {
        public DbContextLocal() : base(@"Data Source=.\SQLEXPRESS;Initial Catalog=B2C_API; Integrated Security = true;") { }

        public DbSet<Forum> Forum { get; set; }
    }
}