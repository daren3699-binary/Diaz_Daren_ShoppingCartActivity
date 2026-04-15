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
                             Price = 6.00,
                             RemainingStock = 30,},

                new Product {Id = 2,
                             Name = "Pad Paper",
                             Price = 20.00,
                             RemainingStock = 25,},

                new Product {Id = 3,
                             Name = "Black Ballpen",
                             Price = 9.00,
                             RemainingStock = 28,},

                new Product {Id = 4,
                             Name = "Correction Tape",
                             Price = 28.00,
                             RemainingStock = 18,},

                new Product {Id = 5,
                             Name = "Whiteboard Marker",
                             Price = 12.00,
                             RemainingStock = 21,},
            };

            List<CartItem> cart = new List<CartItem>();

            while (true)
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("            STORE MENU");
                Console.WriteLine("=================================");
                foreach (Product product in products)
                {
                    product.DisplayProduct();
                }
                Console.WriteLine("=================================");

                int product_index = ValidateProductNumber(products);
                int quantity = ValidateQuantity();

                Product selectedproduct = products[product_index];

                if (!selectedproduct.HasEnoughStock(quantity))
                {
                    Console.WriteLine("ERROR: Not enough stock.");
                    continue;
                }

                if (selectedproduct.RemainingStock == 0)
                {
                    Console.WriteLine("INFO: Out of Stock.");
                    continue;
                }

                Console.WriteLine($"\nYou selected: {selectedproduct.Name} x {quantity}");
                Add_or_UpdateCart(cart, selectedproduct, quantity);
                DisplayCart(cart);

                selectedproduct.DeductStock(quantity);

                Console.Write("Do you want to continue shopping? (y/n) : ");
                string choice = Console.ReadLine().ToLower();

                if (choice != "y")
                {
                    Console.WriteLine("Thank you for shopping...");
                    break;
                }
                else { Console.WriteLine(); }

            }

            DisplayReceipt(cart, products);
        }

        static int ValidateProductNumber(Product[] products)
        {
            while (true)
            {
                Console.Write("\nEnter the product number you want to buy: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int productnumber))
                {
                    Console.WriteLine("ERROR: Invalid input. Please enter a valid product number.");
                    continue;
                }

                if (productnumber < 1 || productnumber > products.Length)
                {
                    Console.WriteLine("ERROR: Invalid product number.");
                    continue;
                }

                return productnumber - 1;
            }
        }

        static int ValidateQuantity()
        {
            while (true)
            {
                Console.Write("Enter the quantity you want to buy: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int quantity))
                {
                    Console.WriteLine("ERROR: Invalid input. Please enter a valid quantity.");
                    continue;
                }

                if (quantity <= 0)
                {
                    Console.WriteLine("ERROR: Invalid. Quantity must be greater than zero.");
                    continue;
                }

                return quantity;
            }
        }

        static void Add_or_UpdateCart(List<CartItem> cart, Product product, int quantity)
        {
            foreach (CartItem item in cart)
            {
                if (item.Product.Id == product.Id)
                {
                    item.Quantity += quantity;
                    item.SubTotal += product.GetItemTotal(quantity);

                    Console.WriteLine($"UPDATE: Updated {product.Name} quantity. New quantity: {item.Quantity} ");
                    return;
                }
            }

            CartItem new_item = new CartItem();
            new_item.Product = product;
            new_item.Quantity = quantity;
            new_item.SubTotal = product.GetItemTotal(quantity);
            cart.Add(new_item);
            Console.WriteLine($"INFO: Added {product.Name} x {quantity} to the cart");
        }

        static void DisplayCart(List<CartItem> cart)
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("            CURRENT CART");
            Console.WriteLine("=================================");

            if (cart.Count == 0)
            {
                Console.WriteLine("INFO: Your cart is empty.");
                return;
            }

            double total = 0;

            foreach (CartItem item in cart)
            {
                Console.WriteLine($"{item.Product.Name} | Quantity: {item.Quantity} | Subtotal: PHP {item.SubTotal}");
                total += item.SubTotal;
            }

            Console.WriteLine($"Current Total: PHP {total:F2}");
            Console.WriteLine("=========================");
        }

        static void DisplayReceipt(List<CartItem> cart, Product[] products)
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("            RECEIPT");
            Console.WriteLine("=================================");

            if (cart.Count == 0)
            {
                Console.WriteLine("INFO: No items purchased.");
                return;
            }

            double GrandTotal = 0;

            foreach (CartItem item in cart)
            {
                Console.WriteLine($"{item.Product.Name, -20} x{item.Quantity, -3} = PHP {item.SubTotal,8:F2}");
                GrandTotal += item.SubTotal;
            }

            Console.WriteLine("-----------------");
            Console.WriteLine($"Grand Total: PHP {GrandTotal}");

            double discount = 0;

            if (GrandTotal >= 5000)
            {
                discount = GrandTotal * 0.10;
                Console.WriteLine($"INFO: Discount (10%): PHP {discount} off discount applied");
            }
            else
            {
                Console.WriteLine("INFO: Discount : PHP 0 . No discount applied.");
            }

            double finalTotal = GrandTotal - discount;
            Console.WriteLine($" PHP {GrandTotal} - PHP {discount} = Final Total : PHP {finalTotal}");

            Console.WriteLine("\n=================================");
            Console.WriteLine("            REMAINING STOCK (UPDATED)");
            Console.WriteLine("=================================");
            foreach (Product product in products)
            {
                Console.WriteLine($"Product : {product.Name} | Remaining Stock : {product.RemainingStock}");
            }
            Console.WriteLine("=========================");

        }


    }

}
