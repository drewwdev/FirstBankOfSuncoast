using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{
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
                Console.WriteLine("Would you like to:");
                Console.WriteLine("[W]ithdraw");
                Console.WriteLine("[D]eposit");
                Console.WriteLine("[S]how transactions");
                Console.WriteLine("[B]alance of account");
                Console.WriteLine("[T]ransfer");
                Console.WriteLine("[Q]uit");
                var menuOption = Console.ReadLine().ToUpper();

                if (menuOption == "W")
                {
                    var transaction = new Transaction();
                    Console.WriteLine();
                    Console.WriteLine("Which account would you like to withdraw from:");
                    Console.WriteLine("[C]hecking");
                    Console.WriteLine("[S]avings");
                    var userInput = Console.ReadLine().ToUpper();
                    if (userInput == "C")
                    {
                        Console.WriteLine();
                        transaction.Amount = PromptForInteger("How much would you like to withdraw from your checking? ");
                        var accountTotal = transactions.Where(transaction => transaction.Type == "Checking").Sum(transaction => transaction.Amount);
                        if (transaction.Amount > accountTotal)
                        {
                            Console.WriteLine("Not enough money in account");
                            Console.WriteLine();
                        }
                        else
                        {
                            transaction.Account = "Checking";
                            transaction.Type = "Withdraw";
                            transactions.Add(transaction);
                        }
                    }
                    else
                    if (userInput == "S")
                    {
                        Console.WriteLine();
                        transaction.Amount = PromptForInteger("How much would you like to withdraw from your savings? ");
                        var accountTotal = transactions.Where(transaction => transaction.Type == "Savings").Sum(transaction => transaction.Amount);
                        if (transaction.Amount > accountTotal)
                        {
                            Console.WriteLine("Not enough money in account");
                            Console.WriteLine();
                        }
                        else
                        {
                            transaction.Account = "Savings";
                            transaction.Type = "Withdraw";
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
                    Console.WriteLine("Which account would you like to deposit to:");
                    Console.WriteLine("[C]hecking");
                    Console.WriteLine("[S]avings");
                    var userInput = Console.ReadLine().ToUpper();
                    if (userInput == "C")
                    {
                        transaction.Account = "Checking";
                        Console.WriteLine();
                        transaction.Amount = PromptForInteger("How much would you like to deposit to your checking? ");
                        transaction.Type = "Deposit";
                        transactions.Add(transaction);
                    }
                    else
                    if (userInput == "S")
                    {
                        transaction.Account = "Savings";
                        Console.WriteLine();
                        transaction.Amount = PromptForInteger("How much would you like to deposit to your savings? ");
                        transaction.Type = "Deposit";
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
                    Console.WriteLine("Which account would you like to show the transactions of:");
                    Console.WriteLine("[C]hecking");
                    Console.WriteLine("[S]avings");
                    Console.WriteLine("[T]ransfers");
                    var userInput = Console.ReadLine().ToUpper();
                    if (userInput == "C")
                    {

                        var checkingTransactions = transactions.Where(transaction => transaction.Account == "Checking");
                        foreach (var checkingTransaction in checkingTransactions)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{checkingTransaction.Type} of {checkingTransaction.Amount}");
                        }
                    }
                    else
                    if (userInput == "S")
                    {
                        var savingsTransactions = transactions.Where(transaction => transaction.Account == "Savings");
                        foreach (var savingsTransaction in savingsTransactions)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{savingsTransaction.Type} of {savingsTransaction.Amount}");
                        }
                    }
                    else
                    if (userInput == "T")
                    {
                        var savingsTransactions = transactions.Where(transaction => transaction.Type == "Transfer");
                        foreach (var savingsTransaction in savingsTransactions)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Transfer of {savingsTransaction.Amount}");
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
                    Console.WriteLine("Which account would you like to see the balance of:");
                    Console.WriteLine("[C]hecking");
                    Console.WriteLine("[S]avings");
                    var userInput = Console.ReadLine().ToUpper();
                    if (userInput == "C")
                    {
                        var checkingBalance = 0;
                        foreach (var transaction in transactions)
                        {
                            if (transaction.Account == "Checking" && transaction.Type == "Deposit")
                            {
                                checkingBalance += transaction.Amount;
                            }
                            if (transaction.Account == "Checking" && transaction.Type == "Withdraw")
                            {
                                checkingBalance -= transaction.Amount;
                            }
                        }
                        Console.WriteLine($"Checking balance is {checkingBalance}");
                    }
                    else
                    if (userInput == "S")
                    {
                        var savingsBalance = 0;
                        foreach (var transaction in transactions)
                        {
                            if (transaction.Account == "Savings" && transaction.Type == "Deposit")
                            {
                                savingsBalance += transaction.Amount;
                            }
                            if (transaction.Account == "Savings" && transaction.Type == "Withdraw")
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
                if (menuOption == "T")
                {
                    var transaction = new Transaction();
                    Console.WriteLine();
                    Console.WriteLine("Which account would you like to transfer from:");
                    Console.WriteLine("[C]hecking");
                    Console.WriteLine("[S]avings");
                    var userInput = Console.ReadLine().ToUpper();
                    if (userInput == "C")
                    {

                    }
                    else
                    if (userInput == "S")
                    {

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