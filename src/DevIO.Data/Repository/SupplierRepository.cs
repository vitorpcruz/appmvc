﻿using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DataDbContext context) : base(context) { }

        public async Task<Supplier> GetSupplierAddress(Guid supplierId)
        {
            return await Context.Suppliers.AsNoTracking()
                .Include(supplier => supplier.Address)
                .FirstOrDefaultAsync(supplier => supplier.Id == supplierId);
        }

        public async Task<Supplier> GetSupplierProductsAdress(Guid supplierId)
        {
            return await Context.Suppliers.AsNoTracking()
                .Include(supplier => supplier.Products)
                .Include(supplier => supplier.Address)
                .FirstOrDefaultAsync(supplier => supplier.Id == supplierId);
        }
    }
}