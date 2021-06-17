using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace BMICalc.ViewModels
{
    class CalculatorPageViewModel : INotifyPropertyChanged
    {
        public ICommand Calculate { get; }
        public ICommand ClearValues { get; }
        public ICommand SaveValue { get; }

        private readonly BMITable BMIServ = new BMITable();

        #region Eventhandlers
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        private double _height;
        private double _weight;
        private double _calculatedBmi;
        private string _bmiInfo;

        public double Height
        {
            set
            {
                _height = value;
                NotifyPropertyChanged();
            }
            get
            {
                return _height;
            }
        }

        public double Weight
        {
            set
            {
                _weight = value;
                NotifyPropertyChanged();
            }
            get
            {
                return _weight;
            }
        }

        public double CalculatedBMI
        {
            set
            {
                _calculatedBmi = value;
                NotifyPropertyChanged();
            }
            get
            {
                return _calculatedBmi;
            }
        }

        public string BmiInfo
        {
            set
            {
                _bmiInfo = value;
                NotifyPropertyChanged();
            }
            get
            {
                return _bmiInfo;
            }
        }

        #endregion

        public CalculatorPageViewModel()
        {
            Calculate = new Command(() => { CalculatedBMI = CalculateBMI(Height, Weight); BmiInfo = BMIServ.GetBMIInfo(CalculatedBMI); });
            ClearValues = new Command(() => { Height = 0; Weight = 0; CalculatedBMI = 0; BmiInfo = ""; });
            SaveValue = new Command(() => { SaveResult(); });
        }

        private double CalculateBMI(double height, double weight)
        {
            double heightInSuqareMeters = height / 100;
            heightInSuqareMeters = heightInSuqareMeters * heightInSuqareMeters;

            return Math.Round(weight / heightInSuqareMeters, 2);
        }

        async void SaveResult()
        {
            List<string> users = new List<string>();

            foreach (var user in await App.UserDB.GetUsers())
            {
                users.Add(user.Name);
            }

            var choice = await UserDialogs.Instance.ActionSheetAsync("Select user to assign the result", "Cancel", "Confirm", CancellationToken.None, users.ToArray());

            if (choice != "Cancel")
            {
                await App.BmiDB.SaveNewRecord(new Data.BMIData() { Name = choice, Height = Height, Weight = Weight, BMI = CalculatedBMI });
            }
        } 

    }
}
