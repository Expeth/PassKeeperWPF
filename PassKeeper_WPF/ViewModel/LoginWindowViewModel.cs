﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PassKeeper_WPF
{
    public class LoginWindowViewModel:INotifyPropertyChanged
    {
        private string info;

        public string InformationString
        {
            get => info;
            set
            {
                info = value;
                Notify();
            }
        }
        public string Username { get; set; }
        private IRepository<User> users;

        public LoginWindowViewModel(IRepository<User> repository)
        {
            InformationString = "";
            users = repository;

            LogInCommand = new RelayCommand(LogInMethod);
            SignUpCommand = new RelayCommand(SignUpMethod);
        }

        private void SignUpMethod(object obj)
        {
            if (Username == "" || (obj as PasswordBox).Password == "")
            {
                InformationString = "Fill all fields!";
                return;
            }

            var user = new User(Username, (obj as PasswordBox).Password);
            bool res = (users.GetAll() as List<User>).Exists(x => x.Equals(user));
            if (res)
            {
                InformationString = "User already exists!";
                return;
            }

            users.Create(user);
            InformationString = "Signed up";
        }

        private void LogInMethod(object obj)
        {
            if (Username == "" || (obj as PasswordBox).Password == "")
            {
                InformationString = "Fill all fields!";
                return;
            }

            var user = new User(Username, (obj as PasswordBox).Password);
            bool res = (users.GetAll() as List<User>).Exists(x => x.Equals(user));
            if (!res)
            {
                InformationString = "Incorrect login or password!";
                return;
            }

            InformationString = "Logged in";
            //TODO: open mainwindow
        }

        #region Commands
        public ICommand LogInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        #endregion

        #region PropertyChanged
        public void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
