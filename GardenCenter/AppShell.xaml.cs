using GardenCenter.Views;

namespace GardenCenter
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ProductsPage), typeof(ProductsPage));
            Routing.RegisterRoute(nameof(BasketPage), typeof(BasketPage));
        }
    }
}
