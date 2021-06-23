using Xamarin.Forms;

namespace BMICalc
{
    public partial class ViewContainer : Shell
    {
        public ViewContainer()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.CalculatorPage), typeof(Views.CalculatorPage));
            Routing.RegisterRoute(nameof(Views.HistoryPage), typeof(Views.HistoryPage));
            Routing.RegisterRoute(nameof(Views.LandingPage), typeof(Views.LandingPage));
            Routing.RegisterRoute(nameof(Views.SettingsPage), typeof(Views.SettingsPage));
        }
    }
}