using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrojanChat.Core;
using TrojanChat.Helpers;
using TrojanChat.MVVM.Model;

namespace TrojanChat.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public APIHelper ApiHelperConnection { get; set; }

        //Commands
        public RelayCommand SendCommand { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand SignUpCommand { get; set; }
        public RelayCommand MakeFriendCommand   { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }




        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set { _selectedContact = value;
                OnPropertyChanged();
            }
        }
        private bool _loginBool;
        public bool LoginBool
        {
            get { return _loginBool; }
            set
            {
                if (value != _loginBool)
                {
                    _loginBool = value;
                    OnPropertyChanged("LoginBool");
                }
            }

        }
        private bool _logoutBool;
        public bool LogoutBool
        {
            get { return _logoutBool; }
            set
            {
                if (value != _logoutBool)
                {
                    _logoutBool = value;
                    OnPropertyChanged("LogoutBool");
                }
            }

        }

        private string _uid;
        public string Uuid
        {
            get { return _uid; }
            set
            {
                if (value != _uid)
                {
                    _uid = value;
                    OnPropertyChanged("Uuid");
                }
            }
        }
        private string _friendUid;
        public string FriendUid
        {
            get { return _friendUid; }
            set
            {
                if (value != _friendUid)
                {
                    _friendUid = value;
                    OnPropertyChanged("FriendUid");
                }
            }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }


        private string _message;

        public string Message
        {   
            get { return _message; }
            set { 
                _message = value;
                OnPropertyChanged();

            }
        }

        public MainViewModel()
        {
            LoginBool = true;
            LogoutBool = false;
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();
            ApiHelperConnection = new APIHelper();
            Uuid = "0";
            //var resp = ApiHelperConnection.Authenticate("lujia", "123");

            RefreshCommand = new RelayCommand(async o =>
            {
                var __ = await ApiHelperConnection.getFriends(this, Username, Password, Uuid);

                var _ = await ApiHelperConnection.GetMessages(this, Username, Password, Uuid);
            });
            LogoutCommand = new RelayCommand(o =>
            {
                this.Uuid = "0";
                this.Contacts.Clear();
                this.Username = "";
                this.Password = "";
                this.LoginBool = true;
                this.FriendUid = "0";
                this.LogoutBool = false;
            });

            SendCommand = new RelayCommand(async o =>
            {
                var outMsg = new MessageModel(Message);
                var response = await ApiHelperConnection.SendMessages(this, Username, Password, Uuid, outMsg.MessageText);
                //Messages.Add(new MessageModel(Message)
         //);

                Message = "";

            });
            MakeFriendCommand = new RelayCommand(async o =>
            {
                var response = await ApiHelperConnection.MakeFriend(this, Username, Password, Uuid);


            });

            //LoginCommand = new AsyncCommandBase(Login);
            SignUpCommand = new RelayCommand(async o =>
            {
                var response = await ApiHelperConnection.SingUp(this, Username, Password);
         
                if (response != null)
                {
                    LoginBool = false;
                    LogoutBool = true;
                    return;
                    // call login
                    /// Call refresh
                }

            });

            LoginCommand = new RelayCommand(async o =>
            {
                var response =  await ApiHelperConnection.LogIn(this, Username, Password, Uuid);
                if (response != null)
                {
                    LoginBool = false;
                    LogoutBool = true;
                    //RefreshMsg(this);




                    return;
                    /// Call refresh here
                }
            });
            












        }
        //public async void RefreshMsg(TrojanChat.MVVM.ViewModel.MainViewModel context)
        //{
        //    while (true)
        //    {
        //        foreach (var contact in context.Contacts)
        //        {
        //            await ApiHelperConnection.GetMessages(this, Username, Password,contact.UserName);
        //            await Task.Delay(100);




        //        }
        //    }

        //}


    }
}
