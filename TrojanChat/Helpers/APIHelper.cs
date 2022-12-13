using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrojanChat.MVVM.Model;

namespace TrojanChat.Helpers
{
    public class APIHelper
    {
        public static HttpClient ApiClient { get; set; }

        public APIHelper() // constructor
        {
            InitializeClient();
        }
        private static void InitializeClient()
        {
            ApiClient = new HttpClient();
            //ApiClient.BaseAddress = new Uri("http://35.89.23.162:3000");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<object> SingUp(TrojanChat.MVVM.ViewModel.MainViewModel context, string username, string password) //Sign Up method
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });
            var response = await ApiClient.PostAsync("http://35.90.137.151:3000/user", data);
            if (response.IsSuccessStatusCode)
            {
                var raspContent = await response.Content.ReadAsStringAsync();
                var jo = JObject.Parse(raspContent);
                // login here instead
                context.Uuid = jo["userCreate"]["uid"].ToString();
                var _ = await LogIn(context, username, password, context.Uuid);
                return "";
                //MessageBox.Show(raspContent);

                //var raspContent = await response.Content.ReadAsStringAsync();

            }
            else return null;
            //var jsonStr = JsonConvert.DeserializeObject<object>(raspContent);
            //var jo = JObject.Parse(raspContent);
            // login here instead
            //context.Uuid = jo["userCreate"]["uid"].ToString();
            return "";
            //context.Uuid = jsonStr;
            //return raspContent;

        }
        public async Task<HttpResponseMessage> LogIn(TrojanChat.MVVM.ViewModel.MainViewModel context, string username, string password, string uid)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });
            var response = await ApiClient.PostAsync("http://35.90.137.151:3000/user/" + username, data);
            var raspContent = await response.Content.ReadAsStringAsync();

            var jo = JObject.Parse(raspContent);
            if (raspContent.Contains("uid"))
            {
              

                context.Uuid = jo["loginwithname"]["uid"].ToString();
                uid = context.Uuid;
                var __ = await getFriends(context, username, password, uid);

                var _ = await GetMessages(context, username, password, uid);

                return response;
            }
            else
            {
                return null;
            }

        }
        public async Task<object> GetMessages(TrojanChat.MVVM.ViewModel.MainViewModel context, string username, string password, string uid) //Sign Up method
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            //friend.MessageHistory.Clear();
            var response = await ApiClient.GetAsync("http://35.90.137.151:3000/user/" + uid);
            var raspContent = await response.Content.ReadAsStringAsync();
            var jo = JObject.Parse(raspContent);
            var msgList = jo["message"];
            var updatedMessage = new ObservableCollection<MessageModel>();
            var friendsDict = new Dictionary<string, ObservableCollection<MessageModel>>();
            foreach (var message in msgList)
            {
                var msgColor = "Gray";
                string from = message["from"].ToString();
                //string to = message["to"].ToString();
                if (from == uid) //if from me
                {
                    from = message["to"].ToString();
                    msgColor = "DarkRed";

                }

                if (!(friendsDict.ContainsKey(from)))
                {
                    friendsDict[from] = new ObservableCollection<MessageModel>();
                    var thisMsg = new MessageModel(message["content"].ToString(), msgColor);
                    friendsDict[from].Add(thisMsg);



                }
                else
                {
                    var thisMsg = new MessageModel(message["content"].ToString(), msgColor);
                    friendsDict[from].Add(thisMsg);

                }





            }

            // outgoing messages 
            //foreach (var friend in context.Contacts) 
            //{
            //    var friend_response = await ApiClient.GetAsync("http://35.90.137.151:3000/user/" + friend.UserName);
            //    var friend_raspContent = await response.Content.ReadAsStringAsync();
            //    var friend_jo = JObject.Parse(raspContent);
            //    var friend_msgList = jo["message"];

            //}
            foreach (var friend in context.Contacts)
            {
                if (friendsDict.ContainsKey(friend.UserName))
                    {

                    friend.MessageHistory.Clear();
                    friend.MessageHistory = friendsDict[friend.UserName];
                }




            }
            //var response = await ApiClient.GetAsync("http://35.90.137.151:3000/user/"+uid);
            //var raspContent = await response.Content.ReadAsStringAsync();
            //MessageBox.Show(raspContent);
            return "";
            //if (response.IsSuccessStatusCode)
            //{
            //    var jo = JObject.Parse(raspContent);
            //     //= jo["message"].ToString();
            //    return "";

            //}
            //else return null;
        }
        public async Task<object> SendMessages(TrojanChat.MVVM.ViewModel.MainViewModel context, string username, string password, string uid, string message) //Sign Up method
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from", uid),
                new KeyValuePair<string, string>("message", message)
            });
            var response = await ApiClient.PostAsync("http://35.90.137.151:3000/user/" + context.SelectedContact.UserName + "/text", data);
            var raspContent = await response.Content.ReadAsStringAsync();
            //MessageBox.Show(raspContent);
            if (response.IsSuccessStatusCode)
            {
                //var jo = JObject.Parse(raspContent);
                //= jo["message"].ToString();
                return "";

            }
            else return null;
        }
        public async Task<object> MakeFriend(TrojanChat.MVVM.ViewModel.MainViewModel context, string username, string password, string uid) //Sign Up method
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from", uid),

            });
            var response = await ApiClient.PostAsync("http://35.90.137.151:3000/friend/" + context.FriendUid, data);
            var raspContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var jo = JObject.Parse(raspContent);
                var newFriend = new ContactModel();
                newFriend.UserName = context.FriendUid;
                //add friend messages
                context.Contacts.Add(newFriend);
                return "";

            }
            else return null;
        }
        public async Task<object> getFriends(TrojanChat.MVVM.ViewModel.MainViewModel context, string username, string password, string uid) //Sign Up method
        {
            //var data = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("from", uid),

            //});
            var response = await ApiClient.GetAsync("http://35.90.137.151:3000/friend/" + uid);
            var raspContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                context.Contacts.Clear();
                var jo = JObject.Parse(raspContent);
                var friendlist = jo["friend"];
                List<string> uidList = new List<string>();
                foreach (var friend in friendlist)
                {
                    var friendUid = friend["uid"].ToString().Replace("`","\"");
                    var friendName = friend["username"].ToString();
                    if (uidList.Contains(friendUid))
                    {
                        continue;
                    }
                    var newFriend = new ContactModel();
                    newFriend.UserName = friendUid;
                    newFriend.Name = friendName;
                    context.Contacts.Add(newFriend);
                    uidList.Add(friendUid);
                    


                }

 
                //newFriend.UserName = context.FriendUid;
                ////add friend messages
                //context.Contacts.Add(newFriend);
                return "";

            }
            else return null;
        }




    }
    
}
