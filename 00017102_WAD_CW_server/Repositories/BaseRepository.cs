﻿using _00017102_WAD_CW_server.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace _00017102_WAD_CW_server.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly GeneralDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(GeneralDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}