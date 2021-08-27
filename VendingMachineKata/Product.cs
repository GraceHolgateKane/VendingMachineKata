using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineKata
{
    public class Product
    {
        public string ProductName;
        public decimal ProductPrice;
        public int ProductStock;

        public Product(string Name, decimal Price, int Stock)
        {
            ProductName = Name;
            ProductPrice = Price;
            ProductStock = Stock;
        }
    }
}
