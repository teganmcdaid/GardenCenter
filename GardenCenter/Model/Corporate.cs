using SQLite;

namespace GardenCenter.Model;

public class Corporate 
{
    [PrimaryKey, AutoIncrement]
    public int PurchaseId { get; set; }
    public String Name { get; set; }
    
    public String PhoneNumber { get; set; }
    public string ProductName { get; set; }
    public double TotalItemPrice { get; set; }
    public int Quantity { get; set; }

    public double AccountTotal { get; set; }
}