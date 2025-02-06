using SQLite;
namespace GardenCenter.Model;


	public class Products()
	{
		[PrimaryKey]
	
		public string Name { get; set; }
		public double Price { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public string Image { get; set; }

	}