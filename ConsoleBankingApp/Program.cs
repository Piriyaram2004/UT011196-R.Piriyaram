using System;
using System.Collections.Generic;

namespace BankConsoleApp
{
    class BankAccount
    {
        public string AccountHolderName { get; private set; }
        public int AccountNumber { get; private set; }
        public decimal Balance { get; private set; }

        private List<string> transactions = new List<string>();

        public BankAccount(string holderName, int accountNumber, decimal initialBalance)
        {
            AccountHolderName = holderName;
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"Account Holder: {AccountHolderName}");
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Current Balance: {Balance:F2}");
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                transactions.Add($"Deposited {amount:F2} at {DateTime.Now}");
                Console.WriteLine($"Deposit successful! New balance: {Balance:F2}");
            }
            else
            {
                Console.WriteLine("Enter a valid amount!");
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                transactions.Add($"Withdrew {amount:F2} at {DateTime.Now}");
                Console.WriteLine($"Withdrawal successful! New balance: {Balance:F2}");
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds!");
                return false;
            }
        }

        public void ShowTransactions()
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions yet.");
            }
            else
            {
                Console.WriteLine("===== Transaction History =====");
                foreach (var t in transactions)
                {
                    Console.WriteLine(t);
                }
            }
        }
    }

    internal class Program
    {

        static Dictionary<string, string> users = new Dictionary<string, string>();


        static Dictionary<string, (string firstName, string lastName, string nic, string mobile)> userDetails =
            new Dictionary<string, (string, string, string, string)>();

        static void Main(string[] args)
        {
            Console.WriteLine("===============================");
            Console.WriteLine("WELCOME TO BANK OF ABC");
            Console.WriteLine("===============================");

            bool loggedIn = false;
            string loggedUser = "";

            while (!loggedIn)
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Create Account");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter Username: ");
                    string username = Console.ReadLine();

                    Console.Write("Enter Password: ");
                    string password = Console.ReadLine();

                    if (users.ContainsKey(username) && users[username] == password)
                    {
                        Console.WriteLine("Login Successful ✅");
                        loggedIn = true;
                        loggedUser = username;


                        var details = userDetails[username];
                        Console.WriteLine($"Welcome {details.firstName} {details.lastName}!");
                        Console.WriteLine($"NIC: {details.nic}, Mobile: {details.mobile}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid credentials ❌. Try again.");
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Choose a Username: ");
                    string newUser = Console.ReadLine();

                    if (users.ContainsKey(newUser))
                    {
                        Console.WriteLine("Username already exists ❌. Try another.");
                        continue;
                    }

                    Console.Write("Choose a Password: ");
                    string newPass = Console.ReadLine();

                    Console.Write("Enter First Name: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Enter Last Name: ");
                    string lastName = Console.ReadLine();

                    Console.Write("Enter NIC No: ");
                    string nic = Console.ReadLine();

                    Console.Write("Enter Mobile No: ");
                    string mobile = Console.ReadLine();


                    users[newUser] = newPass;
                    userDetails[newUser] = (firstName, lastName, nic, mobile);

                    Console.WriteLine("Account created successfully ✅. You can now login.");
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }
            }


            Console.Write("Enter your account holder name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your account number: ");
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Enter the valid Account Number !!! ");
            }

            Console.Write("Enter the Amount to deposit initially ? ");
            decimal balance;
            while (!decimal.TryParse(Console.ReadLine(), out balance))
            {
                Console.WriteLine("Enter the valid Initial Balance !!! ");
            }

            BankAccount account = new BankAccount(name, number, balance);

            bool running = true;
            while (running)
            {
                Console.Clear();
                DisplayMenu();

                Console.Write("Please choose an option (1-7): ");
                if (int.TryParse(Console.ReadLine(), out int selectedOption))
                {
                    switch (selectedOption)
                    {
                        case 1:
                            account.PrintDetails();
                            break;
                        case 2:
                            ShowWelcomeMessage(account.AccountHolderName);
                            break;
                        case 3:
                            Console.Write("Enter amount to deposit: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                                account.Deposit(depositAmount);
                            else
                                Console.WriteLine("Enter valid input !!!");
                            break;
                        case 4:
                            Console.Write("Enter amount to withdraw: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                                account.Withdraw(withdrawAmount);
                            else
                                Console.WriteLine("Enter valid input !!!");
                            break;
                        case 5:
                            Console.WriteLine($"Your Balance is {account.Balance:F2}");
                            break;
                        case 6:
                            account.ShowTransactions();
                            break;
                        case 7:
                            Console.WriteLine("Exiting... Goodbye!");
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Enter valid input !!! ");
                }

                if (running)
                {
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("===== BANK MENU =====");
            Console.WriteLine("1. Account Details");
            Console.WriteLine("2. User Welcome");
            Console.WriteLine("3. Deposit Funds");
            Console.WriteLine("4. Withdraw Funds");
            Console.WriteLine("5. Check Balance");
            Console.WriteLine("6. Transaction History");
            Console.WriteLine("7. Exit");
        }

        static void ShowWelcomeMessage(string userName)
        {
            Console.WriteLine($"Hello {userName}! WELCOME TO BANK OF ABC");
        }
    }
}
