using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BMICalc
{
    public partial class ViewContainer : Xamarin.Forms.Shell
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