﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaboratoryEntity
{
    [Table("Diary")]
    public class Diary
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; } //НОМЕР МЕРОПРИЯТИЯ
        public DateTime Date { get; set; } //ДАТА
        public DateTime Time { get; set; } //ВРЕМЯ
        public string Event { get; set; } //СОБЫТИЕ
        public DateTime Duration { get; set; } //ПРОДОЛЖИТЕЛЬНОСТЬ
        public string Place { get; set; } //МЕСТО

        public virtual ICollection<DiaryEvents> DiaryEvents { get; set; } = new List<DiaryEvents>();
        public virtual ICollection<DiaryPlaces> DiaryPlaces { get; set; } = new List<DiaryPlaces>();
    }

    [Table("DiaryEvents")]
    public class DiaryEvents
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; } //НОМЕР МЕРОПРИЯТИЯ
        public string Event { get; set; } //CОБЫТИЕ
        public string Description { get; set; } //ОПИСАНИЕ

        public virtual ICollection<Diary> Diary { get; set; } = new List<Diary>();
        public virtual ICollection<DiaryPlaces> DiaryPlaces { get; set; } = new List<DiaryPlaces>();
    }

    [Table("DiaryPlaces")]
    public class DiaryPlaces
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; } //НОМЕР СОБЫТИЯ
        public string Event { get; set; } //СОБЫТИЕ
        public string Place { get; set; } //МЕСТО

        public virtual ICollection<Diary> Diary { get; set; } = new List<Diary>();
        public virtual ICollection<DiaryEvents> DiaryEvents { get; set; } = new List<DiaryEvents>();
    }

    public class DiaryContext : DbContext
    {
        public DbSet<Diary> Diary { get; set; }
        public DbSet<DiaryEvents> DiaryEvents { get; set; }
        public DbSet<DiaryPlaces> DiaryPlaces { get; set; }
    }
}
