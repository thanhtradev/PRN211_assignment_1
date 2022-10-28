namespace ASM1.Entity;

public class Transaction
{
    public String ID { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string Type { get; set; }

    public Transaction(double amount, string type)
    {
        ID = Guid.NewGuid().ToString();
        Date = DateTime.Now;
        Amount = amount;
        Type = type;
    }
    override
    public string ToString()
    {
        return $"ID: {ID}, Date: {Date}, Amount: {Amount}, Type: {Type}";
    }
}