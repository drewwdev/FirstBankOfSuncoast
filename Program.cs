using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

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
            var transactions = new List<Transaction>();

            if (File.Exists("transactions.csv"))
            {
                var fileReader = new StreamReader("transactions.csv");
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);

                transactions = csvReader.GetRecords<Transaction>().ToList();
                fileReader.Close();
            }

            var keepGoing = true;

            while (keepGoing)
            {
                Console.WriteLine();
                Console.WriteLine("Would you like to:\n[W]ithdraw\n[D]eposit\n[S]how transactions\n[B]alance of account\n[Q]uit");
                var menuOption = Console.ReadLine().ToUpper();

                if (menuOption == "W")
                {
                    var transaction = new Transaction();
                    Console.WriteLine();
                    Console.WriteLine("Which account would you like to withdraw from:\n[C]hecking\n[S]avings");
                    var checkingOrSavings = Console.ReadLine().ToUpper();
                    if (checkingOrSavings == "C")
                    {
                        Console.WriteLine();
                        transaction.Amount = PromptForInteger("How much would you like to withdraw from your checking? ");
                        var accountTotal = transactions.Where(transaction => transaction.TransactionType == "Checking").Sum(transaction => transaction.Amount);
                        if (transaction.Amount > accountTotal)
                        {
                            Console.WriteLine("Not enough money in account");
                            Console.WriteLine();
                        }
                        else
                        {
                            transaction.AccountType = "Checking";
                            transaction.TransactionType = "Withdraw";
                            transactions.Add(transaction);
                        }
                    }
                    else
                    if (checkingOrSavings == "S")
                    {
                        Console.WriteLine();
                        transaction.Amount = PromptForInteger("How much would you like to withdraw from your savings? ");
                        var accountTotal = transactions.Where(transaction => transaction.TransactionType == "Savings").Sum(transaction => transaction.Amount);
                        if (transaction.Amount > accountTotal)
                        {
                            Console.WriteLine("Not enough money in account");
                            Console.WriteLine();
                        }
                        else
                        {
                            transaction.AccountType = "Savings";
                            transaction.TransactionType = "Withdraw";
                            transactions.Add(transaction);
                        }
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
                    Console.WriteLine();
                    Console.WriteLine("Which account would you like to deposit to:\n[C]hecking\n[S]avings");
                    var checkingOrSavings = Console.ReadLine().ToUpper();
                    if (checkingOrSavings == "C")
                    {
                        transaction.AccountType = "Checking";
                        Console.WriteLine();
                        transaction.Amount = PromptForInteger("How much would you like to deposit to your checking? ");
                        transaction.TransactionType = "Deposit";
                        transactions.Add(transaction);
                    }
                    else
                    if (checkingOrSavings == "S")
                    {
                        transaction.AccountType = "Savings";
                        Console.WriteLine();
                        transaction.Amount = PromptForInteger("How much would you like to deposit to your savings? ");
                        transaction.TransactionType = "Deposit";
                        transactions.Add(transaction);
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }
                }
                else
                if (menuOption == "S")
                {
                    Console.WriteLine();
                    Console.WriteLine("Which account would you like to show the transactions of:\n[C]hecking\n[S]avings");
                    var checkingOrSavings = Console.ReadLine().ToUpper();
                    if (checkingOrSavings == "C")
                    {

                        var checkingTransactions = transactions.Where(transaction => transaction.AccountType == "Checking");
                        foreach (var checkingTransaction in checkingTransactions)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{checkingTransaction.TransactionType} of {checkingTransaction.Amount}");
                        }
                    }
                    else
                    if (checkingOrSavings == "S")
                    {
                        var savingsTransactions = transactions.Where(transaction => transaction.AccountType == "Savings");
                        foreach (var savingsTransaction in savingsTransactions)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{savingsTransaction.TransactionType} of {savingsTransaction.Amount}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }
                }
                else
                if (menuOption == "B")
                {
                    Console.WriteLine();
                    Console.WriteLine("Which account would you like to see the balance of:\n[C]hecking\n[S]avings");
                    var checkingOrSavings = Console.ReadLine().ToUpper();
                    if (checkingOrSavings == "C")
                    {
                        var checkingBalance = 0;
                        foreach (var transaction in transactions)
                        {
                            if (transaction.AccountType == "Checking" && transaction.TransactionType == "Deposit")
                            {
                                checkingBalance += transaction.Amount;
                            }
                            if (transaction.AccountType == "Checking" && transaction.TransactionType == "Withdraw")
                            {
                                checkingBalance -= transaction.Amount;
                            }
                        }
                        Console.WriteLine($"Checking balance is {checkingBalance}");
                    }
                    if (checkingOrSavings == "S")
                    {
                        var savingsBalance = 0;
                        foreach (var transaction in transactions)
                        {
                            if (transaction.AccountType == "Savings" && transaction.TransactionType == "Deposit")
                            {
                                savingsBalance += transaction.Amount;
                            }
                            if (transaction.AccountType == "Savings" && transaction.TransactionType == "Withdraw")
                            {
                                savingsBalance -= transaction.Amount;
                            }
                        }
                        Console.WriteLine($"Savings balance is {savingsBalance}");
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
                var fileWriter = new StreamWriter("transactions.csv");

                var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

                csvWriter.WriteRecords(transactions);

                fileWriter.Close();
            }
        }
    }
}