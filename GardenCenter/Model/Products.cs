using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace GardenCenter.Model;


	public class Products : INotifyPropertyChanged
	{
    

    [PrimaryKey]
	
		public string Name { get; set; }
		public double Price { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public string Image { get; set; }

    //public int Quantity { get; set; }
    private int quantity;
    public int Quantity
    {
        get => quantity;
        set
        {
            if (quantity != value)
            {
                quantity = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}