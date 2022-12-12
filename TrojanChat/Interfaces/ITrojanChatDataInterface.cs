using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrojanChat.MVVM.Model;

namespace TrojanChat.Interfaces
{
    public interface ITrojanChatDataInterface
    {
        bool Login();
        bool Logout();

        Dictionary<string, List<MessageModel>> Refresh(string Username_self);
           
        void sendMessage(string message, string Username_self, string Username_target);
        void sendImage(string? message, string imageURL);


       




    }
}
