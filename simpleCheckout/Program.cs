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

            var checkout = new Checkout();

            var item = string.Empty;
            do
            {
                item = Console.ReadLine();

                if(item == "TOTAL")
                {
                    var total = checkout.GetTotalPrice();
                    Console.WriteLine($"Total is: {total}");
                }
                else
                {
                    checkout.Scan(item);
                }
            }
            while (item != "DONE");
        }
    }
}