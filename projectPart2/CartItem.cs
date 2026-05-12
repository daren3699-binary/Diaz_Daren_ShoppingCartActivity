using System;
using System.Collections.Generic;
using System.Text;

namespace v1_DIAZ_DAREN_V_SHOPPINGCARTACTIVTY
{
    class CartItem
    {
        private Product product;
        private int quantity;
        private double subTotal;

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }
       

      
    }
}
