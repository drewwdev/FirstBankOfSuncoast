using System;
using System.Collections.Generic;

namespace FirstBankOfSuncoast
{
    class Transaction
    {
        public string AccountType { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }
    }

    class Program
    {
        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                Console.WriteLine();
                return 0;
            }
        }

        static void Main(string[] args)
        {
            var transactions = new Transaction();

            var keepGoing = true;

            while (keepGoing)
            {
                Console.WriteLine("Would you like to:\n(W)ithdraw\n(D)eposit\n(Q)uit");
                var menuOption = Console.ReadLine().ToUpper();

                if (menuOption == "W")
                {
                    Console.WriteLine("Which account would you like to withdraw from:\n(C)hecking\n(S)avings");
                    var innerMenuOption = Console.ReadLine().ToUpper();
                    if (innerMenuOption == "C")
                    {
                        transactions.AccountType = innerMenuOption;
                        transactions.Amount = PromptForInteger("How much would you like withdraw from your checking? ");
                        transactions.TransactionType = menuOption;
                    }
                    else
                    if (innerMenuOption == "S")
                    {
                        transactions.AccountType = innerMenuOption;
                        transactions.Amount = PromptForInteger("How much would you like to withdraw from your savings? ");
                        transactions.TransactionType = menuOption;
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }
                }
                else
                if (menuOption == "D")
                {
                    Console.WriteLine("Which account would you like to deposit to:\n(C)hecking\n(S)avings");
                    var innerMenuOption = Console.ReadLine().ToUpper();
                    if (innerMenuOption == "C")
                    {
                        transactions.AccountType = innerMenuOption;
                        transactions.Amount = PromptForInteger("How much would you like to deposit to your checking? ");
                        transactions.TransactionType = menuOption;
                    }
                    else
                    if (innerMenuOption == "S")
                    {
                        transactions.AccountType = innerMenuOption;
                        transactions.Amount = PromptForInteger("How much would you like to deposit to your savings? ");
                        transactions.TransactionType = menuOption;
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }
                }
                else
                if (menuOption == "Q")
                {
                    keepGoing = false;
                }
                else
                {
                    Console.WriteLine("Invalid selection");
                }
            }
        }
    }
}