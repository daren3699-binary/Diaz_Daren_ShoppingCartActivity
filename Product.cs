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

        public void DisplayProduct()
        {
          Console.WriteLine($"{Id}. {Name} - PHP{Price} (Stock: {RemainingStock}) ");
        }
        /*
        public double GetItemTotal(int quantity)
        {

        }

        public bool HasEnoughStock(int quantity)
        {

        }

        public void DeductStock(int quantity)
        {

        }*/






    }
}
