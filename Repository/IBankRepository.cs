using ASM1.Entity;

namespace ASM1.Repository;

public interface IBankRepository
{
    public List<Branch> GetBranchList();
    public void AddBranch(Branch branch);
    public void AddCustomerToBranch(string branchID, List<Customer> customers);
    public Customer GetCustomerByID(string customerID);
    public List<Customer> GetCustomerList();
    public List<Customer> GetCustomerListByBranch(Branch branch);
    public double GetTotalBalanceOfEachCustomer(Customer customer);
    public Customer FindCustomerWithHighestNumberOfTransactions();

    public void AddAccountToCustomer(string customerID, List<Account> account);
    public Account GetAccountByAccountNumber(string accountNumber);
    public void TopupOrWithdrawAccount(Account account, Transaction transaction);
    public List<Account> FindAllAccountsOfEachCustomerWithBiggestBalance(Customer customer);


}