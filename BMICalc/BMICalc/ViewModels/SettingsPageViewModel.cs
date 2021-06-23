using Acr.UserDialogs;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace BMICalc.ViewModels
{
    class SettingsPageViewModel : INotifyPropertyChanged
    {
        public ICommand AddUser { get; }
        public ICommand RemoveUser { get; }
        public ICommand RemoveAllUsers { get; }
        public ICommand RemoveAllBMIs { get; }

        #region Eventhandlers

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public SettingsPageViewModel()
        {
            AddUser = new Command(() => { NewUserPrompt(); });
            RemoveUser = new Command(() => { RemoveUserPrompt(); });
            RemoveAllUsers = new Command(() => { App.UserDB.ClearDB();});
            RemoveAllBMIs = new Command(() => { App.BmiDB.ClearDB(); });
        }

        private async void NewUserPrompt()
        {
            PromptResult input = await UserDialogs.Instance.PromptAsync("Type in a Username", "New User", "Confirm", "Cancel");

            if (input.Ok && input.Value != string.Empty)
            {
                await App.UserDB.AddUser(new Data.UserData() { Name = input.Value });
                UserDialogs.Instance.Alert($"Successfully added user: {input.Value}.", "User added");
            }
        }

        private async void RemoveUserPrompt()
        {
            string choice = await UserDialogs.Instance.ActionSheetAsync("Which user should be removed?", "Cancel", "Remove", CancellationToken.None, App.UserDB.GetUsers().ToArray());

            if (choice != "Cancel" && choice != "Remove")
            {
                var user = await App.UserDB.GetUser(choice);

                await App.UserDB.RemoveUser(user);

                UserDialogs.Instance.Alert($"Removed user: {user.Name}.", "User removed");
            }
        }
    }
}
