using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kaban.Annotations;
using Kaban.Context;
using Kaban.Model;
using Kaban.ViewModel;

namespace Kaban.Services
{
    public class DataServices
    {
        private readonly KabanDbContext _dbContext;

        public DataServices([NotNull] KabanDbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;
        }

        public IEnumerable<Card> GetCards()
        {
            return _dbContext.Cards.ToList();
        }
        public IEnumerable<Card> GetCards([NotNull] Expression<Func<Card,bool>> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            return _dbContext.Cards.Where(specification).ToList();
        }
    }
}