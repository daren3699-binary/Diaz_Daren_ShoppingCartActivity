using System;
using System.Collections.Generic;
using System.Text;

namespace v1_DIAZ_DAREN_V_SHOPPINGCARTACTIVTY
{
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;
        public string Category;

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
