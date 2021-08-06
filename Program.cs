using System;
using System.Collections.Generic;

namespace FirstBankOfSuncoast
{
    class Transaction
    {
        public int WithdrawFromChecking { get; set; }
        public int WithdrawFromSavings { get; set; }
        public int DepositToChecking { get; set; }
        public int DepositToSavings { get; set; }
    }

    class TransactionDatebase
    {
        private List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void AddTransaction(Transaction newTransaction)
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
                return 0;
            }
        }

        static void Main(string[] args)
        {
            var transactions = new TransactionDatebase();

            var keepGoing = true;

            while (keepGoing)
            {
                Console.WriteLine("Would you like to:\n(W)ithdraw\n(D)eposit\n(Q)uit\n:");
                var menuOption = Console.ReadLine().ToUpper();

                if (menuOption == "W")
                {
                    var transaction = new Transaction();
                    Console.WriteLine("Which account would you like to withdraw from:\n(C)hecking\n(S)avings\n:");
                    var innerMenuOption = Console.ReadLine().ToUpper();
                    if (innerMenuOption == "C")
                    {
                        transaction.WithdrawFromChecking = PromptForInteger("How much would you like to deposit to your checking? ");
                        transactions.AddTransaction(transaction);
                    }
                    else
                        if (innerMenuOption == "S")
                    {
                        transaction.WithdrawFromSavings = PromptForInteger("How much would you like to deposit to your checking? ");
                        transactions.AddTransaction(transaction);
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }

                }
                else
                if (menuOption == "D")
                {
                    var transaction = new Transaction();
                    Console.WriteLine("Which account would you like to deposit to:\n(C)hecking\n(S)avings\n:");
                    var innerMenuOption = Console.ReadLine().ToUpper();
                    if (innerMenuOption == "C")
                    {
                        transaction.DepositToChecking = PromptForInteger("How much would you like to deposit to your checking? ");
                        transactions.AddTransaction(transaction);
                    }
                    else
                    if (innerMenuOption == "S")
                    {
                        transaction.DepositToSavings = PromptForInteger("How much would you like to deposit to your checking? ");
                        transactions.AddTransaction(transaction);
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