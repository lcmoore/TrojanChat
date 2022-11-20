using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrojanChat.MVVM.Model;

namespace TrojanChat.MVVM.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            //Here is where data population will occur
            Messages.Add(new MessageModel
            {
                Username = "Test User",
                UsernameColor = "#409aff",
                //ImageSource = "https://i.imgur.com/yMWvLXd.png",
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
                    UsernameColor = "#409aff",
                    //ImageSource = "https://i.imgur.com/yMWvLXd.png",
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
                    UserName = $" Contact Number {i}",
                    //ImageSource = "https://i.imgur.com/yMWvLXd.png",
                    MessageHistory = Messages
                });
            }

        }
    }
}
