namespace v1_DIAZ_DAREN_V_SHOPPINGCARTACTIVTY
{
    class Program
    {
        static int receiptCounter = 1; //global counter for receipt
        static string[] orderHistory = new string[50]; //transactions history array
        static int historyCount = 0; //counter for order history
        static void Main(string[] args)
        {
            //initial product array
            Product[] products =
            {
                new Product {Id = 1,
                             Name = "Pencil",
                             Price = 6.00,
                             RemainingStock = 30,
                             Category = "Supplies"}, //i added category for all products

                new Product {Id = 2,
                             Name = "Pad Paper",
                             Price = 20.00,
                             RemainingStock = 25,
                             Category = "Supplies"},

                new Product {Id = 3,
                             Name = "Black Ballpen",
                             Price = 9.00,
                             RemainingStock = 28,
                             Category = "Supplies"},

                new Product {Id = 4,
                             Name = "Correction Tape",
                             Price = 28.00,
                             RemainingStock = 18,
                             Category = "Supplies"},

                new Product {Id = 5,
                             Name = "Whiteboard Marker",
                             Price = 12.00,
                             RemainingStock = 21,
                             Category = "Supplies"},

                new Product {Id = 6,
                             Name = "Bag",
                             Price = 250.00,
                             RemainingStock = 15,
                             Category = "Accessories"},

                new Product {Id = 7,
                             Name = "White Board",
                             Price = 300.00,
                             RemainingStock = 25,
                             Category = "Equipment"},

                new Product {Id = 8,
                             Name = "Aquaflask Tumbler",
                             Price = 500.00,
                             RemainingStock = 30,
                             Category = "Accessories"},
            };

            CartItem[] cart = new CartItem[5];
            int cartCount = 0;

            while (true)
            {
                int choice = ShoppingMenu(products, cart, cartCount);

                if (choice == 1)
                {
                    SearchProducts(products);
                }
                else if (choice == 2)
                {
                    FilterByCategory(products);
                }
                else if (choice == 3)
                {
                    if (cartCount == cart.Length)
                    {
                        Console.WriteLine("INFO: Cart is full.");
                    }
                    else
                    {
                        int productIndex = ValidateProductNumber(products);
                        int quantity = ValidateQuantity();

                        Product selectedProduct = products[productIndex];

                        if (selectedProduct.RemainingStock == 0)
                        {
                            Console.WriteLine("INFO: Out of Stock");
                            continue;
                        }

                        if (!selectedProduct.HasEnoughStock(quantity))
                        {
                            Console.WriteLine("ERROR: Not enough stock.");
                            continue;
                        }

                        Console.WriteLine($"\nYou selected: {selectedProduct.Name} x {quantity}");
                        AddOrUpdateCart(cart, ref cartCount, selectedProduct, quantity);
                        selectedProduct.DeductStock(quantity);
                    }
                }
                else if (choice == 4)
                {
                    DisplayCart(cart, cartCount);
                }
                else if (choice == 5)
                {
                    string optionToUpdate = PromptValidator("Are you sure you want to update item quantity? (Y/N): ");
                    if (optionToUpdate == "Y")
                    {
                        UpdateQuantity(cart, cartCount);
                    }
                }
                else if (choice == 6)
                {
                    string optionToRemove = PromptValidator("Are you sure you want to remove an item from the cart? (Y/N): ");
                    if (optionToRemove == "Y")
                    {
                        RemoveItem(cart, ref cartCount);
                    }
                }
                else if (choice == 7)
                {
                    string optionToClear = PromptValidator("Are you sure you want to clear the cart? (Y/N): ");
                    if (optionToClear == "Y")
                    {
                        ClearCart(cart, ref cartCount);
                    }
                }
                else if (choice == 8)
                {
                    string optionToCheckout = PromptValidator("Are you sure you want to checkout? (Y/N): ");
                    if (optionToCheckout == "Y")
                    {
                        DisplayReceipt(cart, cartCount, products);
                        break;
                    }
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

        static void AddOrUpdateCart(CartItem[] cart, ref int cartCount, Product product, int quantity)
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

            Console.WriteLine($"Receipt No: {receiptCounter:D4}");
            Console.WriteLine($"Date: {DateTime.Now:MMMM dd, yyyy hh:mm tt}");
            Console.WriteLine("================================================");

            double grandTotal = 0;
            Console.WriteLine("PURCHASED ITEMS:");
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

            orderHistory[historyCount] = $"Receipt No: {receiptCounter:D4} - Final Total : PHP {finalTotal:F2} Date: {DateTime.Now:MMMM dd, yyyy hh:mm tt}";
            historyCount++;

            Console.WriteLine($"Final Total after discount (PHP {grandTotal:F2} - PHP {discount:F2}): PHP {finalTotal:F2}");
            double payment = GetValidPayment(finalTotal);
            double change = payment - finalTotal;
            Console.WriteLine($"\nFinal Total : PHP {finalTotal:F2}");
            Console.WriteLine($"Payment : PHP {payment:F2}");
            Console.WriteLine($"Change (PHP {payment:F2} - PHP {finalTotal:F2}) : PHP {change:F2}");

            Console.WriteLine("\n=====================================================");
            Console.WriteLine("            REMAINING STOCK (UPDATED)");
            Console.WriteLine("=====================================================");
            foreach (Product product in products)
            {
                Console.WriteLine($"Product : {product.Name,-20} | Remaining Stock : {product.RemainingStock,-3}");
            }
            Console.WriteLine("=====================================================");

            Console.WriteLine("\n=====================================================");
            Console.WriteLine("                  LOW STOCK ALERT");
            Console.WriteLine("=====================================================");

            bool hasLowStock = false;

            foreach (Product product in products)
            {
                if (product.RemainingStock <= 5)
                {
                    Console.WriteLine($"ALERT: {product.Name} has only {product.RemainingStock} stock(s) left.");
                    hasLowStock = true;
                }
            }
            if (!hasLowStock)
            {
                Console.WriteLine("INFO: No low stock products.");
            }

            string viewHistory = PromptValidator("\nDo you want to view order history? (Y/N): ");
            if (viewHistory == "Y")
            {
                DisplayOrderHistory();
            }

            Console.WriteLine("Thank you for shopping!");
            receiptCounter++;
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

        static int ShoppingMenu(Product[] products, CartItem[] cart, int cartCount)
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
            Console.WriteLine("================================================");
            Console.WriteLine("1. Search Product");
            Console.WriteLine("2. Filter by Category");
            Console.WriteLine("3. Add Item to Cart");
            Console.WriteLine("4. View Cart");
            Console.WriteLine("5. Update Item Quantity");
            Console.WriteLine("6. Remove Item");
            Console.WriteLine("7. Clear Cart");
            Console.WriteLine("8. Checkout");
            Console.WriteLine("================================================");

            while (true)
            {
                Console.Write("Enter choice (1-8): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 8)
                {
                    return choice;
                }
                Console.WriteLine("ERROR: Invalid choice. Please enter a number between 1 and 8.");
            }
        }

        static void ClearCart(CartItem[] cart, ref int cartCount)
        {
            for (int i = 0; i < cartCount; i++)
            {
                cart[i].Product.RemainingStock += cart[i].Quantity;
                cart[i] = null;
            }

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

            cart[removeIndex].Product.RemainingStock += cart[removeIndex].Quantity;
            Console.WriteLine($"INFO: Removed {cart[removeIndex].Product.Name} x {cart[removeIndex].Quantity}");

            for (int i = removeIndex; i < cartCount - 1; i++)
            {
                cart[i] = cart[i + 1];
            }
            cart[cartCount - 1] = null;
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

            int oldQuantity = cart[itemIndex].Quantity;
            int difference = newQuantity - oldQuantity;

            if (difference > 0)
            {
                if (!cart[itemIndex].Product.HasEnoughStock(difference))
                {
                    Console.WriteLine("ERROR: Not enough stock to increase quantity.");
                    return;
                }
                cart[itemIndex].Product.DeductStock(difference);
            }
            else if (difference < 0)
            {
                cart[itemIndex].Product.RemainingStock += (-difference);
            }

            cart[itemIndex].Quantity = newQuantity;
            cart[itemIndex].SubTotal = cart[itemIndex].Product.GetItemTotal(newQuantity);
            Console.WriteLine("INFO: Quantity updated.");
        }

        static double GetValidPayment(double finalTotal)
        {
            while (true)
            {
                Console.Write($"\nEnter payment amount: PHP ");
                string input = Console.ReadLine();

                if (!double.TryParse(input, out double payment))
                {
                    Console.WriteLine("ERROR: Invalid payment. Numbers only.");
                    continue;
                }

                if (payment < finalTotal)
                {
                    Console.WriteLine("ERROR: Insufficient payment.");
                    continue;
                }
                return payment;
            }
        }

        static void DisplayOrderHistory()
        {
            Console.WriteLine("\n================================================");
            Console.WriteLine("                 ORDER HISTORY");
            Console.WriteLine("================================================");

            if (historyCount == 0)
            {
                Console.WriteLine("INFO: No transactions yet.");
                return;
            }
            for (int i = 0; i < historyCount; i++)
            {
                Console.WriteLine(orderHistory[i]);
            }
        }

        static void SearchProducts(Product[] products)
        {
            Console.Write("\nEnter product name to search: ");
            string keyword = Console.ReadLine().Trim().ToLower();

            Console.WriteLine("\n================================================");
            Console.WriteLine("                   SEARCH RESULTS");
            Console.WriteLine("================================================");

            bool found = false;
            foreach (Product product in products)
            {
                if (product.Name.ToLower().Contains(keyword))
                {
                    product.DisplayProduct();
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("INFO: No matching products found.");
            }
        }

        static void FilterByCategory(Product[] products)
        {
            Console.WriteLine("\n================================================");
            Console.WriteLine("              CATEGORIES");
            Console.WriteLine("================================================");
            Console.WriteLine("1. Supplies");
            Console.WriteLine("2. Accessories");
            Console.WriteLine("3. Equipment");

            Console.Write("Enter category number (1-3): ");
            string input = Console.ReadLine().Trim();
            string selectedCategory;

            if (input == "1") selectedCategory = "Supplies";
            else if (input == "2") selectedCategory = "Accessories";
            else if (input == "3") selectedCategory = "Equipment";
            else
            {
                Console.WriteLine("ERROR: Invalid category.");
                return;
            }
            Console.WriteLine($"\n========== {selectedCategory.ToUpper()} PRODUCTS ==========");

            bool found = false;
            foreach (Product product in products)
            {
                if (product.Category == selectedCategory)
                {
                    product.DisplayProduct();
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("INFO: No products found in this category.");
            }
        }


    }
}

