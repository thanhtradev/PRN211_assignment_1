namespace ASM1.Entity;

public class Customer
{
    private string _ID;
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Account> Accounts { get; set; }

    public Customer(string name, string address)
    {
        // Generate uuid for customer id
        _ID = Guid.NewGuid().ToString();
        Name = name;
        Address = address;
        Accounts = new List<Account>();
    }
    public string ID
    {
        get => _ID;
    }

    override
    public string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Address: {Address}";
    }
}

public struct Address
{
    public string Street { get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string City { get; set; }
}