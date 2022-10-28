namespace ASM1.Entity;

public class Account
{
    public string AccountNumber { get; set; }
    public double Balance { get; set; }
    public List<Transaction> Transactions { get; set; }

    public Account(string accountNumber, double balance)
    {
        AccountNumber = accountNumber;
        Balance = balance;
        Transactions = new List<Transaction>();
    }
    override
    public string ToString()
    {
        return $"AccountNumber: {AccountNumber}, Balance: {Balance}";
    }
}