using SQLite;

namespace GardenCenter.Model;

public class User
{
    public String Name { get; set; }
    [PrimaryKey]
    public String PhoneNumber { get; set; }

    public bool IsCorporate { get; set; }
}