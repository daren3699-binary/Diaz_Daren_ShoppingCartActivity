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

                new Product {Id = 6,
                             Name = "Bag",
                             Price = 250.00,
                             RemainingStock = 15,},


                new Product {Id = 7,
                             Name = "White Board",
                             Price = 300.00,
                             RemainingStock = 25,},


                new Product {Id = 8,
                             Name = "Aquaflask Tumbler",
                             Price = 500.00,
                             RemainingStock = 30,},
            };

            CartItem[] cart = new CartItem[10];
            int cartCount = 0;

            while (true)
            {
                Console.WriteLine("\n================================================");
                Console.WriteLine("                 STORE MENU");
                Console.WriteLine("================================================");
                foreach (Product product in products)
                {
                    product.DisplayProduct();
                }
                Console.WriteLine("================================================");

                int product_index = ValidateProductNumber(products);
                int quantity = ValidateQuantity();

                Product selectedproduct = products[product_index];

                if (selectedproduct.RemainingStock == 0)
                {
                    Console.WriteLine("INFO: Out of Stock.");
                    continue;
                }

                if (!selectedproduct.HasEnoughStock(quantity))
                {
                    Console.WriteLine("ERROR: Not enough stock.");
                    continue;
                }

                Console.WriteLine($"\nYou selected: {selectedproduct.Name} x {quantity}");
                Add_or_UpdateCart(cart, ref cartCount, selectedproduct, quantity);
                selectedproduct.DeductStock(quantity);
                DisplayCart(cart, cartCount);

                string choice = ValidateContinueShopping("\nDo you want to continue shopping? (Y/N): ");
                if (choice == "N")
                {
                    Console.WriteLine("INFO: Proceeding to checkout...");
                    break;
                }

            }

            DisplayReceipt(cart, cartCount, products);
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

        static void Add_or_UpdateCart(CartItem[] cart, ref int cartCount, Product product, int quantity)
        {
            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Product.Id == product.Id)
                {
                    cart[i].Quantity += quantity;
                    cart[i].SubTotal += product.GetItemTotal(quantity);

                    Console.WriteLine($"UPDATE: Updated {product.Name} quantity. New quantity: {cart[i].Quantity} ");
                    return;
                }
            }

            if (cartCount == cart.Length)
            {
                Console.WriteLine("ERROR: Cart is full.");
                return;
            }

            cart[cartCount] = new CartItem()
            {
                Product = product,
                Quantity = quantity,
                SubTotal = product.GetItemTotal(quantity)
            };

            cartCount++;

            Console.WriteLine($"INFO: Added {product.Name} x {quantity} to cart.");
        }

        static void DisplayCart(CartItem[] cart, int cartCount)
        {
            Console.WriteLine("\n================================================");
            Console.WriteLine("                 CURRENT CART");
            Console.WriteLine("================================================");

            if (cartCount == 0)
            {
                Console.WriteLine("INFO: Your cart is empty.");
                return;
            }

            double total = 0;

            for (int i = 0; i < cartCount; i++)
            {
                CartItem item = cart[i];
                Console.WriteLine($"{item.Product.Name,-20} | Quantity: {item.Quantity,-3} | Subtotal: (PHP {item.Product.Price:F2} x {item.Quantity}) = PHP {item.SubTotal:F2}");
                total += item.SubTotal;
            }

            Console.WriteLine($"Current Total: PHP {total:F2}");
            Console.WriteLine("================================================");
        }

        static void DisplayReceipt(CartItem[] cart, int cartCount, Product[] products)
        {
            Console.WriteLine("\n================================================");
            Console.WriteLine("                  RECEIPT");
            Console.WriteLine("================================================");

            if (cartCount == 0)
            {
                Console.WriteLine("INFO: No items purchased.");
                return;
            }

            double grandTotal = 0;

            for (int i = 0; i < cartCount; i++)
            {
                CartItem item = cart[i];
                Console.WriteLine($"{item.Product.Name,-20} x {item.Quantity,-3} = PHP {item.SubTotal,8:F2}");
                grandTotal += item.SubTotal;
            }

            Console.WriteLine("================================================");
            Console.WriteLine($"Grand Total: PHP {grandTotal:F2}");

            double discount = 0;

            if (grandTotal >= 5000)
            {
                discount = grandTotal * 0.10;
                Console.WriteLine($"INFO: 10% Discount Applied: PHP {discount:F2}");
            }
            else
            {
                Console.WriteLine("INFO: 0% Discount Applied : PHP 0");
            }

            double finalTotal = grandTotal - discount;
            Console.WriteLine($"PHP {grandTotal:F2} - PHP {discount:F2} = Final Total : PHP {finalTotal:F2}");

            Console.WriteLine("\n================================================");
            Console.WriteLine("            REMAINING STOCK (UPDATED)");
            Console.WriteLine("================================================");
            foreach (Product product in products)
            {
                Console.WriteLine($"Product : {product.Name,-20} | Remaining Stock : {product.RemainingStock,-3}");
            }
            Console.WriteLine("================================================");
            Console.WriteLine("Thank you for shopping!");

        }

        static string ValidateContinueShopping(string message)
        {
            while (true)
            {
                Console.Write(message);
                string choice = Console.ReadLine().Trim().ToUpper();

                if (choice == "Y" || choice == "YES")
                {
                    return "Y";
                }
                else if (choice == "N" || choice == "NO")
                {
                    return "N";
                }
                else
                {
                    Console.WriteLine("ERROR: Invalid input. Please enter 'Y' or 'N'.");
                }
            }
        }
    }
}
