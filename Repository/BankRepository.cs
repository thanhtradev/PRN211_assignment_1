using ASM1.DataAccess;
using ASM1.Entity;

namespace ASM1.Repository;

public class BankRepository : IBankRepository
{
    public List<Branch> GetBranchList() => TCBDBContext.Instance.GetBranchList;
    public void AddBranch(Branch branch) => TCBDBContext.Instance.AddBranch(branch);
    public void AddCustomerToBranch(string branchID, List<Customer> customers) => TCBDBContext.Instance.AddCustomerToBranch(branchID, customers);
    public Customer GetCustomerByID(string customerID) => TCBDBContext.Instance.GetCustomerByID(customerID);
    public List<Customer> GetCustomerList() => TCBDBContext.Instance.GetCustomerList;
    public List<Customer> GetCustomerListByBranch(Branch branch) => TCBDBContext.Instance.GetCustomerListByBranch(branch);
    public double GetTotalBalanceOfEachCustomer(Customer customer) => TCBDBContext.Instance.GetTotalBalanceOfEachCustomer(customer);
    public Customer FindCustomerWithHighestNumberOfTransactions() => TCBDBContext.Instance.FindCustomerWithHighestNumberOfTransactions();

    public void AddAccountToCustomer(string customerID, List<Account> accounts) => TCBDBContext.Instance.AddAccountToCustomer(customerID, accounts);
    public Account GetAccountByAccountNumber(string accountNumber) => TCBDBContext.Instance.GetAccountByAccountNumber(accountNumber);
    public void TopupOrWithdrawAccount(Account account, Transaction transaction) => TCBDBContext.Instance.TopupOrWithdrawAccount(account, transaction);
    public List<Account> FindAllAccountsOfEachCustomerWithBiggestBalance(Customer customer) => TCBDBContext.Instance.FindAllAccountsOfEachCustomerWithBiggestBalance(customer);

}