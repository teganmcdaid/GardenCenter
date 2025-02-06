using GardenCenter.Services;

namespace GardenCenter
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; }

        public App()
        {
            InitializeComponent();

            // Set the database path
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                    "Products.db3");
            Database = new DatabaseService(dbPath);

            MainPage = new AppShell();
        }
    }
}
