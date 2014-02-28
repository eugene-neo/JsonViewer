namespace Newtonsoft.Json.Tests.classes
{
    using System;
    using System.Collections.Generic;

    public class Store
    {
        public StoreColor Color = StoreColor.Yellow;

        public DateTime Establised = new DateTime(2010, 1, 22);

        public double Width = 1.1;

        public int Employees = 999;

        public int[] RoomsPerFloor = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public bool Open = false;

        public char Symbol = '@';

        public List<string> Mottos = new List<string>();

        public decimal Cost = 100980.1M;

        public string Escape = "\r\n\t\f\b?{\\r\\n\"\'";

        public List<Product> product = new List<Product>();

        public Store()
        {
            Mottos.Add("Hello World");
            Mottos.Add("цдьЦДЬ\\'{new Date(12345);}[222]_µ@Іі~");
            Mottos.Add(null);
            Mottos.Add(" ");

            Product rocket = new Product();
            rocket.Name = "Rocket";
            rocket.Expiry = new DateTime(2000, 2, 2, 23, 1, 30);
            Product alien = new Product();
            alien.Name = "Alien";

            product.Add(rocket);
            product.Add(alien);
        }
    }
}