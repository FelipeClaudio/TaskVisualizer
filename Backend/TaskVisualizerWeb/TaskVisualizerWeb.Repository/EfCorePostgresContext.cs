﻿namespace TaskVisualizerWeb.Repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using TaskVisualizerWeb.Domain;

/// <summary>The Entity Framework Core and PostgreSQL context.</summary>
public sealed class EfCorePostgreContext : DbContext
{
    /// <summary>Initializes a new instance of the <see cref="EfCorePostgreContext"/> class.</summary>
    public EfCorePostgreContext() { }

    /// <summary>Initializes a new instance of the <see cref="EfCorePostgreContext"/> class.</summary>
    /// <param name="options">The options.</param>
    public EfCorePostgreContext(DbContextOptions<EfCorePostgreContext> options)
            : base(options)
    {
        Database.EnsureCreated();
    }

    #region [public] Schema Tables

    /// <summary>Gets or sets the roles.</summary>
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    #endregion

    /// <summary>The on configuring.</summary>
    /// <param name="optionsBuilder">The options builder.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                                                .AddJsonFile("appsettings.Development.json", false)
                                                .Build();

        optionsBuilder.UseNpgsql(builder.GetConnectionString("WebApiDatabase"));
    }

    /// <summary>The on model creating.</summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIdentityColumns();

        modelBuilder.HasDefaultSchema("public");

        base.OnModelCreating(modelBuilder);
    }
}