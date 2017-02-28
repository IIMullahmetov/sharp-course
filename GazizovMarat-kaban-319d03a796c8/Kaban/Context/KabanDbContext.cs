using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using Kaban.Entities;
using Kaban.Model;
using Kaban.ViewModel;

namespace Kaban.Context
{
    public class DataBaseInitializer : DropCreateDatabaseAlways<KabanDbContext>
    {
        
        protected override void Seed(KabanDbContext context)
        {
            var cards = new List<Card>
            {
                new Card("name1", "desc1"),
                new Card("name2", "desc2"),
                new Card("name3", "desc3"),
                new Card("name4", "desc4", CardCategory.Doing),
                new Card("name5", "desc5", CardCategory.Doing),
                new Card("name6", "desc6", CardCategory.Doing),
                new Card("name7", "desc7", CardCategory.Done),
                new Card("name8", "desc8", CardCategory.Done),
                new Card("name9", "desc9", CardCategory.Done),
            };

            foreach (var card in cards)
                context.Cards.Add(card);

            context.SaveChanges();
            base.Seed(context);
        }
    }
    public class KabanDbContext : DbContext
    {

        public DbSet<Card> Cards { get; set; }
        public KabanDbContext() :
            base("KabanDbContex")
        {
            Database.SetInitializer(new DataBaseInitializer());
        }

       
    }
}