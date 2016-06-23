﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cik.Domain;
using Cik.Services.Magazine.MagazineService.Model;
using Microsoft.EntityFrameworkCore;

namespace Cik.Services.Magazine.MagazineService.QueryModel
{
    public class CategoryQueryModelFinder : IQueryModelFinder
    {
        private readonly MagazineDbContext _dbContext;

        public CategoryQueryModelFinder(MagazineDbContext dbContext)
        {
            Guard.NotNull(dbContext);

            _dbContext = dbContext;
        }

        public async Task<CategoryDto> Find(Guid categoryId)
        {
            var dbItem = _dbContext
                .Categories
                .Where(x => x.Id == categoryId)
                .Select(x =>
                    new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate
                    })
                .FirstAsync();
            return await dbItem;
        }

        public async Task<List<CategoryDto>> Query()
        {
            var categories = _dbContext
                .Categories
                .Select(x =>
                    new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                .ToListAsync();
            return await categories;
        }

        #region "reactive"

        /*public IObservable<CategoryDto> Find(Guid categoryId)
        {
            var dbItem = _dbContext
                .Categories
                .FromSql("SELECT Id, Name FROM Categories WHERE Id=(@p0)", categoryId);

            return dbItem
                .ToObservable()
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstAsync();
            // return Observable.Return(new CategoryDto());
        }

        public IObservable<CategoryDto> Query()
        {
            var categories = _dbContext.Categories;
            return categories
                .ToObservable()
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                });
        } */

        #endregion
    }
}