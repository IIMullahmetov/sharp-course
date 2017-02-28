using System;
using Kaban.Entities;
using Kaban.ViewModel;

namespace Kaban.Model
{
    public class Card : HasIdBase<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }

        public TimeSpan WorkTime { get; set; }

        public CardCategory Category { get; set; }

        public Card()
        {
            Id = Guid.NewGuid();
            Name = "";
            Description = "";
            StartTime = DateTime.Parse("2000.12.31");
            WorkTime = default(TimeSpan);
            Category = CardCategory.ToDo;
        }
        public Card(string name, string description, CardCategory category = CardCategory.ToDo)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            StartTime = DateTime.Parse("2000.12.31");
            WorkTime = default(TimeSpan);
            Category = category;
        }
    }
}