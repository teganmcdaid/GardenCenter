using SQLite;
namespace GardenCenter.Model;


	public class Products()
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Price { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }

	}