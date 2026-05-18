namespace PRACTISE
{
    internal class Program
    {
        static void Main(string[] args)
        {


                BankAccount adam = new BankAccount();

                adam.AccountNumber = "1000A";
                adam.Balance = 1000.00m;

                adam.Deposit(2000.00m);
                adam.Deposit(2000.00m);



                adam.Deposit(1000.00m);
                adam.PrintStatement();



                Console.ReadKey();

            }
        }

        public class BankAccount
        {
            public string AccountNumber;
            public decimal Balance;


            List<Transaction> Transactions = new List<Transaction>();


            public void Deposit(decimal amount)
            {
                Balance += amount;

                Transaction t = new Transaction
                {
                    TransactionType = "Deposit",
                    Balance = Balance,
                    Date = DateTime.Now
                };

                Transactions.Add(t);

            }

            public void PrintStatement()
            {
                foreach (var t in Transactions)
                {
                    Console.WriteLine($"{t.Balance}  {t.TransactionType}  {t.Date}");
                }

            }


        }

        public class Transaction
        {
            public int TransactionID;

            public string TransactionType;

            public DateTime Date;

            public decimal Balance;
        }

    }
