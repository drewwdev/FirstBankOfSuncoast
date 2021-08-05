using System;
using System.Collections.Generic;

namespace FirstBankOfSuncoast
{
    class Transaction
    {
        public void Withdraw()
        {

        }
        public void Deposit()
        {

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to C#");

            var transactionList = new List<Transaction>();

            var keepGoing = true;

            while (keepGoing)
            {
                Console.WriteLine("Would you like to:\n(W)ithdraw\n(D)eposit\n(Q)uit\n:");
                var menuOption = Console.ReadLine().ToUpper();

                if (menuOption == "W")
                {
                    Console.WriteLine("Which account would you like to withdraw from:\n(C)hecking\n(S)avings\n:");
                }
                else
                if (menuOption == "D")
                {
                    Console.WriteLine("Which account would you like to deposit to:\n(C)hecking\n(S)avings\n:");
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