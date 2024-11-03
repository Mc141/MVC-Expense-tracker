﻿using Microsoft.EntityFrameworkCore;

namespace Expense_tracker.Models
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
            
        }
    }
}