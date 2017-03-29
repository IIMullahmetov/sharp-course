namespace LabWork
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class Model1 : DbContext
    {
        // Контекст настроен для использования строки подключения "Model1" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "LabWork.Model1" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "Model1" 
        // в файле конфигурации приложения.
        public Model1()
            : base("name=Model1")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Product> Products { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class Manufacturer
    {
        //[Key]
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string CEO { get; set; }
    }

    public class Product
    {
        //[Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int ManufacturerId { get; set; }
    }
}