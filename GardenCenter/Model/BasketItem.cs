using SQLite;
using System.Security.Cryptography.X509Certificates;

namespace GardenCenter.Model;

public class BasketItem 
{
		[PrimaryKey, AutoIncrement]
		public int ItemId {get; set;}
		public string PhoneNumber { get; set; }
		public string Username { get; set; }
		public string ProductName { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public bool IsCorporate { get; set; }

}