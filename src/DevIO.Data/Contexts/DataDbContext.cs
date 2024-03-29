﻿using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Contexts;

public class DataDbContext : DbContext
{
	public DataDbContext(DbContextOptions options) : base(options) { }

	public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);
    }
}
