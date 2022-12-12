using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrojanChat.MVVM.Model
{
    public class ContactModel
    {
        public string? UserName { get; set; }
        public string? ImageSource { get; set; }
        public ObservableCollection<MessageModel> MessageHistory { get; set; }
        public string? LastMessage => MessageHistory.LastOrDefault() is not null ? MessageHistory.LastOrDefault().MessageText : "";
        public ContactModel()
        {
            MessageHistory = new ObservableCollection<MessageModel>();

            //var firstMsg = new MessageModel(text);
            //MessageHistory.Add(firstMsg);
            //string text = LastMessage is not null ? LastMessage : "";

        }


    }
    
}
