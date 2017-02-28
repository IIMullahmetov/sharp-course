using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LabWork
{
    public static class Program
    {

        public static byte Quantity { get; set; }

        private static IEnumerable<int> Fibonacci
        {
            get
            {
                decimal prev = 1;
                decimal curr = 1;
                byte i = 0;
                yield return (int) prev;
                yield return (int) curr;
                while (i < Quantity)
                {
                    decimal next = 0;
                    try
                    {
                        next = prev + curr;
                    }
                    catch (OverflowException e)
                    {
                        Console.WriteLine(e.ToString());
                        break;
                    }
                    yield return (int) next;
                    prev = curr;
                    curr = next;
                    ++i;
                }
            }
        }

        public static bool Check(this IEnumerable<int> ar, IEnumerable<int> ar2)
        {
            for (int i = 0; i < ar.Count(); i++)
            {
                if (ar2.Contains(ar.ElementAt(i)))
                {
                    continue;
                }
                else
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            Console.Write("Сколько элементов вывести? ");
            Quantity = byte.Parse(Console.ReadLine());
            //foreach (var v in Fibonacci) Console.WriteLine(v);
            //Console.ReadKey();
        
            IEnumerable<int> test = new [] {1, 3, 5,8,12};
            IEnumerable<int> test2 = new [] {1, 3, 4};

            //1
            Console.WriteLine(test.Check(test2));

            /*
            foreach (int i in ar)
                Console.WriteLine(i);
            */

            //2
            IEnumerable<int> ar2 = test.Except(Fibonacci);

            Console.WriteLine(ar2.Count() == 0);
            /*
            foreach (int i in ar)
                Console.WriteLine(i);
            */

            /*
            using (Model1 context = new Model1())
            {
                XDocument xdoc = XDocument.Load("data.xml");
                var items = from sd in xdoc.Element("StoreData").Element("Manufacturers").Elements("Manufacturer")

                            select new Manufacturer
                            {
                                ManufacturerId = Int32.Parse(sd.Element("ManufacturerId").Value),
                                Name = sd.Element("Name").Value,
                                Country = sd.Element("Country").Value,
                                CEO = sd.Element("CEO").Value
                            };

                foreach (var item in items)
                {
                    context.Manufacturers.Add(item);
                }


                var items2 = from sd in xdoc.Element("StoreData").Element("Products").Elements("Product")

                             select new Product
                             {
                                 ProductId = Int32.Parse(sd.Element("ProductId").Value),
                                 Name = sd.Element("Name").Value,
                                 Cost = Int32.Parse(sd.Element("Cost").Value),
                                 ManufacturerId = Int32.Parse(sd.Element("ManufacturerId").Value)
                             };

                foreach (var item in items2)
                {
                    context.Products.Add(item);
                }

                context.SaveChanges();

                var prod = context.Products.Where(p => p.Cost == (context.Products.Where(x => x.Cost < (context.Products.Average(y => y.Cost))).Max(x => x.Cost))).FirstOrDefault();

                Console.WriteLine(prod.Name + " - " + prod.Cost);


                //List<Manufacturer> manufacturers = new List<Manufacturer>();
                //List<Product> products = new List<Product>();

                /*
                using (Model1 context = new Model1())
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("data.xml");
                    // получим корневой элемент
                    XmlElement xRoot = xDoc.DocumentElement;
                    // обход всех узлов в корневом элементе
                    foreach (XmlNode xnode in xRoot)
                    {
                        if (xnode.Name == "Manufacturers")
                        {
                            foreach (XmlNode childnode in xnode.ChildNodes)
                            {
                                Manufacturer manufacturer = new Manufacturer();

                                foreach (XmlNode downchildnode in childnode.ChildNodes)
                                {
                                    Console.WriteLine(downchildnode.InnerText);

                                    if (downchildnode.Name == "ManufacturerId") { manufacturer.ManufacturerId = Int32.Parse(downchildnode.InnerText); }
                                    if (downchildnode.Name == "Name") { manufacturer.Name = downchildnode.InnerText; }
                                    if (downchildnode.Name == "Country") { manufacturer.Country = downchildnode.InnerText; }
                                    if (downchildnode.Name == "CEO") { manufacturer.CEO = downchildnode.InnerText; }
                                }
                                context.Manufacturers.Add(manufacturer);
                            }
                        }
                        if (xnode.Name == "Products")
                        {
                            foreach (XmlNode childnode in xnode.ChildNodes)
                            {
                                Product product = new Product();

                                foreach (XmlNode downchildnode in childnode.ChildNodes)
                                {

                                    if (downchildnode.Name == "ProductId") { product.ProductId = Int32.Parse(downchildnode.InnerText); }
                                    if (downchildnode.Name == "Name") { product.Name = downchildnode.InnerText; }
                                    if (downchildnode.Name == "Cost") { product.Cost = Int32.Parse(downchildnode.InnerText); }
                                    if (downchildnode.Name == "ManufacturerId") { product.ManufacturerId = Int32.Parse(downchildnode.InnerText); }
                                }
                                context.Products.Add(product);

                            }
                        }
                    }
                    context.SaveChanges();
                    Console.Read();
                }
                Console.Read();
            }*/
            Console.Read();
        }
    }
}
