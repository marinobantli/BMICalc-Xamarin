using System.Windows.Input;
using Xamarin.Forms;

namespace BMICalc.ViewModels
{
    class LandingPageViewModel
    {
        public ICommand StartCalculator { get; }

        public LandingPageViewModel()
        {
            //Switches to the ViewContainer
            StartCalculator = new Command(() => { Application.Current.MainPage = new ViewContainer(); });
        }
    }
}
