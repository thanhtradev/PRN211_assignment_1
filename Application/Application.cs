using ASM1.Entity;
using ASM1.Repository;
using ASM1.Utility;

namespace ASM1.Application;

public class Application
{
    private static IBankRepository _bankRepository = new BankRepository();
    // This is main function
    static void Main(string[] args)
    {
        Application app = new Application();
        // Menu for user to choose
        Menu mainMenu = new Menu("Welcome to Techcombank management system", new List<string> { "Create a new bank branch", "Find customer", "Top up or Withdraw", "List all transaction", "List account with biggest balance of each customer", "Show list of customer and sort by total of balance", "Show customer with the most number of transactions", "List of customer", "Exit" });
        // Loop until user choose to exit
        while (true)
        {
            // Display menu
            mainMenu.Display();
            // Get user choice
            int choice = mainMenu.GetChoice();
            switch (choice)
            {
                case 1:
                    // Create a new branch
                    CreateNewBranch();
                    break;
                case 2:
                    // Find customer
                    FindCustomer();
                    break;
                case 3:
                    // Top up or withdraw
                    TopUpOrWithdraw();
                    break;
                case 4:
                    // List all transaction
                    ListAllTransaction();
                    break;
                case 5:
                    // List account with biggest balance of each customer
                    ListAccountWithBiggestBalanceOfEachCustomer();
                    break;
                case 6:
                    // Show customer sort by total of balance
                    ShowCustomerSortByTotalOfBalance();
                    break;
                case 7:
                    // Show customer with the most number of transactions
                    ShowCustomerWithTheMostNumberOfTransactions();
                    break;
                case 8:
                    // List of customer
                    GetCustomerList();
                    break;
            }

            // If user choose to exit, break the loop
            if (choice == mainMenu.MenuItems.Count)
            {
                break;
            }
        }

        void CreateNewBranch()
        {
            Branch branch = AddBranchToDatabase();
            // Menu for user to choose
            Menu BranchMenu = new Menu("Bank branch management system", new List<string> { "Add customers to branch", "Add accounts to customer", "Exit" });
            while (true)
            {
                BranchMenu.Display();
                int choice = BranchMenu.GetChoice();
                switch (choice)
                {
                    case 1:
                        // Add customer to branch
                        AddCustomersToBranch(branch);
                        break;
                    case 2:
                        // Add account to customer
                        AddAccountToCustomer();
                        break;
                }

                if (choice == BranchMenu.MenuItems.Count)
                {
                    break;
                }
            }
        }
        Branch AddBranchToDatabase()
        {
            // Create new branch
            Branch tmpBranch = app.InputBranch();
            // Save branch to database
            _bankRepository.AddBranch(tmpBranch);
            return tmpBranch;
        }

        void AddCustomersToBranch(Branch branch)
        {
            List<Customer> customers = new List<Customer>();
            while (true)
            {
                // Create new customer
                Customer tmpCustomer = app.InputCustomer();
                // Add customer to branch
                customers.Add(tmpCustomer);
                // Ask user to continue or not
                if (!Input.AskContinue("Do you want to add more customer? (y/n) "))
                {
                    _bankRepository.AddCustomerToBranch(branch.ID, customers);
                    break;
                }
            }
        }

        void AddAccountToCustomer()
        {
            List<Account> accounts = new List<Account>();
            // Input customer id 
            string customerID = Input.GetString("Enter customer id: ");
            // Check if customer id is existed
            try
            {
                Customer customer = _bankRepository.GetCustomerByID(customerID);
                while (true)
                {
                    // Create new account
                    Account tmpAccount = app.InputAccount();
                    // Add account to customer
                    accounts.Add(tmpAccount);
                    Console.WriteLine(tmpAccount.ToString());
                    Console.WriteLine("Account added successfully!");
                    // Ask user to continue or not
                    if (!Input.AskContinue("Do you want to add more account? (y/n) "))
                    {
                        _bankRepository.AddAccountToCustomer(customerID, accounts);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        void FindCustomer()
        {
            // Input customer id 
            string customerID = Input.GetString("Enter customer id: ");
            try
            {
                // Get customer by id
                Customer customer = _bankRepository.GetCustomerByID(customerID);
                // Display customer information
                Console.WriteLine(customer.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        void TopUpOrWithdraw()
        {
            // Input account number
            string accountNumber = Input.GetString("Enter account number: ");
            // Check if account id is existed
            try
            {
                Account account = _bankRepository.GetAccountByAccountNumber(accountNumber);
                // Loop until user input correct type
                while (true)
                {
                    // Input type
                    string type = Input.GetString("Enter type (T/W): ");
                    // Check if type is topup or withdraw
                    if (type == "T" || type == "W")
                    {
                        // Input amount
                        double amount = Input.GetDouble("Enter amount: ");
                        // Check if amount is greater than 0
                        if (amount > 0)
                        {
                            // Create new transaction
                            Transaction transaction = new Transaction(amount, type);
                            // Top up or withdraw
                            _bankRepository.TopupOrWithdrawAccount(account, transaction);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Amount must be greater than 0.");
                        }
                    }

                    else
                    {
                        Console.WriteLine("Type must be T or D.");
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        void GetCustomerList()
        {
            // Get list of customer
            List<Customer> customers = _bankRepository.GetCustomerList();
            // Display all customer in database
            if (customers.Count == 0)
            {
                Console.WriteLine("There is no customer in database.");
            }
            else
            {
                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer.ToString());
                }
            }
        }
        void ListAllTransaction()
        {
            // Get all branch
            List<Branch> branches = _bankRepository.GetBranchList();
            // Display all transaction each account in each customer in each branch
            foreach (Branch branch in branches)
            {
                Console.WriteLine("Branch: " + branch.ToString());
                foreach (Customer customer in branch.Customers)
                {
                    Console.WriteLine("\tCustomer:" + customer.ToString());
                    foreach (Account account in customer.Accounts)
                    {
                        Console.WriteLine("\t\tAccount:" + account.ToString());
                        foreach (Transaction transaction in account.Transactions)
                        {
                            Console.WriteLine("\t\t\tTransaction:" + transaction.ToString());
                        }
                    }
                }
            }
        }
        void ListAccountWithBiggestBalanceOfEachCustomer()
        {
            // Get all branch
            List<Branch> branches = _bankRepository.GetBranchList();
            // Display all account with biggest balance of each customer in each branch
            foreach (Branch branch in branches)
            {
                Console.WriteLine("Branch: " + branch.ToString());
                foreach (Customer customer in branch.Customers)
                {
                    Console.WriteLine("\tCustomer: " + customer.ToString());
                    foreach (Account account in _bankRepository.FindAllAccountsOfEachCustomerWithBiggestBalance(customer))
                    {
                        Console.WriteLine("\t\tAccount: " + account.ToString());
                    }

                }
            }
        }
        void ShowCustomerSortByTotalOfBalance()
        {
            // Get all branch
            List<Branch> branches = _bankRepository.GetBranchList();
            // Display all customer sort by total of balance in each branch
            foreach (Branch branch in branches)
            {
                Console.WriteLine(branch.ToString());
                // Get list of customer with total of balance 
                List<Customer> customers = _bankRepository.GetCustomerListByBranch(branch).OrderBy(c => _bankRepository.GetTotalBalanceOfEachCustomer(c)).ToList();
                foreach (Customer customer in customers)
                {
                    Console.WriteLine("\t" + customer.ToString() + " Total balance: " + _bankRepository.GetTotalBalanceOfEachCustomer(customer));
                }
            }
        }
        void ShowCustomerWithTheMostNumberOfTransactions()
        {
            Console.WriteLine("Customer with the most number of transactions: ");
            Console.WriteLine(_bankRepository.FindCustomerWithHighestNumberOfTransactions().ToString());
        }
    }

    private Branch InputBranch()
    {
        // Get branch name
        string branchName = Input.GetString("Enter branch name: ");
        // Get branch address
        string branchAddress = Input.GetAddress("Enter branch address");

        return new Branch(branchName, branchAddress);
    }
    private Customer InputCustomer()
    {
        string customerName = Input.GetString("Enter customer name: ");
        string customerAddress = Input.GetAddress("Enter customer address");

        return new Customer(customerName, customerAddress);
    }
    private Account InputAccount()
    {
        string accountNumber = Input.GetString("Enter account number: ");
        double accountBalance = Input.GetDouble("Enter account balance: ");
        return new Account(accountNumber, accountBalance);
    }
}