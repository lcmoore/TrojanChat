using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrojanChat.Interfaces;

namespace TrojanChat.MVVM.Model
{


    public class JsonDataModel : ITrojanChatDataInterface
    {
        public string Username_Self { get; set; }

        public JsonDataModel(string Username_Self, string JsonDir)
        {
            this.Username_Self = Username_Self;

        }

        public bool Login()
        {
            return true;
        }

        public bool Logout()
        {
            return true;
        }

        public Dictionary<string, List<MessageModel>> Refresh(string Username_self)
        {
            throw new NotImplementedException();
        }

        public void sendMessage(string message, string Username_self, string Username_target)
        {
            throw new NotImplementedException();
        }

        public void sendImage(string? message, string imageURL)
        {
            throw new NotImplementedException();
        }
    }
}
