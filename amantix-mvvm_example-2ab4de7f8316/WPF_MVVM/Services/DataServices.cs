using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WPF_MVVM.Annotations;
using WPF_MVVM.Context;
using WPF_MVVM.Models;
using WPF_MVVM.ViewModels;

namespace WPF_MVVM.Services
{
    public class DataServices
    {
        private readonly TaskDbContext _dbContext;

        public DataServices([NotNull] TaskDbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;
        }

        public IEnumerable<Note> GetCards()
        {
            return _dbContext.Notes.ToList();
        }
        public IEnumerable<Note> GetCards([NotNull] Expression<Func<Note, bool>> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return _dbContext.Notes.Where(specification).ToList();
        }
    }
}