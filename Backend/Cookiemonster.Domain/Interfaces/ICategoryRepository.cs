﻿using Cookiemonster.Infrastructure.EFRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookiemonster.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public IQueryable<Recipe> GetAllRecipes(int id);
        public IQueryable<Category> GetMostRecent(int amount);
        public List<int> GetSortedWinningImages(Recipe winningRecipe);

        // public Recipe? GetWinningRecipe(int amount);
        public IQueryable<Recipe>? GetSortedWinningRecipes(int id, int amount);
    }
}