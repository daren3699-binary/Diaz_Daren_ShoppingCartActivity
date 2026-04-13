namespace v1_DIAZ_DAREN_V_SHOPPINGCARTACTIVTY
{
    class Program
    {
        static void Main(string[] args)
        {
            //initial product array
            Product[] products =
            {
                new Product {Id = 1,
                             Name = "Pencil",
                             Price = 6.00f,
                             RemainingStock = 30,},

                new Product {Id = 2,
                             Name = "Pad Paper",
                             Price = 20.00f,
                             RemainingStock = 17,},

                new Product {Id = 3,
                             Name = "Black Ballpen",
                             Price = 9.00f,
                             RemainingStock = 20,},

                new Product {Id = 4,
                             Name = "Correction Tape",
                             Price = 28.00f,
                             RemainingStock = 10,},

                new Product {Id = 5,
                             Name = "Whiteboard Marker",
                             Price = 12.00f,
                             RemainingStock = 21,},
            };

            Console.WriteLine("-----STORE MENU-----");
            foreach (Product product in products)
            {
                product.DisplayProduct();
            }


        }
    }
}
