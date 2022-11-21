using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrojanChat.Core;
using TrojanChat.MVVM.Model;

namespace TrojanChat.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        //Commands
        public RelayCommand SendCommand { get; set; }


        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set { _selectedContact = value;
                OnPropertyChanged();
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
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    MessageText = Message,
                    IsFirstMessage = false
                });

                Message = "";

            });


            //Here is where data population will occur
            Messages.Add(new MessageModel
            {
                Username = "Test User",
                UsernameColor = "White",
                ImageSource = "https://i.imgur.com/yMWvLXd.png",
                MessageText = "This is the message text",
                TimeStamp = DateTime.Now,
                IsFirstMessage = true,
                IsNativeOrigin = false
            });

            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Test User Self",
                    UsernameColor = "White",
                    ImageSource = "https://i.imgur.com/yMWvLXd.png",
                    MessageText = "This is the message text",
                    TimeStamp = DateTime.Now,
                    IsFirstMessage = false,
                    IsNativeOrigin = true
                });
             

            }
            for (int i = 0; i < 5; i++)
            {
                Contacts.Add(new ContactModel
                {
                    UserName = $" Chat Number {i}",
                    ImageSource = "https://i.imgur.com/yMWvLXd.png",
                    MessageHistory = Messages
                });
            }

        }
    }
}
