﻿using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;

namespace Project1.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
