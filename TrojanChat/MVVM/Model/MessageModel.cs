﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrojanChat.MVVM.Model
{
    class MessageModel
    {
        public string Username { get; set; }
        public string UsernameColor { get; set; }
        public string? ImageSource { get; set; }
        public string MessageText { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsNativeOrigin { get; set; }
        public bool? IsFirstMessage { get; set; }
    }
}
