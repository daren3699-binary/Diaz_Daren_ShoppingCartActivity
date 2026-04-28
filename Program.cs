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

            CartItem[] cart = new CartItem[5];
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
                Console.WriteLine($"Available cart slots: {cart.Length - cartCount}");

                if (cartCount == cart.Length)
                {
                    Console.WriteLine("INFO: Cart is full.");

                    int fullChoice = CartMenu();

                    if (fullChoice == 6)
                    {
                        DisplayReceipt(cart, cartCount, products);
                        return;
                    }

                    if (fullChoice == 1)
                    {
                        DisplayCart(cart, cartCount);
                    }

                    continue;
                }

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

                bool checkoutNow = false;

                while (true)
                {
                    int cartMenuChoice = CartMenu();

                    if (cartMenuChoice == 1)
                    {
                        DisplayCart(cart, cartCount);
                    }
                    else if (cartMenuChoice == 2)
                    {
                        UpdateQuantity(cart, cartCount);
                    }
                    else if (cartMenuChoice == 3)
                    {
                        RemoveItem(cart, ref cartCount);
                    }
                    else if (cartMenuChoice == 4)
                    {
                        string confirm = PromptValidator("Are you sure you want to clear the cart? (Y/N): ");
                        if (confirm == "Y")
                        {
                            ClearCart(ref cartCount);
                        }
                    }
                    else if (cartMenuChoice == 5)
                    {
                        break;
                    }
                    else if (cartMenuChoice == 6)
                    {
                        string choice = PromptValidator("Are you sure you want to checkout? (Y/N): ");

                        if (choice == "Y")
                        {
                            checkoutNow = true;
                            break;
                        }
                    }

                }

                if (checkoutNow)
                {
                    DisplayReceipt(cart, cartCount, products);
                    break;
                }

            }


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
                Console.WriteLine($"{i + 1}. {item.Product.Name,-20} | Quantity: {item.Quantity,-3} | Subtotal: (PHP {item.Product.Price:F2} x {item.Quantity}) = PHP {item.SubTotal:F2}");
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
        //Part 2 Codes / Methods
        static string PromptValidator(string message) //a method for Y/N input validation
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

        static int CartMenu()
        {
            while (true)
            {
                Console.WriteLine("\n============== CART MENU ==============");
                Console.WriteLine("1. View Cart");
                Console.WriteLine("2. Update Item Quantity");
                Console.WriteLine("3. Remove Item");
                Console.WriteLine("4. Clear Cart");
                Console.WriteLine("5. Continue Shopping");
                Console.WriteLine("6. Checkout");
                Console.Write("Enter choice (1-6): ");

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice >= 1 && choice <= 6)
                    {
                        return choice;
                    }
                }
                Console.WriteLine("ERROR: Invalid choice. Please enter a number between 1 and 6.");
            }
        }

        static void ClearCart(ref int cartCount)
        {
            cartCount = 0;
            Console.WriteLine("INFO: Cart has been cleared.");
        }

        static void RemoveItem(CartItem[] cart, ref int cartCount)
        {
            if (cartCount == 0)
            {
                Console.WriteLine("INFO: Cart is empty. No items to remove.");
                return;
            }

            DisplayCart(cart, cartCount);

            Console.Write($"Enter item number to remove (1 to {cartCount}): ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int removeIndex))
            {
                Console.WriteLine("ERROR: Invalid number.");
                return;
            }

            removeIndex--;

            if (removeIndex < 0 || removeIndex >= cartCount)
            {
                Console.WriteLine("ERROR: Item does not exist.");
                return;
            }
            Console.WriteLine($"INFO: Removed {cart[removeIndex].Product.Name}");

            for (int i = removeIndex; i < cartCount - 1; i++)
            {
                cart[i] = cart[i + 1];
            }
            cartCount--;
        }

        static void UpdateQuantity(CartItem[] cart, int cartCount)
        {
            if (cartCount == 0)
            {
                Console.WriteLine("INFO: Cart is empty. No items to update.");
                return;
            }

            DisplayCart(cart, cartCount);

            Console.Write($"Enter item number to update (1 to {cartCount}): ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int itemIndex))
            {
                Console.WriteLine("ERROR: Invalid number.");
                return;
            }

            itemIndex--;

            if (itemIndex < 0 || itemIndex >= cartCount)
            {
                Console.WriteLine("ERROR: Invalid item.");
                return;
            }

            Console.Write("Enter new quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int newQuantity))
            {
                Console.WriteLine("ERROR: Invalid quantity.");
                return; 
            }

            if (newQuantity <= 0)
            {
                Console.WriteLine("ERROR: Quantity must be greater than zero.");
                return;
            }

            cart[itemIndex].Quantity = newQuantity;
            cart[itemIndex].SubTotal = cart[itemIndex].Product.GetItemTotal(newQuantity);
            Console.WriteLine("INFO: Quantity updated.");
        }
    }
}
