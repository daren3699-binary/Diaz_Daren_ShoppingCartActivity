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
                Console.WriteLine("========STORE MENU========");
                foreach (Product product in products)
                {
                    product.DisplayProduct();
                }
                Console.WriteLine("==========================");

                int product_index = ValidateProductNumber(products);
                int quantity = ValidateQuantity();

                Product selectedproduct = products[product_index];

                if (!selectedproduct.HasEnoughStock(quantity))
                {
                    Console.WriteLine("Not enough stock.");
                    continue;
                }

                if (selectedproduct.RemainingStock == 0)
                {
                    Console.WriteLine("Out of Stock.");
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
                    Console.WriteLine("Invalid input. Please enter a valid product number.");
                    continue;
                }

                if (productnumber < 1 || productnumber > products.Length)
                {
                    Console.WriteLine("Invalid product number.");
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
                    Console.WriteLine("Invalid input. Please enter a valid quantity.");
                    continue;
                }

                if (quantity <= 0)
                {
                    Console.WriteLine("Invalid. Quantity must be greater than zero.");
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

                    Console.WriteLine($"Updated {product.Name} quantity. New quantity: {item.Quantity} ");
                    return;
                }
            }

            CartItem new_item = new CartItem();
            new_item.Product = product;
            new_item.Quantity = quantity;
            new_item.SubTotal = product.GetItemTotal(quantity);
            cart.Add(new_item);
            Console.WriteLine($"Added {product.Name} x {quantity} to the cart");
        }

        static void DisplayCart(List<CartItem> cart)
        {
            Console.WriteLine("\n===== Current Cart ======");

            if (cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }

            double total = 0;

            foreach (CartItem item in cart)
            {
                Console.WriteLine($"{item.Product.Name} | Quantity: {item.Quantity} | Subtotal: PHP {item.SubTotal}");
                total += item.SubTotal;
            }

            Console.WriteLine($"Total so far: PHP {total}");
            Console.WriteLine("=========================");
        }

        static void DisplayReceipt(List<CartItem> cart, Product[] products)
        {
            Console.WriteLine("\n===== Receipt =====");

            if (cart.Count == 0)
            {
                Console.WriteLine("No items purchased.");
                return;
            }

            double GrandTotal = 0;

            foreach (CartItem item in cart)
            {
                Console.WriteLine($"{item.Product.Name} x {item.Quantity} = PHP {item.SubTotal}");
                GrandTotal += item.SubTotal;
            }

            Console.WriteLine("-----------------");
            Console.WriteLine($"Grand Total: PHP {GrandTotal}");

            double discount = 0;

            if (GrandTotal >= 5000)
            {
                discount = GrandTotal * 0.10;
                Console.WriteLine($"Discount (10%): PHP {discount}");
            }
            else
            {
                Console.WriteLine("Discount : PHP 0 . No discount applied.");
            }

            double finalTotal = GrandTotal - discount;
            Console.WriteLine($" PHP {GrandTotal} - PHP {discount} = Final Total : PHP {finalTotal}");

            Console.WriteLine("\n===== Updated Remaining Stock ======");
            foreach (Product product in products)
            {
                Console.WriteLine($"Product : {product.Name} | Remaining Stock : {product.RemainingStock}");
            }
            Console.WriteLine("=========================");

        }


    }

}
