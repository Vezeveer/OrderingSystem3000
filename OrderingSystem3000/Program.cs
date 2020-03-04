using System;

// Description: Displays a list of items 
// for the user to add to his cart and
// calculates the total cost

namespace OrderingSystem3000
{
    class Program
    {
        static int[] cart = { 0, 0, 0 };
        static int[] stockAvailable = { 200, 150, 400 };
        static double[] price = { 52.00, 99.00, 34.00 };
        static string[] labels = { "apple", "mango", "banana" };
        static double dueCost = 0.00;

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
            bool loop = true, exitBrowseChoice = false;
            
            do
            {
                renderScreen(homeTitle, homeMenuOptions); //home options menu
                switch (checkChoice(homeTitle, homeMenuOptions, 3)) 
                {
                    case 1: //product options
                        do
                        {
                            renderScreen(browseTitle, productOptions);

                            switch (checkChoice(browseTitle, productOptions, 4)) 
                            {
                                case 1: //add or remove options for selected product
                                    bool exitLoop = false;
                                    do
                                    {
                                        renderScreen("Apples", addRemoveOptions);
                                        switch(checkChoice("Apples", addRemoveOptions, 3))
                                        {
                                            case 1:
                                                //add
                                                addToCart(0, "Apples");
                                                exitLoop = true;
                                                break;
                                            case 2:
                                                //delete
                                                removeFromCart(0, "Apples");
                                                exitLoop = true;
                                                break;
                                            case 3:
                                                //go back
                                                exitLoop = true;
                                                break;
                                        }
                                        if (exitLoop) break;
                                    } while (true);
                                    break;
                                case 2:
                                    
                                    break;
                                case 3:
                                    
                                    break;
                                case 4: //Go Back
                                    exitBrowseChoice = true; 
                                    break;
                                default:
                                    break;
                            }
                            if (exitBrowseChoice) break;
                        } while (true);
                        
                        break;
                    case 2: //Checkout
                        break;
                    case 3: //Exit
                        loop = false;
                        break;
                }
            } while (loop);
            

            //Items To Show with costs
            //Add or Remove
            //Show Cart of only Items added to Cart, with cost

            //On checkout display all items and total cost


        }

        static void addToCart(int id, string name)
        {
            string[] addMenu = { "How many would you like to add?",
                                                        "Available stock: " + stockAvailable[id],
                                                        "In Cart: " + cart[0]};
            if (stockAvailable[id] == 0)
            {
                renderScreen("Sorry", new string[] { "Nothing in stock today!" });
                Console.WriteLine("Press any key to go back...");
                Console.ReadLine();
            }
            else
            {
                renderScreen("Adding " + name, addMenu);
                int addAmount = checkChoice("Adding" + name,
                    addMenu,
                    stockAvailable[id]);

                cart[id] += addAmount;
                stockAvailable[id] -= addAmount;

                renderScreen("Success!", new string[] { "Added " + addAmount + " " + name + " to cart." });
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }


        //gets input, returns a choice
        static int checkChoice(string title, string[] menu, int maxChoices)
        {
            int choice;
            bool invalid = false;
            do
            {
                try
                {
                    if(invalid)
                    {
                        renderScreen(title, menu);
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

        static void renderScreen(string title, string[] options)
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
                renderScreen("Oops", new string[] { "Nothing in your cart to delete!" });
                Console.WriteLine("Press any key to go back...");
                Console.ReadLine();
            }
            else
            {
                renderScreen(name + " Removing", deleteMenu);
                int deleteAmount = checkChoice(name + " Removing", deleteMenu, cart[id]);

                cart[id] -= deleteAmount;
                stockAvailable[id] += deleteAmount;

                renderScreen("Success!", new string[] { "Removed " + deleteAmount + " " + name + " from cart." });
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
            
        }
    }
}
