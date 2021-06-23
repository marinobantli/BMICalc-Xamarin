using System.Windows.Input;
using Xamarin.Forms;

namespace BMICalc.ViewModels
{
    class LandingPageViewModel
    {
        public ICommand StartCalculator { get; }

        public LandingPageViewModel()
        {
            StartCalculator = new Command(() => { Application.Current.MainPage = new ViewContainer(); });
        }
    }
}
