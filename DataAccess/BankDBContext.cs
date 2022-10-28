using ASM1.Entity;

namespace ASM1.DataAccess;

// ReSharper disable once InconsistentNaming
public class TCBDBContext
{
    private static List<Branch> _branchList = new List<Branch>();

    // Using Singleton Design Pattern
    private static TCBDBContext? _instance = null;
    private static readonly object InstanceLock = new object();
    private TCBDBContext() { }
    public static TCBDBContext Instance
    {
        get
        {
            lock (InstanceLock)
            {
                return _instance ?? new TCBDBContext();
            }
        }
    }

    // Get all branches
    public List<Branch> GetBranchList => _branchList;

    // ________________Branch_________________
    // Get branch by branch ID
    public Branch GetBranchByID(string branchID)
    {
        Branch? branch = _branchList.SingleOrDefault(b => b.ID == branchID);
        if (branch == null)
        {
            throw new Exception("Branch not found");
        }
        return branch;
    }

    // Add new branch
    public void AddBranch(Branch branch)
    {
        _branchList.Add(branch);
    }

    // Add new customer to branch
    public void AddCustomerToBranch(string branchID, List<Customer> customers)
    {
        Branch branch = GetBranchByID(branchID);
        branch.Customers.AddRange(customers);
    }


    // __________________Customer_________________
    // Get customer by customer ID
    public Customer GetCustomerByID(string customerID)
    {
        Customer? customer = _branchList.SelectMany(b => b.Customers).SingleOrDefault(c => c.ID == customerID);
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }
        return customer;
    }
    // Get list of customer
    public List<Customer> GetCustomerList => _branchList.SelectMany(b => b.Customers).ToList();

    // Get all customer of each branch
    public List<Customer> GetCustomerListByBranch(Branch branch)
    {
        return branch.Customers;
    }
    // Get total of balance of all accounts of each customer
    public double GetTotalBalanceOfEachCustomer(Customer customer)
    {
        return customer.Accounts.Sum(a => a.Balance);
    }

    // Find customer with highest number of transactions
    public Customer FindCustomerWithHighestNumberOfTransactions()
    {
        return _branchList.SelectMany(b => b.Customers).OrderByDescending(c => c.Accounts.Sum(a => a.Transactions.Count)).First();
    }

    // __________________Account_________________
    // Add new account to customer
    public void AddAccountToCustomer(string customerID, List<Account> accounts)
    {
        Customer customer = GetCustomerByID(customerID);
        customer.Accounts.AddRange(accounts);
    }
    // Get account by account number
    public Account GetAccountByAccountNumber(string accountNumber)
    {
        Account? account = _branchList.SelectMany(b => b.Customers).SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        return account;
    }
    public void TopupOrWithdrawAccount(Account account, Transaction transaction)
    {
        if (transaction.Type == "T")
        {
            account.Balance += transaction.Amount;
        }
        else
        {
            // Check if balance is enough to withdraw
            if (account.Balance - transaction.Amount < 0)
            {
                throw new Exception("Balance is not enough");
            }
            account.Balance -= transaction.Amount;
        }
    }
    // Find all accounts of each customer with biggest balance
    public List<Account> FindAllAccountsOfEachCustomerWithBiggestBalance(Customer customer)
    {
        List<Account> accounts = customer.Accounts;
        List<Account> accountsWithBiggestBalance = new List<Account>();
        double biggestBalance = accounts.Max(a => a.Balance);
        foreach (Account account in accounts)
        {
            if (account.Balance == biggestBalance)
            {
                accountsWithBiggestBalance.Add(account);
            }
        }
        return accountsWithBiggestBalance;
    }


}
