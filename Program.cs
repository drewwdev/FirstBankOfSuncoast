using System;
using System.Collections.Generic;

namespace FirstBankOfSuncoast
{
    class Transaction
    {
        public string AccountType { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void Deposit(Transaction newTransaction)
        {
            Transactions.Add(newTransaction);
        }
        public void Withdraw(Transaction newTransaction)
        {
            Transactions.Add(newTransaction);
        }

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
                    transactions.TransactionType = menuOption;
                    var transaction = new Transaction();
                    Console.WriteLine("Which account would you like to withdraw from:\n(C)hecking\n(S)avings");
                    var innerMenuOption = Console.ReadLine().ToUpper();
                    if (innerMenuOption == "C")
                    {
                        transaction.AccountType = innerMenuOption;
                        transaction.Amount = PromptForInteger("How much would you like withdraw from your checking? ");
                        transactions.Withdraw(transaction);
                    }
                    else
                    if (innerMenuOption == "S")
                    {
                        transaction.AccountType = innerMenuOption;
                        transaction.Amount = PromptForInteger("How much would you like to withdraw from your checking? ");
                        transactions.Withdraw(transaction);
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }
                }
                else
                if (menuOption == "D")
                {
                    transactions.TransactionType = menuOption;
                    var transaction = new Transaction();
                    Console.WriteLine("Which account would you like to deposit to:\n(C)hecking\n(S)avings");
                    var innerMenuOption = Console.ReadLine().ToUpper();
                    if (innerMenuOption == "C")
                    {
                        transaction.AccountType = innerMenuOption;
                        transaction.Amount = PromptForInteger("How much would you like to deposit to your checking? ");
                        transactions.Deposit(transaction);
                    }
                    else
                    if (innerMenuOption == "S")
                    {
                        transaction.AccountType = innerMenuOption;
                        transaction.Amount = PromptForInteger("How much would you like to deposit to your checking? ");
                        transactions.Deposit(transaction);
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