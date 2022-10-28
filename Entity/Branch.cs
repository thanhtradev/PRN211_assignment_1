namespace ASM1.Entity;
using System;
public class Branch
{
    private string _ID;
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Customer> Customers { get; set; }

    public Branch(string name, string address)
    {
        // Generate uuid for branch id
        _ID = Guid.NewGuid().ToString();
        Name = name;
        Address = address;
        Customers = new List<Customer>();
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