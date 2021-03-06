﻿using System;

// Description: Displays a list of items 
// for the user to add to his cart and
// calculates the total cost
// Programmers: Emmanuel Valdueza, and Cyril Alonzo

namespace OrderingSystem3000
{
    class Program
    {
        static int[] cart = { 0, 0, 0 };
        static int[] stockAvailable = { 200, 150, 400 };
        static double[] price = { 52.00, 99.00, 34.00 };
        static double totalCost = 0.00;
        static bool loopMain = true;

        static string homeTitle = "Welcome, user!";
        static string[] homeMenuOptions = {
            "1. Browse Items",
            "2. Check Cart & Checkout",
            "3. Exit" };
        static string browseTitle = "Choose your product";
        static string[] productOptions = {
            "1. Apple",
            "2. Mango",
            "3. Banana",
            "4. Go Back"};
        static string[] addRemoveOptions = {
            "1. Add",
            "2. Remove",
            "3. Go Back" };

        static void Main(string[] args)
        {
            do
            {
                switch (getChoice(homeTitle, homeMenuOptions, 3)) 
                {
                    case 1:
                        BrowseAndPick();
                        break;
                    case 2:
                        Checkout();
                        break;
                    case 3:
                        Exit();
                        break;
                }
            } while (loopMain);
        }

        static void Exit()
        {
            if (getChoice("EXIT", new string[] {
                            "Are you sure you want to exit?",
                            "", "1. Yes", "2. No" }, 2) == 1)
            {
                loopMain = false;
            }
            else
            {
                loopMain = true;
            }
        }

        static void BrowseAndPick()
        {
            do
            {
                int productID = getChoice(browseTitle, productOptions, 4) - 1;

                switch (productID)
                {
                    case 0:
                        addRemovePrompt(productID, "Apples");
                        break;
                    case 1:
                        addRemovePrompt(productID, "Mangoes");
                        break;
                    case 2:
                        addRemovePrompt(productID, "Bananas");
                        break;
                }
                if (productID == 3) break; //exit product options
            } while (true);
        }

        static void Checkout()
        {
            if (cart[0] + cart[1] + cart[2] == 0)
            {
                printScreen("Oops!", new string[] {
                    "Your cart is empty!",
                    "",
                    "Press any key to go back..." });
                Console.ReadLine();
            }
            else
            {
                bool exitCheckout = false;
                do
                {
                    calculateTotalCost();

                    string[] checkoutConfirmOptions = {
                                createBasket(),
                                "",
                                "Total Cost: " + totalCost + " PHP",
                                "",
                                "1. Continue to Checkout",
                                "2. Go back",
                                ""
                            };

                    int coChoice = getChoice("CHECKOUT", checkoutConfirmOptions, 2);

                    switch (coChoice)
                    {
                        case 1: // print Thank you & Cart & total cost
                            string[] checkoutCompleteOptions = { 
                                createBasket(), 
                                "",
                                "Total Cost: " + totalCost + " PHP",
                                "",
                                "Thank you for your purchase!",
                                "",
                                "Press any key to exit..."
                            };

                            printScreen("CHECKOUT SUCCESS", checkoutCompleteOptions );
                            
                            Console.ReadLine();
                            exitCheckout = true;
                            loopMain = false;
                            break;
                        case 2: // go back
                            exitCheckout = true;
                            break;
                    }
                    if (exitCheckout) break;
                } while (true);
            }
        }

        static string createBasket()
        {
            string cartStr = "";
            if (cart[0] != 0)
            {
                cartStr = "Apples in cart: " + cart[0];
            }
            if (cart[1] != 0)
            {
                if(cart[0] == 0)
                    cartStr = "Mangoes in cart: " + cart[1];
                else
                    cartStr += "\n|| Mangoes in cart: " + cart[1];
            }
            if (cart[2] != 0)
            {
                if (cart[0] != 0)
                {
                    cartStr += "\n|| Bananas in cart: " + cart[2];
                }
                else if( cart[1] != 0)
                {
                    cartStr += "\n|| Bananas in cart: " + cart[2];
                }
                else
                {
                    cartStr = "Bananas in cart: " + cart[2];
                }
            }
            return cartStr;
        }

        static void calculateTotalCost()
        {
            double tempCost = 0.00;
            for(int i = 0; i < cart.Length; i++)
            {
                tempCost += cart[i] * price[i];
            }
            totalCost = tempCost;
        }

        static void addRemovePrompt(int id, string name)
        {
            printScreen(name, addRemoveOptions);
            switch (getChoice(name, addRemoveOptions, 3))
            {
                case 1:
                    //add
                    addToCart(id, name);
                    break;
                case 2:
                    //delete
                    removeFromCart(id, name);
                    break;
                case 3:
                    //go back
                    break;
            }
        }

        static void addToCart(int id, string name)
        {
            string[] addMenu = { "How many would you like to add?",
                                                        "Available stock: " + stockAvailable[id],
                                                        "In Cart: " + cart[id]};
            if (stockAvailable[id] == 0)
            {
                printScreen("Sorry", new string[] { "Nothing in stock today!" });
                Console.WriteLine("Press any key to go back...");
                Console.ReadLine();
            }
            else
            {
                printScreen("Adding " + name, addMenu);
                int addAmount = getChoice("Adding " + name,
                    addMenu,
                    stockAvailable[id]);

                cart[id] += addAmount;
                stockAvailable[id] -= addAmount;

                printScreen("Success!", new string[] { "Added " + addAmount + " " + name + " to cart." });
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }

        static int getChoice(string title, string[] menu, int maxChoices)
        {
            int choice;
            bool invalid = false;
            do
            {
                printScreen(title, menu);
                try
                {
                    if(invalid)
                    {
                        printScreen(title, menu);
                        Console.WriteLine(">Invalid Input.");
                    }
                    Console.Write(">");
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    invalid = true;
                    continue;
                }
                if(choice < 1 || choice > maxChoices)
                {
                    invalid = true;
                    continue;
                }
                return choice;
            } while (true);
        }

        static void printScreen(string title, string[] options)
        {
            Console.Clear();
            Console.WriteLine("|| " + title);
            Console.WriteLine("|| -----------------");
            for(int i = 0; i < options.Length; i++)
            {
                Console.WriteLine("|| " + options[i]);
            }
            Console.WriteLine("||\n");
        }

        static void removeFromCart(int id, string name)
        {
            string[] deleteMenu = { "How many do you want to remove?", name + " in cart: " + cart[id] };
            if (cart[id] == 0)
            {
                printScreen("Oops", new string[] { "Nothing in your cart to delete!" });
                Console.WriteLine("Press any key to go back...");
                Console.ReadLine();
            }
            else
            {
                printScreen(name + " Removing", deleteMenu);
                int deleteAmount = getChoice(name + " Removing", deleteMenu, cart[id]);

                cart[id] -= deleteAmount;
                stockAvailable[id] += deleteAmount;

                printScreen("Success!", new string[] { "Removed " + deleteAmount + " " + name + " from cart." });
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
            
        }
    }
}
