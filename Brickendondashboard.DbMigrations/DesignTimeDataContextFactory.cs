﻿using BrickendonDashboard.DbPersistence;
using BrickendonDashboard.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace BrickendonDashboard.DbMigrations
{
  public class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<DataContext>
  {
    public DataContext CreateDbContext(string[] args)
    {
      var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
      var connectionString = configuration.GetConnectionString("DbConnection");

      var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

      optionsBuilder.UseNpgsql(connectionString, delegate (NpgsqlDbContextOptionsBuilder o)
      {
        o.MigrationsAssembly(typeof(DesignTimeDataContextFactory).Assembly.FullName);
      });

      var context = new DataContext(optionsBuilder.Options, new DateTimeService());

      return context;
    }
  }
}