using System;
using System.Collections.Generic;
using System.Text;

namespace v1_DIAZ_DAREN_V_SHOPPINGCARTACTIVTY
{
    class Product
    {
        private int id;
        private string name;
        private double price;
        private int remainingStock;
        private string category;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public int RemainingStock
        {
            get { return remainingStock; }
            set { remainingStock = value; }
        }
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        public void DisplayProduct()
        {
            Console.WriteLine($"{Id}. {Name,-20} - PHP {Price,-6:F2} (Stock: {RemainingStock}) ");
        }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        public bool HasEnoughStock(int quantity)
        {
            return RemainingStock >= quantity;
        }

        public void DeductStock(int quantity)
        {
            RemainingStock -= quantity;
        }
    }
}
