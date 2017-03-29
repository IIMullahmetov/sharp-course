﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
namespace WpfApplication4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private nGruberContext _context = new nGruberContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource customerViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            System.Windows.Data.CollectionViewSource categoryViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("salespersonViewSource")));
            System.Windows.Data.CollectionViewSource orderViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));

            // Load is an extension method on IQueryable, 
            // defined in the System.Data.Entity namespace.
            // This method enumerates the results of the query, 
            // similar to ToList but without creating a list.
            // When used with Linq to Entities this method 
            // creates entity objects and adds them to the context.
            _context.Salespeople.Load();
            _context.Orders.Load();
            _context.Customers.Load();

            // After the data is loaded call the DbSet<T>.Local property 
            // to use the DbSet<T> as a binding source.
            categoryViewSource.Source = _context.Salespeople.Local;
            orderViewSource.Source = _context.Orders.Local;
            customerViewSource.Source = _context.Customers.Local;

            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // orderViewSource.Source = [универсальный источник данных]
        }

        //private void buttonSave_Click(object sender, RoutedEventArgs e)
        //{
        //    // When you delete an object from the related entities collection 
        //    // (in this case Products), the Entity Framework doesn’t mark 
        //    // these child entities as deleted.
        //    // Instead, it removes the relationship between the parent and the child
        //    // by setting the parent reference to null.
        //    // So we manually have to delete the products 
        //    // that have a Category reference set to null.

        //    // The following code uses LINQ to Objects 
        //    // against the Local collection of Products.
        //    // The ToList call is required because otherwise the collection will be modified
        //    // by the Remove call while it is being enumerated.
        //    // In most other situations you can use LINQ to Objects directly 
        //    // against the Local property without using ToList first.
        //    foreach (var product in _context.Products.Local.ToList())
        //    {
        //        if (product.Category == null)
        //        {
        //            _context.Products.Remove(product);
        //        }
        //    }

        //    _context.SaveChanges();
        //    // Refresh the grids so the database generated values show up.
        //    this.categoryDataGrid.Items.Refresh();
        //    this.productsDataGrid.Items.Refresh();
        //}


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            this._context.Dispose();
        }

        //private void buttonSave_Click(object sender, RoutedEventArgs e)
        //{
        //    foreach (var product in _context.Salespeople.Local.ToList())
        //    {
        //        if (product.Salespeople == null)
        //        {
        //            _context.Products.Remove(product);
        //        }
        //    }

        //    _context.SaveChanges();
        //    // Refresh the grids so the database generated values show up.
        //    this.categoryDataGrid.Items.Refresh();
        //    this.productsDataGrid.Items.Refresh();
        //    _context.SaveChanges();
        //    this.salespersonDataGrid.Items.Refresh();
        //    this.orderDataGrid.Items.Refresh();
        //    this.customerDataGrid.Items.Refresh();
        //}

        private void buttonSave_Click_1(object sender, RoutedEventArgs e)
        {
            _context.SaveChanges();
            this.salespersonDataGrid.Items.Refresh();
            this.orderDataGrid.Items.Refresh();
            this.customerDataGrid.Items.Refresh();
        }
    }
}