using simpleCheckout.Exceptions;
using System;

namespace simpleCheckout
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to simple Checkout!");
            Console.WriteLine();
            Console.WriteLine("Please enter the code for each item followed by return.");
            Console.WriteLine();
            Console.WriteLine("To find out the total cost so far enter TOTAL followed by return.");
            Console.WriteLine("To exit enter DONE followed by return.");
            Console.WriteLine();

            var pricer = new Pricer();

            var checkout = new Checkout(pricer);

            var item = string.Empty;
            do
            {
                item = Console.ReadLine();

                if(item == "TOTAL")
                {
                    var total = checkout.GetTotalPrice();
                    Console.WriteLine($"Total is: {total}");
                }
                else if(item != "DONE")
                {
                    try
                    {
                        checkout.Scan(item);
                    } 
                    catch(Exception ex)
                    {
                        if(ex is ItemCodeMissingException)
                        {
                            Console.WriteLine("Item code missing");
                            Console.WriteLine();
                        }
                        else if (ex is ItemCodeInvalidException)
                        {
                            Console.WriteLine("Item code invalid");
                            Console.WriteLine();
                        }
                    }
                }
            }
            while (item != "DONE");
        }
    }
}