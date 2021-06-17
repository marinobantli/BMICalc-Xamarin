﻿using BMICalc.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BMICalc.ViewModels
{
    class HistoryPageViewModel : INotifyPropertyChanged
    {
        #region Eventhandlers

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        private List<string> _usernames;
        private string _selUser;
        private List<BMIData> _datas;

        public List<string> UserNames
        {
            set
            {
                _usernames = value;
                NotifyPropertyChanged();
            }
            get
            {
                return _usernames;
            }
        }

        public string SelectedUser
        {
            set
            {
                _selUser = value;
                NotifyPropertyChanged();
                GetData();
            }
            get
            {
                return _selUser;
            }
        }

        public List<BMIData> Data
        {
            set
            {
                _datas = value;
                NotifyPropertyChanged();
            }
            get
            {
                return _datas;
            }
        }

        #endregion

        public HistoryPageViewModel()
        {
            GetData();
            RefreshPicker();
        }

        async void RefreshPicker()
        {
            List<string> users = new List<string>();

            foreach (var user in await App.UserDB.GetUsers())
            {
                users.Add(user.Name);
            }
            if(users.Count == 0)
            {
                users.Add("No users registered");
            }

            UserNames = users;
        }

        async void GetData()
        {
            List<BMIData> tempData = new List<BMIData>();

            if (String.IsNullOrEmpty(SelectedUser)) {

                foreach (var data in await App.BmiDB.GetBMIs())
                {
                    tempData.Add(data);
                }
            }
            else
            {
                foreach (var data in await App.BmiDB.GetBMIForName(SelectedUser))
                {
                    tempData.Add(data);
                }
            }


            Data = tempData;
        }
    }
}
