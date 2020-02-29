﻿using System;

// Description: Displays a list of items 
// for the user to add to his cart and
// calculates the total cost

namespace OrderingSystem3000
{
    class Program
    {
        static int[] cart = { 0, 0, 0 };
        static int[] stock = { 200, 150, 400 };
        static double[] price = { 52.00, 99.00, 34.00 };
        static string[] labels = { "apple", "mango", "banana" };
        static double dueCost = 0.00;

        static string homeTitle = "Welcome, user!";
        static string[] homeMenu = {
            "1. Browse Items",
            "2. Check Cart & Checkout",
            "3. Exit" };
        static int homeChoice, browseChoice, itemChoice;
        static string browseTitle = "Choose your product";
        static string[] browseItems = {
            "1. Apple",
            "2. Mango",
            "3. Banana",
            "4. Go Back"};
        static string[] itemMenu = {
            "1. Add",
            "2. Remove",
            "3. Go Back" };

        static void Main(string[] args)
        {
            bool loop = true;
            
            renderScreen(homeTitle, homeMenu);

            do
            {
                homeChoice = checkChoice(homeTitle, homeMenu, 3);
                switch (homeChoice)
                {
                    case 1:
                        renderScreen(browseTitle, browseItems);
                        browseChoice = checkChoice(browseTitle, browseItems, 4);
                        switch(browseChoice)
                        {
                            case 1:
                                renderScreen("Apples", itemMenu);
                                itemChoice = checkChoice("Apples", itemMenu, 3);
                                break;
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        loop = false;
                        break;
                }
            } while (loop);
            

            //Items To Show with costs
            //Add or Remove
            //Show Cart of only Items added to Cart, with cost

            //On checkout display all items and total cost


        }


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

        static void addToCart(int itemID, int amount)
        {
            cart[itemID] += amount;
        }

        static void removeFromCart(int itemID, int amount)
        {
            cart[itemID] -= amount;
        }
    }
}
